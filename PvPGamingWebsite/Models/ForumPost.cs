using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PvPGamingWebsite.Models
{
    public class ForumPost
    {
        public int Id { get; set; }

        [Required, AllowHtml]
        public string Body { get; set; }

        public string Poster { get; set; }

        public string Rank { get; set; }

        public DateTime PostDateTime { get; set; }
    }
}