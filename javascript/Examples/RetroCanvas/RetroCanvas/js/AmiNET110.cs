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

namespace RetroCanvas.js.AmiNET110
{
    [Script, ScriptApplicationEntryPoint]
    public class AmiNET110
    {
        public static string[] Links = 
        {
            "http://www.aminocom.com/products/ipstb/aminet110.html",
            "http://www.vario.tv/espace-clients/aide/variotv_AmiNET110_UserGuide_Book2.pdf"
        };

        public AmiNET110()
        {
            typeof(AmiNET110).ToWindowText();

            var img = new IHTMLImage(Assets.Path + "/Amino-remote.jpg");

            var reddot_array = Extensions.ToArray(0, 1, 3, 6);

            var reddot = reddot_array.ToArray(index =>
                new { index, image = new IHTMLImage(Assets.Path + "/reddot-" + index + ".png") }
            );


            100.AtInterval(
                t1 =>
                {
                    if (reddot.Any(i => !i.image.complete))
                        return;

                    t1.Stop();

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

                                                x.Dispose();

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

                            var map = new Dictionary<int, Point>
                            {
                                {13, new Point(173, 331)}, // enter

                                {38, new Point(173, 304)}, // up
                                {37, new Point(148, 327)}, // left
                                {39, new Point(201, 331)}, // right
                                {40, new Point(174, 357)}, // down

                                {33, new Point(164, 431)}, // page up
                                {34, new Point(187, 435)}, // page down

                                {48, new Point(172, 207)}, // 0
                                {49, new Point(141, 121)},
                                {50, new Point(171, 120)},
                                {51, new Point(206, 131)},
                                {52, new Point(143, 152)},
                                {53, new Point(175, 149)},
                                {54, new Point(207, 158)},
                                {55, new Point(141, 178)},
                                {56, new Point(174, 177)}, // 8
                                {57, new Point(209, 185)}, // 9

                                {8, new Point(140, 204)}, // backspace
                            };

                            Native.Document.onkeypress +=
                                ev =>
                                {
                                    ev.PreventDefault();

                                    if (map.ContainsKey(ev.KeyCode))
                                        SpawnDot(map[ev.KeyCode]);

                                    Console.WriteLine(new { ev.KeyCode });
                                };
                        }
                    );
                }
            );


        }

        static AmiNET110()
        {


            typeof(AmiNET110).SpawnTo(
                i =>
                {
                    Func<IHTMLElement, IHTMLElement> WithStyle =
                        e =>
                        {
                            if (e == null)
                                return e;

                            e.style.overflow = ScriptCoreLib.JavaScript.DOM.IStyle.OverflowEnum.hidden;
                            e.style.margin = "0px";
                            e.style.padding = "0px";
                            return e;
                        };

                    WithStyle((IHTMLElement)WithStyle(Native.Document.body).parentNode);

                    Timer.DoAsync(
                        () => new AmiNET110()
                        );
                });
        }
    }
}
