// -----------------------------------------------------------------------
// <copyright file="BuildItem.cs" company="DFBerry">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace ServiceLibrary
{
    using System;
    
    /// <summary>
    /// Build information to create data sources
    /// </summary>
    public class BuildItem
    {
        /// <summary>
        /// Gets or sets Timer 
        /// </summary>
        public System.Threading.Timer Timer { get; set; }

        /// <summary>
        /// Gets or sets Uri
        /// </summary>
        public Uri Uri { get; set; }

        /// <summary>
        /// Gets or sets Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets Description
        /// </summary>
        public string Description { get; set; }
    }
}
