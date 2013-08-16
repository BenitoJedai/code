using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.Shared.Drawing;
using AmiNET110.HTML.Pages;

namespace AmiNET110
{
    public delegate void ActionParam<T0, TParams>(T0 a0, params TParams[] a1);

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
        public Application(IDefault  page)
        {
            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );


            string[] Links = 
        {
            "http://www.aminocom.com/products/ipstb/aminet110.html",
            "http://www.vario.tv/espace-clients/aide/variotv_AmiNET110_UserGuide_Book2.pdf"
        };



            var img = new HTML.Images.FromAssets.Amino_remote();
            //var img_bg = new IHTMLImage(Assets.Path + "/Amino-remote-bg.png");

            var reddot_array = Extensions.ToArray(0, 1, 3, 6);

            var reddot = new[]
            {
                new { index = 0, image = (IHTMLImage)new HTML.Images.FromAssets.reddot_0() },
                new { index = 1, image = (IHTMLImage)new HTML.Images.FromAssets.reddot_1() },
                new { index = 3, image = (IHTMLImage)new HTML.Images.FromAssets.reddot_3() },
                new { index = 6, image = (IHTMLImage)new HTML.Images.FromAssets.reddot_6() }
            };


            100.AtInterval(
                t1 =>
                {
                    if (reddot.Any(i => !i.image.complete))
                        return;

                    //if (!img_bg.complete)
                    //    return;

                    t1.Stop();


                    //Native.Document.body.style.background = "url(" + img_bg.src + ") repeat-x";

                    img.InvokeOnComplete(
                        delegate
                        {
                            var div = new IHTMLDiv();

                            div.style.SetLocation(0, 0, img.width, img.height);
                            div.style.SetBackground(img.src, false);
                            div.AttachToDocument();

                            Action<Point> SpawnDot =
                                pos =>
                                {
                                    var c = reddot.Last();
                                    var x = new IHTMLImage(c.image.src);// c.image.cloneNode();

                                    x.AttachTo(div);
                                    x.SetCenteredLocation(pos);

                                    (1000 / 16).AtInterval(
                                        t =>
                                        {
                                            c = reddot.Previous(y => y.index == c.index);

                                            x.src = c.image.src;
                                        },
                                        () => c.index == reddot.First().index,
                                        () => 400.AtTimeout(
                                            t2 =>
                                            {

                                                x.Orphanize();

                                            }
                                        )

                                    );
                                };

                            div.onclick +=
                                ev =>
                                {
                                    var pos = ev.CursorPosition;

                                    Console.WriteLine(pos);

                                    SpawnDot(pos);
                                };

                            div.ontouchmove +=
                               ev =>
                               {
                                   ev.preventDefault();


                                   for (uint i = 0; i < ev.touches.length; i++)
                                   {
                                       var pos = new Point(ev.touches[i].clientX, ev.touches[i].clientY);

                                       SpawnDot(pos);
                                   }


                               };


                            var map = new[]
                            {
                                new { KeyCode = 13, Point = new Point(173, 331)}, // enter
                                new { KeyCode = 13, Point = new Point(206, 216)}, // enter

                                new { KeyCode =38, Point = new Point(173, 304)}, // up
                                new { KeyCode =37, Point = new Point(148, 327)}, // left
                                new { KeyCode =39, Point = new Point(201, 331)}, // right
                                new { KeyCode =40, Point = new Point(174, 357)}, // down

                                new { KeyCode =33, Point = new Point(164, 431)}, // page up
                                new { KeyCode =34, Point = new Point(187, 435)}, // page down

                                new { KeyCode =48, Point = new Point(172, 207)}, // 0


                                new { KeyCode =49, Point = new Point(141, 121)}, // 1
                                new { KeyCode =50, Point = new Point(171, 120)}, // 2
                                new { KeyCode =51, Point = new Point(206, 131)},
                                new { KeyCode =52, Point = new Point(143, 152)},
                                new { KeyCode =53, Point = new Point(175, 149)},
                                new { KeyCode =54, Point = new Point(207, 158)},
                                new { KeyCode =55, Point = new Point(141, 178)},
                                new { KeyCode =56, Point = new Point(174, 177)}, // 8
                                new { KeyCode =57, Point = new Point(209, 185)}, // 9

                                new { KeyCode =8, Point = new Point(140, 204)}, // backspace


                                new { KeyCode = 8492, Point = new Point(209, 252)}, // prog up
                                new { KeyCode = 8494, Point = new Point(205, 290)}, // prog down

                                new { KeyCode = 8512, Point = new Point(147, 380)}, // red
                                new { KeyCode = 8513, Point = new Point(167, 386)}, // green
                                new { KeyCode = 8514, Point = new Point(184, 389)}, // yellow
                                new { KeyCode = 8515, Point = new Point(206, 385)}, // blue

                                new { KeyCode = 8516, Point = new Point(175, 267)}, // menu

                                new { KeyCode = 8502, Point = new Point(144, 403)}, // rewind
                                new { KeyCode = 8499, Point = new Point(164, 406)}, // play
                                new { KeyCode = 8504, Point = new Point(188, 414)}, // pause
                                new { KeyCode = 8500, Point = new Point(208, 413)}, // pause
                            };

                            ActionParam<int, int> AddKeyCodes =
                                (_old, _new) =>
                                {
                                    var _2 = map.Where(i => i.KeyCode == _old).First();

                                    map = map.Concat(_new.Select(KeyCode => new { KeyCode, _2.Point })).ToArray();

                                };

                            Action<int, string> AddKeyCodesString =
                                (_old, _new) =>
                                {
                                    var _U = _new.ToUpper();
                                    var _L = _new.ToLower();


                                    for (int i = 0; i < _U.Length; i++)
                                    {
                                        Console.WriteLine(_old + " -> " + _U[i] + " , " + _L[i]);

                                        AddKeyCodes(_old, _U[i], _L[i]);
                                    }


                                };

                            var alpha = new Dictionary<char, string>
                            {
                                { '2', "abc" },
                                { '3', "def" },
                                { '4', "ghi" },
                                { '5', "jkl" },
                                { '6', "mno" },
                                { '7', "pqrs" },
                                { '8', "tuv" },
                                { '9', "wxyz" },
                            };

                            alpha.ForEach(i => AddKeyCodesString(i.Key, i.Value));


                            Native.document.onkeyup +=
                                ev =>
                                {
                                    ev.preventDefault();

                                    var z = map.Where(i => i.KeyCode == ev.KeyCode).ToArray();

                                    if (z.Length > 0)
                                        z.ForEach(i => SpawnDot(i.Point));

                                    Console.WriteLine(new { ev.KeyCode });
                                };

                            map.ForEach(
                                (i, next) =>
                                {
                                    SpawnDot(i.Point);

                                    25.AtTimeout(
                                        delegate
                                        {
                                            next();

                                        }
                                        );
                                }
                            );
                        }
                    );
                }
            );



        }

    }


    static class Extensions
    {
        public static void AtInterval(this int x, Action<Timer> h, Func<bool> stop, Action done)
        {
            new Timer(
                t =>
                {
                    h(t);

                    if (stop())
                    {
                        t.Stop();
                        done();
                    }

                }, x, x);
        }

        public static void AtInterval(this int x, Action<Timer> h)
        {
            new Timer(t => h(t), x, x);
        }

        public static void AtTimeout(this int x, Action<Timer> h)
        {
            new Timer(t => h(t), x, 0);
        }


        public static T[] ToArray<T>(params T[] e)
        {
            return e;
        }

        public static T[] ToArray<TSource, T>(this IEnumerable<TSource> e, Func<TSource, T> f)
        {
            return e.Select(f).ToArray();
        }

        public static void ToWindowText(this Type e, string s)
        {
            if (string.IsNullOrEmpty(s))
                Native.Window.document.title = e.Name;
            else
                Native.Window.document.title = e.Name + " - " + s;
        }

        public static void ToWindowText(this Type e)
        {
            ToWindowText(e, null);
        }

        public static int Random(this int i)
        {
            return new Random().Next(i);
        }
    }

}
