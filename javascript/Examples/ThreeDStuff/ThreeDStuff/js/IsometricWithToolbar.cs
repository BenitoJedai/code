using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Lambda;

using System.Linq;
using System;
using System.Collections.Generic;


namespace ThreeDStuff.js
{
    using ScriptCoreLib.JavaScript.Controls.NatureBoy;
    using ScriptCoreLib.JavaScript.Controls;
    using ScriptCoreLib.JavaScript.Runtime;

    //[Script]
    //public delegate void Action<A, B, C, D, E>(A a, B b, C c, D d, E e);

    [ScriptApplicationEntryPoint,
        ApplicationDescription(
            Description = "Now with toolbar!",
            FlashMovie = "http://www.youtube.com/watch?v=kCgCSMpRN40"

        )]
    public partial class IsometricWithToolbar
    {
        [Script]
        class TileElement
        {
            public Point<double> Position;
            public IHTMLImage Image;

        }

        public IsometricWithToolbar()
        {
            Native.Document.body.style.overflow = IStyle.OverflowEnum.hidden;
            Native.Document.body.style.color = Color.White;
            Native.Document.body.style.backgroundColor = Color.Black;

            var info_text = "This example demostrates how would an isometric javascript game look like in your browser. You can see landscape from <i>Transport Tycoon</i> and the characters are from <i>Wolfenstein 3D</i> and <i>Doom</i>.";

            var info = new IHTMLDiv("loading...");

            info.style.SetLocation(4, 4, Native.Window.Width - 8, 0);
            info.style.height = "auto";


            var paused = false;

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

            var MapSize = new Rectangle
            {
                Left = -12,
                Width = 24,
                Top = -12,
                Height = 24
            };


            var bg_size = new
            {
                w = (64 * Zoom).ToInt32(),
                h = (32 * Zoom).ToInt32()
            };

            Func<Point<double>> GetCenter = () => new Point<double>
            {
                X = bg_size.w * (MapSize.Width + 2) / 2 /*Native.Window.Width / 2*/,
                Y = bg_size.h * (MapSize.Height + 2) / 2 /*Native.Window.Height / 2*/
            };

            var arena = new ArenaControl();

            arena.Control.AttachToDocument();

            arena.Layers.Canvas.style.backgroundColor = Color.FromRGB(0, 0, 0);


            arena.SetLocation(Rectangle.Of(0, 0, Native.Window.Width, Native.Window.Height));

            arena.SetCanvasSize(new Point(
                ((MapSize.Width + 2) * bg_size.w).ToInt32(),
                ((MapSize.Height + 2) * bg_size.h).ToInt32()
                ));

            Native.Window.onresize +=
                ev =>
                {
                    info.style.SetLocation(4, 4, Native.Window.Width - 8, 0);
                    info.style.height = "auto";

                    arena.SetLocation(Rectangle.Of(0, 0, Native.Window.Width, Native.Window.Height));
                };

            info.AttachTo(arena.Layers.Info);

            var TileResources = new
                {
                    Dirt = new { Source = "assets/ThreeDStuff/4.png", Height = 32 },
                    Tree = new { Source = "assets/ThreeDStuff/t1.png", Height = 65 }
                };

            #region Translate
            Func<double, double, Point<double>> Translate =
                (_x, _y) =>
                {
                    var _r = ZeroPoint.GetRotation(_x, _y) + RotationA;
                    var _d = ZeroPoint.GetDistance(_x, _y) * bg_size.h * 2d.Sqrt();

                    _x = Math.Cos(_r) * _d;
                    _y = Math.Sin(_r) * _d * RotationB;

                    return new Point<double> { X = _x, Y = _y };
                };
            #endregion

            #region GetCanvasPosition
            Func<Point<double>, Point<double>> GetCanvasPosition =
                map_coords =>
                {
                    var canvas_coords = Translate(map_coords.X, map_coords.Y);
                    var c = GetCenter();

                    canvas_coords.X += c.X;
                    canvas_coords.Y += c.Y;

                    return canvas_coords;

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


            #region CreateDiv
            Func<double, double, IHTMLDiv> CreateDiv =
                (_x, _y) =>
                {

                    var _div = new IHTMLDiv();

                    _div.style.backgroundColor = Color.Red;

                    ApplyPosition(_x, _y, _div);

                    return _div;
                };
            #endregion


            var TileColor = Color.Gray;
            var TileColor2 = Color.White;
            var TileColorHouse1 = Color.FromRGB(41, 0, 0);
            var TileColorHouse2 = Color.FromRGB(42, 0, 0);
            var TileColorTree1 = Color.FromRGB(51, 0, 0);
            var TileColorRoad1 = Color.FromRGB(61, 0, 0);
            var TileColorRoad2 = Color.FromRGB(62, 0, 0);
            var TileColorRoad3 = Color.FromRGB(63, 0, 0);

            var data = new[] {
                new { x = -0.5, y = -0.5, color = Color.Red },
                new { x = 0.5, y = -0.5, color = Color.Green },
                new { x = 0.5, y = 0.5, color = Color.Blue },
                new { x = -0.5, y = 0.5, color = Color.Yellow },
            };

            #region IsDefined
            Func<double, double, bool> IsDefined =
                (x, y) => data.Any(
                    i =>
                    {
                        if (i.x != x) return false;
                        if (i.y != y) return false;

                        return true;
                    }
            );
            #endregion


            data = data.Concat(
                from x in Enumerable.Range(MapSize.Left, MapSize.Width + 1)
                from y in Enumerable.Range(MapSize.Top, MapSize.Height + 1)
                select new { x = (double)x, y = (double)y, color = TileColor }
                   ).ToArray();


            #region CreateNewItemsRandomly
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
            #endregion


            #region ApplyTileToCanvas
            Action<double, double, IHTMLImage, double> ApplyTileToCanvas =
                (x, y, bg, height) =>
                {
                    height *= Zoom;

                    var c = GetCenter();

                    var p = Translate(x, y);

                    c.X += p.X.ToInt32(bg_size.w / 2);
                    c.Y += p.Y.ToInt32(bg_size.h / 2);

                    var _x = (c.X - bg_size.w / 2);
                    var _y = (c.Y - bg_size.h / 2 - (height - bg_size.h));

                    bg.style.SetLocation(
                        _x.ToInt32(),
                        _y.ToInt32(),
                        bg_size.w, height.ToInt32()
                    );


                    if (height > bg_size.h)
                    {
                        bg.style.zIndex = (_y + height - bg_size.h / 2).ToInt32();
                    }
                    else
                    {
                        bg.style.zIndex = (_y).ToInt32();
                    }
                };
            #endregion

            // http://wiki.openttd.org/index.php/Enhanced_GFX_replacement

            var KnownTileElements = new List<TileElement>();

            #region SpawnItems
            Action<Color, Func<string>, double> SpawnItems =
                (c, src, h) =>
                {
                    var tiles_query =
                         from point in data
                         where point.color == c
                         let img = new IHTMLImage(src())
                         let update = (Action)(() => ApplyTileToCanvas(point.x, point.y, img, h))
                         let img2 = img.Aggregate(
                            i =>
                            {
                                update();
                            }).AttachTo(arena.Layers.Canvas)
                         select
                            new TileElement
                            {
                                Position = new Point<double> { X = point.x, Y = point.y },
                                Image = img
                                //, update 
                            };

                    KnownTileElements.AddRange(tiles_query.ToArray());//.ForEach(i => i.update());
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

            info.innerText = "Loading items...";

            Timer.DoAsync(
                delegate
                {
                    SpawnItems(TileColor,
                        () =>
                        {
                            if (0.05.ByChance()) return "assets/ThreeDStuff/1.png";
                            if (0.09.ByChance()) return "assets/ThreeDStuff/2.png";

                            return "assets/ThreeDStuff/0.png";
                        }, 32);

                    SpawnItems(TileColorRoad1, () => "assets/ThreeDStuff/r1.png", 32);
                    SpawnItems(TileColorRoad2, () => "assets/ThreeDStuff/r2.png", 32);
                    SpawnItems(TileColorRoad3, () => "assets/ThreeDStuff/r3.png", 32);
                    SpawnItems(TileColorHouse1, () => "assets/ThreeDStuff/h1.png", 52);
                    SpawnItems(TileColorHouse2, () => "assets/ThreeDStuff/h2.png", 96);
                    SpawnItems(TileColorTree1, () => "assets/ThreeDStuff/t1.png", 65);





                    Func<bool> IsDoneRotatingA = () => RotationA.ToDegrees() == 45;


                    #region SpawnLookingDude
                    Func<FrameInfo[], int, int, Dude2> SpawnLookingDude =
                               (f, x, y) =>
                               {
                                   var r = new Dude2
                                   {
                                       Frames = f,
                                   };

                                   r.AnimationInfo.Frames_Stand = f;

                                   r.Zoom.DynamicZoomFunc = a => 1;
                                   r.Zoom.StaticZoom = 1;

                                   r.SetSize(48, 72);
                                   r.TeleportTo(x, y);


                                   r.Control.AttachTo(arena.Layers.Canvas);

                                   r.Direction = Math.PI.Random() * 2;

                                   return r;
                               };
                    #endregion


                    var dude = new DudeAnimationInfo
                    {
                        Frames_Stand = Frames.WolfSoldier,
                        Frames_Walk = Frames.WolfSoldier_Walk
                    };

                    var imp = new DudeAnimationInfo
                    {
                        Frames_Stand = Frames.DoomImp,
                        Frames_Walk = Frames.DoomImp_Walk
                    };



                    var loaded = 0;

                    loaded += dude.Frames_Stand.Length + dude.Frames_Walk.Sum(i => i.Length);
                    loaded += imp.Frames_Stand.Length + imp.Frames_Walk.Sum(i => i.Length);

                    Action loaded_done = delegate { };
                    Action<IHTMLImage> loadone = img =>
                    {
                        loaded--;
                        //Console.WriteLine("loaded: " + img.src + " images to be loaded: " + loaded);

                        if (loaded == 0) loaded_done();
                    };


                    info.innerText = "Loading images...";

                    dude.Frames_Stand.ForEach(i => i.Image.InvokeOnComplete(img => loadone(img)));
                    dude.Frames_Walk.ForEach(j => j.ForEach(i => i.Image.InvokeOnComplete(img => loadone(img))));

                    imp.Frames_Stand.ForEach(i => i.Image.InvokeOnComplete(img => loadone(img)));
                    imp.Frames_Walk.ForEach(j => j.ForEach(i => i.Image.InvokeOnComplete(img => loadone(img))));



                    var Dudes = new List<Dude2>();


                    #region loaded_done
                    loaded_done +=
                        delegate
                        {

                            info.innerText = "Loading images... done";

                            #region GetRandomCanvasPosition
                            Func<Point> GetRandomCanvasPosition =
                                () =>
                                {
                                    var x = (MapSize.Width - 1).Random() + MapSize.Left + 1;
                                    var y = (MapSize.Height - 1).Random() + MapSize.Top + 1;


                                    var target = Translate(
                                        x, y
                                    );

                                    var c = GetCenter();

                                    var p = new Point { X = (target.X + c.X).ToInt32(), Y = (target.Y + c.Y).ToInt32() };

                                    return p;
                                }
                            ;
                            #endregion


                            #region CreateDude
                            Func<DudeAnimationInfo, bool, double, Dude2> CreateDude =
                                (ani, ScoutIfIdle, dudezoom) =>
                                {
                                    var w2c = GetRandomCanvasPosition();
                                    var w2 = SpawnLookingDude(ani.Frames_Stand, w2c.X.ToInt32(), w2c.Y.ToInt32());
                                    w2.Zoom.StaticZoom = dudezoom;
                                    w2.AnimationInfo.Frames_Walk = ani.Frames_Walk;
                                    w2.TargetLocationDistanceMultiplier = 1;

                                    Action<Action> GoSomeWhere =
                                        done =>
                                        {
                                            w2.DoneWalkingOnce += done;
                                            w2.WalkTo(GetRandomCanvasPosition());
                                        };

                                    Action WaitSomeAndGoSomeWhere = null;

                                    WaitSomeAndGoSomeWhere =
                                        () => 5000.Random().AtTimeout(
                                            t =>
                                            {
                                                if (paused)
                                                {
                                                    WaitSomeAndGoSomeWhere();
                                                    return;
                                                }

                                                var CurrentlyWalking = Dudes.Count(i => i.IsWalking);

                                                if (w2.IsWalking)
                                                {
                                                    WaitSomeAndGoSomeWhere();
                                                    return;
                                                }

                                                // if we've been selected, then wait for orders
                                                if (w2.IsSelected)
                                                {
                                                    WaitSomeAndGoSomeWhere();
                                                    return;
                                                }

                                                if (w2.IsHot)
                                                {
                                                    WaitSomeAndGoSomeWhere();
                                                    return;
                                                }

                                                // dont make them all walk at the same time
                                                if (CurrentlyWalking > 3)
                                                {
                                                    w2.Direction = (Math.PI * 2).Random();

                                                    WaitSomeAndGoSomeWhere();
                                                    return;
                                                }


                                                if (ScoutIfIdle)
                                                    GoSomeWhere(WaitSomeAndGoSomeWhere);
                                                else
                                                    WaitSomeAndGoSomeWhere();
                                            }
                                        );

                                    // make only imps wander on their own
                                    WaitSomeAndGoSomeWhere();

                                    return w2;
                                };
                            #endregion

                            info.innerHTML = "Creating dudes...";

                            8.Times(() => Dudes.Add(CreateDude(dude, false, 0.5)));
                            8.Times(() => Dudes.Add(CreateDude(imp, true, 0.5)));

                            info.innerHTML = info_text;
                        };
                    #endregion


                    //Point KnownCanvasPosition = new Point();

                    #region GetMapPosition
                    Func<Point, Point<double>> GetMapPosition =
                        canvas =>
                        {
                            var c = GetCenter();

                            var offset = new Point<double> { X = canvas.X - c.X, Y = (canvas.Y - c.Y) / RotationB };

                            var d = ZeroPoint.GetDistance(offset) / (bg_size.h * 2d.Sqrt());
                            var r = ZeroPoint.GetRotation(offset) - RotationA;

                            var realoffset = new Point<double>
                            {
                                X = Math.Cos(r) * d,
                                Y = Math.Sin(r) * d
                            };

                            return realoffset;
                        };
                    #endregion

    



                    // create a draggable toolbar
                    #region creating the toolbar

                    var toolbar_size = new Point(96, 32);
                    var toolbar_pos = new Point(8, Native.Window.Height - toolbar_size.Y - 8);
                    var toolbar_color = Color.FromRGB(0, 0x80, 0);

                    var toolbar = CreateToolbar(toolbar_pos, toolbar_size, toolbar_color);

                    Native.Window.onresize +=
                        delegate
                        {
                            toolbar.ApplyPosition();
                        };

                    toolbar.Control.AttachToDocument();

                    var toolbar_btn_pause = new ToolbarButton(
                        toolbar, "assets/ThreeDStuff/btn_pause.png"
                        );

                    toolbar_btn_pause.Clicked +=
                        btn =>
                        {
                            paused = btn.IsActivated;

                            Dudes.ForEach(i => i.Paused = paused);
                        };

                    #region toolbar_btn_demolish
                    var toolbar_btn_demolish = new ToolbarButton(
                        toolbar, "assets/ThreeDStuff/btn_demolish.png"
                    );

                    var tile_selector = new IHTMLImage(64, 32)
                        {
                            src = "assets/THreeDStuff/3.png"
                        };

                    tile_selector.style.SetLocation(4, 4);
                    tile_selector.AttachTo(arena.Layers.Canvas);
                    tile_selector.Hide();

                    Func<Point, Point<double>> GetNearestMapPosition =
                        p => GetMapPosition(p).Round().BoundTo(MapSize);



                    Func<Point<double>, IEnumerable<TileElement>> GetTileElementsAt =
                        map_coords =>
                            from i in KnownTileElements
                            where i != null
                            where i.Position.X == map_coords.X && i.Position.Y == map_coords.Y
                            select i;


                    #region AddTileElement
                    Func<Point<double>, string, int, TileElement> AddTileElement =
                        (map_coords, source, height) =>
                        {
                            var n = new TileElement
                            {
                                Position = map_coords,
                                Image = new IHTMLImage(source)
                            };

                            KnownTileElements.Add(n);
                            n.Image.AttachTo(arena.Layers.Canvas);
                            ApplyTileToCanvas(map_coords.X, map_coords.Y, n.Image, height);

                            return n;
                        };
                    #endregion
                    #region ReplaceTileWithDirt
                    Action<Point<double>> ReplaceTileWithDirt =
                        map_coords =>
                        {
                            foreach (var t in GetTileElementsAt(map_coords))
                            {
                                t.Image.Dispose();
                                KnownTileElements.Remove(t);
                            }

                            AddTileElement(map_coords, TileResources.Dirt.Source, TileResources.Dirt.Height);

                        };
                    #endregion




                    var ShowingTileSelector = default(Func<bool>);


                    #endregion

                    /*
                    var toolbar_btn_sign = new ToolbarButton(
                       toolbar, "assets/ThreeDStuff/btn_sign.png"
                    );
                    */
                    #region toolbar_btn_trees
                    var toolbar_btn_trees = new ToolbarButton(
                       toolbar, "assets/ThreeDStuff/btn_trees.png"
                    );



                    #endregion

                    /*
                    var toolbar_btn_landinfo = new ToolbarButton(
                        toolbar, "assets/ThreeDStuff/btn_landinfo.png"
                    );*/

                    ShowingTileSelector =
                        () =>
                        {
                            if (toolbar_btn_demolish.IsActivated)
                                return true;

                            if (toolbar_btn_trees.IsActivated)
                                return true;

                            return false;
                        };

                    // show tile selection
                    arena.MouseMove +=
                       p =>
                       {
                           if (ShowingTileSelector())
                           {

                               // get map coords from canvas coords
                               var map_coords = GetNearestMapPosition(p);

                               ApplyTileToCanvas(map_coords.X, map_coords.Y, tile_selector, 32);

                               // must be on top of new dirt
                               tile_selector.style.zIndex++;
                           }
                       };

                    arena.SelectionClick +=
                     (p, ev) =>
                     {
                         var map_coords = GetNearestMapPosition(p);

                         if (toolbar_btn_demolish.IsActivated)
                         {
                             ReplaceTileWithDirt(map_coords);
                         }
                         if (toolbar_btn_trees.IsActivated)
                         {
                             ReplaceTileWithDirt(map_coords);
                             AddTileElement(map_coords, TileResources.Tree.Source, TileResources.Tree.Height);
                         }
                     };

                    toolbar_btn_demolish.Clicked +=
                      delegate
                      {
                          if (toolbar_btn_trees.IsActivated)
                              toolbar_btn_trees.Counter++;


                          tile_selector.Show(ShowingTileSelector());
                      };

                    toolbar_btn_trees.Clicked +=
                        delegate
                        {
                            if (toolbar_btn_demolish.IsActivated)
                                toolbar_btn_demolish.Counter++;

                            tile_selector.Show(ShowingTileSelector());
                        };


                    #endregion


                    #region arena.SelectionClick
                    arena.SelectionClick +=
                        (p, ev) =>
                        {
                            if (paused)
                                return;

                            if (ShowingTileSelector())
                                return;

                            var selection = Dudes.Where(i => i.IsSelected).ToArray();


                            //KnownCanvasPosition = p;

                            var target = GetMapPosition(p).BoundTo(MapSize);

                            if (selection.Length == 0)
                            {
                                // single select?
                                return;
                            }


                            if (selection.Length == 1)
                            {

                                var canvas = GetCanvasPosition(target);

                                //canvas.X += GetCenter().X;
                                //canvas.Y += GetCenter().Y;

                                //new
                                //{
                                //    target = new { target.X, target.Y },
                                //    canvas = new { canvas.X, canvas.Y }
                                //}.ToConsole(); ;

                                selection.ForEach(i => i.WalkTo(
                                    new Point(canvas.X.ToInt32(), canvas.Y.ToInt32())
                                    ));
                            }
                            else
                            {
                                #region Circle

                                var center = GetCenter();


                                #region GetRotatedTargetPoint
                                Func<double, double, Point<double>> GetRotatedTargetPoint =
                                    (direction, distance) =>
                                        new Point<double>
                                        {
                                            X = target.X + (Math.Cos(direction) * distance),
                                            Y = target.Y + (Math.Sin(direction) * distance),
                                        };
                                #endregion

                                Func<Point<double>, Point> OffsetToCenter =
                                    mcanvas =>
                                        new Point
                                        {
                                            X = (mcanvas.X + center.X).ToInt32(),
                                            Y = (mcanvas.Y + center.Y).ToInt32(),
                                        };

                                var dest =
                                    from index in selection.Length.Range()
                                    let direction = (((double)index / (selection.Length)) * (Math.PI * 2)).ToConsole()
                                    let distance = 0.5
                                    let mtarget = GetRotatedTargetPoint(direction, distance)
                                    let mcanvas = Translate(mtarget.X, mtarget.Y)

                                    select new
                                    {
                                        index,
                                        canvas = OffsetToCenter(mcanvas)
                                    };

                                foreach (var v in dest)
                                {
                                    selection[v.index].WalkTo(v.canvas);
                                }

                                #endregion

                            }
                        };
                    #endregion

                    #region arena.ApplySelection
                    arena.ApplySelection +=
                        (r, ev) =>
                        {
                            if (paused)
                                return;

                            if (ShowingTileSelector())
                                return;

                            foreach (var v in Dudes)
                            {
                                if (ev.shiftKey)
                                    v.IsSelected |= r.Contains(v.CurrentLocation);
                                else
                                    v.IsSelected = r.Contains(v.CurrentLocation);
                            }
                        };
                    #endregion


                });



        }

        [Script]
        class Toolbar
        {

            public IHTMLDiv Control;
            public DragHelper Drag;
            public Color Color;

            public Point Size;

            public readonly List<ToolbarButton> Buttons = new List<ToolbarButton>();

            public void Grow()
            {
                this.Size.X = (24 * (this.Buttons.Count) + 4);

                this.Control.style.SetSize(Size.X, Size.Y);
            }

            public void ApplyPosition()
            {
                // toolbar must remain visible all times
                var pos = this.Drag.Position;

                pos.X = pos.X.Max(0);
                pos.Y = pos.Y.Max(0);

                pos.X = pos.X.Min(Native.Window.Width - (Size.X + 2));
                pos.Y = pos.Y.Min(Native.Window.Height - (Size.Y + 2));

                this.Control.style.SetLocation(pos.X, pos.Y);
            }
        }

        private static Toolbar CreateToolbar(Point toolbar_pos, Point toolbar_size, Color toolbar_color)
        {
            var t = new Toolbar
            {
                Color = toolbar_color,
                Size = toolbar_size
            };


            t.Control = new IHTMLDiv();
            t.Drag = new DragHelper(t.Control);
            t.Drag.Position = toolbar_pos;

            t.Control.style.SetLocation(t.Drag.Position.X, t.Drag.Position.Y, toolbar_size.X, toolbar_size.Y);

            SetDialogColor(t.Control, toolbar_color);
            t.Drag.Enabled = true;
            t.Drag.DragMove += t.ApplyPosition;

            return t;
        }

        [Script]
        class ToolbarButton
        {
            public IHTMLDiv Control;
            public int Counter;
            public bool IsDown;

            public bool IsActivated
            {
                get
                {
                    return Counter % 2 == 1;
                }
            }

            public ToolbarButton AttachTo(Toolbar e)
            {
                Control.AttachTo(e.Control);

                return this;
            }

            public ToolbarButton()
            {

            }

            public Toolbar Toolbar;

            public event Action<ToolbarButton> Clicked;

            public ToolbarButton(Toolbar t, string img)
            {
                this.Toolbar = t;
                this.Toolbar.Buttons.Add(this);

                var btn = this;

                btn.Control = new IHTMLDiv();
                btn.IsDown = false;
                btn.Counter = 0;

                SetDialogColor(btn.Control, t.Color);

                btn.Control.style.background = "url(" + img + ") no-repeat";
                btn.Control.style.SetLocation(2 + 24 * (this.Toolbar.Buttons.Count - 1), 8, 22, 22);


                t.Grow();

                btn.Control.onclick +=
                    ev =>
                    {
                        RaiseClicked();
                    };

                var onmouseup = default(ScriptCoreLib.Shared.EventHandler<IEvent>);


                btn.Control.onmousedown +=
                    ev =>
                    {
                        ev.StopPropagation();

                        btn.IsDown = true;
                        SetDialogColor(btn.Control, t.Color, false);

                        Native.Document.onmouseup += onmouseup;
                    };


                onmouseup =
                    ev =>
                    {
                        if (btn.IsDown)
                        {
                            ev.StopPropagation();

                            btn.IsDown = false;
                            SetDialogColor(btn.Control, t.Color, true);

                            Native.Document.onmouseup -= onmouseup;
                        }
                    };

                this.AttachTo(t);

            }

            public void RaiseClicked()
            {
                this.Counter++;

                if (Clicked != null)
                    Clicked(this);
            }
        }


        private static void SetDialogColor(IHTMLDiv toolbar, Color toolbar_color)
        {
            SetDialogColor(toolbar, toolbar_color, true);
        }

        private static void SetDialogColor(IHTMLDiv toolbar, Color toolbar_color, bool up)
        {


            if (up)
            {
                toolbar.style.backgroundColor = toolbar_color;

                var toolbar_color_light = toolbar_color.AddLum(+20);
                var toolbar_color_shadow = toolbar_color.AddLum(-20);

                toolbar.style.borderLeft = "1px solid " + toolbar_color_light;
                toolbar.style.borderTop = "1px solid " + toolbar_color_light;
                toolbar.style.borderRight = "1px solid " + toolbar_color_shadow;
                toolbar.style.borderBottom = "1px solid " + toolbar_color_shadow;
                toolbar.style.backgroundPosition = "0px 0px";
            }
            else
            {
                toolbar.style.backgroundColor = toolbar_color.AddLum(+15);

                var toolbar_color_light = toolbar_color.AddLum(+20 + 15);
                var toolbar_color_shadow = toolbar_color.AddLum(-20 + 15);

                toolbar.style.borderLeft = "1px solid " + toolbar_color_shadow;
                toolbar.style.borderTop = "1px solid " + toolbar_color_shadow;
                toolbar.style.borderRight = "1px solid " + toolbar_color_light;
                toolbar.style.borderBottom = "1px solid " + toolbar_color_light;
                toolbar.style.backgroundPosition = "1px 1px";
            }

        }

        static IsometricWithToolbar()
        {
            typeof(IsometricWithToolbar).Spawn();
        }


    }

}
