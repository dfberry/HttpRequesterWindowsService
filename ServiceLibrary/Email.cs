// -----------------------------------------------------------------------
// <copyright file="Email.cs" company="DFBerry">
// Contains two different approaches to configuring sender. One uses hardcoded strings, the other uses config files. 
// a. Host tested against only allows email to send inside same domain. 
// b. Host tested required username/password with specific host/port combo.
// c. Methods ignore server certificate failures.
// d. MailSettings.config separates out settings but doesn't protect them in anyway.
// e. Make sure MailSettings.config is copied to output dir.
// f. App settings contains tracing hooks so the "SmtpTraceFile.log" contains more information about why 
// sending email barfed as opposed to work.
// </copyright>
// -----------------------------------------------------------------------
namespace ServiceLibrary
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Diagnostics;
    using System.Linq;
    using System.Net;
    using System.Net.Configuration;
    using System.Net.Mail;
    using System.Net.Security;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    
    /// <summary>
    /// Sends Email
    /// </summary>
    public class Email
    {
        /// <summary>
        /// Prefix string to email subject
        /// </summary>
        private string subjectPrefix = "DFBService: ";

        /// <summary>
        /// From email. My specific host requires same domain
        /// recipients so I'll use it as both.
        /// </summary>
        private string fromemail = "dina@dinafberry.com";

        /// <summary>
        /// Mail server username
        /// </summary>
        private string username = "dina@dinafberry.com";

        /// <summary>
        /// Mail server password
        /// </summary>
        private string password = "moby123cat";

        /// <summary>
        /// Mail server host name
        /// </summary>
        private string host = "mail.dinafberry.com";

        /// <summary>
        /// Mail server port
        /// </summary>
        private int port = 26;

        /// <summary>
        /// Configures settings from .config file
        /// </summary>
        /// <param name="title">email's title</param>
        /// <param name="text">email's text</param>
        public void SendWithConfigSettings(string title, string text)
        {
            try
            {
                if (string.IsNullOrEmpty(title))
                {
                    throw new ArgumentNullException("title");
                }

                if (string.IsNullOrEmpty(text))
                {
                    throw new ArgumentNullException("text");
                }

                // server requires same domain emails only 
                // and I'm using it as an IT info email anyway
                // so the from and to are the same
                string to = this.fromemail;

                MailMessage message = new MailMessage(to, to);

                if (message == null)
                {
                    throw new NullReferenceException("Message");
                }

                message.Subject = this.subjectPrefix + title;

                // Add date stamp to top of email
                message.Body = DateTime.Now.ToString() + "\n" + text + "\n"; 
                
                SmtpClient client = new SmtpClient();

                // HACK - gets around the cert exception 
                ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

                client.Send(message);
            }
            catch (Exception ex)
            {
                // exceptions can come from server response such as
                // user/password wrong, email from or do doesn't exist
                Trace.TraceWarning("Email.cs::Send - exception = " + ex.InnerException);
            }
        }

        /// <summary>
        /// Sends mail via hardcoded settings in method. Doesn't (not supposed to)
        /// read settings from .config files.
        /// </summary>
        /// <param name="subject">subject of email</param>
        /// <param name="text">content of email</param>
        public void SendWithClassSettings(string subject, string text)
        {
            try
            {
                MailMessage message = new MailMessage(this.fromemail, this.fromemail);
                message.Subject = this.subjectPrefix + subject;
                message.Body = DateTime.Now.ToString() + "\n" + text + "\n";

                // constructor with params doesn't call config file
                SmtpClient client = new SmtpClient(this.host, this.port);
                client.Credentials = new System.Net.NetworkCredential(this.username, this.password);

                // HACK - gets around the cert exception 
                ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

                client.Send(message);
            }
            catch (Exception ex)
            {
                // exceptions can come from server response such as
                // user/password wrong, email from or do doesn't exist
                Trace.TraceInformation("Smtp error: " + ex.InnerException);
            }
        }
    }
}
