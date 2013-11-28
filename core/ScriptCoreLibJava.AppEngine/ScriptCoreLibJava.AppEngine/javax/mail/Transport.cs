using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace javax.mail
{
    [Script(IsNative = true)]
    public abstract class Transport // : Service
    {
        //[ScriptMethodThrows(typeof(MessagingException))]
        public static void send(Message value)
        {
        }
    }
}
