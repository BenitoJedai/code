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
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using SVGHTMLElement;
using SVGHTMLElement.Design;
using SVGHTMLElement.HTML.Pages;
using ScriptCoreLib.JavaScript.DOM.SVG;

namespace SVGHTMLElement
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
            //ms InternalFieldsFromTypeInitializer view-source:35337
            //13ms GetInternalFields load fromlocalstorage!  view-source:35337
            //22ms IHTMLImage <- IHTMLDiv view-source:35337
            //23ms Task<ISVGSVGElement> <- IHTMLDiv view-source:35337
            //31ms ContinueWhenAll { Length = 1, scheduler = [object Object], ManagedThreadId = 1 } view-source:35337
            //32ms IHTMLImage <- Task<ISVGSVGElement> view-source:35337
            //32ms enter contentImage view-source:35337
            //Application Cache Progress event (2 of 3) http://192.168.1.200:16552/view-source 192.168.1.200/:1
            //Application Cache Progress event (3 of 3)  192.168.1.200/:1
            //Application Cache Cached event 192.168.1.200/:1
            //133ms ContinueWhenAll_yield { scheduler = [object Object] } view-source:35337
            //134ms { clientWidth = 679, clientHeight = 249 } view-source:35337
            //170ms yield contentImage view-source:35337
            //61247ms enter contentImage view-source:35337
            //61248ms yield contentImage view-source:35337

            page.body.css.before.contentImage = new Foo().AsNode();
            page.body.css.before.style.position = IStyle.PositionEnum.absolute;
            page.body.css.before.style.bottom = "0";

            var s = new ISVGSVGElement
            {

            };

            var f = new ISVGForeignObject().AttachTo(s);

            var fdiv = new IHTMLDiv().AttachTo(f);

            fdiv.style.fontFamily = IStyle.FontFamilyEnum.Verdana;

            // we need to serialize styles now
            // svg wont have any default html css styles at all
            fdiv.style.fontSize = "12px";

            var div = new Foo();

            var hidden = new IHTMLDiv { }.AttachTo(Native.document.documentElement);
            hidden.style.position = IStyle.PositionEnum.@fixed;
            hidden.style.visibility = IStyle.VisibilityEnum.hidden;
            //hidden.style.display = IStyle.DisplayEnum.none;

            div.PageContainer.style.display = IStyle.DisplayEnum.inline_block;
            div.AttachTo(hidden);

            new IHTMLButton { "do " + new { div.PageContainer.clientWidth, div.PageContainer.clientHeight } }.AttachToDocument().WhenClicked(
               async button =>
               {

                   //div.querySelectorAll("img").WithEach(
                   div.ImageElements().WithEach(
                       q =>
                       {
                           q.src = q.toDataURL();

                       }
                   );

                   button.Orphanize();
                   s.setAttribute("width", div.PageContainer.clientWidth + 0);
                   s.setAttribute("height", div.PageContainer.clientHeight + 0);
                   div.AttachTo(fdiv);

                   // Uncaught SecurityError: Failed to execute 'toDataURL' on 'HTMLCanvasElement': Tainted canvases may not be exported.
                   IHTMLImage i = s;
                   //Task<IHTMLCanvas> ic = s;

                   //var c = await ic;


                   //var ii = i.toDataURL();

                   page.body.css.after.contentImage = i;
                   page.body.css.after.style.position = IStyle.PositionEnum.absolute;
                   page.body.css.after.style.right = "0";

                   Console.WriteLine("cursor");
                   // cursor no longer appears?
                   Native.document.documentElement.style.cursorImage = i;
                   //Native.css.style.cursorImage = s;
                   Console.WriteLine("icon");
                   Native.document.icon = i;
               }
           );

        }

    }
}
