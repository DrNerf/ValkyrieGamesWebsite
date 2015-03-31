using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PvPGamingWebsite.Models;
using PvPGamingWebsite.Contexts;
using WebMatrix.WebData;
using System.Web.Security;
using PvPGamingWebsite.ViewModels;
using System.Data.Entity;

namespace PvPGamingWebsite.Controllers
{
    public class AdministrationController : Controller
    {
        Context DataBase = new Context();
        SimpleRoleProvider RoleProvider = new SimpleRoleProvider();

        private bool IsAdministrator()
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    return User.IsInRole("Administrator");
                }
                catch (Exception e)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        [Authorize]
        public ActionResult Index()
        {
            if (IsAdministrator())
            {
                return View();
            }
            else
            {
                return Redirect("/Home/Index");
            }
        }

        [Authorize]
        public ActionResult NewsPosts()
        {
            if (IsAdministrator())
            {
                return View();
            }
            else
            {
                return Redirect("/Home/Index");
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult NewsPosts(Post post)
        {
            if (IsAdministrator())
            {
                if (ModelState.IsValid)
                {
                    post.Author = User.Identity.Name;
                    post.PostDate = DateTime.Now;

                    DataBase.Posts.Add(post);
                    DataBase.SaveChanges();
                }
                else
                {
                    return Content("Invalid post data!");
                }

                return Redirect("/Home/Index");
            }
            else
            {
                return Redirect("/Home/Index");
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult DeletePost(Post post)
        {
            if (IsAdministrator())
            {
                Post PostToDelete = DataBase.Posts.Where(x => x.Id == post.Id).FirstOrDefault();
                DataBase.Posts.Remove(PostToDelete);
                DataBase.SaveChanges();

                return Redirect("/Home/Index");
            }
            else
            {
                return Redirect("/Home/Index");
            }
        }

        [Authorize]
        public ActionResult RolesPanel()
        {
            if (IsAdministrator())
            {
                return View();
            }
            else
            {
                return Redirect("/Home/Index");
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddRoleToUser(RolesPanelModel model)
        {
            if (IsAdministrator())
            {
                if (ModelState.IsValid)
                {
                    Roles.AddUserToRole(model.Name, model.RoleName);
                }
                else
                {
                    return Content("Role data given is invalid!");
                }
                return Redirect("/Administration/RolesPanel");
            }
            else
            {
                return Redirect("/Home/Index");
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult RemoveRoleToUser(RolesPanelModel model)
        {
            if (IsAdministrator())
            {
                if (ModelState.IsValid)
                {
                    Roles.RemoveUserFromRole(model.Name, model.RoleName);
                }
                else
                {
                    return Content("Role data given is invalid!");
                }
                return Redirect("/Administration/RolesPanel");
            }
            else
            {
                return Redirect("/Home/Index");
            }
        }

        [Authorize]
        public ActionResult TicketsPanel(bool? showSeen, string date)
        {
            if (IsAdministrator())
            {
                List<Ticket> tickets = DataBase.Tickets.ToList();
                if (!showSeen.HasValue || !showSeen.Value)
                {
                    tickets = tickets.Where(x => x.Seen == false).ToList();
                }
                if (!String.IsNullOrEmpty(date))
                {
                    tickets = tickets.Where(x => x.DateSend.Date.Equals(DateTime.Parse(date))).ToList();
                }
                return View(tickets);
            }
            else
            {
                return Redirect("/Home/Index");
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult TicketMarkAsSeen(int id)
        {
            if (IsAdministrator())
            {
                Ticket ticket = DataBase.Tickets.Where(x => x.Id == id).FirstOrDefault();
                ticket.Seen = true;
                DataBase.SaveChanges();
                return Redirect("/Administration/TicketsPanel");
            }
            else
            {
                return Redirect("/Home/Index");
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult TicketDelete(int id)
        {
            if (IsAdministrator())
            {
                Ticket ticket = DataBase.Tickets.Where(x => x.Id == id).FirstOrDefault();
                DataBase.Tickets.Remove(ticket);
                DataBase.SaveChanges();
                return Redirect("/Administration/TicketsPanel");
            }
            else
            {
                return Redirect("/Home/Index");
            }
        }

        public ActionResult ForumPanel()
        {
            List<Category> categories = DataBase.ForumCategories.ToList();
            return View(categories);
        }

        [Authorize]
        [HttpPost]
        public ActionResult CreateForumCategory(Category model)
        {
            if (IsAdministrator())
            {
                if (ModelState.IsValid)
                {
                    model.CategoryForums = new List<Forum>();
                    DataBase.ForumCategories.Add(model);
                    DataBase.SaveChanges();
                    return Redirect("/Administration/ForumPanel");
                }
                else
                {
                    return Content("Model is not valid!");
                }
            }
            else
            {
                return Redirect("/Home/Index");
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult CreateForum(CreateForumInCategoryViewModel model)
        {
            if (IsAdministrator())
            {
                if (ModelState.IsValid)
                {
                    Forum forum = new Forum { Name = model.ForumName, Description = model.ForumDescription, ForumTopics = new List<Topic>() };
                    Category category = DataBase.ForumCategories.Where(x => x.Id == model.CategoryID).FirstOrDefault();
                    DataBase.Forums.Add(forum);
                    DataBase.SaveChanges();
                    if (category.CategoryForums != null)
                    {
                        category.CategoryForums.Add(forum);
                    }
                    else
                    {
                        category.CategoryForums = new List<Forum> { forum };
                    }
                    DataBase.SaveChanges();
                    return Redirect("/Administration/ForumPanel");
                }
                else
                {
                    return Content("Model is not valid!");
                }
            }
            else
            {
                return Redirect("/Home/Index");
            }
        }

        [Authorize]
        public ActionResult ProjectsStatus()
        {
            if (IsAdministrator())
            {
                return View(DataBase.ProjectsStatus.ToList());
            }
            else
            {
                return Redirect("/Home/Index");
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult CreateProjectStatus(ProjectStatus model)
        {
            if (IsAdministrator())
            {
                DataBase.ProjectsStatus.Add(model);
                DataBase.SaveChanges();
                return Redirect("/Administration/ProjectsStatus");
            }
            else
            {
                return Redirect("/Home/Index");
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult DeleteProjectStatus(int id)
        {
            if (IsAdministrator())
            {
                ProjectStatus status = DataBase.ProjectsStatus.FirstOrDefault(x => x.Id == id);
                DataBase.ProjectsStatus.Remove(status);
                DataBase.SaveChanges();
                return Redirect("/Administration/ProjectsStatus");
            }
            else
            {
                return Redirect("/Home/Index");
            }
        }

        [Authorize]
        public ActionResult ProjectsPanel()
        {
            if (IsAdministrator())
            {
                return View(new ProjectsPanelViewModel 
                { 
                    AllProjectsStatuses = DataBase.ProjectsStatus.ToList(),
                    Projects = DataBase.Projects.Include(x => x.Status).ToList()
                });
            }
            else
            {
                return Redirect("/Home/Index");
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult CreateProject(Project model, int StatusID)
        {
            if (IsAdministrator())
            {
                ProjectStatus status = DataBase.ProjectsStatus.FirstOrDefault(x => x.Id == StatusID);
                model.DateCreated = DateTime.Now;
                model.Status = status;
                DataBase.Projects.Add(model);
                DataBase.SaveChanges();
                return Redirect("/Administration/ProjectsPanel");
            }
            else
            {
                return Redirect("/Home/Index");
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult DeleteProject(int ProjectID)
        {
            if (IsAdministrator())
            {
                Project project = DataBase.Projects.FirstOrDefault(x => x.Id == ProjectID);
                DataBase.Projects.Remove(project);
                DataBase.SaveChanges();
                return Json(true);
            }
            else
            {
                return Redirect("/Home/Index");
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditProject(Project model, int projectStatusID)
        {
            if (IsAdministrator())
            {
                Project project = DataBase.Projects.FirstOrDefault(x => x.Id == model.Id);
                project.Title = model.Title;
                project.Description = model.Description;
                project.ThumbnailURL = model.ThumbnailURL;
                project.HeaderURL = model.HeaderURL;
                project.Status = DataBase.ProjectsStatus.FirstOrDefault(x => x.Id == projectStatusID);
                DataBase.SaveChanges();
                return RedirectToAction("ProjectsPanel");
            }
            else
            {
                return Redirect("/Home/Index");
            }
        }
    }
}
