namespace PvPGamingWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Body = c.String(),
                        PostDate = c.DateTime(nullable: false),
                        Author = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(),
                        Sender = c.String(nullable: false),
                        Title = c.String(),
                        Message = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        DateSend = c.DateTime(nullable: false),
                        Seen = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Fora",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Category_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Category_Id)
                .Index(t => t.Category_Id);
            
            CreateTable(
                "dbo.Topics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Creator = c.String(),
                        DateTimeCreated = c.DateTime(nullable: false),
                        Rank = c.String(),
                        IsSticky = c.Boolean(nullable: false),
                        IsLocked = c.Boolean(nullable: false),
                        Forum_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Fora", t => t.Forum_Id)
                .Index(t => t.Forum_Id);
            
            CreateTable(
                "dbo.ForumPosts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Body = c.String(nullable: false),
                        Poster = c.String(),
                        Rank = c.String(),
                        PostDateTime = c.DateTime(nullable: false),
                        Topic_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Topics", t => t.Topic_Id)
                .Index(t => t.Topic_Id);
            
            CreateTable(
                "dbo.ProjectStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        ThumbnailURL = c.String(),
                        HeaderURL = c.String(),
                        Status_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProjectStatus", t => t.Status_Id)
                .Index(t => t.Status_Id);
            
            CreateTable(
                "dbo.UserProfileDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Email = c.String(),
                        PictureURL = c.String(),
                        Color = c.String(),
                        IsMuted = c.Boolean(nullable: false),
                        IsBanned = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Projects", new[] { "Status_Id" });
            DropIndex("dbo.ForumPosts", new[] { "Topic_Id" });
            DropIndex("dbo.Topics", new[] { "Forum_Id" });
            DropIndex("dbo.Fora", new[] { "Category_Id" });
            DropForeignKey("dbo.Projects", "Status_Id", "dbo.ProjectStatus");
            DropForeignKey("dbo.ForumPosts", "Topic_Id", "dbo.Topics");
            DropForeignKey("dbo.Topics", "Forum_Id", "dbo.Fora");
            DropForeignKey("dbo.Fora", "Category_Id", "dbo.Categories");
            DropTable("dbo.UserProfileDatas");
            DropTable("dbo.Projects");
            DropTable("dbo.ProjectStatus");
            DropTable("dbo.ForumPosts");
            DropTable("dbo.Topics");
            DropTable("dbo.Fora");
            DropTable("dbo.Categories");
            DropTable("dbo.Tickets");
            DropTable("dbo.Posts");
        }
    }
}
