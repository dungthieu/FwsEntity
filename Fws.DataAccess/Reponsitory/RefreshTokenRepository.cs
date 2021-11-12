using Fws.DataAccess.Interface;
using Fws.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fws.DataAccess.Reponsitory
{
    public class RefreshTokenRepository : BaseRepository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(NotesContext context) : base(context)
        {

        }

        public RefreshToken GetTokenByName(string name)
        {
            if (name != null)
            {
                return Dbset.Include("User").FirstOrDefault(x => x.Token.Equals(name));
            }
            return null;
        }
        public override RefreshToken Update(RefreshToken entity)
        {
            if (entity != null)
            {
                Dbset.Update(entity);
                return entity;
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
