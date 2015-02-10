namespace KnowledgeCenter.Framework.Extensions
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public static class ObjectExtensions
    {
        /// <summary>
        /// Converts the object to a JSON string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The JSON formatted value.</returns>
        public static string ToJson(this object value)
        {
            return JsonConvert.SerializeObject(value,
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });
        }
    }
}
