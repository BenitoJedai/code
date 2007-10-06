//using System.Linq;

using ScriptCoreLib;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.Shared;

using ScriptCoreLib.Shared.Drawing;

using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Controls;
using ScriptCoreLib.JavaScript.Controls.Effects;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM.XML;

using global::System.Collections.Generic;

using System.Linq;
using System;

namespace HotPolygon.js
{
    [Script]
    public class DynamicProperty<T>
    {
        Action<DynamicProperty<T>> _Changed;

        public DynamicProperty(Action<DynamicProperty<T>> e)
        {
            _Changed = e;
        }

        private T _Value;

        public T Value
        {
            get { return _Value; }
            set { _Value = value; _Changed(this); }
        }

    }



    [Script]
    public delegate void ActionParams<X, T>(X x, params T[] e);
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
            IStyleSheet.Default.AddRule("html", "height: 100%; overflow: hidden; margin: 0; padding: 0; ", 0);
            IStyleSheet.Default.AddRule("a", "color: blue; text-decoration: none;", 0);
            IStyleSheet.Default.AddRule("a:hover", "border-bottom: 1px dashed blue;", 0);

            IStyleSheet.Default.AddRule("body", "height: 100%; overflow: hidden; margin: 0; padding: 0; background-color: black; color: white;", 0);


            var img = new [] {
                "assets/HotPolygon/99851426_7f408a6cc3_o_gray.png",
                "assets/HotPolygon/up.png",
                "assets/HotPolygon/up_neg.png",
                "assets/HotPolygon/down.png",
                "assets/HotPolygon/here/here0.png",
                "assets/HotPolygon/here/here1.png",
                "assets/HotPolygon/here/here2.png",
                "assets/HotPolygon/here/here3.png",
                "assets/HotPolygon/here/here4.png",
                "assets/HotPolygon/here/here5.png",
            }.Select(src => new IHTMLImage(src)).ToArray();

            var loading =  new IHTMLElement(IHTMLElement.HTMLElementEnum.pre);

            loading.attachToDocument();

