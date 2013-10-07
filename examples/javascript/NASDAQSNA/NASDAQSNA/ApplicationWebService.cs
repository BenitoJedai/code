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
using System.Threading.Tasks;

namespace NASDAQSNA
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
    {

        public string qid = "NASDAQ:FB";

        public Action<string, string, string> yield = null;


        public async Task GetRelatedCompanies()
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
