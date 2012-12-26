using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace TestNameValueCollection
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
            var x = new NameValueCollection();

            var w = new StringBuilder();

            w.AppendLine("adding foo");

            //x.Add("foo", e);
            x["foo"] = e;



            w.AppendLine("reading foo");

            var foo = x["foo"];

            var message = new
            {
                //w = w.ToString(),
                foo
            };

            y(message.ToString());
        }

    }
}
