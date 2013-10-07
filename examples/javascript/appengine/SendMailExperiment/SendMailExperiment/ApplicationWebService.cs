using java.util;
using javax.mail;
using javax.mail.internet;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SendMailExperiment
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
    {

        public string FromAddress;
        public string FromName;

        public string ToAddress;
        public string ToName;

        public string Subject;
        public string MessageString;

        public Task SendEMail()
        {
            // https://developers.google.com/appengine/docs/java/mail/usingjavamail

            Properties props = new Properties();
            Session session = Session.getDefaultInstance(props, null);


            try
            {
                Message msg = new MimeMessage(session);
                //msg.setFrom(new InternetAddress("admin@example.com", "Example.com Admin"));
                msg.setFrom(new InternetAddress(FromAddress, FromName));
                //msg.addRecipient(Message.RecipientType.TO, new InternetAddress("user@example.com", "Mr. User"));
                msg.addRecipient(Message.RecipientType.TO, new InternetAddress(ToAddress, ToName));
                //msg.setSubject("Your Example.com account has been activated");
                msg.setSubject(Subject);
                //msg.setText(msgBody);
                msg.setText(MessageString);
                Transport.send(msg);

            }
            catch
            {
                Console.WriteLine("fail!");
            }


            return Task.FromResult(default(object));

        }

    }
}
