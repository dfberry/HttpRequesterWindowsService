// -----------------------------------------------------------------------
// <copyright file="Build.cs" company="DFBerry">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------
namespace ServiceLibrary
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Calls specific URIs and sends IT-style informational email that event happened. 
    /// The server on the other end does the building, not this class.
    /// </summary>
    public class Build
    {
        /// <summary>
        /// Builds data by requesting uri and assuming server finishes data pull
        /// </summary>
        /// <param name="uri">http location</param>
        /// <param name="buildTitle">title of build action</param>
        /// <param name="buildDescription">description of build action</param>
        public void BuildItem(Uri uri, string buildTitle, string buildDescription)
        {
            Email email = new Email();
            Http http = new Http(uri);

            string result = http.GetRequest();

            email.SendWithClassSettings(buildTitle + http.ResponseStatusCode, buildDescription + "\n" + result);
        }
    }
}
