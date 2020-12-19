using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HelloWORD.Models.Logic
{
    public class QuestionCategoryLogic
    {
        public string checkQuestionCategory(int number)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["InformationContext"].ConnectionString;
            string category = "";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_CheckQuestionCategory", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Number", number);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    category = (string)rdr["que_Category"];
                }
            }

            return category;
        }
    }
}