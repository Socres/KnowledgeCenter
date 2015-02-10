namespace KnowledgeCenter.Web.Infrastructure.ClientConfiguration
{
    public interface IClientConfigProvider
    {
        /// <summary>
        /// Gets the configuration for a client.
        /// </summary>
        /// <param name="baseTemplateUrl">The base template URL.</param>
        /// <param name="culture">The culture.</param>
        /// <returns></returns>
        ClientConfig GetConfiguration(string baseTemplateUrl, string culture);
    }
}