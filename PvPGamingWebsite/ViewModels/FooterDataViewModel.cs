using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PvPGamingWebsite.Models;

namespace PvPGamingWebsite.ViewModels
{
    public class FooterDataViewModel
    {
        public List<Topic> LatestTopics { get; set; }

        public List<ForumPost> LatestPosts { get; set; }

        public List<Post> LatestNews { get; set; }

        public List<Project> LatestProjects { get; set; }
    }
}