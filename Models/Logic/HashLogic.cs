using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace HelloWORD.Models.Logic
{
    public class HashLogic
    {
        public string HashString(string dataToHash)
        {
            SHA256 sha = SHA256.Create();

            byte[] hashedPassword = sha.ComputeHash(Encoding.UTF8.GetBytes(dataToHash));

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < hashedPassword.Length; i++)
            {
                builder.Append(hashedPassword[i].ToString("x2"));
            }

            return builder.ToString();
        }
    }
}