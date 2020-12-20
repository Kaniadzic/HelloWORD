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
    public class QuestionLogic
    {
        public List<Questions> getQuestion(string category, int questionsNumber)
        {
            // Zdefiniowanie potrzebnych obiektów, list etc.
            string connectionString = ConfigurationManager.ConnectionStrings["QuestionContext"].ConnectionString;
            List<Questions> questions = new List<Questions>();
            List<int> questionsIDs = new List<int>(); 
            List<int> usedIDs = new List<int>();
            Random rnd = new Random();

            // Wybranie wszystkich numerów pytań z bazy
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_SelectQuestionsNumbers", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    questionsIDs.Add((int)rdr["que_number"]);
                }
            }

            // Wybranie x losowych id pytań, gdzie x to questionsNumber
            while (usedIDs.Count < questionsNumber)
            {
                int randomNumber = rnd.Next(0, questionsIDs.Count);

                usedIDs.Add(questionsIDs[randomNumber]);
                questionsIDs.RemoveAt(randomNumber);
            }

            for (int i=0; i<usedIDs.Count; i++)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_SelectOneQuestion", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Category", category);
                    cmd.Parameters.AddWithValue("Number", usedIDs[i]);

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        Questions quest = new Questions();
                        quest.Number = (int)rdr["que_Number"];
                        quest.Category = (string)rdr["que_Category"];
                        quest.Question = (string)rdr["que_Question"];
                        quest.AnswerA = (string)rdr["que_AnswerA"];
                        quest.AnswerB = (string)rdr["que_AnswerB"];
                        quest.AnswerC = (string)rdr["que_AnswerC"];
                        quest.AnswerD = (string)rdr["que_AnswerD"];
                        quest.Picture = (string)rdr["que_Picture"];

                        questions.Add(quest);
                    }
                }
            }

            return questions;
        }
    }
}