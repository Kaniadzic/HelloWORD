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
        public ActionResult Start(string category)
        {
            ViewBag.Category = category;
            return View();
        }

        [HttpGet]
        public ActionResult Exam(string category)
        {
            TrafficQuestion trafficQuestion = new TrafficQuestion();
            CategorizedQuestion categorizedQuestion = new CategorizedQuestion();
            ExamQuestionLogic examLogic = new ExamQuestionLogic();

            // Listy pytań ogólnych i wyspecjalizowanych
            List<TrafficQuestion> trafficQuestions = new List<TrafficQuestion>();
            List<CategorizedQuestion> categorizedQuestions = new List<CategorizedQuestion>();

            trafficQuestions = examLogic.getTrafficQuestions(20);
            categorizedQuestions = examLogic.getCategorizedQuestions(category, 12);

            return View();
        }
    }
}