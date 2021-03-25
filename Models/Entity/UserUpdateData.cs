using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelloWORD.Models.Entity
{
    public class UserUpdateData
    {
        public int id { get; set; }
        public string password { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
    }
}