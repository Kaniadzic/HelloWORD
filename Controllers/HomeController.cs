using HelloWORD.Models.Entity;
using HelloWORD.Models.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace HelloWORD.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            InformationLogic infoLogic = new InformationLogic();
            List<Information> informations = infoLogic.getInformations();

            return View(informations);
        }

        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(ContactEmail contactEmail)
        {
            // @TODO wysłanie emaila lub zapis wiadomości do bazy

            return View();
        }

        public ActionResult LearnInfo()
        {
            return View();
        }

        public ActionResult QuizInfo()
        {
            return View();
        }

        public ActionResult ToS()
        {
            return View();
        }
    }
}