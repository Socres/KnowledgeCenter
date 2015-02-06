namespace KnowledgeCenter.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.KbFolders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        ParentFolderId = c.Int(),
                        DomainId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.KbFolders", t => t.ParentFolderId)
                .Index(t => t.ParentFolderId);
            
            CreateTable(
                "dbo.KbItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        Markdown = c.String(nullable: false),
                        KbFolderId = c.Int(nullable: false),
                        DomainId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.KbFolders", t => t.KbFolderId)
                .Index(t => t.KbFolderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.KbItems", "KbFolderId", "dbo.KbFolders");
            DropForeignKey("dbo.KbFolders", "ParentFolderId", "dbo.KbFolders");
            DropIndex("dbo.KbItems", new[] { "KbFolderId" });
            DropIndex("dbo.KbFolders", new[] { "ParentFolderId" });
            DropTable("dbo.KbItems");
            DropTable("dbo.KbFolders");
        }
    }
}
