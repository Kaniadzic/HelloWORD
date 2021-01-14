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
            return View();
        }
    }
}