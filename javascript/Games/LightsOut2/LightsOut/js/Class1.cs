//using System.Linq;

using ScriptCoreLib;
using ScriptCoreLib.Shared;

using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Lambda;

using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM.XML;
using ScriptCoreLib.JavaScript.Controls.Effects;
//using global::System.Collections.Generic;

using System.Linq;


namespace LightsOut.js
{
    using web.assets;
    using System;

    [Script]
    class __Type1
    {
        public int x;
        public int y;

        public __Type2 tile;
    }

    [Script]
    class __Type2
    {
        public int w;
        public int h;
        public double cold;
    }

    [Script, ScriptApplicationEntryPoint]
    public class Class1
    {

        /// <summary>
        /// Creates a new control
        /// </summary>
        /// <param name="DataElement">The hidden data element</param>
        public Class1(IHTMLElement DataElement)
        {

            // based on http://www.cjcraft.com/Blog/PermaLink,guid,5c35b1f1-dc66-4d85-ac04-22fc97503d4a.aspx

            // what happens in beta2 when the anonymous types are immutable? :)


            var usersettings = new __Type1 { x = 5, y = 5, tile = new __Type2 { w = 64, h = 64, cold = 0.8 } };
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

            canvas.className = "canvas";

            var canvas_size = new __Type1 { x = ((a.XLength + 1) * usersettings.tile.w), y = ((a.YLength + 1) * usersettings.tile.h) };

            canvas.style.position = IStyle.PositionEnum.relative;
            canvas.style.border = "2px solid black";
            canvas.style.width = canvas_size.x + "px";
            canvas.style.height = canvas_size.y + "px";

            var canvas_bg = new IHTMLDiv();
            //var canvas_bg_tween = new TweenDataDouble();

            //canvas_bg_tween.Value = 1;
            //canvas_bg_tween.ValueChanged += delegate { canvas_bg.style.Opacity = canvas_bg_tween.Value; };

            canvas_bg.style.backgroundImage = Assets.Default.Background.StyleSheetURL;
            canvas_bg.style.SetLocation(0, 0, canvas_size.x * 2, canvas_size.y);

            canvas.appendChild(canvas_bg);


            IStyleSheet.Default.AddRule(".info").style
                .Aggregate(s =>
                               {
                                   s.backgroundColor = Color.Black;
                                   s.color = Color.White;
                                   s.padding = "2em";
                                   s.fontFamily = IStyle.FontFamilyEnum.Tahoma;
                                   s.Float = IStyle.FloatEnum.right;
                               })
                ;

            IStyleSheet.Default.AddRule(".canvas").style
                .Aggregate(s => s.overflow = IStyle.OverflowEnum.hidden)
                .Aggregate(s => s.backgroundColor = Color.Black)
                ;

            IStyleSheet.Default.AddRule(".on").style
                .Aggregate(s => s.backgroundImage = Assets.Default.LogoOn.StyleSheetURL)
                //.Aggregate(s => s.Opacity = 0.8)
                ;

            IStyleSheet.Default.AddRule(".off").style
                .Aggregate(s => s.backgroundImage = Assets.Default.LogoOff.StyleSheetURL)
                //.Aggregate(s => s.Opacity = 0.5)
                ;




            Action<int, int> UpdateColor =
                (x, y) =>
                {
                    var n = a[x, y];

                    if (m[x, y])
                    {
                        n.className = "on";
                    }
                    else
                    {
                        n.className = "off";
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
                    //Console.WriteLine("click at: " + new { x, y } + " = " + m[x, y]);

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
                    info_stats_on.innerHTML = m.Count(i => i) + " blocks are on";
                    info_stats_off.innerHTML = m.Count(i => !i) + " blocks are off";

                };

            var info_stats = new IHTMLDiv(info_stats_clicks, info_stats_off, info_stats_on);
            info_stats.className = "info";


            a.ForEach(
                (x, y) =>
                {
                    var n = new IHTMLDiv();

                    n.style.left = (x * usersettings.tile.w + usersettings.tile.w / 2) + "px";
                    n.style.top = (y * usersettings.tile.h + usersettings.tile.h / 2) + "px";
                    n.style.width = usersettings.tile.w + "px";
                    n.style.height = usersettings.tile.h + "px";
                    n.style.position = IStyle.PositionEnum.absolute;
                    n.style.overflow = IStyle.OverflowEnum.hidden;

                    //n.style.border = "1px solid black";
                    n.style.cursor = IStyle.CursorEnum.pointer;

                    canvas.appendChild(n);

                    var tween = new TweenDataDouble();


                    tween.ValueChanged += () => n.style.Opacity = tween.Value;
                    tween.Value = usersettings.tile.cold;

                    n.style.Opacity = tween.Value;

                    n.onmouseover += delegate
                    {
                        tween.Value = 1;
                        //canvas_bg_tween.Value = 0.5;
                    };

                    n.onmouseout += delegate
                    {
                        tween.Value = usersettings.tile.cold;
                        //canvas_bg_tween.Value = 1;
                    };

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

            var ani = new Timer(t =>
                               canvas_bg.style.left = -(int)System.Math.Floor((double)((IDate.Now.getTime() / 75) % canvas_size.x)) + "px");




            var info = new IHTMLDiv();

            var info_header_text = "Lights out 2";

            Native.Document.title = info_header_text;

            info.appendChild(new IHTMLElement(IHTMLElement.HTMLElementEnum.h1, info_header_text));

            info.appendChild(new IHTMLAnchor("http://www.cjcraft.com/Blog/PermaLink,guid,5c35b1f1-dc66-4d85-ac04-22fc97503d4a.aspx", "based on SilverlightsOut"));
            info.appendChild(new IHTMLBreak());
            info.appendChild(new IHTMLAnchor("http://www.cjcraft.com/Blog/CommentView,guid,5c35b1f1-dc66-4d85-ac04-22fc97503d4a.aspx", "cjcraft blog post"));

            info.appendChild(new IHTMLElement(IHTMLElement.HTMLElementEnum.p,
                @"Lights out is a one player puzzle that is played on a 5 by 5 grid of squares in which every square has two states: on and off. The game starts off with all squares off, where the goal is to turn on every square. By selecting a square, all the surrounding squares' (up, down, left, right) state is turned toggled. For example, on a 3 by 3 grid of squares with all squares off, if the center one is selected, it will turn 'on' the 4 up, down, left, right squares from it."));

            info.appendChild(new IHTMLDiv("Mozilla based browsers seem to suffer in performance while animating contents under semitransparent elements."));

            info.appendChild(new IHTMLButton("Animate background").Aggregate(btn => btn.onclick += delegate { ani.StartInterval(50); }));
            info.appendChild(new IHTMLButton("Freeze background").Aggregate(btn => btn.onclick += delegate { ani.Stop(); }));
            info.appendChild(info_stats);
            info.appendChild(canvas);

            info_stats_update();

            DataElement.insertNextSibling(info);

        }




        static Class1()
        {
            //Console.EnableActiveXConsole();

            typeof(Class1).SpawnTo(i => new Class1(i));


        }


    }

}
