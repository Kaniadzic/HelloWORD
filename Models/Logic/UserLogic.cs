using HelloWORD.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace HelloWORD.Models.Logic
{
    public class UserLogic
    {
        public UserRegistrationData PrepareUserRegistrationData(UserRegistrationData userRegistrationData)
        {
            userRegistrationData.CreationDate = DateTime.Now;
            userRegistrationData.RepeatPassword = "";
            userRegistrationData.RepeatEmail = "";

            // hashowanie haseł
            using (SHA256 sha = SHA256.Create())
            {
                byte[] hashedPassword = sha.ComputeHash(Encoding.UTF8.GetBytes(userRegistrationData.Password));

                StringBuilder builder = new StringBuilder();
                for(int i=0; i<hashedPassword.Length; i++)
                {
                    builder.Append(hashedPassword[i].ToString("x2"));
                }

                userRegistrationData.Password = builder.ToString();
            }
  
            return userRegistrationData;
        }

        public void CreateUser(UserRegistrationData userData)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DatabaseContext"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_InsertUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Login", userData.Login);
                cmd.Parameters.AddWithValue("Password", userData.Password);
                cmd.Parameters.AddWithValue("FirstName", userData.FirstName);
                cmd.Parameters.AddWithValue("LastName", userData.LastName);
                cmd.Parameters.AddWithValue("CreationDate", userData.CreationDate);
                cmd.Parameters.AddWithValue("Email", userData.Email);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
            }
        }

        public int CheckExistingUsers(string login, string email)
        {
            int userCount = 0;
            string connectionString = ConfigurationManager.ConnectionStrings["DatabaseContext"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_CountExistingUsers", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Login", login);
                cmd.Parameters.AddWithValue("Email", email);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while(rdr.Read())
                {
                    userCount++;
                }
            }

            return userCount;
        }
    }
}