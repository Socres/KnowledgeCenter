namespace KnowledgeCenter.Data.Context
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration;
    using System.Linq;
    using KnowledgeCenter.Data.Core.Models;

    [DbConfigurationType(typeof(KnowledgeCenterContextConfiguration))]
    public class KnowledgeCenterContext : DbContext
    {
        public DbSet<KbFolder> KbFolders { get; set; }

        public DbSet<KbItem> KbItems { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="KnowledgeCenterContext"/> class.
        /// </summary>
        public KnowledgeCenterContext()
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            Database.SetInitializer(new NullDatabaseInitializer<KnowledgeCenterContext>());
        }

        /// <summary>
        /// Initializes the specified test data density.
        /// </summary>
        public void Initialize()
        {
            Database.SetInitializer(new KnowledgeCenterContextDbInitializer());

            Database.Initialize(false);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // We do not want Cascading deletes
            modelBuilder.Conventions
                .Remove<System.Data.Entity.ModelConfiguration.Conventions.OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions
                .Remove<System.Data.Entity.ModelConfiguration.Conventions.ManyToManyCascadeDeleteConvention>();

            AddCustomConfigurations(modelBuilder);
        }

        private void AddCustomConfigurations(DbModelBuilder modelBuilder)
        {
            foreach (var configuration in GetType().Assembly.GetTypes()
                .Where(t => !t.IsAbstract && IsEntityTypeConfiguration(t)).Select(Activator.CreateInstance))
            {
                modelBuilder.Configurations.Add((dynamic)configuration);
            }
        }

        private static bool IsEntityTypeConfiguration(Type type)
        {
            var result = false;
            if (type.IsGenericType &&
                typeof(EntityTypeConfiguration<>).IsAssignableFrom(type.GetGenericTypeDefinition()))
            {
                result = true;
            }
            else
            {
                if (type.BaseType != null)
                {
                    result = IsEntityTypeConfiguration(type.BaseType);
                }
            }
            return result;
        }
    }
}
