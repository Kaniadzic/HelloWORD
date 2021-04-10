using HelloWORD.Models.Entity;
using HelloWORD.Models.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelloWORD.Controllers
{
    public class QuizController : Controller
    {
        [HttpGet]
        public ActionResult Quiz(string category, short questions)
        {
            QuestionLogic questionLogic = new QuestionLogic();
            List<Questions> questionsList = new List<Questions>();

            questionsList = questionLogic.getQuestion(category, questions);

            if (questionsList.Count() == 0)
            {
                return RedirectToAction("Index", "Home");
            }

            // Zapisanie listy pytań w sesji, przyda się to do stworzenia listy z niepoprawnymi odpowiedziami
            System.Web.HttpContext.Current.Session["questionsList"] = questionsList;

            return View(questionsList);
        }

        [HttpPost]
        public ActionResult Quiz(UserAnswersList userAnswers)
        {
            // Sprawdzenie jaki test został oddany (przyda się do generowania buttona w widoku z wynikiem)
            bool longQuiz = false;
            if (userAnswers.userAnswersList.Count() == 40)
            {
                longQuiz = true;
            }

            // Zdobycie niepoprawnych odpowiedzi użytkownika
            IncorrectAnswersLogic incorrectAnswersLogic = new IncorrectAnswersLogic();
            List<QuestionsAndAnswers> incorrectAnswers = incorrectAnswersLogic.getIncorrectAnswers(userAnswers);

            // Sprawdzenie kategorii pytania
            QuestionCategoryLogic questionCategoryLogic = new QuestionCategoryLogic();
            string category = questionCategoryLogic.checkQuestionCategory(userAnswers.userAnswersList[0].Number);

            // Sprawdzenie ilości punktów
            ResultLogic resultLogic = new ResultLogic();
            int score = resultLogic.calculateResult(userAnswers);
       
            // Stworzenie stringa z wynikiem pasującego do rodzaju testu
            string result;
            if (longQuiz)
            {
                result = score + "/40";
            }
            else
            {
                if (score == 1)
                {
                    result = "Dobrze!";
                }
                else
                {
                    result = "Źle!";
                }
            }

            // Pobranie z sesji listy pytań
            List<Questions> questionsList = (List<Questions>)System.Web.HttpContext.Current.Session["questionsList"];

            // Przekazanie danych z listy pytań do odpowiedzi
            for (int i=0; i<questionsList.Count(); i++)
            {
                for (int j=0; j<incorrectAnswers.Count(); j++)
                {
                    if (questionsList[i].Number == incorrectAnswers[j].Number)
                    {
                        incorrectAnswers[j].Question = questionsList[i].Question;
                        incorrectAnswers[j].AnswerA = questionsList[i].AnswerA;
                        incorrectAnswers[j].AnswerB = questionsList[i].AnswerB;
                        incorrectAnswers[j].AnswerC = questionsList[i].AnswerC;
                        incorrectAnswers[j].AnswerD = questionsList[i].AnswerD;
                        incorrectAnswers[j].Picture = questionsList[i].Picture;
                        break;
                    }
                }
            }

            // Zapisanie listy niepoprawnych odpowiedzi w sesji
            System.Web.HttpContext.Current.Session["incorrectAnswers"] = incorrectAnswers;

            // Dodanie histori nauki (tylko dla testu długiego)
            if (System.Web.HttpContext.Current.Session["userID"] != null && longQuiz)
            {
                UserHistory userHistory = new UserHistory();
                userHistory.UserID = (int)System.Web.HttpContext.Current.Session["userID"];
                userHistory.Category = category;
                userHistory.Date = DateTime.Now;
                userHistory.Type = "Quiz";
                userHistory.Score = score;

                userHistory.Passed = false;

                HistoryLogic historyLogic = new HistoryLogic();
                historyLogic.InsertUserHistory(userHistory);
            }

            return RedirectToAction("QuizResult", "Quiz", new { result = result, category = category, longQuiz = longQuiz});
        }


        [HttpGet]
        public ActionResult QuizResult(string result, string category, bool longQuiz)
        {  
            // Dane w ViewBagach służą do wyświetlenia wyniku, generowania przycisków itp.
            ViewBag.Result = result;
            ViewBag.Category = category;
            ViewBag.LongQuiz = longQuiz;

            // Lista niepoprawnych pytań
            List<QuestionsAndAnswers> incorrectAnswers = (List<QuestionsAndAnswers>)System.Web.HttpContext.Current.Session["incorrectAnswers"];

            return View(incorrectAnswers);
        }
    }
}