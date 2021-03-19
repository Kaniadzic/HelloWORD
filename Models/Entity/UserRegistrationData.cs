using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HelloWORD.Models.Entity
{
    public class UserRegistrationData : UserShowData
    {
        [Required]
        [MinLength(6)]
        public string Login { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [MinLength(6)]
        public string RepeatPassword { get; set; }

        [Required]
        public string RepeatEmail { get; set; }
    }
}