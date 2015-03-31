using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PvPGamingWebsite.ViewModels
{
    public class CreateForumInCategoryViewModel
    {
        [Required]
        public int CategoryID { get; set; }

        [Required]
        public string ForumName { get; set; }

        [Required]
        public string ForumDescription { get; set; }
    }
}