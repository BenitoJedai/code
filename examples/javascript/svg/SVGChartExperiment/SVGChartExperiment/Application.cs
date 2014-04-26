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
using SVGChartExperiment;
using SVGChartExperiment.Design;
using SVGChartExperiment.HTML.Pages;
using ScriptCoreLib.JavaScript.DOM.SVG;

namespace SVGChartExperiment
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
            // http://www.w3schools.com/svg/svg_polygon.asp
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201404/20140426


            //<svg height="210" width="500">
            //      <polygon points="200,10 250,190 160,210" style="fill:lime;stroke:purple;stroke-width:1" />
            //  </svg>


            //{
            //    var svg = new ISVGSVGElement { width = 200, height = 200 };

            //    var polygon = new ISVGElementBase("polygon")
            //    {

            //    }.AttachTo(svg);

            //    polygon.setAttribute("points", "200,10 250,190 160,210");
            //    polygon.setAttribute("style", "fill:lime;stroke:purple;stroke-width:1;");


            //    svg.AttachToDocument();
            //}



            {
                var svg = new ISVGSVGElement
                {
                    width = 200,
                    height = 200
                };
                // width: 780px;

                //svg.setAttribute("width", "200");

                var w = new StringBuilder();

                Action<double, double> add = (x, y) =>
                {
                    w.Append((x * 200.0) + "," + (y * 200.0) + " ");

                };

                add(1, 1);
                add(0, 1);

                add(0, 0.8);
                add(0.2, 1.0);
                add(0.4, 0.8);
                add(0.6, 0.6);
                add(0.8, 0.8);
                add(1.0, 0.0);

                var polygon = new ISVGPolygonElement
                {
                    //points = "200,10 250,190 160,210"
                    points = w.ToString()

                }.AttachTo(svg);

                polygon.setAttribute("style", "fill:lime;stroke:purple;stroke-width:1;");


                svg.AttachToDocument();

                // { polygon_style = fill:lime;stroke:purple;stroke-width:1; }
                var polygon_style = polygon.getAttribute("style");

                new IHTMLPre { 
                    new {polygon_style}
                }.AttachToDocument();
            }

        }

    }
}
