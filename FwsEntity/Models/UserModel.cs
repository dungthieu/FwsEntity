using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fws.Model.Models
{
    public class UserModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string PassWord { get; set; }

    }
    public class UserCommon
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; }


    }
}