            new Timer(
                t =>
                {
                    var a = (from i in img where !i.complete select i.src).ToArray();

                    loading.innerHTML = "";

                    foreach (var v in a)
	                {

                        loading.innerHTML += "loading: " + v + "<br />";
	                }

                    if (a.Length > 0)
                        return;

                    t.Stop();

                    loading.Dispose();

                    Spawn();

                }, 1, 300);
        }

        private static void Spawn()
        {
            // this ctor creates a new div which has a text and a button element
            // on mouseover over the color text is changed
            // on pressing the button the next message in text element is displayed


            IStyleSheet.Default.AddRule("img.fx1",
                r =>
                {
                    r.style.position = IStyle.PositionEnum.absolute;
                    r.style.top = "0";
                    r.style.border = "0";
                });



            IStyleSheet.Default.AddRule("*", "cursor: url('assets/HotPolygon/cursor01.cur'), auto;", 0);

            var img = new IHTMLImage("assets/HotPolygon/99851426_7f408a6cc3_o_gray.png") { className = "fx1" };

            var img_up = new IHTMLImage("assets/HotPolygon/up.png") { className = "fx1" };
            var img_up_neg = new IHTMLImage("assets/HotPolygon/up_neg.png") { className = "fx1" };
            var img_down = new IHTMLImage("assets/HotPolygon/down.png") { className = "fx1" };



            img.attachToDocument();
            img_up.attachToDocument();
            img_up_neg.attachToDocument();


            var info_size = new
                {
                    width = 350,
                    height = 200
                };

            var info_bg = new IHTMLDiv();

            info_bg.style.SetLocation(60, 60, info_size.width, info_size.height);
            info_bg.style.Opacity = 0.2;
            info_bg.style.zIndex = 1;
            info_bg.attachToDocument();

            var info_borders = new IHTMLDiv();

            info_borders.style.SetLocation(60 - 4, 60 - 4, info_size.width + 8, info_size.height + 8);
            info_borders.style.Opacity = 0.2;
            info_borders.style.zIndex = 4;
            info_borders.attachToDocument();

            var info_drag_tween = new TweenDataDouble();

            var info_bg_useimage_cookie = new Cookie("setting1");

            var info_bg_useimage = new DynamicProperty<bool>(
                p =>
                {
                    info_bg_useimage_cookie.BooleanValue = p.Value;

                    if (p.Value)
                    {
                        info_borders.style.backgroundImage = "url(assets/HotPolygon/up_neg.png)";
                        info_bg.style.backgroundImage = "url(assets/HotPolygon/up_neg.png)";
                    }
                    else
                    {
                        info_borders.style.backgroundImage = "";
                        info_bg.style.backgroundImage = "";

                    }

                    info_drag_tween.Value = 0.5;
                }
             ) { Value = info_bg_useimage_cookie.BooleanValue };




            var info_drag = new DragHelper(info_borders);

            info_drag_tween.Value = 0;
            info_drag_tween.ValueChanged +=
                delegate
                {
                    var i = ScriptCoreLib.JavaScript.Runtime.Convert.ToInteger(255 * info_drag_tween.Value);

                    if (!info_bg_useimage.Value)
                    {
                        info_bg.style.backgroundColor = Color.FromRGB(i, i, 0);
                        info_borders.style.backgroundColor = Color.FromRGB(i, i, 0);
                    }
                    else
                    {
                        if (i < 1)
                            i = 1;

                        info_bg.style.Opacity = i / 255;
                        info_borders.style.Opacity = i / 255 * 0.5;
                    }

                };

            info_borders.style.cursor = IStyle.CursorEnum.move;

            info_borders.onmouseover +=
                delegate
                {
                    if (info_drag.IsDrag)
                        return;

                    info_drag_tween.Value = 1;
                };



            info_borders.onmouseout +=
                delegate
                {
                    if (info_drag.IsDrag)
                        return;

                    info_drag_tween.Value = 0;
                };




            var info = new IHTMLDiv();

            info.onmouseover +=
                delegate
                {
                    if (info_drag.IsDrag)
                        return;

                    info_drag_tween.Value = 0.5;
                };



            info.onmouseout +=
                delegate
                {
                    if (info_drag.IsDrag)
                        return;

                    info_drag_tween.Value = 0;
                };

            info.style.color = Color.White;
            info.style.fontFamily = IStyle.FontFamilyEnum.Tahoma;

            Func<string, IHTMLElement> par = texty => new IHTMLElement(IHTMLElement.HTMLElementEnum.p, texty);


            Native.Document.title = "HotPolygon";

            info.innerHTML = @"<h1>HotPolygon</h1>";

            var preview = new IHTMLImage("Preview.png");

            preview.style.Float = IStyle.FloatEnum.right;
            preview.style.margin = "1em";

            info.appendChild(
                preview,
                par("This example demonstrates the use of custom cursors, map, area, timed animation, cookies and a custom dialog."),
                par("You can change the background of this page by hovering above the tree or one of the clouds."),
                par("And yes you can drag this dialog at the borders :)"),

                new IHTMLDiv(
                @"
                    <ul>
                        <li>visit <a href='http://jsc.sf.net/'>jsc homepage</a></li>
                        <li>visit <a href='http://zproxy.wordpress.com/'>blog</a></li>
                    </ul>
                                ")
                );



            var info_option = new IHTMLInput(HTMLInputTypeEnum.checkbox);
            var info_option_label = new IHTMLLabel("Alternative background", info_option);


            info.appendChild(new IHTMLElement(IHTMLElement.HTMLElementEnum.p, info_option, info_option_label));



            info_option.onclick += i => info_bg_useimage.Value = info_option.@checked;
            info_option.@checked = info_bg_useimage.Value;

            info.style.overflow = IStyle.OverflowEnum.auto;

            info.style.SetLocation(70, 70, info_size.width - 20, info_size.height - 20);
            info.style.zIndex = 5;
            info.attachToDocument();

            info_drag.DragMove +=
                delegate
                {
                    if (info_bg_useimage.Value)
                    {
                        info_borders.style.backgroundPosition = (-(info_drag.Position.X - 4)) + "px " + (-(info_drag.Position.Y - 4)) + "px";
                        info_bg.style.backgroundPosition = (-info_drag.Position.X) + "px " + (-info_drag.Position.Y) + "px";
                    }

                    info_borders.style.SetLocation(info_drag.Position.X - 4, info_drag.Position.Y - 4);
                    info_bg.style.SetLocation(info_drag.Position.X, info_drag.Position.Y);
                    info.style.SetLocation(info_drag.Position.X + 10, info_drag.Position.Y + 10);
                };

            info_drag.Position = new Point(60, 60);
            info_drag.Enabled = true;

            // 416 x 100

            var img_here_src_off = (from i in Enumerable.Range( 0, 5)
                                    select string.Format("assets/HotPolygon/here/here{0}.png", i)).ToArray();

            var img_here_src_on = (from i in Enumerable.Range(0, 5)
                                   select string.Format("assets/HotPolygon/here/here{0}.png", 5 - i)).ToArray();


            var img_here = new IHTMLDiv();

            img_here.style.SetSize(416, 100);

            Action<Action, int> Wait =
                (done, time) =>
                {
                    new Timer(t => done(), time, 0);
                };


            ActionParams<Action, Action> DelayFrames =
                (done, h) =>
                {
                    int i = 0;

                    var next = default(Action);

                    next = () =>
                    {
                        if (i < h.Length)
                        {
                            var v = h[i];
                            i++;

                            Wait(() => { if (v != null) v(); next(); }, 1000 / 24);
                        }
                        else
                        {
                            Wait(done, 1000 / 24);
                        }
                    };

                    next();
                };


            var AnimationOn = default(Action);
            var AnimationOff = default(Action);
            var AnimationRandomOn = default(Action);


            AnimationOff =
                () =>
                {
                    DelayFrames(
                        () =>
                        {

                            img_here.style.display = IStyle.DisplayEnum.none;

                            Wait(() =>
                             {

                                 AnimationRandomOn();
                             }, 5000);
                        }
                        ,
                        img_here_src_off.Select<string, Action>(i => () => img_here.style.backgroundImage = "url(" + i + ")").ToArray()
                    );




                };

            AnimationOn =
                () =>
                {
                    img_here.style.display = IStyle.DisplayEnum.block;

                    DelayFrames(
                        () => Wait(AnimationOff, 3000),
                        img_here_src_on.Select<string, Action>(i => () => img_here.style.backgroundImage = "url(" + i + ")").ToArray()
                    );
                };

            AnimationRandomOn =
                () =>
                {
                    try
                    {
                        if (new System.Random().NextDouble() > 0.5)
                        {
                            img_here.SetCenteredLocation(589, 509);
                        }
                        else
                        {
                            img_here.SetCenteredLocation(686, 141);
                        }

                        Wait(AnimationOn, new System.Random().Next() % 15000);
                    }
                    catch
                    {

                    }
                };


            img_here.style.SetLocation(100, 100);
            img_here.style.zIndex = 1;

            AnimationRandomOn();


            img_here.attachToDocument();

            img_down.attachToDocument();

            img_up.style.Opacity = 0;
            img_up_neg.style.Opacity = 0;

            var tw_up = new TweenDataDouble();

            tw_up.Value = 0;
            tw_up.ValueChanged += delegate { img_up.style.Opacity = tw_up.Value; };

            var tw_up_neg = new TweenDataDouble();

            tw_up_neg.Value = 0;
            tw_up_neg.ValueChanged += delegate { img_up_neg.style.Opacity = tw_up_neg.Value; };



            var map = new IHTMLElement(IHTMLElement.HTMLElementEnum.map);

            map.id = "map1";
            map.name = "map1";

            var area1 = new XHTMLArea
                {
                    shape = XHTMLArea.ShapeEnum.polygon,
                    coords = "477, 178, 515, 144, 557, 160, 576, 194, 614, 181, 629, 206, 648, 209, 659, 163, 719, 154, 730, 103, 845, 118, 891, 168, 949, 213, 917, 246, 931, 266, 859, 300, 787, 302, 756, 274, 721, 294, 658, 282, 615, 257, 537, 239, 492, 230, 470, 195"


                };

            area1.onmouseover += i =>
                                     {
                                         System.Console.WriteLine("over");
                                         tw_up_neg.Value = 1; /*tw_down.Value = 0.2;*/
                                     };
            area1.onmouseout += i =>
                                    {

                                        try
                                        {
                                            System.Console.WriteLine("out");
                                            tw_up_neg.Value = 0;/* tw_down.Value = 1;*/
                                        }
                                        catch
                                        {
                                        }
                                    };


            var area2 = new XHTMLArea
                        {
                            shape = XHTMLArea.ShapeEnum.polygon,
                            coords = "677, 556, 718, 551, 747, 570, 758, 594, 756, 613, 729, 625, 688, 629, 663, 604, 657, 585"
                        };




            area2.onmouseover += i =>
                                     {
                                         System.Console.WriteLine("over");
                                         tw_up.Value = 1; /*tw_down.Value = 0.2;*/
                                     };
            area2.onmouseout += i =>
                                    {
                                        try
                                        {
                                            System.Console.WriteLine("out");
                                            tw_up.Value = 0;/* tw_down.Value = 1;*/
                                        }
                                        catch
                                        {
                                        }
                                    };

            img_down.onclick += i => System.Console.WriteLine(i.CursorPosition);

            //area1.href = "http://google.com";
            //area1.target = "_blank";

            map.appendChild(area1);
            map.appendChild(area2);

            map.attachToDocument();


            img_down.style.zIndex = 2;
            img_down.setAttribute("useMap", "#map1");


            //img_overlay.style.backgroundColor = Color.Red; 
        }




        [Script(InternalConstructor = true)]
        class XHTMLArea : IHTMLElement
        {
            #region ctor
            public XHTMLArea()
            {

            }

            static internal XHTMLArea InternalConstructor()
            {
                return (XHTMLArea)new IHTMLElement(HTMLElementEnum.area);
            }
            #endregion

            public string coords;
            public string href;
            public ShapeEnum shape;
            public string target;

            [Script(IsStringEnum = true)]
            public enum ShapeEnum
            {
                polygon = 0,
                rect = 1,
                circle = 2,
            }
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
