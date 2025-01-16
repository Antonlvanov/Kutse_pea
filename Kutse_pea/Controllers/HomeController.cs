using Kutse_pea.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Kutse_pea.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            int hour = DateTime.Now.Hour;
            if (hour >= 6 && hour <= 10)
            {
                ViewBag.Greeting = "Tere hommikust!";
            }
            else if (hour >= 11 && hour <= 17)
            {
                ViewBag.Greeting = "Tere päevast!";
            }
            else if (hour >= 18 && hour <= 21)
            {
                ViewBag.Greeting = "Tere õhtust!";
            }
            else
            {
                ViewBag.Greeting = "Head ööd!";
            }

            int month = DateTime.Now.Month;
            if (month == 12)
            {
                ViewBag.Message = "Häid jõule ja head uut aastat!";
                ViewBag.ImagePath = "~/Images/srozhdestvom.jpg";
            }
            else if (month == 1)
            {
                ViewBag.Message = "Happy New Year!";
                ViewBag.ImagePath = "~/Images/new_year.jpg";
            }
            else if (month == 2)
            {
                ViewBag.Message = "Head sõbrapäeva";
                ViewBag.ImagePath = "~/Images/soberpaev.jpg";
            }
            else if (month == 4)
            {
                ViewBag.Message = "Lihavõttepühade 1. päev";
                ViewBag.ImagePath = "~/Images/haid_puhi.jpg";
            }
            else if (month == 5)
            {
                ViewBag.Message = "Kevadpäev";
                ViewBag.ImagePath = "~/Images/kevadpaev.jpg";
            }
            else if (month == 6)
            {
                ViewBag.Message = "Võidupüha";
                ViewBag.ImagePath = "~/Images/voidupuha.jpg";
            }
            else
            {
                ViewBag.Message = "Ootan sind minu peole! Palun tule!!!";
                ViewBag.ImagePath = "~/Images/wave-waving.gif";
            }

            return View();
        }

        [HttpGet]
        public ViewResult Ankeet() { return View(); }

        [HttpPost]
        public ViewResult Ankeet(Guest guest)
        {
            E_mail(guest);
            if (ModelState.IsValid)
            {
                return View("Thanks", guest);
            }
            return View();
        }

        public void E_mail(Guest guest)
        {
            try
            {
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.SmtpPort = 587;
                WebMail.EnableSsl = true;
                WebMail.UserName = "anton9032@gmail.com";
                WebMail.Password = "smd";
                WebMail.From = "anton9032@gmail.com";
                WebMail.Send(guest.Email, "Meeldetuletus", guest.Name + ", ära unusta. Pidu toimub 12.03.20! Sind ootavad väga!",
                    null, "anton9032@gmail.com",
                    filesToAttach: new string[] {
                        Path.Combine(Server.MapPath("~/Images"), "wave-waving.gif")
                    }
                    );
                ViewBag.Message = "Kiri on saadetud!";
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Mul on kahju!Ei saa kirja saada!!! " + ex.Message;
            }
        }
    }
}