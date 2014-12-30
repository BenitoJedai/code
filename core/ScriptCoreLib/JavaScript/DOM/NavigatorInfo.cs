using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    // http://mxr.mozilla.org/mozilla-central/source/dom/webidl/Navigator.webidl
    // http://mxr.mozilla.org/mozilla-central/source/dom/interfaces/base/nsIDOMNavigator.idl
    // https://github.com/adobe/webkit/blob/master/Source/WebCore/page/Navigator.idl
    // X:\opensource\github\WootzJs\WootzJs.Web\Navigator.cs

    // https://src.chromium.org/viewvc/blink/trunk/Source/core/frame/Navigator.idl
    // https://src.chromium.org/viewvc/blink/trunk/Source/core/frame/NavigatorCPU.idl
    // hardwareConcurrency

    // rename to INavigator ?
    [Script]
    public partial class NavigatorInfo
    {
        // http://src.chromium.org/viewvc/blink/trunk/Source/modules/credentialmanager/NavigatorCredentials.idl

        // http://w3c.github.io/netinfo/
        public INetworkInformation connection;


        // http://wiki.whatwg.org/wiki/NavigatorCores
        // tested by?
        // X:\jsc.svn\examples\javascript\async\Test\TestNavigatorCores\TestNavigatorCores\Application.cs
        //public uint hardwareConcurrency;
        public int hardwareConcurrency;

        // http://www.w3.org/TR/nfc/#extensions-to-navigator-object
        // tested by ?
        public object nfc;


        // see also:
        // X:\jsc.svn\examples\javascript\Test\TestMediaCaptureAPI\TestMediaCaptureAPI\Application.cs
        // X:\jsc.svn\market\javascript\Abstractatech.JavaScript.Avatar\Abstractatech.JavaScript.Avatar\Application.cs
        // X:\jsc.svn\examples\javascript\WebCamToGIFAnimation\WebCamToGIFAnimation\Application.cs

        // http://www.whatwg.org/specs/web-apps/current-work/multipage/offline.html#dfnReturnLink-0
        [Obsolete("ServiceWorker")]
        public bool onLine;

        public string userAgent;
        public string appVersion;

        [Script]
        public class PluginInfo
        {
            public string description;
        }

        // http://www.comptechdoc.org/independent/web/cgi/javamanual/javamimetype.html
        // http://www.irt.org/xref/MimeType.htm
        [Script]
        public class MimeTypeInfo
        {
            public string description;
            public string type;
        }

        // tested by
        // X:\jsc.svn\examples\javascript\Test\TestEIDPIN2\TestEIDPIN2\Application.cs
        public IArray<MimeTypeInfo> mimeTypes;
        public IArray<PluginInfo> plugins;


        // http://src.chromium.org/viewvc/blink/trunk/Source/modules/serviceworkers/NavigatorServiceWorker.idl
        // tested by?
        // X:\jsc.svn\examples\javascript\test\TestNavigatorServiceWorker\TestNavigatorServiceWorker\Application.cs
        // chrome OS apps and server will be the first to have this tested on? then android? then app engine?
        // Chrome Canary. Type: "chrome://flags/" in the URL bar and turn on "enable-service-worker" and "experimental-web-platform-features".
        public readonly ServiceWorkerContainer serviceWorker;

    }

}
