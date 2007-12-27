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
        public Func<Point, Point> Wrapper;

        public Point Vector = new Point(1, 0);

        [Script]
        public class Part
        {
            public Point Location;

            public Func<int> GetZoom;
            public Func<Point, Point> Wrapper;

            internal protected readonly IHTMLDiv Control = new IHTMLDiv();

            public Part()
            {
                Control.style.backgroundColor = Color.Green;
            }

            public Part AttachTo(IHTMLDiv canvas)
            {
                MoveToLocation();

                Control.AttachTo(canvas);

                return this;
            }

            public void MoveToLocation()
            {
                var zoom = GetZoom();

                Location = Wrapper(Location);
                Control.style.SetLocation(Location.X * zoom, Location.Y * zoom, zoom, zoom);
            }

            public void Dispose()
            {
                this.Control.Dispose();
            }
        }

        public readonly List<Part> Parts = new List<Part>();

        public Worm GrowToVector()
        {
            return GrowTo(this.Vector);
        }

        public Point NextLocation
        {
            get
            {
                return Wrapper(this.Location + this.Vector);
            }
        }

        public Worm GrowTo(Point p)
        {
            var x = Wrapper(this.Location + p);

            Parts.Add(
                new Part { Location = x, GetZoom = GetZoom, Wrapper = Wrapper }.AttachTo(Canvas)
            );

            Location = x;

            return this;
        }

        public IHTMLDiv Canvas { get; set; }

        Color _Color = Color.Green;

        public Color Color
        {
            get
            {
                return _Color;
            }

            set
            {
                _Color = value;

                this.Parts.ForEach(v => v.Control.style.backgroundColor = value);
            }
        }

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
        public Func<Point> GetRandomLocation;

        public Point Location;

        public Func<int> GetZoom;

        readonly IHTMLDiv Control = new IHTMLDiv();

        public Func<Point, Point> Wrapper;


        public Apple()
        {
            Control.style.backgroundColor = Color.Red;
        }

        public Apple MoveToRandomLocation()
        {
            Location = GetRandomLocation();
            MoveToLocation();
            return this;
        }

        public Apple AttachTo(IHTMLDiv canvas)
        {
            MoveToLocation();
            Control.AttachTo(canvas);

            return this;
        }

        public void MoveToLocation()
        {
            var zoom = GetZoom();

            Location = Wrapper(Location);

            Control.style.SetLocation(Location.X * zoom, Location.Y * zoom, zoom, zoom);
        }

        public void Dispose()
        {
            this.Control.Dispose();
        }
    }

    [Script, ScriptApplicationEntryPoint]
    public class ConsoleWorm
    {
        // vNext should be semi 3D - http://www.freeworldgroup.com/games/3dworm/index.html

        public ConsoleWorm()
        {

            typeof(ConsoleWorm).ToWindowText();


            var canvas = new IHTMLDiv();

            canvas.style.SetLocation(0, 0, Native.Window.Width, Native.Window.Height);
            canvas.style.backgroundColor = Color.Black;
            canvas.AttachToDocument();
            canvas.style.position = IStyle.PositionEnum.relative;

            new IHTMLImage(Assets.Path + "/avatar14683_21.gif").InvokeOnComplete(
                 scull =>
                 {

                     var zoom = 24;

                     Func<int> RoomWidth = () => (Native.Window.Width / zoom).ToInt32();
                     Func<int> RoomHeight = () => ((Native.Window.Height /*- scull.height - 16*/) / zoom).ToInt32();

                     var score = 0;
                     var status = new IHTMLDiv("0$");
                     var isdead = false;
                     var paused = false;

                     Action<int> AddScore = x =>
                     {
                         score += x;

                         if (isdead)
                             status.innerText = score + "$ - Game Over - Enter to continue";
                         else if (paused)
                             status.innerText = score + "$ - Paused - Zoom: " + zoom;
                         else
                             status.innerText = score + "$";
                     };

                     status.style.color = Color.Green;
                     status.style.fontFamily = IStyle.FontFamilyEnum.Consolas;


                     scull.AttachTo(canvas).style.SetLocation(
                         8,
                         Native.Window.Height - scull.height - 8);


                     status.AttachTo(canvas).style.SetLocation(
                         8 + scull.width + 8,
                         Native.Window.Height - scull.height
                     );


                     Func<Point> GetRandomLocation =
                         () => new Point(
                                 (RoomWidth() - 1).Random(),
                                 (RoomHeight() - 1).Random()
                             );

                     var game_colors = new
                     {
                         worm = new
                         {
                             active = Color.FromRGB(0, 0xff, 0),
                             inactive = Color.FromRGB(0, 0x7F, 0)
                         }

                     };

                     Func<Point, Point> Wrapper =
                         p => new Point((p.X + RoomWidth()) % RoomWidth(), (p.Y + RoomHeight()) % RoomHeight());


                     Func<Apple> CreateApple =
                         () => new Apple
                         {
                             GetRandomLocation = GetRandomLocation,
                             GetZoom = () => zoom,
                             Wrapper = Wrapper
                         }.MoveToRandomLocation();


                     var apples = new List<Apple>
                     {
                     };

                     10.Times(() =>
                         apples.Add(
                             CreateApple()
                         )
                     );

                     apples.ForEach(a => a.AttachTo(canvas));





                     var worm = new Worm
                     {
                         Wrapper = Wrapper,
                         Location = new Point { X = 4, Y = 8 },
                         GetZoom = () => zoom,
                         Canvas = canvas,
                         Vector = new Point(0, 1),
                         Color = game_colors.worm.active
                     }
                     .Grow()
                     .GrowToVector()
                     .GrowToVector();



                     100.AtInterval(
                         t =>
                         {
                             if (paused)
                             {
                                 // slowdown
                                 if (t.Counter % 4 == 0)
                                 {
                                     if (worm.Color == game_colors.worm.active)
                                     {
                                         worm.Color = game_colors.worm.inactive;
                                         status.style.color = game_colors.worm.inactive;
                                     }
                                     else
                                     {
                                         worm.Color = game_colors.worm.active;
                                         status.style.color = game_colors.worm.active;
                                     }

                                 }

                                 return;
                             }

                             if (worm.Parts.Any(i => i.Location.IsEqual(worm.NextLocation)))
                             {
                                 paused = true;
                                 isdead = true;

                                 AddScore(0);

                                 return;
                             }

                             worm.GrowToVector();

                             // did we find an apple?
                             var a = apples.Where(i => i.Location.IsEqual(worm.Location)).ToArray();

                             if (a.Length > 0)
                             {
                                 foreach (var v in a)
                                 {
                                     v.MoveToRandomLocation();
                                 }

                                 AddScore(1);
                             }
                             else
                             {
                                 worm.Shrink();
                             }
                         }
                     );

                     var map = new[]
                        {
                            new { KeyCode = 38, Point = new Point(0, -1)}, // up
                            new { KeyCode = 37, Point = new Point(-1, 0)}, // left
                            new { KeyCode = 39, Point = new Point(1, 0)}, // right
                            new { KeyCode = 40, Point = new Point(0, 1)}, // down
                        };

                     Func<IEvent, bool> IsPauseKey =
                         ev => ev.IsKeyCode("pP") || ev.KeyCode == 8504;


                     Native.Document.onkeyup +=
                         ev =>
                         {
                             if (isdead)
                                 if (ev.IsReturn)
                                 {

                                     while (worm.Parts.Count > 2)
                                         worm.Shrink();

                                     paused = false;
                                     isdead = false;
                                     AddScore(-score);
                                 }

                             if (!paused)
                             {
                                 var v = map.SingleOrDefault(i => i.KeyCode == ev.KeyCode);

                                 if (v != null)
                                 {
                                     if ((worm.Vector + v.Point).IsZero())
                                         return;

                                     worm.Vector = v.Point;
                                 }

                                 if (ev.IsReturn)
                                 {
                                     apples.Add(
                                         CreateApple().AttachTo(canvas)
                                     );
                                 }
                                 else
                                     if (ev.KeyCode == 33)
                                     {
                                         zoom--;
                                         zoom = zoom.Max(8);

                                         apples.ForEach(a => a.MoveToLocation());
                                         worm.Parts.ForEach(p => p.MoveToLocation());
                                     }
                                     else if (ev.KeyCode == 34)
                                     {
                                         zoom++;
                                         zoom = zoom.Min(64);
                                         apples.ForEach(a => a.MoveToLocation());
                                         worm.Parts.ForEach(p => p.MoveToLocation());
                                     }


                             }

                             if (!isdead)
                                 if (IsPauseKey(ev))
                                 {
                                     paused = !paused;

                                     if (!paused)
                                     {
                                         worm.Color = game_colors.worm.active;
                                         status.style.color = game_colors.worm.active;
                                     }

                                     AddScore(0);
                                 }

                         };


                 }
            );
        }

        static ConsoleWorm()
        {

            typeof(ConsoleWorm).SpawnTo(i =>
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
                        () => new ConsoleWorm()
                    );
                });
        }


    }

}
