using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PvPGamingWebsite.Models
{
    public class Project
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime DateCreated { get; set; }

        [DisplayName("Thumbnail URL")]
        public string ThumbnailURL { get; set; }

        [DisplayName("Header URL")]
        public string HeaderURL { get; set; }

        public ProjectStatus Status { get; set; }
    }
}