using Fws.DataAccess.Interface;
using Fws.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fws.DataAccess.Reponsitory
{
    public class UserRepository :BaseRepository<User> ,IUserRepository
    {
        public UserRepository(NotesContext context) :base(context)
        {
        }

        public User GetUser(string name, string password)
        {
            return Dbset.Where(x => x.Name.Contains(name) && x.PassWord.Contains(password)).FirstOrDefault();
        }
    }
}
