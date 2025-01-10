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
            ViewBag.Message = "Ootan sind minu peole! Palun tule!";
            int hour = DateTime.Now.Hour;
            ViewBag.Greeting = hour < 10 ? "Tere hommikust!" : "Tere päevast!";
            return View();
        }
        [HttpGet]
        public ViewResult Ankeet() { return View(); }

        public ViewResult Ankeet(Guest guest)
        {
            E_mail(guest);
            if (ModelState.IsValid)
            {
                return View("Thanks ", guest);
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
                WebMail.Password = "sssss";
                WebMail.From = "anton9032@gmail.com";
                WebMail.Send(guest.Email, "Meeldetuletus", guest.Name + ", ära unusta. Pidu toimub 12.03.20! Sind ootavad väga!",
                    null, "marina.oleinik@tthk.ee",
                    filesToAttach: new String[] {
                        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Images\"),
                        Path.GetFileName("kutse.png") }
                   );
                ViewBag.Message = "Kiri on saadetud!";
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
            }
        }
    }
}