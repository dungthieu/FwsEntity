using System;
using System.Collections.Generic;

#nullable disable

namespace Fws.Model.Entities
{
    public partial class Note
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal? Age { get; set; }
        public decimal? Score { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
}
