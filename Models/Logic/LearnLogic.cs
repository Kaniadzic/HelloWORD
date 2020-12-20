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
    public class LearnLogic
    {
        public List<LearnCategory> getLessons(string category)
        {
            List<LearnCategory> lessons = new List<LearnCategory>();

            string connectionString = ConfigurationManager.ConnectionStrings["QuestionContext"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_SelectLessons", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Category", category);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    LearnCategory lesson = new LearnCategory();

                    lesson.ID = (int)rdr["les_ID"];
                    lesson.Header = (string)rdr["les_Header"];
                    lesson.Picture = (string)rdr["les_Picture"];

                    lessons.Add(lesson);
                }
            }

            return lessons;
        }


        public List<Lesson> getLesson(int LessonID)
        {
            List<Lesson> lessons = new List<Lesson>();

            string connectionString = ConfigurationManager.ConnectionStrings["QuestionContext"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_SelectLessoncontent", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("LessonID", LessonID);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Lesson lesson = new Lesson();

                    lesson.Content = (string)rdr["lc_Content"];
                    lesson.Picture = (string)rdr["lc_Picture"];
                    lesson.Tooltip = (string)rdr["lc_Tooltip"];

                    lessons.Add(lesson);
                }
            }

            return lessons;
        }
    }
}