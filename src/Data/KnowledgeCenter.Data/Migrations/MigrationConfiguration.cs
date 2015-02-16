// ReSharper disable PossibleNullReferenceException
namespace KnowledgeCenter.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Text;
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
            var items = new List<KbItem>();
            var random = new Random();
            for (var i = 0; i < random.Next(3, 10); i++)
            {
                AddItems(random, items, null);
            }
            context.KbItems.AddOrUpdate(f => f.Name,
                items.ToArray());
        }

        private static void AddItems(Random random, ICollection<KbItem> items, KbItem parent)
        {
            var name = 
                parent == null
                ? ("Folder " + (items.Count + 1))
                : parent.Name + "." + (items.Count + 1);
            var sb = new StringBuilder();
            sb.AppendLine("```csharp");
            sb.AppendLine("public class " + name.Replace(" ", string.Empty).Replace(".", "_"));
            sb.AppendLine("{");
            sb.AppendLine(@"    Console.WriteLine(""This is a test!"");");
            sb.AppendLine("}");
            sb.AppendLine("```");

            var item = new KbItem
            {
                DomainId = Guid.NewGuid(),
                Name = name,
                Markdown = "##" + name + Environment.NewLine + Environment.NewLine + sb
            };
            items.Add(item);

            // Nasty....if we're 4 levels deep....
            if (name.Count(x => x == '.') <= 3)
            {
                for (var i = 0; i < random.Next(3, 10); i++)
                {
                    AddItems(random, item.ChildItems, item);
                }
            }
        }
    }
}
