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
    [Script, ScriptApplicationEntryPoint]
    public class IsometricRotating
    {

        public IsometricRotating()
        {
            Native.Document.body.style.overflow = IStyle.OverflowEnum.hidden;
            Native.Document.body.style.color = Color.White;
            Native.Document.body.style.backgroundColor = Color.Black;

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
            var Zoom = 1;
            var Dot = 4;

            Func<double, double, Point<double>> Translate =
                (_x, _y) =>
                {
                    var _pos = GetCenter();

                    var _r = ZeroPoint.GetRotation(_x, _y) + RotationA;
                    var _d = ZeroPoint.GetDistance(_x, _y) * Zoom * 32 * 2d.Sqrt();

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



            /*
            Action<double, double, IHTMLImage> bg_update =
                (x, y, bg) =>
                {
                    var c = GetCenter();

                    var p = Translate(x, y);

                    c.X += p.X.ToInt32(bg_size.w / 2);
                    c.Y += p.Y.ToInt32(bg_size.h / 2);

                    var _x = (c.X - bg_size.w / 2);
                    var _y = (c.Y - bg_size.h / 2);

                    bg.style.SetLocation(
                        _x.ToInt32(),
                        _y.ToInt32(),
                        bg_size.w, bg_size.h
                    );
                };

            var tiles_query =
                from point in data
                where point.color == TileColor
                let img = new IHTMLImage("assets/ThreeDStuff/0.png").AttachToDocument()
                let update = (Action)(() => bg_update(point.x, point.y, img))
                select new { pos = point, img, update };

            var tiles = tiles_query.ToArray().ForEach(i => i.update());
            */

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

            
            50.AtInterval(
                t =>
                {

                    //Zoom += 0.5;
                    RotationA += 1.ToRadians();
                    // RotationB += 1.ToRadians();

                    points.ForEach(p => p.update());

                    //tiles.ForEach(p => p.update());

                    //bg_update();

                }
            );
            //.Until(IsDoneRotatingA);
        }

        static IsometricRotating()
        {
            typeof(IsometricRotating).SpawnTo(i => new IsometricRotating());
        }


    }

}
