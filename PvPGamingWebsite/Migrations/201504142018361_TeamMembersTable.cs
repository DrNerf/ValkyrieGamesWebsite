namespace PvPGamingWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TeamMembersTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TeamMembers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FullName = c.String(nullable: false),
                        Position = c.String(nullable: false),
                        Information = c.String(nullable: false),
                        Facebook = c.String(),
                        Twitter = c.String(),
                        Linkedin = c.String(),
                        Youtube = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TeamMembers");
        }
    }
}
