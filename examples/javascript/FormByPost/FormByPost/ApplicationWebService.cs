using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Linq;
using System.Xml.Linq;
using ScriptCoreLib.Ultra.WebService;
using System.Diagnostics;
using System.Web;

namespace FormByPost
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
        }

        //        A first chance exception of type 'System.Threading.ThreadAbortException' occurred in mscorlib.dll
        //An exception of type 'System.Threading.ThreadAbortException' occurred in mscorlib.dll but was not handled in user code
        //The thread 'BridgeStreamTo' (0xd0c) has exited with code 0 (0x0).
        // X:\jsc.internal.svn\compiler\jsc.meta\jsc.meta\Commands\Rewrite\RewriteToJavaScriptDocument.WebService.cs
        // X:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Ultra\WebService\InternalGlobalExtensions.cs

        public void InternalHandler(WebServiceHandler h)
        {
            if (h.IsDefaultPath)
                return;

            Console.WriteLine("InternalHandler: " + h.Context.Request.Path);

            if (h.Context.Request.Path == "/Action7")
            {

                var c = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;

                var TextContent = h.Context.Request.Form["TextContent"];

                Console.WriteLine(TextContent);



                foreach (HttpPostedFile item in h.Context.Request.Files.AllKeys.Select(k => h.Context.Request.Files[k]))
                {
                    Console.WriteLine(
                        new { item.FileName, item.ContentLength, item.ContentType }
                    );
                }


                Console.ForegroundColor = c;

                // close
                h.Context.Response.StatusCode = 204;
                h.CompleteRequest();
                return;
            }
        }
    }
}
