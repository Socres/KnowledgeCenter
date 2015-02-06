namespace KnowledgeCenter.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using KnowledgeCenter.Data.Context;
    using KnowledgeCenter.Data.Core.Models;

    internal sealed class MigrationConfiguration : DbMigrationsConfiguration<KnowledgeCenterContext>
    {
        public MigrationConfiguration()
        {
            AutomaticMigrationDataLossAllowed = false;
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(KnowledgeCenterContext context)
        {
            var folders = new List<KbFolder>
            {
                new KbFolder
                {
                    DomainId = Guid.NewGuid(), 
                    Name = "Folder 1"
                },
                new KbFolder
                {
                    DomainId = Guid.NewGuid(), 
                    Name = "Folder 2"
                }
            };
            folders[0].ChildFolders.Add(
                new KbFolder
                {
                    DomainId = Guid.NewGuid(),
                    Name = "Folder 1.1"
                });
            (folders[0].ChildFolders as List<KbFolder>)[0].ChildFolders.Add(
                new KbFolder
                {
                    DomainId = Guid.NewGuid(),
                    Name = "Folder 1.1.1"
                });

            folders[1].ChildFolders.Add(
                new KbFolder
                {
                    DomainId = Guid.NewGuid(),
                    Name = "Folder 2.1"
                });
            (folders[1].ChildFolders as List<KbFolder>)[0].KbItems.Add(
                new KbItem
                {
                    DomainId = Guid.NewGuid(),
                    Name = "Item 2.1 => 1",
                    Markdown = "Markdown item test"
                });

            context.KbFolders.AddOrUpdate(f => f.Name,
                folders.ToArray());
        }
    }
}
