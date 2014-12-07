using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ScriptCoreLib;

namespace ScriptCoreLibJava.AppEngine.Extensions
{
    [Script]
    public class ThreadManagerExtensions
    {
        [Obsolete("called by the compiler, to enable Task.Run for app engine services.")]
        public static void InitializeWebServiceThread()
        {
            Console.WriteLine("InitializeWebServiceThread " + new { Thread.CurrentThread.ManagedThreadId });

            // X:\jsc.svn\examples\java\appengine\Test\TestThreadManager\TestThreadManager\ApplicationWebService.cs
            ScriptCoreLibJava.BCLImplementation.System.Threading.__Thread.new_java_lang_Thread =
                yy => com.google.appengine.api.ThreadManager.createThreadForCurrentRequest(yy);

        }
    }
}
