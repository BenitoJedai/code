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
using System.Threading;

namespace TestThreadManager
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        /// <summary>
        /// The static content defined in the HTML file will be update to the dynamic content once application is running.
        /// </summary>
        public XElement Header = new XElement(@"h1", @"JSC - The .NET crosscompiler for web platforms. ready.");

        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public void WebMethod2(string e, Action<string> y)
        {
            // https://cloud.google.com/appengine/docs/java/javadoc/com/google/appengine/api/ThreadManager#currentRequestThreadFactory()

            // X:\jsc.svn\core\ScriptCoreLibJava.AppEngine\ScriptCoreLibJava.AppEngine\com\google\appengine\api\ThreadManager.cs

            // current version does not show it?
            Console.WriteLine("before ThreadManager.createThreadForCurrentRequest " + new { Thread.CurrentThread.ManagedThreadId });
            // before ThreadManager.createThreadForCurrentRequest { ManagedThreadId = 6 }


            //before ThreadManager.createThreadForCurrentRequest { { ManagedThreadId = 14 } }
            //at ThreadManager.createThreadForCurrentRequest { { ManagedThreadId = 17 } }
            //after ThreadManager.createThreadForCurrentRequest { { ManagedThreadId = 14 } }

            // X:\jsc.svn\core\ScriptCoreLibJava.AppEngine\ScriptCoreLibJava.AppEngine\Extensions\ThreadManagerExtensions.cs
            // 

            var t = com.google.appengine.api.ThreadManager.createThreadForCurrentRequest(new yy
            {

                y = delegate
                {
                    Console.WriteLine("at ThreadManager.createThreadForCurrentRequest " + new { Thread.CurrentThread.ManagedThreadId });

                }
            }
            );

            // before ThreadManager.createThreadForCurrentRequest {{ ManagedThreadId = 10 }}
            //t.start();
            t.start();

            new Thread(
                delegate ()
            {
                Console.WriteLine("at new Thread " + new { Thread.CurrentThread.ManagedThreadId });
            }
            ).Start();

            Thread.Sleep(500);
            Console.WriteLine("after ThreadManager.createThreadForCurrentRequest " + new { Thread.CurrentThread.ManagedThreadId });

            // Send it back to the caller.
            y(e);
        }

        class yy : java.lang.Runnable
        {
            public Action y;

            public void run()
            {
                y();
            }
        }


    }
}
