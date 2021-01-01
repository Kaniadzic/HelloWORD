﻿using HelloWORD.Models.Entity;
using HelloWORD.Models.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelloWORD.Controllers
{
    public class ExamController : Controller
    {
        [HttpGet]
        public ActionResult Quiz(string category, short questions)
        {
            QuestionLogic questionLogic = new QuestionLogic();
            List<Questions> questionsList = new List<Questions>();

            questionsList = questionLogic.getQuestion(category, questions);

            return View(questionsList);
        }

        [HttpPost]
        public ActionResult Quiz(UserAnswersList userAnswers)
        {
            bool longQuiz = false;
            string result;

            if (userAnswers.userAnswersList.Count() == 40)
            {
                longQuiz = true;
            }

            // Zdobycie niepoprawnych odpowiedzi użytkownika
            IncorrectAnswersLogic incorrectAnswersLogic = new IncorrectAnswersLogic();
            List<QuestionsAndAnswers> qaList = incorrectAnswersLogic.getIncorrectAnswers(userAnswers);

            // Sprawdzenie kategorii pytania
            QuestionCategoryLogic questionCategoryLogic = new QuestionCategoryLogic();
            string category = questionCategoryLogic.checkQuestionCategory(userAnswers.userAnswersList[0].Number);

            // Sprawdzenie ilości punktów
            ResultLogic resultLogic = new ResultLogic();
            int score = resultLogic.calculateResult(userAnswers);

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

            // @TODO: przekazać jakoś listę qaList do Result
            return RedirectToAction("Result", "Exam", new { result = result, category = category, longQuiz = longQuiz});
        }


        [HttpGet]
        public ActionResult Result(string result, string category, bool longQuiz, List<QuestionsAndAnswers> qaList)
        {  
            ViewBag.Result = result;
            ViewBag.Category = category;
            ViewBag.LongQuiz = longQuiz;

            return View(qaList);
        }
    }
}