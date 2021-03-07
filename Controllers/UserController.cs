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
            // Przygotowanie danych użytkownika
            userRegistrationData = userLogic.PrepareUserRegistrationData(userRegistrationData);

            // Sprawdzenie czy jest już w bazie użytkownik o podanym loginie lub adresie email
            int userCount = userLogic.CheckExistingUsers(userRegistrationData.Login, userRegistrationData.Email);
            if(userCount == 0)
            {
                userLogic.CreateUser(userRegistrationData);

                ViewBag.Error = "Dodano użytkownika!";
                return View();
            }
            else
            {
                ViewBag.Error = "Użytkownik o podanym loginie lub adresie email już istnieje w bazie!";
                return View();
            }
        }
    }
}