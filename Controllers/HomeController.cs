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
            GMailer.GmailUsername = "licencjathelloword@gmail.com";

            // hasło do konta
            {
                GMailer.GmailPassword = "/a12Uil?09wwE";
            }

            GMailer mailer = new GMailer();
            mailer.ToEmail = "licencjathelloword@gmail.com";
            mailer.Subject = "A message from " + contactEmail.name;
            mailer.Body = "From: " + contactEmail.from + " " + contactEmail.message;
            mailer.IsHtml = true;
            mailer.Send();

            ViewBag.Message = "Pomyślnie wysłano wiadomość! Dziękuję!";
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