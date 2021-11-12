using System;
using System.Collections.Generic;

#nullable disable

namespace Fws.Model.Entities
{
    public partial class RefreshToken
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Token { get; set; }
        public bool IsUsed { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string JwtId { get; set; }

        public virtual User User { get; set; }
    }
}
