using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.Linq;
using System.Threading;
using System.Xml.Linq;

namespace TestMultipartHandler
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
    {
        public /* will not be part of web service itself */ void Handler(WebServiceHandler h)
        {
            Console.WriteLine(h.Context.Request.Path);

            // ERR_UNSAFE_PORT

            // http://andy.wordpress.com/2007/10/01/proposal-multipart-web-requests/
            // http://blog.dubbelboer.com/2012/01/08/x-mixed-replace.html
            // http://stackoverflow.com/questions/1806228/browser-support-of-multipart-responses
            // http://www.howtocreate.co.uk/php/serverpushdemo.php
            // http://minipenguin.com/?tag=real-time

            if (h.IsDefaultPath)
            {
                var HostUri = new
                {
                    Host = h.Context.Request.Headers["Host"].TakeUntilIfAny(":"),
                    Port = h.Context.Request.Headers["Host"].SkipUntilIfAny(":")
                };

                // This operation requires IIS integrated pipeline mode.
                // http://stackoverflow.com/questions/5010201/resolving-this-operation-requires-iis-integration-pipeline-mode-in-asp-net-mvc
                // http://css-tricks.com/using-css-without-html/
                //h.Context.Response.AddHeader("Link", "<Default.css>;rel=stylesheet");

                // webview will not like this
                h.Context.Response.ContentType = "multipart/x-mixed-replace; boundary=endofsection";

                Action<string> WriteContent =
                    c =>
                    {
                        h.Context.Response.Write(
                            @"Content-type: text/plain

" + c + @"
--endofsection");

                    };

                //                Implementation not found for type import :
                //type: System.String
                //method: System.String PadRight(Int32)

                //WriteContent("After 4 seconds this will go away and a cat will appear...".PadRight(512));
                WriteContent("After 4 seconds this will go away and a cat will appear...");
                h.Context.Response.Flush();


                Thread.Sleep(1800);



                h.Context.Response.Write(
                     @"--endofsection
Content-type: text/html
Link: <assets/TestMultipartHandler/Default.css>;rel=stylesheet

<body>
<h1>Cat</h1>

</body>
--endofsection
");


                h.CompleteRequest();


            }
        }
    }
}
