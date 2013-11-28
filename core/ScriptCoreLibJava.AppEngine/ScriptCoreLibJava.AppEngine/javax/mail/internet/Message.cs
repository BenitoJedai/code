using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace javax.mail
{
    [Script(IsNative = true)]
    public abstract class Message // : Part
    {
        [Script(IsNative = true)]
        public class RecipientType //: Serializable
        {
            public static Message.RecipientType BCC;
            public static Message.RecipientType CC;
            public static Message.RecipientType TO;


        }
    }
}
