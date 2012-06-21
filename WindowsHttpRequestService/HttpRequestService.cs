// -----------------------------------------------------------------------
// <copyright file="HttpRequestService.cs" company="DFBerry">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace WindowsHttpRequestService
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Linq;
    using System.ServiceProcess;
    using System.Text;
    using System.Threading;
    using ServiceLibrary;

    /// <summary>
    /// Main Service class
    /// </summary>
    public partial class HttpRequestService : ServiceBase
    {
        /// <summary>
        /// Build manager
        /// </summary>
        private static Build build = new Build();

        /// <summary>
        /// Feeds definition
        /// </summary>
        private static BuildItem feeds;

        /// <summary>
        /// Issues definition
        /// </summary>
        private static BuildItem issues;

#if DEBUG
        /// <summary>
        /// Feeds built each minute in milliseconds
        /// </summary>
        private int feedsTimer = 60000;

        /// <summary>
        /// Issues built each two minutes in milliseconds
        /// </summary>
        private int issuesTimer = 120000;
#else
        /// <summary>
        /// Feeds built each day in Milliseconds
        /// </summary>
        private int feedsTimer = 86400000;

        /// <summary>
        /// Issues built each hour in milliseconds
        /// </summary>
        private int issuesTimer = 3600000;
#endif

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpRequestService" /> class.
        /// </summary>
        public HttpRequestService()
        {
            this.InitializeComponent();
        }
        
        /// <summary>
        /// Service Start actions
        /// </summary>
        /// <param name="args">not used here</param>
        protected override void OnStart(string[] args)
        {
            // start timer in one minute (60000) but then move to regular interval after that
            feeds = new BuildItem()
            {
                Timer = new System.Threading.Timer(new System.Threading.TimerCallback(this.OnFeedsTimedEvent), null, 60000, this.feedsTimer),
                Title = "Feeds",
                Uri = new Uri("http://dfb-dashboard.elasticbeanstalk.com/Verify/Index/?build=true&feeds=true"),
                Description = "Build Azure Feeds"
            };

            // start timer in one minute (60000) but then move to regular interval after that
            issues = new BuildItem()
            {
                Timer = new System.Threading.Timer(new System.Threading.TimerCallback(this.OnIssuesTimedEvent), null, 60000, this.issuesTimer),
                Title = "Issues",
                Uri = new Uri("http://dfb-dashboard.elasticbeanstalk.com/Verify/Index/?build=true&issues=true"),
                Description = "Build Azure Issues"
            };
        }

        /// <summary>
        /// Service Stop Actions
        /// </summary>
        protected override void OnStop()
        {
        }

        /// <summary>
        /// Timer to kick off build of feeds data source
        /// </summary>
        /// <param name="source">not used here</param>
        private void OnFeedsTimedEvent(object source)
        {
            build.BuildItem(feeds.Uri, feeds.Title, feeds.Description);
        }

        /// <summary>
        /// Timer to kick off build of issues data source
        /// </summary>
        /// <param name="source">not used here</param>
        private void OnIssuesTimedEvent(object source)
        {
            build.BuildItem(issues.Uri, issues.Title, issues.Description);
        }
    }
}
