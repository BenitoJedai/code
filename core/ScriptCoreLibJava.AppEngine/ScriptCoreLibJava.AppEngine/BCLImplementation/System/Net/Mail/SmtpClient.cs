using java.util;
using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCoreLibJava.AppEngine.BCLImplementation.System.Net.Mail
{
    // why not move to ScriptCoreLibJava?
    [Script(Implements = typeof(global::System.Net.Mail.SmtpClient))]
    public class __SmtpClient : IDisposable
    {
        // tested by?

        public __SmtpClient(string host, int port)
        {
            // tested by
            // X:\jsc.smokescreen.svn\market\appengine\xmoneseservices\xmoneseservices\ApplicationWebService.cs
            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/04-monese/201312/20131204-appengine-db-test

        }

        public ICredentialsByHost Credentials { get; set; }

        public bool EnableSsl { get; set; }

        public Task SendMailAsync(string from, string recipients, string subject, string body)
        {

            // http://stackoverflow.com/questions/6606529/package-javax-mail-and-javax-mail-internet-do-not-exist
            // partial build of Java and Java.AppEngine are missing it?

            //javax.servlet.Servlet
            var props = new Properties();
            var session = javax.mail.Session.getDefaultInstance(props, null);
            // https://developers.google.com/appengine/docs/java/mail/usingjavamail


            try
            {
                // email
                var msg = new javax.mail.internet.MimeMessage(session);
                msg.setFrom(new javax.mail.internet.InternetAddress(from, from));
                msg.addRecipient(javax.mail.Message.RecipientType.TO,
                                 new javax.mail.internet.InternetAddress(recipients, recipients));
                msg.setSubject(subject);
                msg.setText(body);

                javax.mail.Transport.send(msg);
            }
            catch
            {
                throw;
            }



            var x = new TaskCompletionSource<object>();

            // do we support async yet?
            return x.Task;
        }

        public void Dispose()
        {
        }
    }
}
