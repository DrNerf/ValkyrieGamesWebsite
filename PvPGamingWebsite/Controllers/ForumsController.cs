using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PvPGamingWebsite.Contexts;
using System.Data.Entity;
using PvPGamingWebsite.Models;
using PvPGamingWebsite.ViewModels;
using PagedList;
using PagedList.Mvc;

namespace PvPGamingWebsite.Controllers
{
    [Authorize]
    public class ForumsController : Controller
    {
        Context DataBase = new Context();

        private string GetRank()
        {
            if (User.IsInRole("Administrator"))
            {
                return "Administrator";
            }
            if (User.IsInRole("Moderator"))
            {
                return "Moderator";
            }
            if (User.IsInRole("SuperUser"))
            {
                return "SuperUser";
            }
            return "User";
        }
        //
        // GET: /Forums/

        public ActionResult Index()
        {
            var categories = DataBase.ForumCategories.Include(x => x.CategoryForums).ToList();
            foreach (Category category in categories)
            {
                foreach (Forum forum in category.CategoryForums)
                {
                    forum.ForumTopics = DataBase.Forums.Include(x => x.ForumTopics).FirstOrDefault(x => x.Id == forum.Id).ForumTopics;
                    foreach (Topic topic in forum.ForumTopics)
                    {
                        topic.TopicPosts = DataBase.ForumTopics.Include(x => x.TopicPosts).FirstOrDefault(x => x.Id == topic.Id).TopicPosts;
                    }
                }
            }
            return View(categories);
        }

        public ActionResult Forum(int id, int? page, string date)
        {
            Forum forum = DataBase.Forums.Include(x => x.ForumTopics).FirstOrDefault(x => x.Id == id);
            forum.ForumTopics = forum.ForumTopics.OrderByDescending(x => x.DateTimeCreated).ToList();
            forum.ForumTopics = forum.ForumTopics.OrderBy(x => x.IsSticky ? 0 : 1).ToList();
            if (!String.IsNullOrEmpty(date))
            {
                //string[] dateData = date.Split('/');
                DateTime dateFormated = DateTime.Parse(date);
                forum.ForumTopics = forum.ForumTopics.Where(x => x.DateTimeCreated.Date.Equals(dateFormated.Date)).ToList();
            }
            foreach (var item in forum.ForumTopics)
            {
                item.TopicPosts = DataBase.ForumTopics.Include(x => x.TopicPosts).FirstOrDefault(x => x.Id == item.Id).TopicPosts;
            }
            return View(new ForumViewModel { Forum = forum, ForumTopics = forum.ForumTopics.ToPagedList(page ?? 1, 2) });
        }

        public ActionResult CreateTopic(CreateTopicInForumViewModel model)
        {
            return PartialView("_CreateTopic", model);
        }

        [HttpPost]
        public ActionResult SaveNewTopic(CreateTopicInForumViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.TopicData.DateTimeCreated = DateTime.Now;
                model.TopicData.Creator = User.Identity.Name;
                model.TopicData.Rank = GetRank();
                model.TopicData.TopicPosts = new List<ForumPost>();
                DataBase.ForumTopics.Add(model.TopicData);
                DataBase.SaveChanges();
                DataBase.Forums.FirstOrDefault(x => x.Id == model.ForumID).ForumTopics = new List<Topic> { model.TopicData };
                DataBase.SaveChanges();
                return Redirect("/Forums/Forum/" + model.ForumID);
            }
            else
            {
                ModelState.AddModelError("Invalid topic data", new InvalidOperationException());
                return Content("Invalid topic data! <br /> Let's hope this message will improve :D");
            }
        }

        public ActionResult Topic(int id)
        {
            Topic topic = DataBase.ForumTopics.Include(x => x.TopicPosts).FirstOrDefault(x => x.Id == id);
            return View(topic);
        }

        public ActionResult CreateNewPost(CreatePostInTopicViewModel viewModel)
        {
            return View("_CreateNewPost", viewModel);
        }

