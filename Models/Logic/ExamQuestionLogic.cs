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
        private List<TrafficQuestion> getTrafficQuestions(int questionsNumber, int questionsScore)
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
                cmd.Parameters.AddWithValue("Score", questionsScore);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    questionsIDs.Add((int)rdr["ext_Number"]);
                }

                con.Close();
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
                    cmd.Parameters.AddWithValue("Score", questionsScore);

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        TrafficQuestion quest = new TrafficQuestion();
                        quest.Number = (int)rdr["ext_Number"];
                        quest.Question = (string)rdr["ext_Question"];
                        quest.Score = (int)rdr["ext_Score"];
                        quest.MediaType = (string)rdr["ext_MediaType"];
                        quest.MediaPath = (string)rdr["ext_MediaPath"];

                        questions.Add(quest);
                    }

                    con.Close();
                }
            }

            return questions;
        }

        private List<CategorizedQuestion> getCategorizedQuestions(string category, int questionsNumber, int questionsScore)
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
                cmd.Parameters.AddWithValue("Score", questionsScore);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    questionsIDs.Add((int)rdr["exc_Number"]);
                }

                con.Close();
            }    
            
            // Jeżeli nie ma żadnych pytań to zwracamy pustą liste
            if (questionsIDs.Count == 0)
            {
                return questions;
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
                    cmd.Parameters.AddWithValue("Score", questionsScore);

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        CategorizedQuestion quest = new CategorizedQuestion();
                        quest.Number = (int)rdr["exc_Number"];
                        quest.Question = (string)rdr["exc_Question"];
                        quest.Category = (string)rdr["exc_Category"];
                        quest.Score = (int)rdr["exc_Score"];
                        quest.AnswerA = (string)rdr["exc_AnswerA"];
                        quest.AnswerB = (string)rdr["exc_AnswerB"];
                        quest.AnswerC = (string)rdr["exc_AnswerC"];
                        quest.MediaType = (string)rdr["exc_MediaType"];
                        quest.MediaPath = (string)rdr["exc_MediaPath"];

                        questions.Add(quest);
                    }

                    con.Close();
                }
            }

            return questions;
        }

        public List<TrafficQuestion> createTrafficList()
        {
            ExamQuestionLogic examLogic = new ExamQuestionLogic();

            List<TrafficQuestion> trafficQuestions = new List<TrafficQuestion>();
            List<TrafficQuestion> trafficQuestionsScore3 = new List<TrafficQuestion>();
            List<TrafficQuestion> trafficQuestionsScore2 = new List<TrafficQuestion>();
            List<TrafficQuestion> trafficQuestionsScore1 = new List<TrafficQuestion>();

            trafficQuestionsScore3 = examLogic.getTrafficQuestions(10, 3);
            trafficQuestionsScore2 = examLogic.getTrafficQuestions(6, 2);
            trafficQuestionsScore1 = examLogic.getTrafficQuestions(4, 1);

            trafficQuestions = trafficQuestionsScore3.Concat(trafficQuestionsScore2).Concat(trafficQuestionsScore1).ToList();

            return trafficQuestions;
        }

        public List<CategorizedQuestion> createCategorizedList(string category)
        {
            ExamQuestionLogic examLogic = new ExamQuestionLogic();

            List<CategorizedQuestion> categorizedQuestions = new List<CategorizedQuestion>();
            List<CategorizedQuestion> categorizedQuestionsScore3 = new List<CategorizedQuestion>();
            List<CategorizedQuestion> categorizedQuestionsScore2 = new List<CategorizedQuestion>();
            List<CategorizedQuestion> categorizedQuestionsScore1 = new List<CategorizedQuestion>();

            categorizedQuestionsScore3 = examLogic.getCategorizedQuestions(category, 6, 3);
            categorizedQuestionsScore2 = examLogic.getCategorizedQuestions(category, 4, 2);
            categorizedQuestionsScore1 = examLogic.getCategorizedQuestions(category, 2, 1);

            categorizedQuestions = categorizedQuestionsScore3.Concat(categorizedQuestionsScore2).Concat(categorizedQuestionsScore1).ToList();

            return categorizedQuestions;
        }

    }
}