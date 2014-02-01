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
using SVGCSSContent;
using SVGCSSContent.Design;
using SVGCSSContent.HTML.Pages;
using ScriptCoreLib.JavaScript.DOM.SVG;

namespace SVGCSSContent
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

            //page.body.css.after.contentImage = new HTML.Images.FromAssets.Anonymous_LogosSingle();

            var s = new ISVGSVGElement
            {

            };

            var f = new ISVGForeignObject().AttachTo(s);

            var div = new IHTMLDiv
            {
                innerHTML = "<em>I</em> like <span style='color:white; text-shadow:0 0 2px blue;'>cheese</span>"
            };

            div.style.fontSize = "40px";
            div.style.display = IStyle.DisplayEnum.inline_block;

            div.AttachToDocument();

            page.body.css.before.content = new { div.clientWidth, div.clientHeight }.ToString();

            new IHTMLButton { "do" }.AttachToDocument().WhenClicked(
                delegate
                {
                    s.setAttribute("width", div.clientWidth + 32);
                    s.setAttribute("height", div.clientHeight + 4);
                    div.AttachTo(f);

                    page.body.css.before.content = s.AsXElement().ToString();


              

                    // var data =
                    //"<svg xmlns='http://www.w3.org/2000/svg' width='200' height='200'>" +
                    //  "<foreignObject width='100%' height='100%'>" +
                    //    "<div xmlns='http://www.w3.org/1999/xhtml' style='font-size:40px'>" +
                    //      "<em>I</em> like <span style='color:white; text-shadow:0 0 2px blue;'>cheese</span>" +
                    //    "</div>" +
                    //  "</foreignObject>" +
                    //"</svg>";

                    var img = new IHTMLImage();
                    var url = "data:image/svg+xml;base64," + Convert.ToBase64String(Encoding.UTF8.GetBytes(s.AsXElement().ToString()));
                    img.src = url;

                    img.InvokeOnComplete(
                        delegate
                        {
                            page.body.css.after.contentImage = img;
                        }
                    );
                }
            );

        }

    }
}
