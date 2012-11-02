using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;

namespace PassportVerification
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed partial class ApplicationWebService : Component
    {
        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public void WebMethod2(string e, Action<string> y)
        {
            var c = new WebClient();
            var v = new NameValueCollection();
            var a = "http://www.politsei.ee/et/teenused/e-paringud/dokumendi-kehtivuse-kontroll/";

            var x = c.DownloadString(a);
            //  <input type="hidden" name="csrf" value="PRVXhlghqbVzeWMqD5f3m+2iK/N6w0VilWKClE2DozCAo/KdBk0N7g==" />

            // http://www.enterpriseframework.com/post/2011/10/10/XElement-Xml-nbsp3b-parse-exception.aspx
            // Additional information: Reference to undeclared entity 'nbsp'. Line 1534, position 33.
            //var xml = XElement.Parse(x);
            //var csrf = xml.XPathEvaluate("//*[@name='csrf']");



            var csrf = x.SkipUntilOrEmpty("<input type=\"hidden\" name=\"csrf\" value=\"").TakeUntilOrEmpty("\"");

//cmd:request
//csrf:PRVXhlghqbVc8ZI7y9R5BnsMNh4sCwnklWKClE2DozCAo/KdBk0N7g==
//docNumber:FOO
//subButton:Esita päring

            v["cmd"] = "request";
            v["csrf"] = csrf;
            v["docNumber"] = e.ToUpper();
            v["subButton"] = "Esita päring";

            var r = c.UploadValues(a, v);
            var rr = Encoding.UTF8.GetString(r);
            var rx = rr.SkipUntilOrEmpty("<form name=\"reqForm\" method=\"post\" action=\"\">").SkipUntilOrEmpty("<br/>").TakeUntilOrEmpty("</div>");


            // Send it back to the caller.
            y(rx);
        }

        public static string Host;
        public static string Port;

        public void Handler(WebServiceHandler h)
        {
            Host = h.Context.Request.Headers["Host"].TakeUntilIfAny(":");
            Port = h.Context.Request.Headers["Host"].SkipUntilIfAny(":");

            if (h.Context.Request.Path == "/request")
            {

            }
        }
    }


}
