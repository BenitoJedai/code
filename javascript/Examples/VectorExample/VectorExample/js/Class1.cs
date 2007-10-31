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
    using ScriptCoreLib.JavaScript.DOM.VML;








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
        // http://starkravingfinkle.org/projects/richdraw/richdraw_demo.htm
        // http://www.dynamicdrive.com/dynamicindex11/editor.htm
        // http://draw.labs.autodesk.com/ADDraw/draw.html
        // http://yeonisalive.net/javascript/MindWeb001.php

        /// <summary>
        /// Creates a new control
        /// </summary>
        /// <param name="DataElement">The hidden data element</param>
        public Class1(IHTMLElement DataElement)
        {
            Native.Document.body.style.backgroundColor = Color.System.ThreeDFace;

            "h2".AttachToDocument().innerText = "svg + vml example";

            #region CreateButton
            Func<Action, string, IHTMLButton> CreateButton =
                (h, text) =>
                {
                    var btn = new IHTMLButton(text);

                    btn.attachToDocument();

                    Action onclick =
                        delegate
                        {
                            h();
                            btn.Dispose();
                        };

                    btn.onclick +=
                        delegate
                        {
                            // onclick();

                            try
                            {
                                onclick();
                            }
                            catch (Exception ex)
                            {
                                btn.style.color = Color.Red;
                                btn.innerText = "error: " + ex.Message;
                            }
                        };

                    return btn;
                };
            #endregion


            CreateButton(Test1, "svg hello world").disabled = !ISVGElementBase.Settings.IsSupported;
            CreateButton(Test2, "svg advanced").disabled = !ISVGElementBase.Settings.IsSupported;
            CreateButton(Test3, "vml (IE) hello world").disabled = !IVMLElementBase.Settings.IsSupported;
        }


        private static void Test3()
        {
            //var settings = VMLSettings.Default;

            var container = "div".AttachToDocument();

            container.style.border = "1px solid red";
            container.style.width = "400px";
            container.style.height = "300px";
            container.style.overflow = IStyle.OverflowEnum.hidden;

            container.style.position = IStyle.PositionEnum.relative;

            var layer = new IVMLGroup().AttachTo(container);

            layer.setAttribute("coordsize", "400,300");

            layer.style.width = "400px";
            layer.style.height = "300px";

            var rect = new IHTMLElement("v:rect").AttachTo(layer);

            // http://midiwebconcept.free.fr/

            rect.setAttribute("fillcolor", "red");
            rect.style.left = "10";
            rect.style.top = "10";
            rect.style.width = "200px";
            rect.style.height = "200px";

            var fill = new IHTMLElement("v:fill").AttachTo(rect);

            fill.setAttribute("color2", "blue");
            fill.setAttribute("type", "gradient");

            {
                var image = new IVMLImage()
                    {
                        src = "assets/VectorExample/TILE1436.png"
                    }.AttachTo(layer);

                image.style.width = "200px";
                image.style.height = "200px";
            }

            container.onmousemove +=
                ev =>
                {
                    if (ev.ctrlKey)
                    {
                        if (ev.Element == container)
                        {
                            var image = new IVMLImage()
                                {
                                    src = "assets/VectorExample/TILE1436.png"
                                }.AttachTo(layer);

                            image.style.left = ev.OffsetX + "px";
                            image.style.top = ev.OffsetY + "px";
                            image.style.width = "200px";
                            image.style.height = "200px";
                        }
                    }
                };


            var polyline = new IVMLPolyline();

            polyline.points = "10,8 100,100 55,77";

            polyline.AttachTo(layer);




        }

        private static void Test2()
        {
            var container = "div".AttachToDocument();

            container.style.border = "1px solid red";
            container.style.width = "400px";
            container.style.height = "300px";

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
