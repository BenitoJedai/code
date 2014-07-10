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
using CSSShaderCrumple;
using CSSShaderCrumple.Design;
using CSSShaderCrumple.HTML.Pages;

namespace ScriptCoreLib.JavaScript.Extensions
{
    public static class CSSShaderCrumpleExtensions
    {
        public static void MakeCSSShaderCrumple(this IHTMLElement e)
        {
            // http://html.adobe.com/webplatform/graphics/customfilters/browser-support/
            // http://blogs.adobe.com/webplatform/2012/08/31/css-shaders-now-in-css-filter-effects-specification/
            // http://html.adobe.com/webplatform/graphics/customfilters/
            // https://lists.webkit.org/pipermail/webkit-dev/2014-January/026098.html
            // removal of the CSS Custom Filters code from WebKit.

            var crumple = new
            {
                vs = new CSSShaderCrumple.Shaders.crumpleVertexShader(),
                fs = new CSSShaderCrumple.Shaders.crumpleFragmentShader()
            };

            //var c = "styleclass" + new Random().Next();

            //e.className += " " + c;

            //(IStyleSheet.Default["." + c].style as dynamic).webkitTransition = "-webkit-filter linear 1.5s";


            e.css.style.transition = "-webkit-filter linear 1.5s";

            //(IStyleSheet.Default["." + c + ":hover"].style as dynamic)
            (e.css.hover.style as dynamic)
                .webkitFilter =
                    "custom(url(" + crumple.vs.ToDataUrl() + ") mix(url(" + crumple.fs.ToDataUrl() + ") multiply source-atop), 50 50, amount 0, strength 0.2, lightIntensity 1.05, transform rotateX(0deg) translateZ(0px) )";

            //(IStyleSheet.Default["." + c].style as dynamic).webkitFilter =
            e.css.style.setProperty(
                "-webkit-filter",
                 "custom(url(" + crumple.vs.ToDataUrl() + ") mix(url(" + crumple.fs.ToDataUrl() + ") multiply source-atop), 50 50, amount 1, strength 0.2, lightIntensity 1.05, transform rotateX(0deg) translateZ(0px) )",
                 ""
                );


        }
    }
}

namespace CSSShaderCrumple
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
        public Application(IApp page)
        {
            // http://alteredqualia.com/css-shaders/crumple.html

            page.shader.MakeCSSShaderCrumple();

        }

    }
}
