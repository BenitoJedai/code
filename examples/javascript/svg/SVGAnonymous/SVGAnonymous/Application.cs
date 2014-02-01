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
using SVGAnonymous;
using SVGAnonymous.Design;
using SVGAnonymous.HTML.Pages;
using ScriptCoreLib.JavaScript.DOM.SVG;

namespace SVGAnonymous
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
            // http://edutechwiki.unige.ch/en/Using_SVG_with_HTML5_tutorial


            page.div.onclick +=
                delegate
                {
                    // https://developer.mozilla.org/en/docs/HTML/Canvas/Drawing_DOM_objects_into_a_canvas


                    // Uncaught TypeError: Cannot read property 'style' of null 
                    page.div.style.width = "96px";
                    page.div.style.height = "96px";

                    var svg = (ISVGSVGElement)page.div;

                    svg.AttachToDocument();

                    ////var xml = page.div.AsXElement().ToString();

                    ////page.div.Orphanize();

                    //////Console.WriteLine(new { xml });


                    ////var svg = new ISVGSVGElement().AttachToDocument();

                    ////svg.setAttribute("width", "96");
                    ////svg.setAttribute("height", "96"); ;

                    ////var f = new ISVGElementBase("foreignObject").AttachTo(svg);

                    ////f.innerHTML = xml;
                    //page.div.AttachTo(f);

                    svg.onclick +=
                        delegate
                        {
                            IHTMLImage i = svg;

                            i.AttachToDocument();

                        };

                    //    async delegate
                    //    {
                    //        // http://people.mozilla.org/~roc/rendering-HTML-elements-to-canvas.html
                    //        // http://robert.ocallahan.org/2011/11/drawing-dom-content-to-canvas.html
                    //        // this does not seem to work??

                    //        var svgxml = svg.AsXElement().ToString()
                    //            // fast fix
                    //            .Replace("svg\">", "svg\" />")
                    //            .Replace("<div id", "<div xmlns='http://www.w3.org/1999/xhtml' id");

                    //        Console.WriteLine(new { svgxml });

                    //        var blob = new Blob(
                    //            new[] { svgxml },
                    //            new { type = "image/svg+xml;charset=utf-8" }
                    //           );

                    //        //error on line 57 at column 9: Opening and ending tag mismatch: img line 0 and div

                    //        var url = blob.ToObjectURL();
                    //        //var url = "data:image/svg+xml;base64," + Convert.ToBase64String(Encoding.UTF8.GetBytes(svgxml));

                    //        new IHTMLPre { new { url } }.AttachToDocument();

                    //        var img = new IHTMLImage { src = url }.AttachToDocument();

                    //        await img;


                    //        //var c = new CanvasRenderingContext2D(96, 96);

                    //        //c.drawImage(img, 0, 0, 96, 96);

                    //        //c.canvas.AttachToDocument();

                    //        //URL.revokeObjectURL(url);
                    //    };
                };
        }

    }
}
