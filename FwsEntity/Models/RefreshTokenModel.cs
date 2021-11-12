using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fws.Model.Models
{
    public class RefreshTokenModel
    {

        public string Id { get; set; }
        public string UserId { get; set; }
        public string Token { get; set; }
        public bool IsUsed { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string JwtId { get; set; }
    }
}
