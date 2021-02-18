using HelloWORD.Models.Entity;
using HelloWORD.Models.Logic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace HelloWORD.Controllers
{
    public class ExamController : Controller
    {
        [HttpGet]
        public ActionResult Start(string category)
        {
            ViewBag.Category = category;
            return View();
        }

        public JsonResult GenerateTrafficQuestions()
        {
            // wygenerowanie lsity pytań ogólnych
            List<TrafficQuestion> trafficQuestions = new List<TrafficQuestion>();
            ExamQuestionLogic examLogic = new ExamQuestionLogic();

            trafficQuestions = examLogic.createTrafficList();

            return Json(trafficQuestions, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GenerateCategorizedQuestions(string category)
        {
            // wygenerowanie listy pytań specjalistycznych
            List<CategorizedQuestion> categorizedQuestions = new List<CategorizedQuestion>();
            ExamQuestionLogic examLogic = new ExamQuestionLogic();

            categorizedQuestions = examLogic.createCategorizedList(category);

            return Json(categorizedQuestions, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Exam(string category)
        {
            if (category == null)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Category = category;

            return View();
        }

        [HttpPost]
        public ActionResult ExamResult(string examAnswers)
        {
            var userAnswers = JsonConvert.DeserializeObject<List<ExamAnswer>>(examAnswers);
            bool examPassed = false;

            ResultLogic resultLogic = new ResultLogic();
            int userScore = resultLogic.calculateExamResult(userAnswers);

            if (userScore >= 68)
            {
                examPassed = true;
            }

            ViewBag.Score = userScore;
            ViewBag.ExamPassed = examPassed;

            return View();
        }
    }
}