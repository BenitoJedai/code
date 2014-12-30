using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Shared;
using ScriptCoreLib.JavaScript.DOM.XML;
using ScriptCoreLib.JavaScript.Runtime;
using System.Threading.Tasks;
using ScriptCoreLib.JavaScript.WebGL;

namespace ScriptCoreLib.JavaScript.DOM
{
    public partial class IXMLHttpRequest
    {
        // we are about to redirect web calls

        [Script(DefineAsStatic = true)]
        public void open(HTTPMethodEnum method, string url)
        {
            IXMLHttpRequestActivity.raise_onsend(
                new IXMLHttpRequestActivity { 
                    request = this, method = method, url = url, async = true }
            );

        }

        [Script(DefineAsStatic = true)]
        public void open(HTTPMethodEnum method, string url, bool @async)
        {
            IXMLHttpRequestActivity.raise_onsend(
              new IXMLHttpRequestActivity { request = this, method = method, url = url, async = async }
            );
        }

        // [Custom, ActivityLogging=ForAllWorlds, RaisesException] void open(DOMString method, DOMString url, optional boolean async, optional DOMString user, optional DOMString password);
        [Script(DefineAsStatic = true)]
        public void open(HTTPMethodEnum method, string url, bool @async, string user, string password)
        {
            IXMLHttpRequestActivity.raise_onsend(
              new IXMLHttpRequestActivity { request = this, method = method, url = url, async = async, user = user, password = password }
            );
        }
    }

    [Script(HasNoPrototype = true)]
    internal class __IXMLHttpRequest_open
    {
        public void open(HTTPMethodEnum method, string url, bool @async, string user, string password)
        {
        }
    }

    [Obsolete("handled by service worker onfetch?")]
    [Script]
    public class IXMLHttpRequestActivity
    {
        
        // X:\jsc.internal.svn\compiler\jsc.meta\jsc.meta\Library\Templates\JavaScript\InternalWebMethodRequest.cs

        public IXMLHttpRequest request;

        public HTTPMethodEnum method;
        public string url;
        public bool @async;
        public string user;
        public string password;

        public static event Action<IXMLHttpRequestActivity> onopen;

        internal static void raise_onsend(IXMLHttpRequestActivity value)
        {
            // X:\jsc.svn\examples\javascript\svg\SVGNavigationTiming\SVGNavigationTiming\Application.cs

            if (onopen != null)
                onopen(value);

            var request = ((__IXMLHttpRequest_open)(object)value.request);

            // why isnt jsc respecting the Native name here?
            request.open(
                value.method,
                value.url,
                value.async,
                value.user,
                value.password
            );

           


        }
    }
}
