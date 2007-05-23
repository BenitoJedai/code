//using System.Linq;

using ScriptCoreLib;
using ScriptCoreLib.Shared;

using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Query;

using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Controls.Effects;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM.XML;

using global::System.Collections.Generic;



namespace HotPolygon.js
{
    [Script]
    public class Extension
    {


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
            // this ctor creates a new div which has a text and a button element
            // on mouseover over the color text is changed
            // on pressing the button the next message in text element is displayed


            IStyleSheet.Default.AddRule("html", "height: 100%; overflow: hidden; margin: 0; padding: 0; ", 0);
            IStyleSheet.Default.AddRule("body", "height: 100%; overflow: hidden; margin: 0; padding: 0; background-color: gray;", 0);
            IStyleSheet.Default.AddRule("img",
                r =>
                {
                    r.style.position = IStyle.PositionEnum.absolute;
                    r.style.top = "0";
                    r.style.border = "0";
                });



            IStyleSheet.Default.AddRule("*", "cursor: url('assets/HotPolygon/3dgarro.cur'), auto;", 0);

            var img = new IHTMLImage("assets/HotPolygon/99851426_7f408a6cc3_o_gray.png");
            var img_up = new IHTMLImage("assets/HotPolygon/up.png");
            var img_up_neg = new IHTMLImage("assets/HotPolygon/up_neg.png");
            var img_down = new IHTMLImage("assets/HotPolygon/down.png");



            img.attachToDocument();
            img_up.attachToDocument();
            img_up_neg.attachToDocument();

            var info_bg = new IHTMLDiv();

            info_bg.style.backgroundColor = Color.Black;
            info_bg.style.SetLocation(60, 60, 300, 200);
            info_bg.style.Opacity = 0.2;
            info_bg.style.zIndex = 1;
            info_bg.attachToDocument();

            var info = new IHTMLDiv();

            info.style.color = Color.White;
            info.style.fontFamily = IStyle.FontFamilyEnum.Tahoma;
            info.innerHTML = "This example demonstrates the use of custom cursors, map, area and timed animation. <br /><br /> You can change the background of this page by hovering above the tree or one of the clouds.";

            info.style.overflow = IStyle.OverflowEnum.auto;

            info.style.SetLocation(70, 70, 280, 180);
            info.style.zIndex = 4;
            info.attachToDocument();

            // 416 x 100

            var img_here_src_off = (from i in 0.Range(6)
                                    select string.Format("assets/HotPolygon/here/here{0}.png", i)).ToArray();

            var img_here_src_on = (from i in 0.Range(6)
                                   select string.Format("assets/HotPolygon/here/here{0}.png", 6 - i)).ToArray();


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
                    if (new System.Random().NextDouble() > 0.5)
                    {
                        img_here.SetCenteredLocation(589, 509);
                    }
                    else
                    {
                        img_here.SetCenteredLocation(686, 141);
                    }

                    Wait(AnimationOn, new System.Random().Next() % 15000);
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
                                         Console.WriteLine("over");
                                         tw_up_neg.Value = 1; /*tw_down.Value = 0.2;*/
                                     };
            area1.onmouseout += i =>
                                    {

                                        try
                                        {
                                            Console.WriteLine("out");
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
                                         Console.WriteLine("over");
                                         tw_up.Value = 1; /*tw_down.Value = 0.2;*/
                                     };
            area2.onmouseout += i =>
                                    {
                                        try
                                        {
                                            Console.WriteLine("out");
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
                new Pair<string, EventHandler<IHTMLElement>>(Alias, e => new Class1(e))
                );

        }


    }

}
