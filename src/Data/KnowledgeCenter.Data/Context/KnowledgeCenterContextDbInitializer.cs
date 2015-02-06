namespace KnowledgeCenter.Data.Context
{
    using System.Data.Entity;
    using KnowledgeCenter.Data.Migrations;

    /// <summary>
    /// Implements the Code First Migrations to update the database to the latest version. 
    /// </summary>
    internal class KnowledgeCenterContextDbInitializer : MigrateDatabaseToLatestVersion<KnowledgeCenterContext, MigrationConfiguration>
    {
    }
}
