using HelloWORD.Models.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HelloWORD.Models.Logic
{
    public class PasswordEditLogic
    {
        public void sendCode(string userEmail)
        {
            string code = generateCode();
            insertCode(userEmail, code);

            GMailer.GmailUsername = "licencjathelloword@gmail.com";
            // hasło do konta
            {
                GMailer.GmailPassword = "/a12Uil?09wwE";
            }

            GMailer mailer = new GMailer();
            mailer.ToEmail = userEmail;
            mailer.Subject = "HelloWORD - kod zmiany hasła";
            mailer.Body = "Twój kod to: " + code + ". Wiadomość została wysłana automatycznie, proszę nie odpowiadać na tego maila.";
            mailer.IsHtml = true;
            mailer.Send();
        }

        private string generateCode()
        {
            char[] chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ123456789".ToCharArray();
            string code="";
            Random rnd = new Random();

            for (int i=0; i<6 ;i++)
            {
                short rand = (short)rnd.Next(chars.Length);
                code += chars[rand];
            }
            
            return code;
        }

        private void insertCode(string userEmail, string code)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DatabaseContext"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_InsertPasswordLog", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("UserEmail", userEmail);
                cmd.Parameters.AddWithValue("UserCode", code);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                con.Close();
            }
        }

        public bool verifyPasswordEditData(UserPassword userData)
        {
            if (checkNewPasswords(userData.newPassword, userData.newPasswordRepeat) == false)
            {
                return false;
            }

            if (verifyOldPassword(userData.userData, userData.oldPassword, userData.autorisationCode) == false)
            {
                return false;
            }

            return true;
        }

        // sprawdzenie czy podane hasło i adres email są odpowiednie
        private bool verifyOldPassword(string userEmail, string password, string authCode)
        {
            HashLogic hashLogic = new HashLogic();
            string connectionString = ConfigurationManager.ConnectionStrings["DatabaseContext"].ConnectionString;
            int userID = 0;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_CheckUserPasswordResetData", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("UserEmail", userEmail);
                cmd.Parameters.AddWithValue("Password", hashLogic.HashString(password));

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    userID = (int)rdr["usr_ID"];
                }
                con.Close();
            }

            if (userID > 0)
            {
                if (verifyAuthCode(userID, authCode) == false)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        // sprawdzenie czy kod autoryzacyjny jest poprawny i wciąż ważny
        private bool verifyAuthCode(int userID, string userCode)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DatabaseContext"].ConnectionString;
            string authCode = "";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_CheckAuthCode", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("UserID", userID);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    authCode = (string)rdr["lpa_Code"];
                }
                con.Close();
            }

            if (authCode == userCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // sprawdzenie czy nowe hasła są takie same
        private bool checkNewPasswords(string password, string passwordRepeat)
        {
            HashLogic hashLogic = new HashLogic();

            if (hashLogic.HashString(password) != hashLogic.HashString(passwordRepeat))
            {
                return false;
            }

            return true;
        }
    }
}