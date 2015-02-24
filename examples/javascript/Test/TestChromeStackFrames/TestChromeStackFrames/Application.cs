using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestChromeStackFrames;
using TestChromeStackFrames.Design;
using TestChromeStackFrames.HTML.Pages;

namespace TestChromeStackFrames
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {

            Action<string> foo = data =>
           {
               var StackTrace = Environment.StackTrace;

               new IHTMLPre { StackTrace }.AttachToDocument();

               var StackTraceLines = StackTrace.Split(new[] { "\n" }, StringSplitOptions.None);

               for (int frameIndex = 1; frameIndex < StackTraceLines.Length; frameIndex++)
               {
                   //at _0gAABign_bj2W47U_adfGttA(https://192.168.43.252:13078/view-source:75912:5)
                   //at Aq_afTERoTzCRSXcBCi_akMQ.type$Aq_afTERoTzCRSXcBCi_akMQ.qgAABkRoTzCRSXcBCi_akMQ(https://192.168.43.252:13078/view-source:75171:32)

                   var StackTraceLine = StackTraceLines[frameIndex];

                   var ExternalTarget = StackTraceLine.TakeUntilOrEmpty(" (").SkipUntilOrEmpty("at ");

                   var locationURI_line_column = StackTraceLine.SkipUntilOrEmpty(" (").TakeUntilOrEmpty(")");


                   //                { { at_function = ggAABovarz_arNO9UfOoNzA , locationURI_line_column = https://192.168.43.252:2906/view-source:74473:5 }}
                   //                    at_function = DstVs7OHoT6VDScV62l1nQ.type$DstVs7OHoT6VDScV62l1nQ.fgAABrOHoT6VDScV62l1nQ , locationURI_line_column = https://192.168.43.252:2906/view-source:74413:30 }}


                   var locationURI = locationURI_line_column.TakeUntilLastOrEmpty(":").TakeUntilLastOrEmpty(":");

                   var location_line = int.Parse(locationURI_line_column.TakeUntilLastOrEmpty(":").SkipUntilLastOrEmpty(":"));
                   var location_column = locationURI_line_column.SkipUntilLastOrEmpty(":");

                   //var f = IFunction.ByName(ExternalTarget);


                   var displayName = ExternalTarget;

                   var f = IFunction.Of(Native.self, ExternalTarget);
                   if (f != null)
                   {
                       displayName = f.displayName;
                   }

                   // DstVs7OHoT6VDScV62l1nQ.TypeName = "___ctor_b__2_d__0";
                   // for types we should also start using displayName?
                   //   var type$DstVs7OHoT6VDScV62l1nQ = DstVs7OHoT6VDScV62l1nQ.prototype;

                   // TestChromeStackFrames.Application+<>c__DisplayClass1+<<_ctor>b__2>d__0.MoveNext
                   //type$DstVs7OHoT6VDScV62l1nQ.fgAABrOHoT6VDScV62l1nQ = function()

                   var type_constructor = ExternalTarget.TakeUntilOrNull(".");
                   var type_prototype = ExternalTarget.SkipUntilOrEmpty(".").TakeUntilOrEmpty(".");
                   var type_prototype_method = ExternalTarget.SkipUntilOrEmpty(".").SkipUntilOrEmpty(".");

                   if (type_constructor != null)
                   {
                       var __constructor = IFunction.Of(Native.self, type_constructor);
                       var __method = IFunction.Of(__constructor.prototype, type_prototype_method);

                       //__constructor.prototype[]

                       if (__method != null)
                       {
                           displayName = __method.displayName;
                       }
                   }

                   new IHTMLPre { displayName }.AttachToDocument().title = new
                   {
                       locationURI,
                       location_line,
                       location_column,
                       ExternalTarget,
                       type_constructor,
                       type_prototype,
                       type_prototype_method
                   }.ToString();


                   //var source = await new WebClient().DownloadStringTaskAsync(locationURI);

                   //var sourceLine = source.ToLines()[location_line - 1];

                   //new IHTMLPre { sourceLine }.AttachToDocument();
               }
           };

            new IHTMLButton { "inspect stack strace" }.AttachToDocument().onclick += delegate { foo("should see this on stack!"); };
        }

    }
}
