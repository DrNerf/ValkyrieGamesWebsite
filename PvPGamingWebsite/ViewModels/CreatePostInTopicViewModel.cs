using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PvPGamingWebsite.Models;

namespace PvPGamingWebsite.ViewModels
{
    public class CreatePostInTopicViewModel
    {
        public ForumPost PostData { get; set; }

        public int TopicID { get; set; }
    }
}