        [HttpPost]
        public ActionResult CreatePost(CreatePostInTopicViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Topic topic = DataBase.ForumTopics.FirstOrDefault(x => x.Id == viewModel.TopicID);
                viewModel.PostData.PostDateTime = DateTime.Now;
                viewModel.PostData.Poster = User.Identity.Name;
                viewModel.PostData.Rank = GetRank();
                DataBase.ForumPosts.Add(viewModel.PostData);
                DataBase.SaveChanges();
                topic.TopicPosts = new List<ForumPost> { viewModel.PostData };
                DataBase.SaveChanges();
                return Redirect("/Forums/Topic/" + viewModel.TopicID);
            }
            else
            {
                return Content("Invalid post data!");
            }
        }

        [Authorize(Roles="Moderator"), HttpPost]
        public ActionResult DeletePost(int PostID, int TopicID)
        {
            ForumPost post = DataBase.ForumPosts.FirstOrDefault(x => x.Id == PostID);
            DataBase.ForumPosts.Remove(post);
            DataBase.SaveChanges();
            return Redirect("/Forums/Topic/" + TopicID);
        }

        [Authorize(Roles = "Moderator"), HttpPost]
        public ActionResult DeleteTopic(int TopicID, int ForumID)
        {
            try
            {
                Topic topic = DataBase.ForumTopics.Include(x => x.TopicPosts).FirstOrDefault(x => x.Id == TopicID);
                var posts = topic.TopicPosts.ToList();
                DataBase.ForumTopics.Remove(topic);
                foreach (ForumPost post in posts)
                {
                    DataBase.ForumPosts.Remove(post);
                }
                DataBase.SaveChanges();
                return Json(true);
            }
            catch
            {
                return Json(false);
            }
        }

        [Authorize(Roles = "Moderator")]
        public ActionResult DeleteForum(int ForumID)
        {
            try
            {
                Forum forum = DataBase.Forums.Include(x => x.ForumTopics).FirstOrDefault(x => x.Id == ForumID);
                var topics = forum.ForumTopics.ToList();
                DataBase.Forums.Remove(forum);
                foreach (Topic topic in topics)
                {
                    DataBase.ForumTopics.Remove(topic);
                }
                DataBase.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize(Roles = "Moderator")]
        public ActionResult DeleteCategory(int CategoryID)
        {
            try
            {
                Category category = DataBase.ForumCategories.Include(x => x.CategoryForums).FirstOrDefault(x => x.Id == CategoryID);
                var forums = category.CategoryForums.ToList();
                DataBase.ForumCategories.Remove(category);
                foreach (Forum forum in forums)
                {
                    DataBase.Forums.Remove(forum);
                }
                DataBase.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize(Roles = "Moderator")]
        public ActionResult SetSticky(int topicID, int forumID, bool isSticky)
        {
            DataBase.ForumTopics.FirstOrDefault(x => x.Id == topicID).IsSticky = isSticky;
            DataBase.SaveChanges();
            return Redirect("/Forums/Forum/" + forumID);
        }

        [Authorize(Roles = "Moderator")]
        public ActionResult SetLocked(int topicID, int forumID, bool isLocked)
        {
            DataBase.ForumTopics.FirstOrDefault(x => x.Id == topicID).IsLocked = isLocked;
            DataBase.SaveChanges();
            return Redirect("/Forums/Forum/" + forumID);
        }

        [HttpPost]
        public ActionResult EditTopicData(Topic model)
        {
            Topic topic = DataBase.ForumTopics.FirstOrDefault(x => x.Id == model.Id);
            topic.Description = model.Description;
            DataBase.SaveChanges();
            return Redirect("/Forums/Topic/" + model.Id);
        }

        [HttpPost]
        public ActionResult EditTopicPostData(ForumPost model, int TopicID)
        {
            ForumPost post = DataBase.ForumPosts.FirstOrDefault(x => x.Id == model.Id);
            post.Body = model.Body;
            DataBase.SaveChanges();
            return Redirect("/Forums/Topic/" + TopicID);
        }
    }
}
