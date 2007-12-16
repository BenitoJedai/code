using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Lambda;

using System;
using ScriptCoreLib.JavaScript.Runtime;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleWorm.js
{


    [Script]
    class Worm
    {
        public Point Location;

        public Func<int> GetZoom;

        public Point Vector = new Point(1, 0);

        [Script]
        public class Part
        {
            public Point Location;

            public Func<int> GetZoom;

            readonly IHTMLDiv Control = new IHTMLDiv();

            public Part()
            {
                Control.style.backgroundColor = Color.Green;
            }

            public Part AttachTo(IHTMLDiv canvas)
            {
                var zoom = GetZoom();

                Control.style.SetLocation(Location.X * zoom, Location.Y * zoom, zoom, zoom);
                Control.AttachTo(canvas);

                return this;
            }

            public void Dispose()
            {
                this.Control.Dispose();
            }
        }

        public List<Part> Parts = new List<Part>();

        public Worm GrowToVector()
        {
            return GrowTo(this.Vector);
        }

        public Worm GrowTo(Point p)
        {
            var x = this.Location + p;

            Parts.Add(
                new Part { Location = x, GetZoom = GetZoom }.AttachTo(Canvas)
            );

            Location = x;

            return this;
        }

        public IHTMLDiv Canvas { get; set; }



        public Worm Grow()
        {
            return GrowTo(new Point(0, 0));
        }

        public void Shrink()
        {
            var p = this.Parts.FirstOrDefault();

            if (p == null)
                return;

            this.Parts.Remove(p);

            p.Dispose();
        }
    }

    [Script]
    class Apple
    {
        public Point Location;

        public Func<int> GetZoom;

        readonly IHTMLDiv Control = new IHTMLDiv();

        public Apple()
        {
            Control.style.backgroundColor = Color.Red;
        }

        public Apple AttachTo(IHTMLDiv canvas)
        {
            var zoom = GetZoom();

            Control.style.SetLocation(Location.X * zoom, Location.Y * zoom, zoom, zoom);
            Control.AttachTo(canvas);

            return this;
        }

        public void Dispose()
        {
            this.Control.Dispose();
        }
    }

    [Script, ScriptApplicationEntryPoint]
    public class Class1
    {
        // vNext should be semi 3D - http://www.freeworldgroup.com/games/3dworm/index.html

        public Class1()
        {
            typeof(Class1).ToWindowText();

            var canvas = new IHTMLDiv();

            canvas.style.SetLocation(0, 0, Native.Window.Width, Native.Window.Height);
            canvas.style.backgroundColor = Color.Black;
            canvas.AttachToDocument();
            canvas.style.position = IStyle.PositionEnum.relative;

            var zoom = 12;

            
            Func<Point> RandomLocation =
                () => new Point(
                        (Native.Window.Width / zoom).Random(),
                        (Native.Window.Height / zoom).Random()
                    );

            Func<Apple> CreateApple =
                () => new Apple { Location = RandomLocation(), GetZoom = () => zoom };


            var apples = new List<Apple>
            {
                CreateApple(),
            };

            4.Times(() =>
                apples.Add(
                    CreateApple()
                )
            );

            apples.ForEach(a => a.AttachTo(canvas));

            var worm = new Worm
            {
                Location = new Point { X = 4, Y = 8 },
                GetZoom = () => zoom,
                Canvas = canvas,
                Vector = new Point(0, 1)
            }
            .Grow()
            .GrowToVector()
            .GrowToVector();



            100.AtInterval(
                t =>
                {
                    worm.GrowToVector();

                    var a = apples.Where(i => i.Location.IsEqual(worm.Location)).ToArray();

                    if (a.Length > 0)
                    {
                        foreach (var v in a)
                        {
                            v.Location = RandomLocation();
                            v.AttachTo(canvas);
                        }

                    }
                    else
                        worm.Shrink();
                }
            );

            var map = new[]
            {
                new { KeyCode =38, Point = new Point(0, -1)}, // up
                new { KeyCode =37, Point = new Point(-1, 0)}, // left
                new { KeyCode =39, Point = new Point(1, 0)}, // right
                new { KeyCode =40, Point = new Point(0, 1)}, // down
            };

            Native.Document.onkeyup +=
                ev =>
                {
                    var v = map.SingleOrDefault(i => i.KeyCode == ev.KeyCode);

                    if (v != null)
                    {
                        if ((worm.Vector + v.Point).IsZero())
                            return;

                        worm.Vector = v.Point;
                    }
                };
        }

        static Class1()
        {

            typeof(Class1).SpawnTo(i =>
                {

                    // hide IE scrollbar

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
                        () => new Class1()
                    );
                });
        }


    }

}
