using Fws.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fws.DataAccess.Interface
{
   public interface IUserRepository : IBaseRepository<User>
    {
        public User GetUser(string name, string password);
    }
}
