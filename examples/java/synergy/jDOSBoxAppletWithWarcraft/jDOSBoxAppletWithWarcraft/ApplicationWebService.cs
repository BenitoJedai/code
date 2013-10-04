using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.Linq;
using System.Xml.Linq;

namespace jDOSBoxAppletWithWarcraft
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed partial class ApplicationWebService
    {




        public void Handler(WebServiceHandler e)
        {
            Console.WriteLine(
                new { e.Context.Request.Path }
                );
            // http://isorecorder.alexfeinman.com/W7.htm
            // http://kbarr.net/bochs
            // http://www.imgburn.com/index.php?act=download
            if (e.Context.Request.Path == "/war1.img")
            {
                e.Context.Response.Redirect("/assets/jDOSBoxAppletWithWarcraft/war1.img");
                e.CompleteRequest();
                return;
            }
            
        }
    }
}
