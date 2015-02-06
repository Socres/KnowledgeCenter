namespace KnowledgeCenter.Web.Infrastructure.ClientConfiguration
{
    public interface IClientConfigProvider
    {
        /// <summary>
        /// Gets the configuration for a client.
        /// </summary>
        /// <returns></returns>
        ClientConfig GetConfiguration();
    }
}