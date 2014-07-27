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
using SVGCSSContent.HTML.Images.FromAssets;

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
            //requiredFeatures="http://www.w3.org/TR/SVG11/feature#Extensibility">

            //f.setAttribute("requiredFeatures", "http://www.w3.org/TR/SVG11/feature#Extensibility");

            // http://starkravingfinkle.org/blog/2007/07/firefox-3-svg-foreignobject/
            // http://stackoverflow.com/questions/11194403/svg-foreignobject-not-showing-in-chrome
            var div = new IHTMLDiv
            {
            };

            var divbody = new IHTMLDiv
            {
                innerHTML = "I like <span style='color:white; text-shadow:0 2px 2px blue;'>cheese</span>"
            }.AttachTo(div);
            // https://groups.google.com/forum/#!topic/svg-edit/60HICxGWFNE
            // http://www.w3.org/TR/SVG11/extend.html#ForeignObjectElement

            // http://css.dzone.com/articles/securing-pixel-data-svg-and
            // view-source:http://starkravingfinkle.org/blog/wp-content/uploads/2007/07/foreignobject-transform.svg
            // http://stackoverflow.com/questions/6849192/what-can-be-rendered-in-foreignobject-element-when-svg-is-embedded-in-html5
            new StockToolboxImageForWebGLComponent().AttachTo(divbody);

            new Anonymous_LogosSingle().AttachTo(divbody);

            div.style.fontFamily = IStyle.FontFamilyEnum.Verdana;
            div.style.fontSize = "40px";
            div.style.display = IStyle.DisplayEnum.inline_block;

            div.AttachToDocument();

            //page.body.css.before.content = new { div.clientWidth, div.clientHeight }.ToString();



            #region do
            new IHTMLButton { "do" }.AttachToDocument().WhenClicked(
                button =>
                {
                    //div.querySelectorAll("img").WithEach(
                    div.ImageElements().WithEach(
                        q =>
                        {
                            q.src = q.toDataURL();

                        }
                    );

                    button.Orphanize();

                    //s.width
                    //s.setAttribute("width", div.clientWidth + 0);
                    //s.setAttribute("height", div.clientHeight + 0);

                    s.width = div.clientWidth;
                    s.height = div.clientHeight;

                    div.AttachTo(f);


                    //page.body.css.before.content = xmlstring;




                    // var data =
                    //"<svg xmlns='http://www.w3.org/2000/svg' width='200' height='200'>" +
                    //  "<foreignObject width='100%' height='100%'>" +
                    //    "<div xmlns='http://www.w3.org/1999/xhtml' style='font-size:40px'>" +
                    //      "<em>I</em> like <span style='color:white; text-shadow:0 0 2px blue;'>cheese</span>" +
                    //    "</div>" +
                    //  "</foreignObject>" +
                    //"</svg>";

                    //                    var ss = @"<svg xmlns='http://www.w3.org/2000/svg' width='600' height='200'>
                    //<foreignObject width='100%' height='100%'>
                    //<HTML xmlns='http://www.w3.org/1999/xhtml'>
                    //<BODY style=' font-size: 30px;'>
                    //This is <span style='color: green'>HTML</span>.
                    //An img: <img src='data:image/png;base64,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'/>
                    //
                    //
                    //</BODY>
                    //</HTML>
                    //</foreignObject></svg>";



                    //var img = new IHTMLImage();
                    ////var url = "data:image/svg+xml;base64," + Convert.ToBase64String(Encoding.UTF8.GetBytes(s.AsXElement().ToString()));
                    //var url = "data:image/svg+xml;base64," + Convert.ToBase64String(Encoding.UTF8.GetBytes(xmlstring));
                    ////var url = "data:image/svg+xml;base64," + Convert.ToBase64String(Encoding.UTF8.GetBytes(ss));
                    //img.src = url;

                    //img.InvokeOnComplete(
                    //    delegate
                    //    {
                    //        page.body.css.after.contentImage = img;
                    //        page.body.css.after.style.border = "1px solid red";

                    //    }
                    //);

                    page.body.css.after.contentImage = s;
                    //page.body.css.after.style.border = "1px solid red";

                }
            );
            #endregion


        }

    }
}
