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
                string connectionString = ConfigurationManager.ConnectionStrings["QuestionContext"].ConnectionString;

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
    }
}