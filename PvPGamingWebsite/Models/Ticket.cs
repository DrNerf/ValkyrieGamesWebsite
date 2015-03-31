using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace PvPGamingWebsite.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        [Required]
        public string Sender { get; set; }

        public string Title { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public DateTime DateSend { get; set; }

        [DefaultValue(false)]
        public bool Seen { get; set; }
    }
}