//using System.Linq;

using ScriptCoreLib;


using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Controls.Effects;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM.XML;

using global::System.Collections.Generic;
using global::System.Linq;
using global::ScriptCoreLib.Shared.Lambda;
using System;


namespace VectorExample.js
{
    using ScriptCoreLib.Shared;
    using ScriptCoreLib.Shared.Lambda;
    using ScriptCoreLib.JavaScript.DOM.SVG;
    using ScriptCoreLib.Shared.Drawing;




    [Script]
    public class Class1
    {
        public const string Alias = "Class1";
        public const string DefaultData = "Class1Data";


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

        /// <summary>
        /// Creates a new control
        /// </summary>
        /// <param name="DataElement">The hidden data element</param>
        public Class1(IHTMLElement DataElement)
        {
            Native.Document.body.style.backgroundColor = Color.System.ThreeDFace;

            "h2".AttachToDocument().innerText = "svg + vml example";
            "p".AttachToDocument().innerText = "done loading!";

            #region CreateButton
            Action<Action, string> CreateButton =
                (h, text) =>
                {
                    var btn = new IHTMLButton(text);

                    btn.attachToDocument();

                    btn.onclick +=
                        delegate
                        {
                            try
                            {
                                h();
                                btn.Dispose();
                            }
                            catch (Exception ex)
                            {
                                btn.style.color = Color.Red;
                                btn.innerText = "error: " + ex.Message;
                            }
                        };
                };
            #endregion

            // CreateButton = CreateButton.AsDefaultDelegate();

            CreateButton(Test1, "svg hello world");
            CreateButton(Test2, "svg advanced");
        }

        private static void Test2()
        {
            var container = "div".AttachToDocument();

            container.style.border = "1px solid red";
            container.style.width = "400px";
            container.style.height = "300px";

            var svg = new ISVGSVGElement();
            var layer = new ISVGGElement();

            var defs = new ISVGElement("defs");
            var radialGradient = new ISVGElement("radialGradient");

            radialGradient.id = "myRadGrad";
            radialGradient.setAttribute("r", "10%");
            radialGradient.setAttribute("spreadMethod", "reflect");

            var stop1 = new ISVGElement("stop");

            stop1.setAttribute("offset", "5%");
            stop1.setAttribute("stop-color", "red");
            stop1.setAttribute("stop-opacity", "0.8");

            radialGradient.appendChild(stop1);

            var stop2 = new ISVGElement("stop");

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

            img.href = "assets/VectorExample/TILE1436.png";

            img.setAttribute("x", 32);
            img.setAttribute("y", 32);
            img.setAttribute("width", 200);
            img.setAttribute("height", 200);


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




        static Class1()
        {
            //Console.EnableActiveXConsole();

            // spawn this class when document is loaded 
            Native.Spawn(
                new Pair<string, ScriptCoreLib.Shared.EventHandler<IHTMLElement>>(Alias, e => new Class1(e))
                );

        }


    }

}
