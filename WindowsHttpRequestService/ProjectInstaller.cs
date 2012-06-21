// -----------------------------------------------------------------------
// <copyright file="ProjectInstaller.cs" company="DFBerry">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace WindowsHttpRequestService
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Configuration.Install;
    using System.Linq;
    
    /// <summary>
    /// Installs as Windows Service
    /// </summary>
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectInstaller" /> class.
        /// </summary>
        public ProjectInstaller()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Created by template
        /// </summary>
        /// <param name="sender">calling object</param>
        /// <param name="e">params passed through</param>
        private void ServiceInstaller1_AfterInstall(object sender, InstallEventArgs e)
        {
        }
    }
}
