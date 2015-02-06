namespace KnowledgeCenter.Web.Extensions
{
    using System;
    using System.Web.Mvc;

    /// <summary>
    /// Class with extensions for the URL helper.
    /// </summary>
    public static class UrlHelperExtension
    {
        private static readonly Version AssemblyVersion = typeof(UrlHelperExtension).Assembly.GetName().Version;
        private static readonly string FingerprintVersion = "v-" + AssemblyVersion.ToString().Replace('.', '_');

        /// <summary>
        /// Fingerprints the specified URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="contentName">Name of the content.</param>
        public static string Fingerprint(this UrlHelper url, string contentName)
        {
            var index = contentName.LastIndexOf('/');
            return url.Content(contentName.Insert(index, "/" + FingerprintVersion));
        }
    }
}