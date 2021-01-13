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
            List<TrafficQuestion> trafficQuestions= new List<TrafficQuestion>();
            List<TrafficQuestion> trafficQuestionsScore3 = new List<TrafficQuestion>();
            List<TrafficQuestion> trafficQuestionsScore2 = new List<TrafficQuestion>();
            List<TrafficQuestion> trafficQuestionsScore1 = new List<TrafficQuestion>();
            List<CategorizedQuestion> categorizedQuestions = new List<CategorizedQuestion>();
            List<CategorizedQuestion> categorizedQuestionsScore3 = new List<CategorizedQuestion>();
            List<CategorizedQuestion> categorizedQuestionsScore2 = new List<CategorizedQuestion>();
            List<CategorizedQuestion> categorizedQuestionsScore1 = new List<CategorizedQuestion>();

            // @TODO: popytać czy da się lepiej zrobić wybieranie pytań a nie robić 6 list i je sklejać
            trafficQuestionsScore3 = examLogic.getTrafficQuestions(10, 3);
            trafficQuestionsScore2 = examLogic.getTrafficQuestions(6, 2);
            trafficQuestionsScore1 = examLogic.getTrafficQuestions(4, 1);
            categorizedQuestionsScore3 = examLogic.getCategorizedQuestions(category, 6, 3);
            categorizedQuestionsScore2 = examLogic.getCategorizedQuestions(category, 4, 2);
            categorizedQuestionsScore1 = examLogic.getCategorizedQuestions(category, 2, 1);
            // Sklejanie list
            trafficQuestions = trafficQuestionsScore3.Concat(trafficQuestionsScore2).Concat(trafficQuestionsScore1).ToList();
            categorizedQuestions = categorizedQuestionsScore3.Concat(categorizedQuestionsScore2).Concat(categorizedQuestionsScore1).ToList();

            System.Web.HttpContext.Current.Session["trafficQuestions"] = trafficQuestions;

            return View();
        }
    }
}