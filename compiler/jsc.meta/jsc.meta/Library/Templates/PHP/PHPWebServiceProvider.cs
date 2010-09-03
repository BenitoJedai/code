using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.PHP;
using ScriptCoreLib;
using jsc.meta.Library.Templates.Java;
using ScriptCoreLib.PHP.BCLImplementation.System.Web;
using System.Web;
using ScriptCoreLib.Ultra.WebService;
using ScriptCoreLib.Delegates;

namespace jsc.meta.Library.Templates.PHP
{
    internal static class PHPWebServiceProvider
    {
        // this class is a template
        // this class cannot be used in .net
        // this could be defined in ScriptCoreLib.Ultra

        /// <summary>
        /// This method is to be called in the index.php
        /// </summary>
        [Script(NoDecoration = true)]
        internal static void PHPWebServiceProvider_Serve()
        {
            var i = new InternalGlobalImplementation();
            var g = (InternalGlobal)(object)i;
            var a = (__HttpApplication)(object)g;


            a.Request = (HttpRequest)(object)new __HttpRequest { };
            a.Response = (HttpResponse)(object)new __HttpResponse {  };


            //Native.API.ob_start();
            i.Application_BeginRequest(null, null);
            //Native.API.ob_end_flush();

            var x = g.ToCurrentFile();
            if (x != null)
            {
                // http://www.php.net/manual/en/function.fpassthru.php
                // http://www.php.net/mime-content-type

                Native.header("Content-Length: " + Native.API.filesize(x.Name));
                
                Native.SetContentType("application/octet-stream");

                var fp = Native.API.fopen(x.Name, "rb");
                Native.API.fpassthru(fp);
                Native.API.exit();
            }
        }
    }
}
