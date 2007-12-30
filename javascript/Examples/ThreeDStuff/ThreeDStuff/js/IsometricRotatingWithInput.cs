using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Lambda;

using System.Linq;
using System;


namespace ThreeDStuff.js
{
    //[Script]
    [ScriptApplicationEntryPoint, 
        ApplicationDescription(Description = "Clicking somewhere will let you know what is the original location like.")
    ]
    public partial class IsometricRotatingWithInput
    {

        public IsometricRotatingWithInput()
        {

            Native.Document.body.style.overflow = IStyle.OverflowEnum.hidden;
            Native.Document.body.style.color = Color.White;
            Native.Document.body.style.backgroundColor = Color.Black;

            var info = new IHTMLDiv("location: n/a");

            info.style.SetLocation(4, 4, Native.Window.Width - 8, 0);
            info.style.height = "auto";

            info.AttachToDocument().style.zIndex = 10000;

            Action<Point<double>> NotifyOfSelection =
                p =>
                {
                    info.innerHTML = new { location = new { p.X, p.Y } }.ToString().ToConsole();
                };

            Func<Point<double>> GetCenter = () => new Point<double>
            {
                X = Native.Window.Width / 2,
                Y = Native.Window.Height / 2
            };

            var ZeroPoint = new Point<double>();

            // http://en.wikipedia.org/wiki/Isometric_projection
            // http://en.wikipedia.org/wiki/Dimetric_projection
            // http://en.wikipedia.org/wiki/Axonometric_projection
            // http://en.wikipedia.org/wiki/First_angle_projection
            // http://en.wikipedia.org/wiki/3/4_perspective

            var RotationA = 45.ToRadians();
            var RotationB = 0.5;
            var Zoom = 1.0;
            var Dot = 4;

            Func<double, double, Point<double>> Translate =
                (_x, _y) =>
                {
                    //var _pos = GetCenter();

                    var _r = ZeroPoint.GetRotation(_x, _y) + RotationA;
                    var _d = ZeroPoint.GetDistance(_x, _y) * (Zoom * 32 * 2d.Sqrt());

                    _x = Math.Cos(_r) * _d;
                    _y = Math.Sin(_r) * _d * RotationB;

                    return new Point<double> { X = _x, Y = _y };
                };

            Action<double, double, IHTMLDiv> ApplyPosition =
                (_x, _y, _div) =>
                {
                    try
                    {
                        var _pos = GetCenter();

                        var p = Translate(_x, _y);

          

                        var _dot = (Zoom * Dot).ToInt32();

                        _div.AttachToDocument().style.SetLocation(
                            (_pos.X + p.X).ToInt32() - _dot / 2,
                            (_pos.Y + p.Y).ToInt32() - _dot / 2,
                            _dot,
                            _dot
                        );

                        _div.onmouseover +=
                            delegate
                            {
                                Native.Document.title = new { _x, _y }.ToString();

                            };

                    }
                    catch (Exception ex)
                    {
                        ex.ToConsole();
                    }
                };

            Func<double, double, IHTMLDiv> CreateDiv =
                (_x, _y) =>
                {

                    var _div = new IHTMLDiv();

                    _div.style.backgroundColor = Color.Red;

                    ApplyPosition(_x, _y, _div);

                    return _div;
                };


            var TileColor = Color.Gray;
            var TileColor2 = Color.White;

            var data = new[] {
                //new { x = 0.0, y = 0.0, color = TileColor },


                //new { x = -1.0, y = -1.0, color = Color.Red },
                new { x = -0.5, y = -0.5, color = Color.Red },

                //new { x = 0.0, y = -1.0, color = TileColor },
                //new { x = 0.0, y = -2.0, color = TileColor },

                //new { x = 1.0, y = -1.0, color = Color.Green },
                new { x = 0.5, y = -0.5, color = Color.Green },

                //new { x = 1.0, y = 0.0, color = TileColor },
                //new { x = 2.0, y = 0.0, color = TileColor },

                //new { x = 1.0, y = 1.0, color = Color.Blue },
                new { x = 0.5, y = 0.5, color = Color.Blue },

                //new { x = 0.0, y = 1.0, color = TileColor },
                //new { x = 0.0, y = 2.0, color = TileColor },

                //new { x = -1.0, y = 1.0, color = Color.Yellow },
                new { x = -0.5, y = 0.5, color = Color.Yellow },

                //new { x = -1.0, y = 0.0, color = TileColor },
                //new { x = -2.0, y = 0.0, color = TileColor },
            };



            data = data.Concat(
                from x in Enumerable.Range(-4, 8)
                from y in Enumerable.Range(-4, 8)
                //let y = 4
                select new { x = (double)x, y = (double)y, color = TileColor }
                   ).ToArray();

 


            var bg_size = new
            {
                w = (64 * Zoom).ToInt32(),
                h = (32 * Zoom).ToInt32()
            };



            
            var points =
                (
                from point in data
                let div = CreateDiv(point.x, point.y)
                select new
                       {
                           div,
                           point,
                           update = (Action)(() => ApplyPosition(point.x, point.y, div))
                       }
                )
                .ToArray()
                .ForEach(
                    i => i.div.style.backgroundColor = i.point.color
                )
                ;


            Func<bool> IsDoneRotatingA = () => RotationA.ToDegrees() == 45;

            Point KnownCanvasPosition = new Point();

            Func<Point<double>> UpdateInfo =
                delegate
                {
                    var canvas = KnownCanvasPosition;
                    var c = GetCenter();

                    var offset = new Point<double> { X = canvas.X - c.X, Y = (canvas.Y - c.Y) / RotationB };

                    var d = ZeroPoint.GetDistance(offset) / (Zoom * 32 * 2d.Sqrt());
                    var r = ZeroPoint.GetRotation(offset) - RotationA;

                    var realoffset = new Point<double>
                    {
                        X = Math.Cos(r) * d,
                        Y = Math.Sin(r) * d
                    };

                    NotifyOfSelection(

                        new Point<double> { X = realoffset.X, Y = realoffset.Y }

                        );

                    return realoffset;
                };

            50.AtInterval(
                t =>
                {

                    //Zoom += 0.5;
                    RotationA += 1.ToRadians();
                    // RotationB += 1.ToRadians();

                    points.ForEach(p => p.update());

                    //tiles.ForEach(p => p.update());

                    //bg_update();

                    UpdateInfo();
                }
            );

            Native.Document.onclick +=
                ev =>
                {
                    KnownCanvasPosition = ev.CursorPosition;

                    var p = UpdateInfo();
                    var point = new { x = p.X, y = p.Y, color = Color.FromRGB(0, 0, 0xff.Random()) };

                    var div = CreateDiv(point.x, point.y);

                    points = points.ConcatSingle(
                        new
                        {
                            div,
                            point,
                            update = (Action)(() => ApplyPosition(point.x, point.y, div))
                        }
                    );
                };

            Native.Document.onmousemove +=
                ev =>
                {
                    KnownCanvasPosition = ev.CursorPosition;

                    UpdateInfo();
                };

            Native.Document.body.onmousewheel +=
                ev =>
                {
                    Zoom += (ev.WheelDirection * 0.1);
                };
        }

        static IsometricRotatingWithInput()
        {
            
            typeof(IsometricRotatingWithInput).Spawn();
        }


    }

}
