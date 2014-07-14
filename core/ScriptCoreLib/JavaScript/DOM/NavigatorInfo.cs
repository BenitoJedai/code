using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    // X:\opensource\github\WootzJs\WootzJs.Web\Navigator.cs

    
    [Script]
    public class NavigatorInfo
    {
        // see also:
        // X:\jsc.svn\examples\javascript\Test\TestMediaCaptureAPI\TestMediaCaptureAPI\Application.cs
        // X:\jsc.svn\market\javascript\Abstractatech.JavaScript.Avatar\Abstractatech.JavaScript.Avatar\Application.cs
        // X:\jsc.svn\examples\javascript\WebCamToGIFAnimation\WebCamToGIFAnimation\Application.cs

        // http://www.whatwg.org/specs/web-apps/current-work/multipage/offline.html#dfnReturnLink-0
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


        public IArray<MimeTypeInfo> mimeTypes;
        public IArray<PluginInfo> plugins;
    }

}
