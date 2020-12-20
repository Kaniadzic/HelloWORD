using HelloWORD.Models.Entity;
using HelloWORD.Models.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelloWORD.Controllers
{
    public class StudyController : Controller
    {
        [HttpGet]
        public ActionResult Learn(string category)
        {
            LearnLogic learnLogic = new LearnLogic();
            List<LearnCategory> lessons;

            lessons = learnLogic.getLessons(category);

            return View(lessons);
        }


        [HttpGet]
        public ActionResult Lesson(int id)
        {
            LearnLogic learnLogic = new LearnLogic();
            List<Lesson> lessons;

            lessons = learnLogic.getLesson(id);

            return View(lessons);
        }
    }
}