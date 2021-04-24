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
    }
}