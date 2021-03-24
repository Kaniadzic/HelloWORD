using HelloWORD.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

            HashLogic hashLogic = new HashLogic();

            userRegistrationData.Password = hashLogic.HashString(userRegistrationData.Password);
  
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

        public int LoginUser(UserLoginData userLoginData)
        {
            int userID = -1;

            HashLogic hashLogic = new HashLogic();
            userLoginData.Password = hashLogic.HashString(userLoginData.Password);

            string connectionString = ConfigurationManager.ConnectionStrings["DatabaseContext"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_SelectLoggingUserID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Login", userLoginData.Login);
                cmd.Parameters.AddWithValue("Password", userLoginData.Password);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    userID = (int)rdr["usr_ID"];
                }
            }

            return userID;
        }

        public void SaveUserIdSession(int id)
        {
            System.Web.HttpContext.Current.Session["userID"] = id;
        }

        public void Logout()
        {
            System.Web.HttpContext.Current.Session["userID"] = null;
        }
    }
}