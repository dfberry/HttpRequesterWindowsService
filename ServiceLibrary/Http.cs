// -----------------------------------------------------------------------
// <copyright file="Http.cs" company="DFBerry">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace ServiceLibrary
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Used to make Http Requests
    /// </summary>
    public class Http 
    {
        /// <summary>
        /// URI to get
        /// </summary>
        private Uri uri;

        /// <summary>
        /// Timeout, default is 2 minutes.
        /// </summary>
        private TimeSpan timeSpan = new TimeSpan(0, 2, 0);

        /// <summary>
        /// HttpWebRequest containing call properties.
        /// </summary>
        private HttpWebRequest httpWebRequest;

        /// <summary>
        /// Response Content 
        /// </summary>
        private string responseCode;

        /// <summary>
        /// Response Content 
        /// </summary>
        private string responseContent;

        /// <summary>
        /// Initializes a new instance of the <see cref="Http" /> class.
        /// Given Uri and timeout, set Uri and timeout.
        /// If getUri is null, find default at 
        /// ConfigurationManager.AppSettings["AzureDashboardServiceURL"]
        /// </summary>
        /// <param name="getUri">URI to request</param>
        public Http(Uri getUri)
        {
            // verify param
            if (getUri == null)
            {
                throw new ArgumentNullException();
            }

            // set uri
            this.uri = getUri;

            this.httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(this.uri);
            this.httpWebRequest.Timeout = (int)this.timeSpan.TotalMilliseconds;
            this.httpWebRequest.ReadWriteTimeout = (int)this.timeSpan.TotalMilliseconds * 100;
            this.httpWebRequest.Method = "GET";
            this.httpWebRequest.ContentType = "text/html";
        }

        /// <summary>
        /// Gets ResponseStatusCode. 
        /// </summary>
        public string ResponseStatusCode
        {
            get
            {
                return this.responseCode;
            }
        }

        /// <summary>
        /// GetRequest makes a request and reads and returns response content.
        /// </summary>
        /// <returns>string as HTTPResponse content</returns>
        public string GetRequest()
        {
            try
            {
                using (HttpWebResponse httpWebResponse = (HttpWebResponse)this.httpWebRequest.GetResponse())
                {
                    this.responseCode = httpWebResponse.StatusCode.ToString();

                    using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                    {
                        this.responseContent = streamReader.ReadToEnd();
                        return this.responseContent;
                    }
                }
            }
            catch (WebException webException)
            {
                if (webException.Response != null)
                {
                    using (Stream responseStream = webException.Response.GetResponseStream())
                    {
                        if (responseStream != null)
                        {
                            using (StreamReader reader = new StreamReader(responseStream))
                            {
                                Trace.TraceError(reader.ReadToEnd());
                            }
                        }
                    }
                }

                throw;
            }
        }
    }
}