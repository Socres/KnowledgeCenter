namespace KnowledgeCenter.Web.AppStart
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Web.Hosting;
    using System.Web.Optimization;

    /// <summary>
    /// Class for configuring bundling and minification for JS and CSS files.
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// Setups the bundles.
        /// </summary>
        public void SetupBundles()
        {
            var bundles = BundleTable.Bundles;
            bundles.UseCdn = true;

            bundles.IgnoreList.Clear();
            AddDefaultIgnorePatterns(bundles.IgnoreList);

            var libScriptsFiles = GetLibScriptsFiles().ToList();
            AddDefaultBundles(bundles, libScriptsFiles);
            AddMarkdownBundles(bundles);

            var appScriptsFiles = GetAppScriptsFiles().ToList();
            AddAngularBundles(bundles);
            AddAngularAppBundles(bundles, appScriptsFiles);
        }

        private static IEnumerable<string> GetLibScriptsFiles()
        {
            var scriptsFolder = HostingEnvironment.MapPath("~/Scripts/");
            if (string.IsNullOrEmpty(scriptsFolder))
            {
                throw new InvalidOperationException("Scripts-Folder not available.");
            }

            return Directory.GetFiles(scriptsFolder, "*.js", SearchOption.AllDirectories).ToList();
        }

        private static IEnumerable<string> GetAppScriptsFiles()
        {
            var appScriptsFolder = HostingEnvironment.MapPath("~/App/");
            if (string.IsNullOrEmpty(appScriptsFolder))
            {
                throw new InvalidOperationException("AppScripts-Folder not available.");
            }

            var appScriptsFiles = new List<string>();
            foreach (var directory in Directory.GetDirectories(appScriptsFolder))
            {
                appScriptsFiles.AddRange(
                    Directory.GetFiles(Path.Combine(appScriptsFolder, directory), "*.js", SearchOption.AllDirectories)
                    .Select(f => f.Replace(appScriptsFolder, "~/App/").Replace(@"\", @"/")));
            }

            return appScriptsFiles;
        }

        /// <summary>
        /// Adds the default ignore patterns.
        /// </summary>
        /// <param name="ignoreList">The ignore list.</param>
        /// <exception cref="System.ArgumentNullException">ignoreList is null.</exception>
        private static void AddDefaultIgnorePatterns(IgnoreList ignoreList)
        {
            ignoreList.Ignore("*.intellisense.js");
            ignoreList.Ignore("*-vsdoc.js");
            ignoreList.Ignore("*.debug.js", OptimizationMode.WhenEnabled);
        }

        /// <summary>
        /// Adds the default bundles.
        /// </summary>
        /// <param name="bundles">The bundles.</param>
        /// <param name="scriptFiles">The scripts files.</param>
        private static void AddDefaultBundles(BundleCollection bundles, IEnumerable<string> scriptFiles)
        {
            var scriptFileList = scriptFiles.ToList();

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(
                new ScriptBundle(
                    "~/bundles/jsjquery",
                    "//ajax.aspnetcdn.com/ajax/jQuery/" + GetFileName("jquery-", scriptFileList) + ".min.js")
                {
                    CdnFallbackExpression = "window.$"
                }
                    .Include("~/Scripts/" + GetFileName("jquery-", scriptFileList) + ".js"));

            bundles.Add(
                new ScriptBundle(
                    "~/bundles/jsjquerymigrate",
                    "//ajax.aspnetcdn.com/ajax/jquery.migrate/" + GetFileName("jquery-migrate-", scriptFileList) +
                    ".min.js")
                {
                    CdnFallbackExpression = "window.$.migrateWarnings"
                }
                    .Include("~/Scripts/" + GetFileName("jquery-migrate-", scriptFileList) + ".js"));

            bundles.Add(
                new ScriptBundle("~/bundles/jsextlibs")
                    .Include("~/Scripts/bootstrap.js")
                    .Include("~/Scripts/json2.js")
                    .Include("~/Scripts/moment.js")
                    .Include("~/Scripts/accounting.js")
                    .Include("~/Scripts/toastr.js"));

            bundles.Add(
                new StyleBundle("~/Content/cssbootstrap")
                    .Include("~/Content/bootstrap.min.css"));

            bundles.Add(
                new StyleBundle("~/Content/cssmain")
                    .Include("~/Content/site.css")
                    .Include("~/Content/font-awesome.min.css")
                    .Include("~/Content/toastr.min.css")
                    .Include("~/Content/Highlight/default.css")
                    .Include("~/Content/Highlight/vs.css"));
        }

        /// <summary>
        /// Adds the markdown bundles.
        /// </summary>
        /// <param name="bundles">The bundles.</param>
        private static void AddMarkdownBundles(BundleCollection bundles)
        {
            bundles.Add(
                new ScriptBundle("~/bundles/jsmarkdown")
                    .Include("~/Scripts/marked.js")
                    .Include("~/Scripts/highlight/highlight.pack.js"));
        }

        /// <summary>
        /// Adds the angular bundles.
        /// </summary>
        /// <param name="bundles">The bundles.</param>
        private static void AddAngularBundles(BundleCollection bundles)
        {
            bundles.Add(
                new ScriptBundle("~/bundles/jsangular")
                    .Include("~/Scripts/angular.js")
                    .Include("~/Scripts/angular-animate.js")
                    .Include("~/Scripts/angular-sanitize.js")
                    .Include("~/Scripts/angular-ui-router.js")
                    .Include("~/Scripts/angular-ui/ui-bootstrap-tpls.js"));
        }

        private static void AddAngularAppBundles(BundleCollection bundles, IEnumerable<string> appScriptsFiles)
        {
            var bundle = new ScriptBundle("~/bundles/jsangularApp");
            foreach (var appScriptsFile in appScriptsFiles
                .Where(f => !f.ToLower().Contains("~/App/infrastructure/".ToLower())))
            {
                bundle.Include(appScriptsFile);
            }
            bundles.Add(bundle);
        }

        private static string GetFileName(string fileName, IEnumerable<string> jsFiles)
        {
            var regex = new Regex(fileName + @"\d+\.\d+\.\d+\.js$");
            return Path.GetFileNameWithoutExtension(jsFiles.First(regex.IsMatch));
        }
    }
}