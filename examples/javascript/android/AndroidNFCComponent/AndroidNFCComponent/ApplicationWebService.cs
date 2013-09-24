using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Linq;
using System.Threading;
using System.Xml.Linq;

namespace AndroidNFCComponent
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService :

        IXNFCComponentSource
    {


        public void XNFCComponentSource_add_AtTagDiscovered(Action<string> y)
        {
            Console.WriteLine("XNFCComponentSource_add_AtTagDiscovered " + new { Thread.CurrentThread.ManagedThreadId });

            Thread.Sleep(555);

            y("hi!");
        }

        static ApplicationWebService()
        {
            Console.WriteLine("ApplicationWebService " + new { Thread.CurrentThread.ManagedThreadId });

            //ApplicationWebService { ManagedThreadId = 12 }
            //XNFCComponentSource_add_AtTagDiscovered { ManagedThreadId = 12 }
            //ApplicationWebService Thread { ManagedThreadId = 19 }

            new Thread(
                delegate()
                {

                    Console.WriteLine("ApplicationWebService Thread " + new { Thread.CurrentThread.ManagedThreadId });

                }
            ).Start();
        }
    }


}
