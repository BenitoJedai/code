//using System.Linq;

using ScriptCoreLib;
using ScriptCoreLib.Shared;

using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Query;

using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Net;
using ScriptCoreLib.JavaScript.Controls.Effects;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM.XML;

//using global::System.Collections.Generic;



namespace SimpleRollover.js
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
            // wallpapers at http://labnol.blogspot.com/2006/11/download-windows-vista-wallpapers.html


            #region AnimateCharacterColors
            Func<string, INode> AnimateCharacterColors =
                (text) =>
                {
                    var s = new IHTMLSpan();

                    var l = new global::System.Collections.Generic.List<IHTMLSpan>();

                    foreach (char c in text)
                    {
                        var y = "" + c;
                        var x = new IHTMLSpan(y);

                        if (y == " ")
                        {
                            s.appendChild(" ");
                        }
                        else
                        {
                            l.Add(x);


                            s.appendChild(x);
                        }


                    }



                    new Timer(
                        t =>
                        {
                            var len = l.Count + 40;

                            if (t.Counter % len < l.Count)
                            {
                                if (t.Counter % (len * 2) < l.Count)
                                {
                                    l[t.Counter % len].style.visibility = IStyle.VisibilityEnum.hidden;
                                }
                                else
                                {
                                    l[t.Counter % len].style.visibility = IStyle.VisibilityEnum.visible;
                                }
                            }



                        }, 6000, 400);


                    return s;
                };
            #endregion

            var u = new IHTMLDiv();

            //u.style.backgroundColor = Color.Green;
            u.style.position = IStyle.PositionEnum.absolute;
            u.style.left = "0";
            u.style.top = "0";
            u.style.height = "100%";
            u.style.width = "100%";
            u.style.overflow = IStyle.OverflowEnum.auto;

            var styles = new
            {
                dark = new IStyleSheet(),
                light = new IStyleSheet(),
                switchbutton = new IHTMLAnchor("", "day/night"),
                counter = 0
            };



            styles.switchbutton.onclick +=
                ev =>
                {
                    ev.PreventDefault();

                    styles.counter++;


                    if (styles.counter % 2 == 1)
                    {
                        styles.dark.disabled = false;
                        styles.light.disabled = true;
                    }
                    else
                    {
                        styles.dark.disabled = true;
                        styles.light.disabled = false;
                    }
                };


            var ad = new IHTMLDiv(
                            new IHTMLSpan(
                                AnimateCharacterColors("this application was written in c# and then translated to javascript by jsc to run in your browser")
                            ),
                            new IHTMLAnchor("http://zproxy.wordpress.com", "visit blog"),
                            new IHTMLAnchor("http://jsc.sf.net", "get more examples"),
                            styles.switchbutton
                         )
                         {
                             className = "ad1"
                         };

            u.appendChild(ad);

            var sheet = new IStyleSheet();

            sheet.AddRule(".ad1",
                r =>
                {
                    r.style.marginTop = "1em";
                    r.style.color = Color.White;
                    r.style.fontFamily = IStyle.FontFamilyEnum.Verdana;
                }
            );


            sheet.AddRule(".ad1 > *",
                r =>
                {
                    r.style.padding = "1em";

                    r.style.marginTop = "1em";
                }
            );

            sheet.AddRule(".ad1 > span",
                r =>
                {
                    r.style.Float = IStyle.FloatEnum.right;
                }
            );

            sheet.AddRule(".ad1 > a",
                r =>
                {
                    r.style.Float = IStyle.FloatEnum.left;
                    r.style.color = Color.White;

                    r.style.textDecoration = "none";
                }
            );

            sheet.AddRule(".ad1 a:hover",
                r =>
                {
                    r.style.color = Color.Yellow;
                }
            );



            sheet.AddRule("html",
                r =>
                {

                    r.style.overflow = IStyle.OverflowEnum.hidden;
                }
            );

            sheet.AddRule("body",
                r =>
                {
                    r.style.overflow = IStyle.OverflowEnum.hidden;

                    r.style.padding = "0";
                    r.style.margin = "0";

                    //r.style.backgroundImage = "url(assets/vista.jpg)";

                }
            );



            styles.dark.AddRule("body").style.backgroundImage = "url(assets/vistax.jpg)";
            styles.dark.AddRule(".effect1").style.backgroundImage = "url(assets/vista.jpg)";
            styles.dark.AddRule(".moon1").style.backgroundColor = Color.Yellow;

            styles.light.AddRule("body").style.backgroundImage = "url(assets/vista.jpg)";
            styles.light.AddRule(".effect1").style.backgroundImage = "url(assets/vistax.jpg)";
            styles.light.AddRule(".moon1").style.backgroundColor = Color.Red;


            sheet.AddRule(".special1",
                r =>
                {
                    r.style.background = "none";
                    r.style.border = "0";
                    r.style.width = "100%";
                    r.style.marginTop = "4em";


                }
            );

            sheet.AddRule(".content1",
                r =>
                {
                    r.style.backgroundColor = Color.White;

                    r.style.padding = "1em";
                    r.style.marginLeft = "4em";
                    r.style.marginRight = "4em";
                    r.style.Opacity = 0.5;
                    r.style.border = "1px solid gray";
                }
            );

            sheet.AddRule(".special1 img", "border: 0", 0);
            sheet.AddRule(".special1:hover", "background: url(assets/Untitled-3.png) repeat-x", 1);

            sheet.AddRule(".special1 .hot").style.display = IStyle.DisplayEnum.none;
            sheet.AddRule(".special1:hover .hot").style.display = IStyle.DisplayEnum.inline;

            sheet.AddRule(".special1 .cold", "display: inline;", 1);
            sheet.AddRule(".special1:hover .cold", "display: none;", 1);


            var states = new[] { new { Show = default(Action), Hide = default(Action), Selected = false } }.Where(p => false);


            ActionParams<string> Spawn =
                i =>
                    ((IHTMLImage)i[0]).InvokeOnComplete(cold =>
                    ((IHTMLImage)i[1]).InvokeOnComplete(hot =>
                         {
                             cold.className = "cold";
                             hot.className = "hot";


                             var btn = new IHTMLButton()
                                 {
                                     className = "special1"
                                 };

                             btn.appendChild(cold, hot);

                             var content = new IHTMLElement(IHTMLElement.HTMLElementEnum.pre);

                             content.innerHTML = "...";
                             content.className = "content1";

                             var tween = new TweenDataDouble();
                             var tween_max = 32;

                             tween.ValueChanged +=
                                 delegate
                                 {
                                     content.style.Opacity = tween.Value / tween_max;
                                     content.style.height = tween.Value + "em";

                                     content.style.overflow = IStyle.OverflowEnum.hidden;

                                 };

                             tween.Done += delegate
                             {
                                 if (tween.Value > 0)
                                     content.style.overflow = IStyle.OverflowEnum.auto;
                             };

                             tween.Value = 0;

                             var state = new
                                {
                                    Show = (Action)(() =>
                                                        {
                                                            tween.Value = tween_max;
                                                        }
                                    ),
                                    Hide = (Action)(() => tween.Value = 0),
                                    Selected = false
                                };

                             try
                             {
                                 new IXMLHttpRequest(HTTPMethodEnum.GET, i[2],
                                    request => content.innerHTML = request.responseText
                                 );
                             }
                             catch
                             {
                                 content.innerHTML = string.Format("navigate to <a href='{0}'>{0}</a> manually", i[2]);
                             }

                             states = states.Concat(new[] { state });

                             btn.onclick +=
                                 delegate
                                 {
                                     foreach (var v in states)
                                     {
                                         if (v == state)
                                         {

                                             v.Selected = !v.Selected;

                                             if (v.Selected)
                                             {
                                                 v.Show();
                                             }
                                             else
                                             {
                                                 v.Hide();
                                             }

                                         }
                                         else
                                         {
                                             v.Selected = false;
                                             v.Hide();
                                         }
                                     }
                                 };

                             u.appendChild(btn, content);




                         }
                    ));


            SpawnCursor();


            u.attachToDocument();

            Spawn("assets/Untitled-1_03.png", "assets/Untitled-2_03.png", "assets/cs.htm");
            Spawn("assets/Untitled-1_07.png", "assets/Untitled-2_07.png", "assets/js.htm");


        }

        [Script]
        class SpecialLayer
        {
            public readonly IHTMLDiv div = new IHTMLDiv();

            public int x;
            public int y;

            public double zoom = 1;



            public SpecialLayer()
            {
                div.style.position = IStyle.PositionEnum.absolute;
            }

            public void SetCenteredLocation(int x, int y)
            {
                SetLocation(Native.Math.floor(x - (this.x * zoom) / 2),
                    Native.Math.floor(y - (this.y * zoom) / 2));
            }
            public void SetLocation(int x, int y)
            {
                var s = div.style;

                s.backgroundPosition = (-x) + "px " + (-y) + "px";

                s.left = x + "px";
                s.top = y + "px";
            }

            public void UpdateSize()
            {
                var s = div.style;

                s.SetSize(Native.Math.floor(x * zoom), Native.Math.floor(y * zoom));
            }
        }

        private static void SpawnCursor()
        {
            var cur_size = 128;

            var pi = Native.Math.atan(1) * 4;

            var frames = 24;

            var a =
                from i in
                    (from j in Enumerable.Range(0, (frames / 2) - 0) select j / frames * pi)
                let x = Native.Math.cos(i)
                let y = Native.Math.sin(i)
                select new { x, y };

            //a.ForEach(z => Console.WriteLine(z.ToString()));

            var moon_frames = frames * 1.3;
            var moon_range = Native.Math.floor((moon_frames * 0.33));
            var moon_max = (0 - (moon_range / 2)) / moon_frames * pi;

            var dualmoon =
                (from offset in
                     (from j in Enumerable.Range(0, moon_range) select (j - (moon_range / 2)) / moon_frames * pi)
                 select new
                        {
                            offset,
                            moon1 = new SpecialLayer(),
                            moon2 = new SpecialLayer()
                        }).ToArray();


            dualmoon.ForEach(
                dual =>
                {
                    var op = 1 - (Native.Math.abs(dual.offset / moon_max));

                    var size = Native.Math.floor(4 * (op + 1));

                    dual.moon1.div.style.Opacity = 0.6;
                    dual.moon2.div.style.Opacity = 0.6;

                    dual.moon1.x = size;
                    dual.moon1.y = size;

                    dual.moon2.x = size;
                    dual.moon2.y = size;


                    dual.moon1.div.style.Opacity = op;
                    dual.moon2.div.style.Opacity = op;

                    dual.moon1.div.className = "moon1";
                    dual.moon2.div.className = "moon1";


                    dual.moon1.div.attachToDocument();
                }
            );

            var b = a.Select(
                i =>
                {
                    var layer = new SpecialLayer { x = Native.Math.floor(i.x * cur_size), y = Native.Math.floor(i.y * cur_size) };

                    layer.div.style.Opacity = 0.4;
                    layer.div.className = "effect1";
                    layer.div.attachToDocument();

                    return layer;
                }
            ).ToArray();


            var p = new Point(0, 0);


            dualmoon.ForEach(
                dual =>
                {
                    dual.moon2.div.attachToDocument();
                }
            );


            Action moon_update =
                () =>
                {
                    var seed = IDate.Now.getTime() / 1000;


                    dualmoon.ForEach(
                        dual =>
                        {
                            var rad = seed + dual.offset;

                            var deg = (rad + pi / 2) % (2 * pi);

                            if (deg > pi)
                            {
                                deg = (2 * pi) - deg;
                            }

                            deg /= pi;

                            if (deg > 0.5)
                            {
                                dual.moon1.div.style.visibility = IStyle.VisibilityEnum.hidden;
                                dual.moon2.div.style.visibility = IStyle.VisibilityEnum.visible;
                            }
                            else
                            {
                                dual.moon1.div.style.visibility = IStyle.VisibilityEnum.visible;
                                dual.moon2.div.style.visibility = IStyle.VisibilityEnum.hidden;
                            }

                            new[] { dual.moon1, dual.moon2 }.ForEach(
                                moon =>
                                {
                                    var cos = Native.Math.cos(rad) * cur_size;

                                    moon.SetCenteredLocation(
                                        Native.Math.floor(p.X + cos),
                                        Native.Math.floor(p.Y + -cos * 0.6)
                                    );



                                    moon.zoom = 1 + deg;

                                    moon.UpdateSize();



                                }
                            );
                        });
                };

            Native.Document.onmousemove +=
                delegate(IEvent ev)
                {
                    p = ev.CursorPosition;

                    b.ForEach(
                        layer =>
                        {
                            layer.SetCenteredLocation(p.X, p.Y);
                            layer.UpdateSize();
                        }
                    );

                    moon_update();


                };



            new Timer(
                t =>
                {
                    moon_update();
                }
            , 1, 150);

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
