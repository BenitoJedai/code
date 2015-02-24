using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AsyncNestedServerCallbacks
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


        // what about two way communication? webrtc
        public Task<XElement> FooButton
        {
            get
            {
                // Error	2	Cannot implicitly convert type 'System.Xml.Linq.XElement' to 'System.Threading.Tasks.Task<System.Xml.Linq.XElement>'	X:\jsc.svn\examples\javascript\async\AsyncNestedServerCallbacks\AsyncNestedServerCallbacks\ApplicationWebService.cs	31	24	AsyncNestedServerCallbacks

                // a button whose click handler is here
                Func<Action<string>, Task> onclick = async set_innerText =>
                {
                    // take your tme
                    Thread.Sleep(500);

                    // can we await on anything on the client side?
                    // need event stream first?

                    set_innerText("at FooButton onclick");
                };

                // give them a button

                var x = XElement.Parse(AsyncNestedServerCallbacks.HTML.Pages.XButtonSource.Text);

                x.Add(
                     new XAttribute("data-onclick", onclick.Method.MetadataToken)
                );

                return Task.FromResult(x);


                //return Task.FromResult(
                //    new XElement("button",
                //        new XAttribute("data-onclick", onclick.Method.MetadataToken),
                //        "hi from Foo"
                //    )
                //);
            }
        }

        [Obsolete]
        public async Task InternalWebServiceInvokeAsync(string token, Action<string> set_innerText)
        {
            // yield_MethodToken needs security to only allow
            // calling public methods.
            // we should be encrypting them, so that client cannot tamper?

            Console.WriteLine("InternalWebServiceInvokeAsync " + new { token });

            // how would other platforms do this?
            var method = (MethodInfo)typeof(ApplicationWebService).Module.ResolveMethod(int.Parse(token));

            var f = (Func<Action<string>, Task>)Delegate.CreateDelegate(
                type: typeof(Func<Action<string>, Task>),
                method: method
            );

            var r = f(set_innerText);

            if (!r.IsCompleted)
                throw new InvalidOperationException();

        }
    }
}
