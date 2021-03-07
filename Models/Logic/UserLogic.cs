using HelloWORD.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace HelloWORD.Models.Logic
{
    public class UserLogic
    {
        public UserRegistrationData PrepareUserRegistrationData(UserRegistrationData userRegistrationData)
        {
            userRegistrationData.CreationDate = DateTime.Now;

            // hashowanie haseł
            SHA256 sha = new SHA256Managed();

            byte[] originalPassword = Encoding.UTF8.GetBytes(userRegistrationData.Password);
            byte[] hashedPassword = sha.ComputeHash(originalPassword);

            userRegistrationData.Password = Encoding.UTF8.GetString(hashedPassword);
            userRegistrationData.RepeatPassword = "";
            userRegistrationData.RepeatEmail = "";

            return userRegistrationData;
        }
    }
}