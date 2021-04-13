using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using HelloWORD.Models.Entity;

namespace HelloWORD.Models.Logic
{
    public class InformationLogic
    {
        public List<Information> getInformations()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DatabaseContext"].ConnectionString;

            List<Information> informations = new List<Information>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_SelectInformations", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Information information = new Information();
                    information.Header = (string)rdr["info_Header"];
                    information.Content = (string)rdr["info_Content"];

                    informations.Add(information);
                }

                con.Close();
            }

            return informations;
        } 
    }
}