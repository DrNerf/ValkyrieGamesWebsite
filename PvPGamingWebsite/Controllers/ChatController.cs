using PvPGamingWebsite.Contexts;
using PvPGamingWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PvPGamingWebsite.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        Context DataBase = new Context();

        public ActionResult Index()
        {
            UsersContext db = new UsersContext();
            string username = User.Identity.Name;
            int userID = db.UserProfiles.FirstOrDefault(x => x.UserName == username).UserId;
            return View(DataBase.UsersProfiles.FirstOrDefault(x => x.UserId == userID));
        }
    }
}
