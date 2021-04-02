using CaptchaMvc.HtmlHelpers;
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
        public ActionResult Logon(string alert = "")
        {
            ViewBag.Alert = alert;

            return View();
        }

        [HttpPost]
        public ActionResult Logon(UserLoginData userLoginData)
        {
            UserLogic userLogic = new UserLogic();
            int userID = userLogic.LoginUser(userLoginData);
            userLogic.SaveUserIdSession(userID);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Logout()
        {
            UserLogic userLogic = new UserLogic();
            userLogic.Logout();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Panel()
        {
            // jeżeli ID użytkownika nie jest zapisane w sesji to przekierowujemy do menu głównego
            if (System.Web.HttpContext.Current.Session["userID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.ID = System.Web.HttpContext.Current.Session["userID"];

            return View();
        }

        [HttpGet]
        public ActionResult History(int id)
        {
            // jeżeli ID użytkownika nie jest zapisane w sesji to przekierowujemy do menu głównego
            if (System.Web.HttpContext.Current.Session["userID"] == null || id <= 0 || id == null || id != (int)System.Web.HttpContext.Current.Session["userID"])
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            // jeżeli ID użytkownika nie jest zapisane w sesji to przekierowujemy do menu głównego
            if (System.Web.HttpContext.Current.Session["userID"] == null || id <= 0 || id == null || id != (int)System.Web.HttpContext.Current.Session["userID"])
            {
                return RedirectToAction("Index", "Home");
            }

            UserLogic userLogic = new UserLogic();
            UserUpdateData userData = userLogic.SelectUserData(id);

            return View(userData);
        }

        [HttpPost]
        public ActionResult Edit(UserUpdateData updateData)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Alert = "Proszę poprawnie uzupełnić formularz!";
                return View();
            }
            else if (updateData.email != updateData.repeatEmail)
            {
                ModelState.AddModelError("DifferentEmails", "Podane adresy email nie są takie same!");
                ViewBag.Alert = "Podane adresy email nie są takie same!";
                return View();
            }

            UserLogic userLogic = new UserLogic();
            userLogic.UpdateUser(updateData);

            return RedirectToAction("Panel", "User");
        }

        [HttpGet]
        public ActionResult Buy()
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
            // captcha CaptchaMvc.Mvc5
            if (!this.IsCaptchaValid(""))
            {
                ModelState.AddModelError("InvalidCaptcha", "Proszę poprawnie uzupełnić Captcha!");
                ViewBag.Alert = "Proszę poprawnie uzupełnić Captcha!";
                return View();
            }

            if (userRegistrationData.Password != userRegistrationData.RepeatPassword)
            {
                ModelState.AddModelError("DifferentPasswords", "Wprowadzone hasła nie są takie same!");
                ViewBag.Alert = "Wprowadzone hasła nie są takie same!";
                return View();
            }
            else if (userRegistrationData.Email != userRegistrationData.RepeatEmail)
            {
                ModelState.AddModelError("DifferentEmails", "Podane adresy email nie są takie same!");
                ViewBag.Alert = "Podane adresy email nie są takie same!";
                return View();
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Alert = "Proszę poprawnie uzupełnić formularz!";
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

                ViewBag.Alert = "Możesz zalogować się na swoje konto!";
                return RedirectToAction("Logon", "User", new { alert = ViewBag.Alert});
            }
            else
            {
                ViewBag.Alert = "Użytkownik o podanym loginie lub adresie email już istnieje w bazie!";
                return View();
            }
        }
    }
}