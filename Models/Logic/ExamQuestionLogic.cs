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
    public class ExamQuestionLogic
    {
        public List<TrafficQuestion> getTrafficQuestions(int questionsNumber)
        {
            // Zdefiniowanie potrzebnych obiektów, list etc.
            string connectionString = ConfigurationManager.ConnectionStrings["DatabaseContext"].ConnectionString;
            List<TrafficQuestion> questions = new List<TrafficQuestion>();
            List<int> questionsIDs = new List<int>();
            List<int> usedIDs = new List<int>();
            Random rnd = new Random();

            // Wybranie wszystkich numerów pytań z bazy
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_SelectTrafficQuestionsnumbers", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    questionsIDs.Add((int)rdr["ext_Number"]);
                }
            }

            // Wybranie x losowych id pytań, gdzie x to questionsNumber
            while (usedIDs.Count < questionsNumber)
            {
                int randomNumber = rnd.Next(0, questionsIDs.Count);

                usedIDs.Add(questionsIDs[randomNumber]);
                questionsIDs.RemoveAt(randomNumber);
            }

            // Wybranie pytań z bazy
            for (int i = 0; i < usedIDs.Count; i++)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_SelectExamTrafficQuestion", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Number", usedIDs[i]);

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        TrafficQuestion quest = new TrafficQuestion();
                        quest.Number = (int)rdr["ext_Number"];
                        quest.Answer = (string)rdr["ext_Answer"];
                        quest.Question = (string)rdr["ext_Question"];
                        quest.MediaType = (string)rdr["ext_MediaType"];
                        quest.MediaPath = (string)rdr["ext_MediaPath"];

                        questions.Add(quest);
                    }
                }
            }

            return questions;
        }

        public List<CategorizedQuestion> getCategorizedQuestions(string category, int questionsNumber)
        {
            // Zdefiniowanie potrzebnych obiektów, list etc.
            string connectionString = ConfigurationManager.ConnectionStrings["DatabaseContext"].ConnectionString;
            List<CategorizedQuestion> questions = new List<CategorizedQuestion>();
            List<int> questionsIDs = new List<int>();
            List<int> usedIDs = new List<int>();
            Random rnd = new Random();

            // Wybranie wszystkich numerów pytań z bazy
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_SelectCategorizedQuestionsNumbers", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Category", category);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    questionsIDs.Add((int)rdr["exc_Number"]);
                }
            }

            // Wybranie x losowych id pytań, gdzie x to questionsNumber
            while (usedIDs.Count < questionsNumber)
            {
                int randomNumber = rnd.Next(0, questionsIDs.Count);

                usedIDs.Add(questionsIDs[randomNumber]);
                questionsIDs.RemoveAt(randomNumber);
            }

            // Wybranie pytań z bazy
            for (int i = 0; i < usedIDs.Count; i++)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_SelectExamCategorizedQuestion", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Number", usedIDs[i]);
                    cmd.Parameters.AddWithValue("Category", category);

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        CategorizedQuestion quest = new CategorizedQuestion();
                        quest.Number = (int)rdr["exc_Number"];
                        quest.Answer = (string)rdr["exc_Answer"];
                        quest.Question = (string)rdr["exc_Question"];
                        quest.Category = (string)rdr["exc_Category"];
                        quest.AnswerA = (string)rdr["exc_AnswerA"];
                        quest.AnswerB = (string)rdr["exc_AnswerB"];
                        quest.AnswerC = (string)rdr["exc_AnswerC"];
                        quest.MediaType = (string)rdr["exc_MediaType"];
                        quest.MediaPath = (string)rdr["exc_MediaPath"];

                        questions.Add(quest);
                    }
                }
            }

            return questions;
        }
    }
}