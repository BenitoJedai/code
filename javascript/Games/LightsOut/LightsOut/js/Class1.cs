//using System.Linq;

using ScriptCoreLib;
using ScriptCoreLib.Shared;

using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Lambda;

using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM.XML;

//using global::System.Collections.Generic;

using System.Linq;

namespace LightsOut.js
{


    [Script]
    public class Class1
    {
        public const string Alias = "Class1";
        public const string DefaultData = "Class1Data";

        /// <summary>
        /// Creates a new control
        /// </summary>
        /// <param name="DataElement">The hidden data element</param>
        public Class1(IHTMLElement DataElement)
        {

            // based on http://www.cjcraft.com/Blog/PermaLink,guid,5c35b1f1-dc66-4d85-ac04-22fc97503d4a.aspx

            // what happens in beta2 when the anonymous types are immutable? :)

            var usersettings = new { x = 5, y = 5 };
            var search = Native.Document.location.search;

            if (search.StartsWith("?"))
            {
                var values = from i in search.Substring(1).Split('&')
                             let kvp = i.Split('=')
                             where kvp.Length == 2
                             select new { key = kvp[0], value = kvp[1] };

                var _x = values.FirstOrDefault(i => i.key == "x");
                if (_x != null) usersettings.x = int.Parse(_x.value);

                var _y = values.FirstOrDefault(i => i.key == "y");
                if (_y != null) usersettings.y = int.Parse(_y.value);
            }

            var a = new Array2D<IHTMLDiv>(usersettings.x, usersettings.y);
            var m = a.ToBooleanArray();



            var r = new System.Random();

            m.ForEach(
                (x, y) =>
                {
                    m[x, y] = r.NextDouble() > 0.5;
                }
            );


            var canvas = new IHTMLDiv();

            canvas.style.position = IStyle.PositionEnum.relative;
            canvas.style.border = "2px solid black";
            canvas.style.width = (a.XLength * 32) + "px";
            canvas.style.height = (a.YLength * 32) + "px";

            var settings = new
                           {
                               on = Color.Blue,
                               off = Color.None
                           };

            Action<int, int> UpdateColor =
                (x, y) =>
                {
                    var n = a[x, y];

                    if (m[x, y])
                    {
                        n.style.backgroundColor = settings.on;
                    }
                    else
                    {
                        n.style.backgroundColor = settings.off;
                    }

                };

            Action<int, int> ToggleDirect =
                (x, y) =>
                {
                    var n = a[x, y];

                    if (n == null)
                        return;

                    m[x, y] = !m[x, y];
                    UpdateColor(x, y);
                };

            Action<int, int> Toggle =
                (x, y) =>
                {
                    Console.WriteLine("click at: " + new { x, y } + " = " + m[x, y]);

                    var f = ToggleDirect.WithOffset(x, y);

                    f(-1, 0);
                    f(0, -1);
                    f(0, 0);
                    f(0, 1);
                    f(1, 0);
                };


            var info_stats_clicks = new IHTMLDiv();
            var info_stats_clicks_count = 0;
            var info_stats_off = new IHTMLDiv();
            var info_stats_on = new IHTMLDiv();

            Action info_stats_update =
                () =>
                {
                    info_stats_clicks.innerHTML = info_stats_clicks_count + " clicks made so far";
                    info_stats_on.innerHTML = m.Count(i => i) + " blocks are blue";
                    info_stats_off.innerHTML = m.Count(i => !i) + " blocks are white";
                    
                };

            var info_stats = new IHTMLDiv(info_stats_clicks, info_stats_off, info_stats_on);

            info_stats.style.Float = IStyle.FloatEnum.right;


            a.ForEach(
                (x, y) =>
                {
                    var n = new IHTMLDiv();

                    n.style.left = (x * 32 + 8) + "px";
                    n.style.top = (y * 32 + 8) + "px";
                    n.style.width = "16px";
                    n.style.height = "16px";
                    n.style.position = IStyle.PositionEnum.absolute;
                    n.style.overflow = IStyle.OverflowEnum.hidden;

                    n.style.border = "1px solid black";
                    n.style.cursor = IStyle.CursorEnum.pointer;

                    canvas.appendChild(n);


                    n.onclick += delegate
                    {
                        info_stats_clicks_count++;

                        Toggle(x, y);


                        info_stats_update();
                    };


                    a[x, y] = n;

                    UpdateColor(x, y);

                }
            );

            var info = new IHTMLDiv();
            var info_header_text = "Lights out";

            Native.Document.title = info_header_text;

            info.appendChild(new IHTMLElement(IHTMLElement.HTMLElementEnum.h1, info_header_text));
            info.appendChild(new IHTMLAnchor("http://www.cjcraft.com/Blog/PermaLink,guid,5c35b1f1-dc66-4d85-ac04-22fc97503d4a.aspx", "based on SilverlightsOut"));

            info.appendChild(new IHTMLElement(IHTMLElement.HTMLElementEnum.p,
                @"Lights out is a one player puzzle that is played on a 5 by 5 grid of squares in which every square has two states: on and off. The game starts off with all squares off, where the goal is to turn on every square. By selecting a square, all the surrounding squares' (up, down, left, right) state is turned toggled. For example, on a 3 by 3 grid of squares with all squares off, if the center one is selected, it will turn 'on' the 4 up, down, left, right squares from it."));

            info.appendChild(info_stats);
            info.appendChild(canvas);

            info_stats_update();

            DataElement.insertNextSibling(info);
        }




        static Class1()
        {
            //Console.EnableActiveXConsole();

            // spawn this class when document is loaded 
            Native.Spawn(
                new Pair<string, EventHandler<IHTMLElement>>(Alias, e => new Class1(e))
                );

        }


    }

}
