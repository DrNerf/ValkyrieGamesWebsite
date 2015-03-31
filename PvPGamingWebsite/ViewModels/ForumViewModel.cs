using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PvPGamingWebsite.Models;
using PagedList;
using PagedList.Mvc;

namespace PvPGamingWebsite.ViewModels
{
    public class ForumViewModel
    {
        public Forum Forum { get; set; }

        public IPagedList<Topic> ForumTopics { get; set; }
    }
}