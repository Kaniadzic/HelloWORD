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
    public class ResultLogic
    {
        public int calculateResult(UserAnswersList userAnswerList)
        {
            int score = 0;

            // Usunięcie z listy odpowiedzi pytań na które nie udzielono odpowiedzi
            for (int i=userAnswerList.userAnswersList.Count()-1; i >= 0; i--)
            {
                if (userAnswerList.userAnswersList[i].Answer == null)
                {
                    userAnswerList.userAnswersList.RemoveAt(i);
                }    
            }

            // Jeżeli usunięto wszystkie pytania to test oddano pusty
            if (userAnswerList.userAnswersList.Count() == 0)
            {
                return 0;
            }

            // Stworzenie listy poprawnych odpowiedzi
            List<UserAnswer> correctAnswers = new List<UserAnswer>();

            // Pobranie z bazy odpowiedzi dla pytań i dodanie ich do listy poprawnych odpowiedzi
            for (int i=0; i < userAnswerList.userAnswersList.Count(); i++)
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
                        answer.Answer = (string)rdr["que_QuestionAnswer"];

                        correctAnswers.Add(answer);
                    }
                }
            }


            // Sprawdzenie odpowiedzi i policzenie punktów 
            // jeżeli udzielono złej odpowiedzi pytanie zostaje dodane do listy złych odpowiedzi
            List<UserAnswer> incorrectAnswers = new List<UserAnswer>();

            for (int i=0; i < userAnswerList.userAnswersList.Count(); i++)
            {
                if (userAnswerList.userAnswersList[i].Answer == correctAnswers[i].Answer)
                {
                    score++;
                }
            }

            return score;
        }

        public int calculateExamResult(List<ExamAnswer> examAnswers)
        {
            int userScore = 0;

            for (int i=0; i < examAnswers.Count(); i++)
            {
                userScore += checkExamAnswer(examAnswers[i].Number, examAnswers[i].Type, examAnswers[i].Answer);
            }

            return userScore;
        }

        // sprawdzamy czy odpowiedź jest dobra
        // jesli tak dodajemy jej wartość punktową do wyniku egzaminu
        // jeśli nie dodajemy pytanie do listy złych odpowiedzi
        public int checkExamAnswer(int number, string type, string answer = null)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DatabaseContext"].ConnectionString;
            int answerScore = 0;
            string correctAnswer = null;

            if (number == null || number < 0)
            {
                return 0;
            }

            if (type == "Traffic")
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_SelectTrafficCorrectAnswer", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Number", number);
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        answerScore = (int)rdr["ext_Score"];
                        correctAnswer = (string)rdr["ext_CorrectAnswer"];
                    }
                }   

                if (answer == correctAnswer && answerScore > 0)
                {
                    return answerScore;
                }
                else
                {
                    return 0;
                }
            }
            else if (type == "Categorized")
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_SelectCategorizedCorrectAnswer", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Number", number);
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        answerScore = (int)rdr["exc_Score"];
                        correctAnswer = (string)rdr["exc_CorrectAnswer"];
                    }
                }

                if (answer == correctAnswer && answerScore > 0)
                {
                    return answerScore;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }
    }
}