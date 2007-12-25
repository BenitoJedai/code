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
    [Script, ScriptApplicationEntryPoint, ApplicationDescription(Description = "A grid of isometric tiles.")]
    public class Isometric
    {

        public Isometric()
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

            var MapSize = new
            {
                Left = -8,
                Width = 16,
                Top = -8,
                Height = 16
            };


            var bg_size = new
            {
                w = (64 * Zoom).ToInt32(),
                h = (32 * Zoom).ToInt32()
            };



            #region Translate
            Func<double, double, Point<double>> Translate =
                (_x, _y) =>
                {
                    var _pos = GetCenter();

                    var _r = ZeroPoint.GetRotation(_x, _y) + RotationA;
                    var _d = ZeroPoint.GetDistance(_x, _y) * bg_size.h * 2d.Sqrt();

                    _x = Math.Cos(_r) * _d;
                    _y = Math.Sin(_r) * _d * RotationB;

                    return new Point<double> { X = _x, Y = _y };
                };
            #endregion

            #region ApplyPosition
            Action<double, double, IHTMLDiv> ApplyPosition =
                (_x, _y, _div) =>
                {
                    try
                    {
                        var _pos = GetCenter();

                        var p = Translate(_x, _y);



                        var _dot = (Zoom * Dot).ToInt32();

                        _div.style.SetLocation(
                            (_pos.X + p.X).ToInt32() - _dot / 2,
                            (_pos.Y + p.Y).ToInt32() - _dot / 2,
                            _dot,
                            _dot
                        );

                        _div.AttachToDocument();

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
            #endregion


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
            var TileColorHouse1 = Color.FromRGB(41, 0, 0);
            var TileColorHouse2 = Color.FromRGB(42, 0, 0);
            var TileColorTree1 = Color.FromRGB(51, 0, 0);
            var TileColorRoad1 = Color.FromRGB(61, 0, 0);
            var TileColorRoad2 = Color.FromRGB(62, 0, 0);
            var TileColorRoad3 = Color.FromRGB(63, 0, 0);

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

            Func<double, double, bool> IsDefined =
                (x, y) => data.Any(
                    i =>
                    {
                        if (i.x != x) return false;
                        if (i.y != y) return false;

                        return true;
                    }
            );


            data = data.Concat(
                from x in Enumerable.Range(MapSize.Left, MapSize.Width)
                from y in Enumerable.Range(MapSize.Top, MapSize.Height)
                select new { x = (double)x, y = (double)y, color = TileColor }
                   ).ToArray();


            Action<int, Color> CreateNewItemsRandomly =
                (x, c) =>
                    x.Times(
                             delegate
                             {
                                 var loc = data.Where(i => i.color == TileColor).Random();

                                 data = data.ConcatSingle(
                                        new { x = loc.x, y = loc.y, color = c }
                                 ).ToArray();
                             }
                     );






            #region bg_update_WithHeight
            Action<double, double, IHTMLImage, double> bg_update_WithHeight =
                (x, y, bg, h) =>
                {
                    h *= Zoom;

                    var c = GetCenter();

                    var p = Translate(x, y);

                    c.X += p.X.ToInt32(bg_size.w / 2);
                    c.Y += p.Y.ToInt32(bg_size.h / 2);

                    var _x = (c.X - bg_size.w / 2);
                    var _y = (c.Y - bg_size.h / 2 - (h - bg_size.h));

                    bg.style.SetLocation(
                        _x.ToInt32(),
                        _y.ToInt32(),
                        bg_size.w, h.ToInt32()
                    );
                };
            #endregion

            // http://wiki.openttd.org/index.php/Enhanced_GFX_replacement

            #region SpawnItems
            Action<Color, string, double> SpawnItems =
                (c, src, h) =>
                {
                    var tiles_query =
                         from point in data
                         where point.color == c
                         let img = new IHTMLImage(src).AttachToDocument()
                         let update = (Action)(() => bg_update_WithHeight(point.x, point.y, img, h))
                         select new { pos = point, img, update };

                    var tiles = tiles_query.ToArray().ForEach(i => i.update());
                };
            #endregion


            CreateNewItemsRandomly(5, TileColorHouse1);
            CreateNewItemsRandomly(5, TileColorHouse2);
            CreateNewItemsRandomly(17, TileColorTree1);

            CreateNewItemsRandomly(2, TileColorRoad1);
            CreateNewItemsRandomly(2, TileColorRoad2);

            // expand the random roads through the map
            #region expand

            data.Where(i => i.color == TileColorRoad1).ToArray().ForEach(
                road =>
                {
                    data = data.Concat(
                            from y in Enumerable.Range(MapSize.Top, MapSize.Height)
                            select new { x = road.x, y = (double)y, color = road.color }
                    ).ToArray();
                }
            );

            data.Where(i => i.color == TileColorRoad2).ToArray().ForEach(
                road =>
                {
                    data = data.Concat(
                            from x in Enumerable.Range(MapSize.Left, MapSize.Width)
                            select new { x = (double)x, y = road.y, color = road.color }
                    ).ToArray();
                }
            );

            #endregion

            // remove buildings on the roads
            #region clean road1
            {
                var AllRoads = data.Where(i => i.color == TileColorRoad1).ToArray();

                data = data.Where(
                    v =>
                    {
                        if (v.color == TileColor)
                            return true;

                        if (v.color == TileColorRoad1)
                            return true;

                        if (v.color == TileColorRoad2)
                            return true;

                        return !AllRoads.Any(i => i.x == v.x && i.y == i.y);
                    }
                ).ToArray();
            }

            foreach (var v in from a in data
                              where a.color == TileColorRoad1
                              let b = data.FirstOrDefault(i =>
                                  {
                                      var SameColor = i.color == TileColorRoad2;
                                      var SameLocation = i.x == a.x && i.y == a.y;

                                      return SameColor && SameLocation;
                                  })
                              where b != null
                              let c = new { a.x, a.y, color = TileColorRoad3 }
                              select new { a, b, c })
            {
                data = data
                    .Where(i =>
                        {
                            if (i == v.a) return false;
                            if (i == v.b) return false;

                            return true;
                        })
                    .ConcatSingle(v.c)
                    .ToArray();
            }

            {
                var AllRoads = data.Where(i => i.color == TileColorRoad2).ToArray();

                data = data.Where(
                    v =>
                    {
                        if (v.color == TileColor)
                            return true;

                        if (v.color == TileColorRoad1)
                            return true;

                        if (v.color == TileColorRoad2)
                            return true;

                        return !AllRoads.Any(i => i.x == v.x && i.y == v.y);
                    }
                ).ToArray();
            }
            #endregion

            //#region clean road2
            //{
            //    var AllRoads2 = data.Where(i => i.color == TileColorRoad2).ToArray();

            //    data = data.Where(
            //        v =>
            //        {
            //            if (v.color == TileColor)
            //                return true;

            //            if (v.color == TileColorRoad1)
            //                return true;

            //            if (v.color == TileColorRoad2)
            //                return true;

            //            return !AllRoads2.Any(i => i.x == v.x && i.y == i.y);
            //        }
            //    ).ToArray();
            //}
            //#endregion


            SpawnItems(TileColor, "assets/ThreeDStuff/0.png", 32);
            SpawnItems(TileColorRoad1, "assets/ThreeDStuff/r1.png", 32);
            SpawnItems(TileColorRoad2, "assets/ThreeDStuff/r2.png", 32);
            SpawnItems(TileColorRoad3, "assets/ThreeDStuff/r3.png", 32);
            SpawnItems(TileColorHouse1, "assets/ThreeDStuff/h1.png", 52);
            SpawnItems(TileColorHouse2, "assets/ThreeDStuff/h2.png", 96);
            SpawnItems(TileColorTree1, "assets/ThreeDStuff/t1.png", 65);


            /*
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
            */

            Func<bool> IsDoneRotatingA = () => RotationA.ToDegrees() == 45;

            /*
            1000.AtInterval(
                t =>
                {

                    //Zoom += 0.5;
                    //RotationA += 10.ToRadians();
                    // RotationB += 1.ToRadians();

                    points.ForEach(p => p.update());

                    tiles.ForEach(p => p.update());

                    //bg_update();

                }
            );*/
            //.Until(IsDoneRotatingA);
        }

        static Isometric()
        {
            typeof(Isometric).Spawn();
        }


    }

}
