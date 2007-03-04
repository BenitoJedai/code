using ScriptCoreLib;
using ScriptCoreLib.PHP.IO;
using ScriptCoreLib.Shared;
using ScriptCoreLib.Shared.Serialized;

namespace ScriptCoreLib.PHP.Runtime
{
    [Script]
    public class PHPMailer
    {
        [Script(InternalConstructor = true)]
        public class PHPMailerNative
        {
            public string Subject;
            public string AltBody;
            public string Body;
            public string Mailer;
            public string Host;
            public string From;
            public string FromName;
            public string ContentType;
            public string CharSet;
            public string ErrorInfo;

            public void AddAddress(string email, string title)
            {
            }

            public bool Send()
            {
                return default(bool);
            }

            #region Constructor

            public PHPMailerNative()
            {
                // InternalConstructor
            }


            [Script(OptimizedCode = @"return new PHPMailer();")]
            static PHPMailerNative InternalConstructor()
            {
                return default(PHPMailerNative);
            }

            #endregion

            public bool AddAttachment(string path)
            {
                return default(bool);
            }

            public bool AddAttachment(string path, string name)
            {
                return default(bool);
            }

            public void AddBCC(string to, string name)
            {
            }
        }

        PHPMailerNative m = new PHPMailerNative();

        public PHPMailerNative Base
        {
            get
            {
                return m;
            }
        }

        public PHPMailer()
        {
            
            m.Mailer = "smtp";
            m.CharSet = "utf-8";
        }

        public string ErrorInfo
        {
            get { return m.ErrorInfo; }
        }
	

        public string From
        {
            get { return m.From; }
            set { m.From = value; }
        }

        public string FromName
        {
            get { return m.FromName; }
            set { m.FromName = value; }
        }



        public string Host
        {
            get { return m.Host; }
            set { m.Host = value; }
        }

        string _mail_to = "";

        public void AddAddress(string to)
        {
            _mail_to += to + ";";

            m.AddAddress(to, to);
        }

        TextWriter w = new TextWriter();

        public TextWriter Body
        {
            get
            {
                return w;
            }
        }

        public bool Send()
        {
            m.Body = Native.API.nl2br(w.Text);
            m.AltBody = w.Text;

            if (IsLogging)
            {
                TextWriter u = new TextWriter();

                u.WriteLine(Native.DateTime);
                u.WriteLine("to: " + _mail_to);
                u.WriteLine("from: " + m.From);
                u.WriteLine("subject: " + m.Subject);
                u.WriteLine("body: " + m.Body);

                FileSystemInfo.WriteFile("phpmailer.log", u.Text, true);
            }

            return m.Send();
        }

        public string Subject
        {
            get { return m.Subject; }
            set { m.Subject = value; }
        }

        public bool IsLogging;

        public static PHPMailer Of(SimpleEmailTag z, string host)
        {
            PHPMailer m = new PHPMailer();

            m.Host = host;
            m.AddAddress(z.to);
            m.From = z.from;
            m.FromName = z.from;
            m.Subject = z.subject;
            m.Body.Text = z.body;


            return m;
        }

        public void AddBCC(string p)
        {
            Base.AddBCC(p, p);
        }

        public void AddAttachment(string file, string name)
        {
            Native.Log("PHPMailer: attachment " + file + " as " + name);

            Base.AddAttachment(file, name);
        }
    }
}
