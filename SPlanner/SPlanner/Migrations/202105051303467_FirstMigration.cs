namespace SPlanner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Budget",
                c => new
                    {
                        BudgetID = c.Int(nullable: false, identity: true),
                        NameExp = c.String(),
                        DataOfBudget = c.DateTime(nullable: false),
                        PlanedExp = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ActualExp = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BudgetID)
                .ForeignKey("dbo.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        EmailAddress = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 50),
                        College = c.String(nullable: false, maxLength: 100),
                        RolaID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.Rola", t => t.RolaID, cascadeDelete: true)
                .Index(t => t.RolaID);
            
            CreateTable(
                "dbo.Event",
                c => new
                    {
                        EventID = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Thema = c.String(nullable: false, maxLength: 100),
                        Description = c.String(nullable: false, maxLength: 250),
                        CategoryID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EventID)
                .ForeignKey("dbo.Category", t => t.CategoryID, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.CategoryID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            CreateTable(
                "dbo.Grade",
                c => new
                    {
                        GradeID = c.Int(nullable: false, identity: true),
                        Gradee = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UserID = c.Int(nullable: false),
                        SubjectID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GradeID)
                .ForeignKey("dbo.Subject", t => t.SubjectID, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.SubjectID);
            
            CreateTable(
                "dbo.Subject",
                c => new
                    {
                        SubjectID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.SubjectID);
            
            CreateTable(
                "dbo.Rola",
                c => new
                    {
                        RolaID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.RolaID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User", "RolaID", "dbo.Rola");
            DropForeignKey("dbo.Grade", "UserID", "dbo.User");
            DropForeignKey("dbo.Grade", "SubjectID", "dbo.Subject");
            DropForeignKey("dbo.Event", "UserID", "dbo.User");
            DropForeignKey("dbo.Event", "CategoryID", "dbo.Category");
            DropForeignKey("dbo.Budget", "UserID", "dbo.User");
            DropIndex("dbo.Grade", new[] { "SubjectID" });
            DropIndex("dbo.Grade", new[] { "UserID" });
            DropIndex("dbo.Event", new[] { "UserID" });
            DropIndex("dbo.Event", new[] { "CategoryID" });
            DropIndex("dbo.User", new[] { "RolaID" });
            DropIndex("dbo.Budget", new[] { "UserID" });
            DropTable("dbo.Rola");
            DropTable("dbo.Subject");
            DropTable("dbo.Grade");
            DropTable("dbo.Category");
            DropTable("dbo.Event");
            DropTable("dbo.User");
            DropTable("dbo.Budget");
        }
    }
}
