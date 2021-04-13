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
    public class IncorrectAnswersLogic
    {
        public List<QuestionsAndAnswers> getIncorrectAnswers(UserAnswersList userAnswerList)
        {
            // Stworzenie listy poprawnych odpowiedzi
            List<UserAnswer> correctAnswers = new List<UserAnswer>();

            // Pobranie z bazy odpowiedzi dla pytań i dodanie ich do listy poprawnych odpowiedzi
            for (int i = 0; i < userAnswerList.userAnswersList.Count(); i++)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["DatabaseContext"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_SelectOneAnswer", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Number", userAnswerList.userAnswersList[i].Number);
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        UserAnswer answer = new UserAnswer();
                        answer.Number = userAnswerList.userAnswersList[i].Number;
                        answer.Answer = (string)rdr["que_CorrectAnswer"];

                        correctAnswers.Add(answer);
                    }

                    con.Close();
                }
            }


            // Sprawdzenie odpowiedzi i policzenie punktów 
            // jeżeli udzielono złej odpowiedzi pytanie zostaje dodane do listy złych odpowiedzi
            List<UserAnswer> incorrectAnswers = new List<UserAnswer>();

            for (int i = 0; i < userAnswerList.userAnswersList.Count(); i++)
            {
                if (userAnswerList.userAnswersList[i].Answer != correctAnswers[i].Answer)
                {
                    incorrectAnswers.Add(userAnswerList.userAnswersList[i]);
                }
            }

            // Pobranie z bazy odpowiedzi do niepoprawnych pytań
            // Sklejenie danych w listę z numerem pytania, odpowiedzią usera i poprawną odpowiedzią
            List<QuestionsAndAnswers> qaList = new List<QuestionsAndAnswers>();

            for (int i = 0; i < incorrectAnswers.Count(); i++)
            {
                QuestionsAndAnswers qa = new QuestionsAndAnswers();
                qa.UserAnswer = incorrectAnswers[i].Answer;
                qa.Number = incorrectAnswers[i].Number;

                string connectionString = ConfigurationManager.ConnectionStrings["DatabaseContext"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_SelectOneAnswer", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Number", userAnswerList.userAnswersList[i].Number);
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        qa.CorrectAnswer = (string)rdr["que_CorrectAnswer"];

                        qaList.Add(qa);
                    }

                    con.Close();
                }
            }


            return qaList;
        }
    }
}