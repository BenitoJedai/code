using java.util;
using javax.mail;
using javax.mail.internet;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Linq;
using System.Xml.Linq;

namespace SendMailExperiment
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
    {
        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public void WebMethod2(string msgBody, Action<string> y)
        {
            // https://developers.google.com/appengine/docs/java/mail/usingjavamail

            Properties props = new Properties();
            Session session = Session.getDefaultInstance(props, null);


            try
            {
                Message msg = new MimeMessage(session);
                msg.setFrom(new InternetAddress("admin@example.com", "Example.com Admin"));
                msg.addRecipient(Message.RecipientType.TO,
                                 new InternetAddress("user@example.com", "Mr. User"));
                msg.setSubject("Your Example.com account has been activated");
                msg.setText(msgBody);
                Transport.send(msg);

            }
            catch
            {
                Console.WriteLine("fail!");
            }

        }

    }
}
