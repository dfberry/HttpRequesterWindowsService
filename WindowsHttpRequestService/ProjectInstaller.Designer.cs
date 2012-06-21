// -----------------------------------------------------------------------
// <copyright file="ProjectInstaller.Designer.cs" company="DFBerry">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace WindowsHttpRequestService
{
    /// <summary>
    /// ProjectInstaller created by template
    /// </summary>
    public partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Installer process - made by ProjectInstaller project template
        /// </summary>
        private System.ServiceProcess.ServiceProcessInstaller serviceProcessInstaller1;

        /// <summary>
        /// Installer service  - made by ProjectInstaller project template
        /// </summary>
        private System.ServiceProcess.ServiceInstaller httpRequester;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.serviceProcessInstaller1 = new System.ServiceProcess.ServiceProcessInstaller();
            this.httpRequester = new System.ServiceProcess.ServiceInstaller();
            // 
            // serviceProcessInstaller1
            // 
            this.serviceProcessInstaller1.Account = System.ServiceProcess.ServiceAccount.LocalService;
            this.serviceProcessInstaller1.Password = null;
            this.serviceProcessInstaller1.Username = null;
            // 
            // HttpRequester
            // 
            this.httpRequester.Description = "HttpRequester";
            this.httpRequester.DisplayName = "HttpRequester";
            this.httpRequester.ServiceName = "HttpRequester";
            this.httpRequester.AfterInstall += new System.Configuration.Install.InstallEventHandler(this.ServiceInstaller1_AfterInstall);
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.serviceProcessInstaller1,
            this.httpRequester});

        }
        #endregion
    }
}