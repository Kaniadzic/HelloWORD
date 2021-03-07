using HelloWORD.Models.Entity;
using HelloWORD.Models.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelloWORD.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public ActionResult Logon()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Logon(string login, string password)
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserRegistrationData userRegistrationData)
        {
            if (userRegistrationData.Password != userRegistrationData.RepeatPassword)
            {
                ViewBag.Error = "Wprowadzone hasła nie są takie same!";
                return View();
            }
            else if (userRegistrationData.Email != userRegistrationData.RepeatEmail)
            {
                ViewBag.Error = "Podane adresy email nie są takie same!";
                return View();
            }

            UserLogic userLogic = new UserLogic();
            userRegistrationData = userLogic.PrepareUserRegistrationData(userRegistrationData);

            ViewBag.Error = "Jest ok";
            return View();
        }
    }
}