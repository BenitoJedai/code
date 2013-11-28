using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace javax.mail.internet
{
    [Script(IsNative = true)]
    public class MimeMessage : Message //, MimePart
    {
        public virtual void addRecipient(Message.RecipientType arg0, Address arg1)
        { 
        }

        public  void setFrom(Address value)
        { 
        }

        public  void setSubject(string value)
        { 
        }
        public  void setText(string value)
        { 
        }

        public MimeMessage(Session value)
        { 

        }
    }
}
