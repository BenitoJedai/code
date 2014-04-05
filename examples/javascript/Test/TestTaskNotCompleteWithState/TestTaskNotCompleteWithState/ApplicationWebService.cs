using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TestTaskNotCompleteWithState
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        public Task Invoke()
        {
            Console.WriteLine("enter Invoke " + new { Debugger.IsAttached });

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201404/20140405/task
            return new TaskCompletionSource<object>().Task;

            // jsc server will wait forever basically.
        }

    }
}
