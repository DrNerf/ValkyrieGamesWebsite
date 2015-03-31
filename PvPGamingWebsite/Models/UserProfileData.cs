using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PvPGamingWebsite.Models
{
    public class UserProfileData
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string PictureURL { get; set; }

        public string Color { get; set; }

        public bool IsMuted { get; set; }

        public bool IsBanned { get; set; }
    }
}