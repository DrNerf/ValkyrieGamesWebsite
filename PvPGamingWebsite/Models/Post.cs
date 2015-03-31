using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PvPGamingWebsite.Models
{
    public class Post
    {
        public int Id { get; set; }

        public string Title { get; set; }

        [AllowHtml]
        public string Body { get; set; }

        public DateTime PostDate { get; set; }
        
        public string Author { get; set; }
    }
}