using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HelloWORD.Models.Entity
{
    public class UserUpdateData
    {
        [Required]
        public int id { get; set; }

        [Required]
        [Display(Name = "Imię")]
        public string firstName { get; set; }

        [Required] 
        [Display(Name = "Nazwisko")]
        public string lastName { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string email { get; set; }

        [Required]
        [Display(Name = "Powtórz email")]
        public string repeatEmail { get; set; }
    }
}