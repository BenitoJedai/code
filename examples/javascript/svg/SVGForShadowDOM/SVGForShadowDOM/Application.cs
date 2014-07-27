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
using SVGForShadowDOM;
using SVGForShadowDOM.Design;
using SVGForShadowDOM.HTML.Pages;
using ScriptCoreLib.JavaScript.DOM.SVG;

namespace SVGForShadowDOM
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
            // ! to convert svg to image we would need to normalize source, which defeats shadowdom purpose

            // X:\jsc.svn\examples\javascript\svg\SVGCSSContent\SVGCSSContent\Application.cs

            var s = new ISVGSVGElement
            {

            };

            var f = new ISVGForeignObject().AttachTo(s);
            //requiredFeatures="http://www.w3.org/TR/SVG11/feature#Extensibility">

            //f.setAttribute("requiredFeatures", "http://www.w3.org/TR/SVG11/feature#Extensibility");

            // http://starkravingfinkle.org/blog/2007/07/firefox-3-svg-foreignobject/
            // http://stackoverflow.com/questions/11194403/svg-foreignobject-not-showing-in-chrome
            var div = new IHTMLDiv
            {
            };

            new IHTMLContent { select = "div" }.AttachTo(div);

            //var divbody = new IHTMLDiv
            //{
            //    innerHTML = "I like <span style='color:white; text-shadow:0 2px 2px blue;'>cheese</span>"
            //}.AttachTo(div);
            // https://groups.google.com/forum/#!topic/svg-edit/60HICxGWFNE
            // http://www.w3.org/TR/SVG11/extend.html#ForeignObjectElement

            // http://css.dzone.com/articles/securing-pixel-data-svg-and
            // view-source:http://starkravingfinkle.org/blog/wp-content/uploads/2007/07/foreignobject-transform.svg
            // http://stackoverflow.com/questions/6849192/what-can-be-rendered-in-foreignobject-element-when-svg-is-embedded-in-html5
            //new StockToolboxImageForWebGLComponent().AttachTo(divbody);

            //new Anonymous_LogosSingle().AttachTo(divbody);

            // for detatched calculations!
            //div.style.fontFamily = IStyle.FontFamilyEnum.Verdana;
            //div.style.fontSize = "40px";
            div.style.display = IStyle.DisplayEnum.inline_block;

            // div.AttachToDocument();

            s.width = 400;
            s.height = 100;

            //s.width = div.clientWidth;
            //s.height = div.clientHeight;
            //s.height = div.clientHeight;

            div.AttachTo(f);


            //s.AttachToDocument();
            s.AttachTo(Native.body.shadow);

            // and then show any canvases
            //new IHTMLContent { select = "canvas" }.AttachTo(div);

            
            // https://developer.mozilla.org/en-US/docs/Web/HTML/Canvas/Drawing_DOM_objects_into_a_canvas
            //var c = new CanvasRenderingContext2D(400, 100);

            //c.drawImage(
        }

    }
}
