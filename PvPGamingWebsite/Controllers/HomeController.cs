using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PvPGamingWebsite.Contexts;
using WebMatrix.WebData;
using PvPGamingWebsite.Filters;
using PvPGamingWebsite.Models;

namespace PvPGamingWebsite.Controllers
{
    [InitializeSimpleMembership]
    public class HomeController : Controller
    {
        Context DataBase = new Context();

        private bool IsAdministrator()
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    return User.IsInRole("Administrator");
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public ActionResult Index()
        {
            ViewBag.IsAdmin = IsAdministrator();
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View(DataBase.Posts.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize]
        public ActionResult Ticket()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Ticket(Ticket model)
        {
            try
            {
                if (String.IsNullOrEmpty(model.Message) || String.IsNullOrEmpty(model.FullName) || String.IsNullOrEmpty(model.Email))
                {
                    throw new InvalidOperationException();
                }
                model.DateSend = DateTime.Now;
                model.Seen = false;
                model.Sender = User.Identity.Name;
                model.Message = Server.HtmlEncode(model.Message);
                DataBase.Tickets.Add(model);
                DataBase.SaveChanges();
                return Json(new { IsSuccess = true, Message = "Ticket submitted!" });
            }
            catch
            {
                return Json(new { IsSuccess = false, Message = "Error occured, double check your information!" });
            }
        }

        [Authorize]
        public ActionResult Profile()
        {
            UsersContext db = new UsersContext();
            string username = User.Identity.Name;
            int userID = db.UserProfiles.FirstOrDefault(x => x.UserName == username).UserId;
            UserProfileData profileData = DataBase.UsersProfiles.FirstOrDefault(x => x.UserId == userID);
            return View(profileData);
        }

        public ActionResult SaveProfile(UserProfileData model)
        {
            UsersContext db = new UsersContext();
            string username = User.Identity.Name;
            UserProfile user = db.UserProfiles.FirstOrDefault(x => x.UserName == username);
            if (model.UserId == user.UserId && user.UserName == username)
            {
                UserProfileData profile = DataBase.UsersProfiles.FirstOrDefault(x => x.Id == model.Id);
                profile.Email = model.Email;
                profile.Color = model.Color;
                profile.PictureURL = model.PictureURL;
                DataBase.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return Content("<div class='alert alert-danger'>This profile is not yours!</div>");
            }
        }
        
        [HttpGet]
        public ActionResult ViewProfile(string name)
        {
            UsersContext db = new UsersContext();
            int userId = db.UserProfiles.FirstOrDefault(x => x.UserName == name).UserId;
            return View(DataBase.UsersProfiles.FirstOrDefault(x => x.UserId == userId));
        }
    }
}
