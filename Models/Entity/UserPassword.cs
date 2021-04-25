using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace HelloWORD.Models.Entity
{
    public class UserPassword
    {
        [Required]
        public string userData { get; set; }
        [Required]
        public string oldPassword { get; set; }
        [Required]
        public string newPassword { get; set; }
        [Required]
        public string newPasswordRepeat { get; set; }
        [Required]
        public string autorisationCode { get; set; }
    }
}