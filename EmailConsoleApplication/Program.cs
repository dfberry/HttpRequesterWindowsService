// -----------------------------------------------------------------------
// <copyright file="Program.cs" company="DFBerry">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace EmailConsoleApplication
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using ServiceLibrary;
    
    /// <summary>
    /// Mail program
    /// </summary>
    public class Program
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

        /// <summary>
        /// Main function. This console doesn't do anything with the timer right now.
        /// </summary>
        /// <param name="args">doens't use args right now</param>
        public static void Main(string[] args)
        {
            feeds = new BuildItem()
            {
                Timer = new System.Threading.Timer(new System.Threading.TimerCallback(OnFeedsTimedEvent), null, 60000, 60000),
                Title = "Feeds",
                Uri = new Uri("http://dfb-dashboard.elasticbeanstalk.com/Verify/Index/?build=true&feeds=true"),
                Description = "Build Azure Feeds"
            };

            issues = new BuildItem()
            {
                Timer = new System.Threading.Timer(new System.Threading.TimerCallback(OnIssuesTimedEvent), null, 120000, 120000),
                Title = "Issues",
                Uri = new Uri("http://dfb-dashboard.elasticbeanstalk.com/Verify/Index/?build=true&issues=true"),
                Description = "Build Azure Issues"
            };

            Console.WriteLine(@"Press 'q' and 'Enter' to quit...");

            while (Console.Read() != 'q')
            {
                Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// Timer for Feeds
        /// </summary>
        /// <param name="source">param not used</param>
        public static void OnFeedsTimedEvent(object source)
        {
            build.BuildItem(feeds.Uri, feeds.Title, feeds.Description);
            Console.WriteLine("Feeds timer " + DateTime.Now);
        }

        /// <summary>
        /// Time for Issues
        /// </summary>
        /// <param name="source">param not used</param>
        public static void OnIssuesTimedEvent(object source)
        {
            build.BuildItem(issues.Uri, issues.Title, issues.Description);
            Console.WriteLine("Issues timer " + DateTime.Now);
        } 
    }
}
