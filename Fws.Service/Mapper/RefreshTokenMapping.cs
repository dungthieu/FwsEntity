using Fws.Model.Entities;
using Fws.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fws.Service.Mapper
{
    public static class RefreshTokenMapping
    {
        public static RefreshTokenModel MapToModel(this RefreshToken entity)
        {
            return new RefreshTokenModel
            {
                Id = entity.Id,
                UserId = entity.UserId,
                IsUsed = entity.IsUsed,
                Token = entity.Token,
                CreateDate = entity.CreateDate,
                ExpiryDate = entity.ExpiryDate,
                JwtId = entity.JwtId
            };
        }

        public static RefreshToken MapToEntity(this RefreshTokenModel model)
        {
            RefreshToken token = new RefreshToken();
            token.Id = model.Id;
            token.UserId = model.UserId;
            token.IsUsed = model.IsUsed;
            token.Token = model.Token;
            token.CreateDate = model.CreateDate;
            token.ExpiryDate = model.ExpiryDate;
            token.JwtId = model.JwtId;
            return token;


        }
        public static List<RefreshTokenModel> MapToModels(this List<RefreshToken> entities)
        {
            return entities.Select(x => x.MapToModel()).ToList();
        }
    }
}
