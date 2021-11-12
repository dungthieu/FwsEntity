using Fws.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fws.DataAccess.Interface
{
    public interface IRefreshTokenRepository : IBaseRepository<RefreshToken>
    {
        public RefreshToken GetTokenByName(string name);
    }
}
