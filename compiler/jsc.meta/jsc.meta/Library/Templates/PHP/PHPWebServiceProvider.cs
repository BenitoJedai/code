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
using ScriptCoreLib.Ultra.Library.Delegates;

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

            //g.Application_BeginRequest(null, null);

            //foreach (var item in g.GetFiles())
            //{
            //    Console.WriteLine("<p>" +  item.Name + "</p>");
            //}

            //var ca = g.GetScriptApplications();
            //var c = ca[0];

            //StringAction Write =
            //    e =>
            //    {
            //        Native.echo(e);
            //    };

            //c.WriteTo(Write);

            Native.API.ob_start();
            i.Application_BeginRequest(null, null);
            Native.API.ob_end_flush();
        }
    }
}
