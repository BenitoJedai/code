using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AsyncWebMethod
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
    {
        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public void WebMethod2(string e, Action<string> y)
        {
            // Send it back to the caller.
            y(e);
        }


        public void WebMethod4(string e
        #region compiler generated
, Action<string> y)
        {
            Console.WriteLine("enter WebMethod4");
            var t = InternalWebMethod4(e);

            var x = new AutoResetEvent(false);

            // RunSynchronously may not be called on a task not bound to a delegate, such as the task returned from an asynchronous method.
            t.ContinueWith(
                task =>
                {
                    y(task.Result);

                    x.Set();
                }
            );

            x.WaitOne();

            Console.WriteLine("exit WebMethod4");
        }


        public async Task<string> InternalWebMethod4(string e
        #endregion
)
        {
            Console.WriteLine("delay");
            Thread.Sleep(1000);

            Console.WriteLine("delay done");
            return new { e }.ToString();


        }
    }

    public static class X
    {

        // jsc should support Task and MessageChannels via eventsource
        public static Task<string> WebMethod4(this ApplicationWebService service, string e)
        {
            var x = new TaskCompletionSource<string>();

            service.WebMethod4(e,
                value =>
                {
                    x.SetResult(value);
                }
            );

            return x.Task;
        }
    }
}
