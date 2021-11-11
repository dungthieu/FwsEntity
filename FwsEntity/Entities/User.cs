using System;
using System.Collections.Generic;

#nullable disable

namespace Fws.Model.Entities
{
    public partial class User
    {
        public User()
        {
            RoleDetails = new HashSet<RoleDetail>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string PassWord { get; set; }

        public virtual ICollection<RoleDetail> RoleDetails { get; set; }
    }
}
