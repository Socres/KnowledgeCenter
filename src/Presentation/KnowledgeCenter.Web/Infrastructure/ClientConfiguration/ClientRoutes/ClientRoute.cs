namespace KnowledgeCenter.Web.Infrastructure.ClientConfiguration.ClientRoutes
{
    using System.Collections.Generic;

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
        }

        /// <summary>
        /// Gets or sets the Route.
        /// </summary>
        public string Route { get; set; }

        /// <summary>
        /// Gets or sets the template url.
        /// </summary>
        public string TemplateUrl { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

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
        public string CaptionText { get; set; }

        /// <summary>
        /// Gets or sets the caption HTML user for Navigation bar.
        /// </summary>
        public string CaptionIconCssClass { get; set; }

        /// <summary>
        /// Gets or sets the child routes.
        /// </summary>
        public List<ClientRoute> ChildRoutes { get; set; }

        /// <summary>
        /// Adds the child route.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <param name="templateUrl">The template URL.</param>
        /// <param name="name">The name.</param>
        /// <param name="captionText">The caption text.</param>
        /// <param name="captionIconCssClass">The caption icon CSS class.</param>
        /// <returns>
        /// The new ChildRoute
        /// </returns>
        public ClientRoute AddChildRoute(
            string route,
            string templateUrl,
            string name,
            string captionText,
            string captionIconCssClass)
        {
            var clientRoute = new ClientRoute
            {
                Visible = false,
                Route = route,
                TemplateUrl = templateUrl,
                Name = name,
                NavigationParentName = NavigationParentName,
                CaptionText = captionText,
                CaptionIconCssClass = captionIconCssClass
            };
            ChildRoutes.Add(clientRoute);

            return clientRoute;
        }
    }
}