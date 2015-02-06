namespace KnowledgeCenter.Web.Infrastructure.JsResources
{
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using KnowledgeCenter.Web.Properties;

    /// <summary>
    /// Provider for Javascript Resources.
    /// </summary>
    public class JsResourcesProvider : IJsResourcesProvider
    {
        /// <summary>
        /// Gets the javascript resources.
        /// </summary>
        public dynamic GetResources()
        {
            var jsResources = new List<KeyValuePair<string, string>>();

            AddDateTimeFormatsToJsResources(jsResources);

            AddResourceFileToJsResources(jsResources);

            // Convert to dynamic
            IDictionary<string, object> expando = new ExpandoObject();

            foreach (var kvp in jsResources)
            {
                expando.Add(kvp.Key, kvp.Value);
            }

            return expando;
        }

        private static void AddDateTimeFormatsToJsResources(
            ICollection<KeyValuePair<string, string>> jsResources)
        {
            var dateTimeShortDatePattern = Thread.CurrentThread.CurrentUICulture.DateTimeFormat
                .ShortDatePattern;

            var dateTimeLongTimePattern = Thread.CurrentThread.CurrentUICulture.DateTimeFormat
                .LongTimePattern;

            jsResources.Add(
                new KeyValuePair<string, string>(
                    "dateFormat",
                    dateTimeShortDatePattern));

            jsResources.Add(
                new KeyValuePair<string, string>(
                    "dateTimeFormat",
                    dateTimeShortDatePattern + " " + dateTimeLongTimePattern));

            jsResources.Add(
                new KeyValuePair<string, string>(
                    "timeFormat",
                    dateTimeLongTimePattern));

            jsResources.Add(
                new KeyValuePair<string, string>(
                    "currencySymbol",
                    CultureInfo.GetCultureInfo("nl-nl").NumberFormat.CurrencySymbol));

            jsResources.Add(
                new KeyValuePair<string, string>(
                    "currencyDecimalSeparator",
                    Thread.CurrentThread.CurrentUICulture.NumberFormat.CurrencyDecimalSeparator));

            jsResources.Add(
                new KeyValuePair<string, string>(
                    "currencyGroupSeparator",
                    Thread.CurrentThread.CurrentUICulture.NumberFormat.CurrencyGroupSeparator));
        }

        private static void AddResourceFileToJsResources(ICollection<KeyValuePair<string, string>> jsResources)
        {
            // Add all resources defined in JsResources.resx
            foreach (
                var resource in
                    typeof(JsResources).GetProperties(BindingFlags.Static | BindingFlags.Public)
                        .Where(p => p.PropertyType == typeof(string)))
            {
                var jsonName =
                    char.ToLower(resource.Name[0], CultureInfo.InvariantCulture).ToString(CultureInfo.InvariantCulture);
                if (resource.Name.Length > 1)
                {
                    jsonName = jsonName + resource.Name.Substring(1);
                }

                jsResources.Add(new KeyValuePair<string, string>(jsonName, resource.GetValue(null) as string));
            }
        }
    }
}