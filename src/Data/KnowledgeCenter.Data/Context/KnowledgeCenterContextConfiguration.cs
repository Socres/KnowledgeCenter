namespace KnowledgeCenter.Data.Context
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public class KnowledgeCenterContextConfiguration : DbConfiguration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KnowledgeCenterContextConfiguration"/> class.
        /// </summary>
        public KnowledgeCenterContextConfiguration()
        {
            SetDefaultConnectionFactory(new SqlConnectionFactory());
            SetProviderServices("System.Data.SqlClient", System.Data.Entity.SqlServer.SqlProviderServices.Instance);
        }
    }
}
