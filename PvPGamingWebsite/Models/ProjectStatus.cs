using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PvPGamingWebsite.Models
{
    public class ProjectStatus
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
    }
}