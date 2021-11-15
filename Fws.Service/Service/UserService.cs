using Fws.DataAccess.Interface;
using Fws.Model.Entities;
using Fws.Model.Models;
using Fws.Service.Interface;
using Fws.Service.Mapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Fws.Service.Service
{
    public interface IUserService
    {
        List<UserModel> GetAllUser();
        UserCommon CheckAuthencation(string user, string password);
        string CreateToken(string user);
        UserCommon ValidateToken(TokenResultModel listToken);

    }
    public class UserService : IUserService
    {
        protected IBaseRepository<User> _baseRepository;
        protected IUserRepository _userRepository;
        protected IRefreshTokenRepository _refreshTokenRepository;
        protected IRefreshTokenService _refreshTokenService;
        protected IConfiguration _config;
        protected readonly IUnitOfWork _UnitOfWork;
        protected TokenValidationParameters _tokenValidation;
        private SecurityToken newToken;

        public UserService(IConfiguration config, IBaseRepository<User> baseRepository,
            IUserRepository userRepository, IRefreshTokenService refreshTokenService,
            IRefreshTokenRepository refreshTokenRepository, IUnitOfWork UnitOfWork,
             TokenValidationParameters tokenValidation)
        {
            _baseRepository = baseRepository;
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _refreshTokenService = refreshTokenService;
            _tokenValidation = tokenValidation;
            _UnitOfWork = UnitOfWork;
            _config = config;
        }

        public UserCommon CheckAuthencation(string user, string password)
        {
            var userCommon = new UserCommon();

            var checkUser = _userRepository.GetUser(user, password);


            if (checkUser == null)
            {
                userCommon.Message = "Tài khoản, mật khẩu không đúng, xin vui lòng đăng nhập lại!";
                userCommon.Status = false;
                return userCommon;
            }
            userCommon.Token = CreateToken(user);
            userCommon.Status = true;

            // tạo refresh token

            userCommon.RefreshToken = _refreshTokenService.CreateRefreshToken(newToken.Id, checkUser.Id).Token;

            return userCommon;
        }

        public string CreateToken(string userName)
        {
            var keyHash = _config.GetSection("JwtConfig").GetSection("key").Value;
            // 1. Create Security Token Handler
            var tokenHandler = new JwtSecurityTokenHandler();

            // 2. Create Private Key to Encrypted
            // one user , one key., tự độnh sinh key theo từng user.
            // mã hoá key của mình ra ký tự ascII
            var tokenKey = Encoding.ASCII.GetBytes(keyHash);

            //3. Create JETdescriptor
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Name, userName),
                        //create id token
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())

                    }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            //4. Create Token
            newToken = tokenHandler.CreateToken(tokenDescriptor);

            // 5. Return Token from method
            return tokenHandler.WriteToken(newToken);
        }

        public List<UserModel> GetAllUser()
        {
            return _baseRepository.GetAll().ToList().MapToModels();
        }

        public UserCommon ValidateToken(TokenResultModel listToken)
        {
            // validate token, khởi tạo token kiểu header-pinpal-signature
            var userCommon = new UserCommon();
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken validatedToken;
            var tokenInVerification = tokenHandler.ValidateToken(listToken.Token, _tokenValidation, out validatedToken);

            // check token đó có phải là loại mã hoá đó ko 
            if (validatedToken is JwtSecurityToken jwtSecurityToken)
            {
                // check xem nó có phải hmasSha256 hay ko 
                var IsTokenSecurity = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);

                if (IsTokenSecurity == false)
                {
                    userCommon.Status = false;
                    return userCommon;
                }
            }
            // check xem token hết hạn hay chưa
            var utcExpiryDate = long.Parse(tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

            var expiryDate = UnixTimeStampToDateTime(utcExpiryDate);

            if (expiryDate > DateTime.UtcNow)
            {
                userCommon.Status = false;
                return userCommon;

            }

            //check xem refreshToken có đúng không. 
            var refreshToken = _refreshTokenRepository.GetTokenByName(listToken.RefreshToken);
            if (refreshToken == null)
            {
                userCommon.Status = false;
                return userCommon;
            }
            // check xem refreshToken còn sd được không, nếu ko được thì update lại nó 
            if (!refreshToken.IsUsed)
            {
                refreshToken.IsUsed = true;
            }

            // check xem tokenID có giống với refreshTokenId không?
            var tokenId = tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
            if (refreshToken.JwtId != tokenId)
            {
                userCommon.Status = false;
                return userCommon;
            }
            // cập nhật refreshtoken  và token mới
            var newToken = CreateToken(refreshToken.User.Name);

            _refreshTokenRepository.Update(refreshToken);
            _UnitOfWork.SaveChanges();

            userCommon.Token = newToken;
            userCommon.RefreshToken = refreshToken.Token;
            userCommon.Status = true;

            return userCommon;
        }

        // coppy, chưa debug
        private DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            var dateTimeVal = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTimeVal = dateTimeVal.AddSeconds(unixTimeStamp).ToUniversalTime();

            return dateTimeVal;
        }

    }
}
