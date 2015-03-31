using PvPGamingWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PvPGamingWebsite.ViewModels
{
    public class CreateTopicInForumViewModel
    {
        public Topic TopicData { get; set; }

        public int ForumID { get; set; }
    }
}