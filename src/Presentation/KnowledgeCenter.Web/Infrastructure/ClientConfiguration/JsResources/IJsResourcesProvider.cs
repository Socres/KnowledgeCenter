﻿namespace KnowledgeCenter.Web.Infrastructure.ClientConfiguration.JsResources
{
    using System.Collections.Generic;

    /// <summary>
    /// Provider for Javascript Resources.
    /// </summary>
    public interface IJsResourcesProvider
    {
        /// <summary>
        /// Gets the javascript resources.
        /// </summary>
        dynamic GetResources();
    }
}