using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelloWORD.Models.Entity
{
    public class UserShowData
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreationDate { get; set; }
        public string Email { get; set; }
    }
}