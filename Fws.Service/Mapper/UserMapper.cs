using Fws.Model.Entities;
using Fws.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fws.Service.Mapper
{
    // để static để cho các hàm khác có thể truy cập đuwojc
    public static class UserMapper
    {
        public static UserModel MapToModel(this User entity)
        {
            return new UserModel
            {
                Id = entity.Id,
                Name = entity.Name,
                PassWord = entity.PassWord
            };
           
        }
        public static List<UserModel> MapToModels(this List<User> entities)
        {
            return entities.Select(x => x.MapToModel()).ToList();
        }
    }
}