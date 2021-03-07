using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelloWORD.Models.Entity
{
    public class UserRegistrationData : UserShowData
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
        public string RepeatEmail { get; set; }
    }
}