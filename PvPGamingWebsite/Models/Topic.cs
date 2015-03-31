using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PvPGamingWebsite.Models
{
    public class Topic
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required, AllowHtml]
        public string Description { get; set; }

        public string Creator { get; set; }

        public DateTime DateTimeCreated { get; set; }

        public string Rank { get; set; }

        public bool IsSticky { get; set; }

        public bool IsLocked { get; set; }

        public List<ForumPost> TopicPosts { get; set; }
    }
}