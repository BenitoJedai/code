using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.Extensions
{
    public static class MessageEventExtensions
    {
        // tested by
        // X:\jsc.svn\examples\javascript\chrome\extensions\ChromeExtensionWithWorker\ChromeExtensionWithWorker\Application.cs


        public static void postMessage(this MessageEvent e, object message)
        {
            e.ports.WithEach(p => p.postMessage(message));
        }
    }
}
