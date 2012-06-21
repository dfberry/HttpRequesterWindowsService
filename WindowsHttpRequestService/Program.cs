// -----------------------------------------------------------------------
// <copyright file="Program.cs" company="DFBerry">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace WindowsHttpRequestService
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.ServiceProcess;
    using System.Text;

    /// <summary>
    /// Windows Service Main Program
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the service.
        /// </summary>
        /// <param name="args">not used in this service</param>
        public static void Main(string[] args)
        {
            ServiceBase[] servicesToRun;
            servicesToRun = new ServiceBase[] { new HttpRequestService() };
            ServiceBase.Run(servicesToRun);
        }
    }
}
