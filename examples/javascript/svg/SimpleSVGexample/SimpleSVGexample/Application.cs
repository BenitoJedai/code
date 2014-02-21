using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.Controls.Effects;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM.SVG;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.Shared;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Lambda;
using SimpleSVGexample.Design;
using SimpleSVGexample.HTML.Pages;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace SimpleSVGexample
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
        public Application(IDefault page)
        {

            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );

            // reference:
            // http://apike.ca/prog_svg_basic.html
            // http://apike.ca/prog_svg_shapes.html
            // http://www.milescript.org/graphicsdemo.html
            // http://canarlake.org/index.cgi?theme=svg
            // http://srufaculty.sru.edu/david.dailey/svg/SVGAnimations.htm
            // http://srufaculty.sru.edu/david.dailey/svg/svg_questions.htm
            // http://www.w3.org/TR/2000/CR-SVG-20001102/masking.html#ObjectAndGroupOpacityProperties
            // http://jmvidal.cse.sc.edu/talks/canvassvg/gradient.xml?style=White
            // http://www.treebuilder.de/default.asp?file=163540.xml
            // http://www.ibm.com/developerworks/library/x-svgint/
            // http://starkravingfinkle.org/projects/richdraw/richdraw_demo.htm
            // http://www.dynamicdrive.com/dynamicindex11/editor.htm
            // http://draw.labs.autodesk.com/ADDraw/draw.html
            // http://yeonisalive.net/javascript/MindWeb001.php
            "h2".AttachToDocument().innerText = "svg + vml example";

            if (!ISVGElementBase.Settings.IsSupported)
                Native.window.alert("svg not supported in this browser!");


            Test1();
            Test2();

        }



        private static void Test2()
        {
            var container = "div".AttachToDocument();

            container.style.border = "1px solid red";
            container.style.width = "400px";
            container.style.height = "300px";
            container.style.display = IStyle.DisplayEnum.inline_block;

            var svg = new ISVGSVGElement();
            var layer = new ISVGGElement();

            var defs = new ISVGElementBase("defs");
            var radialGradient = new ISVGElementBase("radialGradient");

            radialGradient.id = "myRadGrad";
            radialGradient.setAttribute("r", "10%");
            radialGradient.setAttribute("spreadMethod", "reflect");

            var stop1 = new ISVGElementBase("stop");

            stop1.setAttribute("offset", "5%");
            stop1.setAttribute("stop-color", "red");
            stop1.setAttribute("stop-opacity", "0.8");

            radialGradient.appendChild(stop1);

            var stop2 = new ISVGElementBase("stop");

            stop2.setAttribute("offset", "95%");
            //stop2.setAttribute("stop-color", "blue");
            stop2.setAttribute("stop-opacity", "0.0");

            radialGradient.appendChild(stop2);

            defs.appendChild(radialGradient);

            var rect = new ISVGRectElement();

            rect.setAttribute("x", 0);
            rect.setAttribute("y", 0);
            rect.setAttribute("width", "100%");
            rect.setAttribute("height", "100%");
            rect.setAttribute("fill", "url(#myRadGrad)");

            layer.appendChild(defs, rect);
            svg.appendChild(layer);
            container.appendChild(svg);
        }

        private static void Test1()
        {
            var container = "div".AttachToDocument();

            container.style.border = "1px solid red";
            container.style.width = "400px";
            container.style.height = "300px";

            var svg = new ISVGSVGElement();
            var layer = new ISVGGElement();
            var text = new ISVGTextElement();
            var rect = new ISVGRectElement();

            rect.setAttribute("x", 70);
            rect.setAttribute("y", 20);
            rect.setAttribute("rx", 0);
            rect.setAttribute("ry", 0);
            rect.setAttribute("width", 160);
            rect.setAttribute("height", 160);
            rect.setAttribute("fill", "blue");

            text.setAttribute("x", "92");
            text.setAttribute("y", "32");
            text.setAttribute("fill", "red");
            //text.style.cursor = IStyle.CursorEnum.move;

            var img = new ISVGImageElement();


            img.href = new SimpleSVGexample.HTML.Images.FromAssets.TILE1436().src;

            img.setAttribute("x", 0);
            img.setAttribute("y", 0);
            img.setAttribute("width", 200);
            img.setAttribute("height", 200);
            // http://www.svgbasics.com/rotate.html
            img.setAttribute("transform", "rotate(-45 10 10)");

            var r = 0;


            (1000 / 20).AtInterval(
                delegate
                {
                    r++;

                    img.setAttribute("transform", "rotate(" + r + " 100 100)");

                }
            );

            var rectlayer = new ISVGGElement();

            rectlayer.appendChild(rect, text);
            //rectlayer.setAttribute("transform", "scale(0.5) translate(0, 0) rotate(45) skewX(20) skewY(5)");

            var tween = new TweenDataDouble();

            tween.Value = 1;
            tween.ValueChanged +=
                delegate
                {
                    rectlayer.setAttribute("opacity", tween.Value);

                };

            rectlayer.onmouseover +=
                delegate
                {
                    tween.Value = 0.5;

                };

            rectlayer.onmouseout +=
                delegate
                {
                    tween.Value = 1;

                };


            text.textContent = "hello world";


            layer.appendChild(rectlayer, img);
            svg.appendChild(layer);

            container.appendChild(svg);
        }



    }
}
