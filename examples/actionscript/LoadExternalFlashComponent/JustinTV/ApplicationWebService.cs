using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Linq;
using System.Xml.Linq;
using System.Net;

namespace JustinTV
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
    {
        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript. JSC supports string data type for all platforms.</param>
        /// <param name="y">A callback to javascript. In the future all platforms will allow Action&lt;XElementConvertable&gt; delegates.</param>
        public void WebMethod2(string e, XElementAction y)
        {
            var c = new WebClient();
            var s = c.DownloadString("http://api.justin.tv/api/stream/list.xml?category=entertainment&limit=9");

            // http://apiwiki.justin.tv/mediawiki/index.php/Stream/list

            // Send it back to the caller.
            y(XElement.Parse(s));
        }

    }

    public static class Bar
    {
        public static void X()
        {

        }
    }

}
