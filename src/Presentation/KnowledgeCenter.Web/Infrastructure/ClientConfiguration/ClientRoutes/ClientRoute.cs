namespace KnowledgeCenter.Web.Infrastructure.ClientConfiguration.ClientRoutes
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    /// <summary>
    /// Information for client side routing.
    /// </summary>
    public class ClientRoute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientRoute"/> class.
        /// </summary>
        public ClientRoute()
        {
            ChildRoutes = new List<ClientRoute>();
            Parameters = new Collection<string>();
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the params a client can supply in the url.
        /// </summary>
        public ICollection<string> Parameters { get; set; }

        /// <summary>
        /// Gets or sets the Url.
        /// </summary>
        public string Url
        {
            get
            {
                return Parameters.Aggregate("/" + Name, (current, parameter) => current + ("/:" + parameter));
            }
        }

        /// <summary>
        /// Gets or sets the Href.
        /// </summary>
        public string Href
        {
            get { return "#/" + Name + (Parameters.Any() ? "/" : string.Empty); }
        }

        /// <summary>
        /// Gets or sets the template url.
        /// </summary>
        public string TemplateUrl { get; set; }

        /// <summary>
        /// Gets or sets the name of the navigation parent.
        /// </summary>
        public string NavigationParentName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ClientRoute"/> is visible in the Navigation bar.
        /// </summary>
        public bool Visible { get; set; }

        /// <summary>
        /// Gets or sets the caption text used far page title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the caption HTML user for Navigation bar.
        /// </summary>
        public string TitleIconCss { get; set; }

        /// <summary>
        /// Gets or sets the child routes.
        /// </summary>
        public List<ClientRoute> ChildRoutes { get; set; }

        /// <summary>
        /// Adds the child route.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="templateUrl">The template URL.</param>
        /// <param name="title">The title.</param>
        /// <param name="titleIconCssClass">The title icon CSS class.</param>
        /// <returns>
        /// The new ChildRoute
        /// </returns>
        public ClientRoute AddChildRoute(
            string name,
            string templateUrl,
            string title,
            string titleIconCssClass)
        {
            var clientRoute = new ClientRoute
            {
                Visible = false,
                Name = name,
                TemplateUrl = templateUrl,
                NavigationParentName = NavigationParentName,
                Title = title,
                TitleIconCss = titleIconCssClass
            };
            ChildRoutes.Add(clientRoute);

            return clientRoute;
        }
    }
}