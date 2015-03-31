using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PvPGamingWebsite.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PvPGamingWebsite.ViewModels
{
    public class ProjectsPanelViewModel
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

        [DisplayName("Project status")]
        public List<ProjectStatus> AllProjectsStatuses { get; set; }

        public List<Project> Projects { get; set; }
    }
}