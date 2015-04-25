using PvPGamingWebsite.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PvPGamingWebsite.Contexts
{
    public class Context : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Category> ForumCategories { get; set; }
        public DbSet<Forum> Forums{ get; set; }
        public DbSet<Topic> ForumTopics { get; set; }
        public DbSet<ForumPost> ForumPosts { get; set; }
        public DbSet<ProjectStatus> ProjectsStatus { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<UserProfileData> UsersProfiles { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
    }
}