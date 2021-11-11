using System;
using System.Collections.Generic;

#nullable disable

namespace Fws.Model.Entities
{
    public partial class Student
    {
        public string Id { get; set; }
        public string StudentName { get; set; }
        public string StudentCode { get; set; }
        public int Age { get; set; }
        public string ClassId { get; set; }
        public string PhoneNumber { get; set; }
    }
}
