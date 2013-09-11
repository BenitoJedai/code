using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using DynamicStylePerspective.Design;
using DynamicStylePerspective.HTML.Pages;

namespace DynamicStylePerspective
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefault  page)
        {
            var container = new IHTMLDiv().AttachToDocument().With(
                e =>
                {

                    //var style = (XIStyle)(object)e.style;
                    var style = e.style;

                    style.height = "300px";
                    style.width = "600px";
                    style.margin = "5em";
                    style.border = "2px solid blue";
                    style.perspective = "500px";
                }
            );


            //new IHTMLDiv()
            new IHTMLIFrame()
                .AttachTo(container).With(
             parent =>
             {
                 parent.setAttribute("mozallowFullScreen", "");
                 parent.setAttribute("webkitAllowFullScreen", "");

                 parent.contentWindow.document.location.replace("/jsc");
                 //parent.contentWindow.document.location.replace("http://example.com");
                 //parent.contentWindow.document.location.replace("http://studio.jsc-solutions.net");
                 //parent.contentWindow.document.location.replace("http://192.168.1.100:29591/");

                 //var style = (XIStyle)(object)parent.style;
                 var style = parent.style;

                 style.height = "300px";
                 style.width = "600px";

                 //style.margin = "10px";

                 style.border = "2px solid red";
                 style.transformStyle = "preserve-3d";

                 parent.onload +=
                         delegate
                         {

                             var HasMouse = false;

                             //parent.onmouseover +=
                             //    delegate
                             //    {
                             //        HasMouse = true;

                             //    };

                             //parent.onmouseout +=
                             //    delegate
                             //    {
                             //        HasMouse = false;
                             //    };

                             var y = 0;


                             Native.window.onframe += delegate
                             {

                                 if (!HasMouse)
                                 {
                                     y++;

                                     page.Header.innerText = "y: " + y;

                                     if (y == 180)
                                         y = 0;

                                     //var yy = y % 180;
                                     var yy = y;



                                     // WebView on Android freezes after 90

                                     var _y = yy - 90;

                                     //if (_y > 60)
                                     //    _y = 60;

                                     page.Header.innerText = "rotateY(" + _y + "deg), y: " + y + ", yy: " + yy;

                                     style.transform = "rotateY(" + _y + "deg)";
                                 }


                             };

                         };

             }
         );

            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }

}
