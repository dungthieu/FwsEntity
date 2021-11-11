using Fws.DataAccess.Interface;
using Fws.Model.Entities;
using Fws.Model.Models;
using Fws.Service.Interface;
using Fws.Service.Mapper;
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
        UserCommon CheckAuthencation(string user, string password, string keyHash);
        string CreateToken(string user, string password, string keyHash);
    }
    public class UserService : IUserService
    {
        protected IBaseRepository<User> _baseRepository;
        protected IUserRepository _userRepository;
        public UserService(IBaseRepository<User> baseRepository, IUserRepository userRepository)
        {
            _baseRepository = baseRepository;
            _userRepository = userRepository;
        }

        public UserCommon CheckAuthencation(string user, string password, string keyHash)
        {
            var checkUser = _userRepository.GetUser(user, password);
            var userCommon = new UserCommon();

            if (checkUser == null)
            {
                userCommon.Message = "Tài khoản, mật khẩu không đúng, xin vui lòng đăng nhập lại!";
                userCommon.Status = false;
                return userCommon;
            }
            userCommon.Token = CreateToken(user, password, keyHash);
            userCommon.Name = user;
            userCommon.PassWord = password;
            userCommon.Status = true;
            userCommon.Message = "xin chào " + user;

            return userCommon;
        }

        public string CreateToken(string user, string password, string keyHash)
        {
            // 1. Create Security Token Handler
            var tokenHandler = new JwtSecurityTokenHandler();

            // 2. Create Private Key to Encrypted
            // one user , one key., tự độnh sinh key theo từng user.
            var tokenKey = Encoding.ASCII.GetBytes(keyHash);
            Console.WriteLine(tokenKey);
            
            //3. Create JETdescriptor
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user)
                    }),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            //4. Create Token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // 5. Return Token from method
            return tokenHandler.WriteToken(token);
        }

        public List<UserModel> GetAllUser()
        {
            return _baseRepository.GetAll().ToList().MapToModels();
        }
    }
}
