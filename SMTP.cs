using System;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace EmailUtility
{
    /// <summary>
    /// Allowed Logging Level
    /// </summary>
    public enum LogLevel
    {
        INFO,
        ERROR
    }
    /// <summary>
    /// Send email using external SMTP Server
    /// </summary>
    public class SMTP
    {
        private SmtpClient _smtpClient;
        private MailMessage _mailMessage;

        public SMTP()
        {
        }

        public string Result { get; private set; }
        /// <summary>
            ///Initialize SMTP Client - MUST BE OFF PRA NETWORK!!
		/// </summary>
        public void GetSmtpClient()
        {
            _smtpClient = new SmtpClient();
            _smtpClient.Host = "smtp.gmail.com";
            _smtpClient.Port = 587;
            _smtpClient.EnableSsl = true;
            _smtpClient.Timeout = 10000;
            _smtpClient.UseDefaultCredentials = false;
            _smtpClient.Credentials = new NetworkCredential("username", "password");
        }
        /// <summary>
        /// Build message (from is determined by smtp server)
        /// </summary>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="cc"></param>
        public void BuildMessage(string to, string subject, string body, string cc)
        {
            //Custom Display Name can be used for process emails that need a certain title
            MailAddress fromAddress = new MailAddress("username@gmail.com", "Custom Display Name"); 
            MailAddress toAddress = new MailAddress(to);

            _mailMessage = new MailMessage(fromAddress, toAddress);
            //_mailMessage.From.
            _mailMessage.CC.Add(cc);
            _mailMessage.Subject = subject;
            _mailMessage.Body = body;
        }
        /// <summary>
        /// send email - log success or exception
        /// </summary>
        /// <returns>Either true or false</returns>
        public bool SendMessage()
        {
            try
            {
                _smtpClient.Send(_mailMessage);
                Result = $"Email successfully sent to {string.Join(",", _mailMessage.To)}";
                Log(Result, LogLevel.INFO);
                return true;
            }
            catch (Exception ex)
            {
                //Result = email to addresses : exception
                Result = $"{string.Join(",", _mailMessage.To)} : {ex.Message}";
                Log(Result, LogLevel.ERROR);
                return false;
            }
        }
        /// <summary>
        /// Static method for sending email
        /// </summary>
        /// <param name="To"></param>
        /// <param name="Subject"></param>
        /// <param name="Body"></param>
        /// <param name="Cc"></param>
        /// <returns>Either true or false</returns>
        public static bool SendEmail(string To, string Subject, string Body, string Cc)
        {
            SMTP smtpExt = new SMTP();
            smtpExt.GetSmtpClient();
            smtpExt.BuildMessage(To, Subject, Body, Cc);
            return smtpExt.SendMessage();
        }
      
        /// <summary>
        /// log message at LogLevel (see enum). logs divided by day (this is optional if you want logging)
        /// </summary>
        /// <param name="message"></param>
        /// <param name="level"></param>
        public void Log(string message, LogLevel level)
        {
            string path = @"c:\temp\ExternalEmailTestingLog_" + DateTime.Now.ToString("yyyy_MM_dd")+".txt";
            string logDateTime = DateTime.Now.ToString("yyyy-MM-dd HH.mm.ss");
            string line = $"{logDateTime},{level},{message}";
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(line);
            }
        }
    }
}
