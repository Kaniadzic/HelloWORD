using HelloWORD.Models.Entity;
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

            return RedirectToAction("Result", "Exam", new { result = result});
        }


        [HttpGet]
        public ActionResult Result(string result)
        {  
            ViewBag.Result = result;

            return View();
        }
    }
}