namespace KnowledgeCenter.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.KbItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        Markdown = c.String(nullable: false),
                        ParentItemId = c.Int(),
                        DomainId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.KbItems", t => t.ParentItemId)
                .Index(t => t.ParentItemId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.KbItems", "ParentItemId", "dbo.KbItems");
            DropIndex("dbo.KbItems", new[] { "ParentItemId" });
            DropTable("dbo.KbItems");
        }
    }
}
