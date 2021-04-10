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
    public class HistoryLogic
    {
        public void AddHistory(UserHistory userData)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DatabaseContext"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_SelectQuestionsNumbers", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("UserID", userData.UserID);
                cmd.Parameters.AddWithValue("Date", userData.Date);
                cmd.Parameters.AddWithValue("Category", userData.Category);
                cmd.Parameters.AddWithValue("Type", userData.Type);
                cmd.Parameters.AddWithValue("Score", userData.Score);
                cmd.Parameters.AddWithValue("Passed", userData.Passed);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                con.Close();
            }
        }
    }
}