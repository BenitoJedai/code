using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.Linq;
using System.Xml.Linq;

namespace OpenSearchExperiment
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


            // http://stackoverflow.com/questions/8650377/opensearch-description-document-discovery-and-chrome
            // http://stackoverflow.com/questions/15743751/google-chrome-search-my-website-from-url-bar
        }

        public void Handler(WebServiceHandler h)
        {
            var HostUri = new
            {
                Host = h.Context.Request.Headers["Host"].TakeUntilIfAny(":"),
                Port = h.Context.Request.Headers["Host"].SkipUntilOrEmpty(":")
            };

            if (HostUri.Port == "")
                HostUri = new { HostUri.Host, Port = "80" };



            Console.WriteLine();
            // cloud make it a nop for server? or jvm or android
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine(h.Context.Request.HttpMethod + " " + h.Context.Request.Path);

            Console.ForegroundColor = ConsoleColor.Red;


            Console.ForegroundColor = ConsoleColor.Green;

            h.Context.Request.Headers.AllKeys.WithEach(
                k => Console.WriteLine(k + ": " + h.Context.Request.Headers[k])
            );

            #region application/opensearchdescription+xml
            if (h.Context.Request.Path == "/opensearchdescription")
            {
                //Firefox could not download the search plugin from:
                //http://192.168.1.100:3148/opensearch
                h.Context.Response.ContentType = "application/opensearchdescription+xml";

                // http://www.dailymotion.com/en/factory/opensearch

                //new EngineURL: template is not a valid URI!
                // _parseURL: failed to add 192.168.1.100:2155/?{searchTerms} as a URL
                // form input will need ?s=

                h.Context.Response.Write(
    @"<OpenSearchDescription xmlns='http://a9.com/-/spec/opensearch/1.1/' xmlns:moz='http://www.mozilla.org/2006/browser/search/'>
<ShortName>OpenSearchExperiment</ShortName>
<Description>
Search OpenSearchExperiment
</Description>
<InputEncoding>UTF-8</InputEncoding>
<Image width='16' height='16' type='image/x-icon'>/favicon.ico</Image>
<Url type='text/html' method='get' template='http://" + HostUri.Host + ":" + HostUri.Port + @"/?s={searchTerms}'/>
</OpenSearchDescription>
"
);

                h.CompleteRequest();
                return;
            }
            #endregion



        }

    }
}
