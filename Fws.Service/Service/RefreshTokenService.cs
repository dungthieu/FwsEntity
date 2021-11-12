using Fws.DataAccess.Interface;
using Fws.Model.Entities;
using Fws.Model.Models;
using Fws.Service.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fws.Service.Service
{
    public interface IRefreshTokenService
    {
        /// <summary>
        /// random ra 1 chuỗi string tạo refreshtoken
        /// </summary>
        /// <returns></returns>
        string RandomStringToken();
        RefreshTokenModel CreateRefreshToken(string tokenId, string userId);
    }
    public class RefreshTokenService : IRefreshTokenService
    {
        protected IBaseRepository<RefreshToken> _baseRepository;
        protected readonly IUnitOfWork UnitOfWork;

        int lengthRandom = 10;
        string charRandom = "ABCDEFGHIJKLMNOPQRSTWXYZ1234567890";

        public RefreshTokenService(IBaseRepository<RefreshToken> baseRepository, IUnitOfWork unitOfWork)
        {
            _baseRepository = baseRepository;
            UnitOfWork = unitOfWork;
        }
        public RefreshTokenModel CreateRefreshToken(string tokenId, string userId)
        {
            // create new refreshtoken
            var newRefreshToken = new RefreshTokenModel()
            {
                Id = Guid.NewGuid().ToString(),
                JwtId = tokenId,
                IsUsed = true,
                UserId = userId,
                CreateDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddYears(1),
                Token = RandomStringToken() + Guid.NewGuid()
            };
            // map and insert
            _baseRepository.Insert(newRefreshToken.MapToEntity());
            //save changes
            UnitOfWork.SaveChanges();
            return newRefreshToken;
        }

        public string RandomStringToken()
        {
            var random = new Random();
            //coppy
            return new string(Enumerable.Repeat(charRandom, lengthRandom)
            .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
