using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Linq;
using System.Xml.Linq;
using System;
using System.Linq;
using System.Net;
using ScriptCoreLib.Extensions;

namespace NASDAQSNA
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


        public void GetRelatedCompanies(string qid = "NASDAQ:FB", Action<string, string, string> yield = null)
        {

            var c = new WebClient();

            var html = c.DownloadString("http://www.google.com/finance/related?q=" + qid);

            var prefix = "google.finance.data = ";


            var data = html.SkipUntilIfAny(prefix).TakeUntilIfAny("\n");

            var values = data.Split(new[] { "values:[" }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 1; i < values.Length; i++)
            {
                var x = values[i];

                var y = x.TakeUntilLastOrEmpty("]");
                var z = y.Split(new[] { "\",\"" }, StringSplitOptions.RemoveEmptyEntries);

                var ns = z[z.Length - 3].SkipUntilIfAny("\"").TakeUntilLastIfAny("\"");
                var id = z[0].SkipUntilIfAny("\"").TakeUntilLastIfAny("\"");
                var CompanyName = z[1].SkipUntilIfAny("\"").TakeUntilLastIfAny("\"");
                var Price = z[2].SkipUntilIfAny("\"").TakeUntilLastIfAny("\"");

                yield(ns + ":" + id, CompanyName, Price);
            }

        
        }
    }
}
