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

using Toolbar.JavaScript;

namespace ThreeDStuff.js
{
    using ScriptCoreLib.JavaScript.Controls.NatureBoy;
    using ScriptCoreLib.JavaScript.Controls;
    using ScriptCoreLib.JavaScript.Runtime;
    using ScriptCoreLib.JavaScript.Controls.LayeredControl;

    public enum IdleBehaviour
    {
        None,
        Look,
        Scout,
    }

    public enum TileSelectorMode
    {
        Unknown,
        Single,
        Horizontal,
        Vertical,
        HorizontalOrVertical,
        Rectangle
    }

    [ScriptApplicationEntryPoint,
        ApplicationDescription(
            Description = "With road vehciles"

        )]
    public partial class Tycoon4
    {
        [Script]
        class TileElement
        {
            public string Source;

            public Point<double> Position;
            public IHTMLImage Image;

            public int DirtAge;

            public int Height;
        }

        public Tycoon4()
        {
            Native.Document.body.style.overflow = IStyle.OverflowEnum.hidden;
            Native.Document.body.style.color = Color.White;
            Native.Document.body.style.backgroundColor = Color.Black;

            var info_text = "This example demostrates how would an isometric javascript game look like in your browser. You can see landscape from <i>Transport Tycoon</i> and the characters are from <i>Wolfenstein 3D</i> and <i>Doom</i>. Notice that the <b>game will be paused</b> as you leave this window.";

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
                Left = -8,
                Width = 16,
                Top = -8,
                Height = 16
            };

            var MapMargins = new Point
            {
                X = 4,
                Y = 4
            };

            var bg_size = new
            {
                w = (64 * Zoom).ToInt32(),
                h = (32 * Zoom).ToInt32()
            };

            Func<Point<double>> GetCenter = () => new Point<double>
            {
                X = bg_size.w * (MapSize.Width + MapMargins.X) / 2 /*Native.Window.Width / 2*/,
                Y = bg_size.h * (MapSize.Height + MapMargins.Y) / 2 /*Native.Window.Height / 2*/
            };

            var arena = new ArenaControl();

            arena.Control.AttachToDocument();

            arena.Layers.Canvas.style.backgroundColor = Color.FromRGB(0, 0, 0);


            arena.SetLocation(Rectangle.Of(0, 0, Native.Window.Width, Native.Window.Height));

            arena.SetCanvasSize(new Point(
                ((MapSize.Width + +MapMargins.X) * bg_size.w).ToInt32(),
                ((MapSize.Height + +MapMargins.Y) * bg_size.h).ToInt32()
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
                    Grass = new { Source = "assets/ThreeDStuff/0.png", Height = 32 },
                    Rocks = new { Source = "assets/ThreeDStuff/1.png", Height = 32 },
                    TileSelector = new { Source = "assets/ThreeDStuff/3.png", Height = 32 },
                    RoughLand = new { Source = "assets/ThreeDStuff/2.png", Height = 32 },
                    Dirt = new { Source = "assets/ThreeDStuff/4.png", Height = 32 },
                    DirtDirtGrass = new { Source = "assets/ThreeDStuff/5.png", Height = 32 },
                    DirtGrassGrass = new { Source = "assets/ThreeDStuff/6.png", Height = 32 },
                    Track1 = new { Source = "assets/ThreeDStuff/r2.png", Height = 32 },
                    Road2 = new { Source = "assets/ThreeDStuff/r1.png", Height = 32 },
                    Road2_Track1 = new { Source = "assets/ThreeDStuff/r3.png", Height = 32 },
                    Road1 = new { Source = "assets/ThreeDStuff/r4.png", Height = 32 },
                    Road1_Road2 = new { Source = "assets/ThreeDStuff/r5.png", Height = 32 },
                    Tree = new { Source = "assets/ThreeDStuff/t1.png", Height = 65 },
                    House1 = new { Source = "assets/ThreeDStuff/h1.png", Height = 52 },
                    House2 = new { Source = "assets/ThreeDStuff/h2.png", Height = 96 },
                    House3 = new { Source = "assets/ThreeDStuff/h3.png", Height = 50 },
                    House4 = new { Source = "assets/ThreeDStuff/h4.png", Height = 53 },
                    House5a = new { Source = "assets/ThreeDStuff/h5a.png", Height = 33 },
                    House5b = new { Source = "assets/ThreeDStuff/h5b.png", Height = 40 },
                    House5c = new { Source = "assets/ThreeDStuff/h5c.png", Height = 40 },
                    House5 = new { Source = "assets/ThreeDStuff/h5.png", Height = 40 }
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

            #region Translator
            var Translator = new CoordTranslatorBase
            {
                ConvertMapToCanvas = map_coords =>
                {
                    var canvas_coords = Translate(map_coords.X, map_coords.Y);
                    var c = GetCenter();

                    canvas_coords.X += c.X;
                    canvas_coords.Y += c.Y;

                    return canvas_coords;

                },
                ConvertCanvasToMap = canvas =>
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
                }
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
            var TileColorHouse3 = Color.FromRGB(43, 0, 0);
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
            var KnownDirtTileElements = new List<TileElement>();

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
                                Image = img,
                                Source = src()
                                //, update 
                            };

                    KnownTileElements.AddRange(tiles_query.ToArray());//.ForEach(i => i.update());
                };
            #endregion


            CreateNewItemsRandomly(4, TileColorHouse1);
            CreateNewItemsRandomly(4, TileColorHouse2);
            CreateNewItemsRandomly(5, TileColorHouse3);
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
                    SpawnItems(TileColorHouse3, () => "assets/ThreeDStuff/h3.png", 50);
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

                    var bus = new DudeAnimationInfo
                    {
                        Frames_Stand = (
                            from i in 8.ToRange()
                            let src = Assets.Tycoon_Bus + "/" + i + ".png"
                            select new FrameInfo { Source = src, Weight = 1 / 8d }
                        ).ToArray()
                    };

                    bus.Frames_Walk = new[] { bus.Frames_Stand };


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
                    var RoadVehicles = new List<Dude2>();


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
                    Func<DudeAnimationInfo, IdleBehaviour, double, Dude2> CreateDude =
                        (ani, idle, dudezoom) =>
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
                                    w2.WalkTo(GetRandomCanvasPosition().ToDouble());
                                };

                            #region WaitSomeAndGoSomeWhere
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


                                        if (idle == IdleBehaviour.Scout)
                                            GoSomeWhere(WaitSomeAndGoSomeWhere);
                                        else
                                            WaitSomeAndGoSomeWhere();
                                    }
                                );

                            // make only imps wander on their own
                            if (idle != IdleBehaviour.None)
                                WaitSomeAndGoSomeWhere();
                            #endregion

                            return w2;
                        };
                    #endregion

                    #region loaded_done
                    loaded_done +=
                        delegate
                        {

                            info.innerText = "Loading images... done";


                            info.innerHTML = "Creating dudes...";

                            8.Times(() => Dudes.Add(CreateDude(dude, IdleBehaviour.Scout, 0.5)));
                            8.Times(() => Dudes.Add(CreateDude(imp, IdleBehaviour.Look, 0.5)));



                            info.innerHTML = info_text;
                        };
                    #endregion


                    // lets build some busses, for that we need to know where are the roads
                    #region building busses
                    {
                        var KnownRoadTiles =
                            from i in KnownTileElements
                            where i.Source == TileResources.Road2.Source
                            select i;

                        var RandomizedRoadTiles = KnownRoadTiles.Randomize().ToArray();

                        var SelectedRoadTile = RandomizedRoadTiles[0];

                        var newbus = CreateDude(bus, IdleBehaviour.None, 1);

                        newbus.HasShadow = false;
                        newbus.RawWalkSpeed *= 0.01;
                        newbus.TargetLocationDistanceMultiplier = 1;

                        // we can use map coords with this actor now
                        newbus.CurrentTranslator = Translator;

                        Func<Point<double>, double, Point<double>> StartMovingFrom =
                            (StartPosition, CurrentSide) =>
                            {
                                var X = StartPosition.X + CurrentSide / 4;

                                var PosCurrent = new Point<double> { X = X, Y = StartPosition.Y };
                                var EndPosition = new Point<double> { X = StartPosition.X, Y = StartPosition.Y - 8 * CurrentSide };
                                var PosTarget = new Point<double> { X = X, Y = EndPosition.Y };

                                new
                                {
                                    from = new { PosCurrent.X, PosCurrent.Y },
                                    to = new { PosTarget.X, PosTarget.Y }
                                }.ToConsole();

                                newbus.TeleportTo(PosCurrent);
                                newbus.WalkTo(PosTarget);

                                return EndPosition;
                            };


                        var _CurrentSide = 1;
                        var _StartPosition = SelectedRoadTile.Position;
                        var _DoOrder = default(Action);

                        _DoOrder =
                            delegate
                            {
                                var _EndPosition = StartMovingFrom(_StartPosition, _CurrentSide);


                                newbus.DoneWalkingOnce +=
                                    delegate
                                    {
                                        Console.WriteLine("done walking: " + newbus.X + "; " + newbus.Y);

                                        _CurrentSide = -_CurrentSide;
                                        _StartPosition = _EndPosition;

                                        _DoOrder();
                                    };
                            };

                        _DoOrder();

                        newbus.AddTo(RoadVehicles);


                    }
                    #endregion



                    // create a draggable toolbar
                    #region creating the toolbar

                    var toolbar_color = Color.FromRGB(0, 0x80, 0);

                    var infotoolbar = ToolbarDialog.CreateToolbar(
                        new Point(64, 64),
                        new Point(200, 64), toolbar_color
                        );

                    var infotoolbar_content = new IHTMLDiv().AttachTo(infotoolbar.Control);

                    infotoolbar_content.style.SetLocation(2, 8, infotoolbar.Size.X - 6, infotoolbar.Size.Y - 12);
                    infotoolbar_content.SetDialogColor(infotoolbar.Color, false);
                    infotoolbar_content.onmousedown += Native.DisabledEventHandler;

                    Action<IStyle> SetInfoAnchorStyle =
                        style =>
                        {
                            style.display = IStyle.DisplayEnum.block;
                            style.textDecoration = "none";
                            style.color = Color.White;
                            style.textAlign = IStyle.TextAlignEnum.center;
                        };

                    new IHTMLAnchor("http://zproxy.wordpress.com", "zproxy.wordpress.com").AttachTo(infotoolbar_content).style.Aggregate(SetInfoAnchorStyle);
                    new IHTMLAnchor("http://jsc.sf.net", "jsc.sf.net").AttachTo(infotoolbar_content).style.Aggregate(SetInfoAnchorStyle);


                    var toolbar_size = new Point(96, 32);
                    var toolbar_pos = new Point(8, Native.Window.Height - toolbar_size.Y - 8);


                    var toolbar = ToolbarDialog.CreateToolbar(toolbar_pos, toolbar_size, toolbar_color);

                    Native.Window.onresize +=
                        delegate
                        {
                            infotoolbar.ApplyPosition();
                            toolbar.ApplyPosition();
                        };

                    infotoolbar.Control.Hide();
                    infotoolbar.Control.AttachToDocument();
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


                    Func<Point, Point<double>> GetNearestMapPosition =
                        p => Translator.ConvertCanvasToMap(p.ToDouble()).Round().BoundTo(MapSize);

                    #region GetNearestMapRect
                    Func<Rectangle, Rectangle> GetNearestMapRect =
                        rect =>
                        {
                            var from = GetNearestMapPosition(new Point(rect.Left, rect.Top));
                            var to = GetNearestMapPosition(new Point(rect.Right, rect.Bottom));

                            return new Rectangle
                            {
                                Left = from.X.ToInt32(),
                                Top = from.Y.ToInt32(),
                                Right = to.X.ToInt32(),
                                Bottom = to.Y.ToInt32()
                            };
                        };
                    #endregion

                    #region GetTileElementsAt
                    Func<Point<double>, IEnumerable<TileElement>> GetTileElementsAt =
                        map_coords =>
                            from i in KnownTileElements
                            where i != null
                            where i.Position.X == map_coords.X && i.Position.Y == map_coords.Y
                            select i;
                    #endregion



                    #region AddTileElement
                    Func<Point<double>, string, int, TileElement> AddTileElement =
                        (map_coords, source, height) =>
                        {
                            var n = new TileElement
                            {
                                Position = map_coords,
                                Image = new IHTMLImage(source),
                                Source = source,
                                Height = height
                            };

                            KnownTileElements.Add(n);
                            ApplyTileToCanvas(map_coords.X, map_coords.Y, n.Image, height);
                            n.Image.AttachTo(arena.Layers.Canvas);



                            return n;
                        };
                    #endregion

                    #region RemoveAllTilesAt
                    Action<Point<double>> RemoveAllTilesAt =
                        map_coords =>
                        {
                            foreach (var t in GetTileElementsAt(map_coords).ToArray())
                            {
                                t.Image.Dispose();

                                t.RemoveFrom(KnownTileElements);
                                t.RemoveFrom(KnownDirtTileElements);



                            }
                        };
                    #endregion


                    #region ReplaceTileWithDirt
                    Func<Point<double>, TileElement> ReplaceTileWithDirt =
                        map_coords =>
                        {
                            RemoveAllTilesAt(map_coords);

                            var r = AddTileElement(map_coords, TileResources.Dirt.Source, TileResources.Dirt.Height);

                            r.AddTo(KnownDirtTileElements);

                            return r;

                        };
                    #endregion

                    Func<Point<double>, TileElement> ReplaceTileWithNewBuilding =
                        map_coords =>
                        {
                            var n = ReplaceTileWithDirt(map_coords);

                            n.DirtAge = -200 - 5.Random();

                            return n;
                        };
                            
                    #region interesting predicates
                    var IsGrass =
                        new[]
                                 {
                                     TileResources.Grass.Source,
                                     TileResources.Dirt.Source,
                                     TileResources.DirtDirtGrass.Source,
                                     TileResources.DirtGrassGrass.Source,
                                     TileResources.Rocks.Source,
                                     TileResources.RoughLand.Source,
                                 }.ToEqualsAny();
                    var IsGrassStrict = TileResources.Grass.Source.ToEquals();
                    var IsRoad1 = TileResources.Road1.Source.ToEquals();
                    var IsRoad2 = TileResources.Road2.Source.ToEquals();
                    var IsTrack1 = TileResources.Track1.Source.ToEquals();
                    var IsTree = TileResources.Tree.Source.ToEquals();
                    var IsTileSelector = TileResources.TileSelector.Source.ToEquals();
                    #endregion

                    var ShowingTileSelector = default(Func<bool>);


                    #endregion


                    var toolbar_btn_track1 = new ToolbarButton(
                       toolbar, "assets/ThreeDStuff/btn_track1.png"
                    );

                    var toolbar_btn_road2 = new ToolbarButton(
                      toolbar, "assets/ThreeDStuff/btn_road2.png"
                   );

                    var toolbar_btn_road1 = new ToolbarButton(
                     toolbar, "assets/ThreeDStuff/btn_road1.png"
                    );

                    var toolbar_btn_road1_road2 = new ToolbarButton(
                     toolbar, "assets/ThreeDStuff/btn_road1_road2.png"
                    );


                    /*
                    var toolbar_btn_sign = new ToolbarButton(
                       toolbar, "assets/ThreeDStuff/btn_sign.png"
                    );
                    */

                    var toolbar_btn_trees = new ToolbarButton(
                       toolbar, "assets/ThreeDStuff/btn_trees.png"
                    );



                    var TileSelectorModes = new Dictionary<ToolbarButton, TileSelectorMode>
                    {
                        {toolbar_btn_demolish, TileSelectorMode.Rectangle},
                        {toolbar_btn_track1, TileSelectorMode.Horizontal},
                        {toolbar_btn_road2, TileSelectorMode.Vertical},
                        {toolbar_btn_road1, TileSelectorMode.Horizontal},
                        {toolbar_btn_road1_road2, TileSelectorMode.HorizontalOrVertical},
                        {toolbar_btn_trees, TileSelectorMode.Rectangle},
                    };

                    var toolbar_btngroup = new ToolbarButtonGroup
                    {
                        Buttons = TileSelectorModes.Keys.ToArray()
                    };

                    var toolbar_btn_landinfo = new ToolbarButton(
                        toolbar, "assets/ThreeDStuff/btn_landinfo.png"
                    );

                    toolbar_btn_landinfo.Clicked +=
                        btn =>
                        {
                            infotoolbar.Control.Show(btn.IsActivated);
                        };

                    ShowingTileSelector =
                        () => toolbar_btngroup.IsActivated;

                    var MultipleTileSelector = new List<TileElement>();


                    #region MultipleTileSelector_Clear
                    Action MultipleTileSelector_Clear =
                        delegate
                        {
                            // framework bug: while iterating and the collection changes an exception sould be thrown
                            foreach (var v in MultipleTileSelector.ToArray())
                            {
                                if (v.Image != null)
                                {
                                    v.Image.Dispose();
                                    v.Image = null;
                                }

                                v.RemoveFrom(KnownTileElements);
                            }


                            MultipleTileSelector.Clear();
                        };
                    #endregion

                    #region MultipleTileSelector_Add
                    Action<Point<double>> MultipleTileSelector_Add =
                         p =>
                         {
                             AddTileElement(
                                 p,
                                 TileResources.TileSelector.Source,
                                 TileResources.TileSelector.Height
                             ).AddTo(MultipleTileSelector);
                         };
                    #endregion


                    // show tile selection
                    #region arena.MouseMove
                    arena.MouseMove +=
                       p =>
                       {
                           if (paused)
                               return;

                           if (ShowingTileSelector())
                           {
                               if (arena.InSelectionMode)
                                   return;

                               // get map coords from canvas coords
                               var map_coords = GetNearestMapPosition(p);

                               if (MultipleTileSelector.Count == 1)
                               {
                                   var n = MultipleTileSelector.SingleOrDefault();

                                   ApplyTileToCanvas(map_coords.X, map_coords.Y, n.Image, n.Height);
                               }
                               else
                               {
                                   MultipleTileSelector_Clear();
                                   MultipleTileSelector_Add(map_coords);
                               }

                               // must be on top of new dirt
                               //SingleTileSelector.style.zIndex++;
                           }
                       };
                    #endregion

                    #region GetActivatedTileSelectorMode
                    Func<TileSelectorMode> GetActivatedTileSelectorMode =
                        delegate
                        {
                            if (toolbar_btngroup.ActivatedButton == null)
                                return TileSelectorMode.Unknown;

                            if (!TileSelectorModes.ContainsKey(toolbar_btngroup.ActivatedButton))
                                return TileSelectorMode.Unknown;

                            return TileSelectorModes[toolbar_btngroup.ActivatedButton];
                        };
                    #endregion


                    #region arena.SelectionPointsPreview - just apply the current TileSelectorMode
                    arena.SelectionPointsPreview +=
                        (from, to) =>
                        {
                            if (paused)
                                return;

                            if (!ShowingTileSelector())
                                return;

                            //SingleTileSelector.Hide();

                            var map_coords = new
                                {
                                    from = GetNearestMapPosition(from),
                                    to = GetNearestMapPosition(to)
                                };

                            #region CurrentMode

                            var CurrentMode = GetActivatedTileSelectorMode();

                            if (CurrentMode == TileSelectorMode.Unknown)
                                return;

                            if (CurrentMode == TileSelectorMode.HorizontalOrVertical)
                            {
                                if ((map_coords.from.X - map_coords.to.X).Abs() > (map_coords.from.Y - map_coords.to.Y).Abs())
                                    CurrentMode = TileSelectorMode.Horizontal;
                                else
                                    CurrentMode = TileSelectorMode.Vertical;
                            }

                            #endregion


                            if (CurrentMode == TileSelectorMode.Horizontal)
                            {
                                MultipleTileSelector_Clear();

                                foreach (var x in map_coords.from.X.ToInt32().RangeTo(map_coords.to.X.ToInt32()))
                                    MultipleTileSelector_Add(new Point<double> { X = x, Y = map_coords.from.Y });
                            }


                            if (CurrentMode == TileSelectorMode.Vertical)
                            {
                                MultipleTileSelector_Clear();

                                foreach (var y in map_coords.from.Y.ToInt32().RangeTo(map_coords.to.Y.ToInt32()))
                                    MultipleTileSelector_Add(new Point<double> { X = map_coords.from.X, Y = y });
                            }



                            if (CurrentMode == TileSelectorMode.Rectangle)
                            {
                                MultipleTileSelector_Clear();

                                foreach (var x in map_coords.from.X.ToInt32().RangeTo(map_coords.to.X.ToInt32()))
                                    foreach (var y in map_coords.from.Y.ToInt32().RangeTo(map_coords.to.Y.ToInt32()))
                                    {
                                        MultipleTileSelector_Add(new Point<double> { X = x, Y = y });
                                    }
                            }
                        };
                    #endregion



                    #region UseCurrentToolAt
                    Action<Point<double>, TileSelectorMode> UseCurrentToolAt =
                        (map_coords, CurrentMode) =>
                        {
                            //"UseCurrentToolAt".ToConsole();



                            var Subject = GetTileElementsAt(map_coords).ToArray();
                            var StatsQuery = Subject.Select(i => i.Source);


                            if (toolbar_btn_demolish)
                            {
                                ReplaceTileWithDirt(map_coords);
                            }

                            #region toolbar_btn_trees
                            if (toolbar_btn_trees)
                            {
                                Func<string, bool> IsOther = s =>
                                    !(IsGrass(s) || IsTileSelector(s));

                                var Stats = new
                                {
                                    GrassStrict = StatsQuery.Any(IsGrassStrict),
                                    Other = StatsQuery.Any(IsOther)
                                };

                                if (!Stats.Other)
                                {
                                    if (!Stats.GrassStrict)
                                    {
                                        RemoveAllTilesAt(map_coords);
                                        AddTileElement(map_coords, TileResources.Grass.Source, TileResources.Grass.Height);
                                    }

                                    AddTileElement(map_coords, TileResources.Tree.Source, TileResources.Tree.Height);


                                }
                            }

                            #endregion



                            #region toolbar_btn_track1
                            if (toolbar_btn_track1)
                            {
                                #region Stats

                                Func<string, bool> IsOther = s =>
                                    !(IsRoad2(s) || IsGrass(s) || IsTree(s) || IsTileSelector(s));

                                var Stats = new
                                {
                                    Grass = StatsQuery.Any(IsGrass),
                                    Road2 = StatsQuery.Any(IsRoad2),
                                    Other = StatsQuery.Any(IsOther)
                                };
                                #endregion

                                if (!Stats.Other)
                                {
                                    RemoveAllTilesAt(map_coords);

                                    if (!Stats.Road2)
                                        AddTileElement(map_coords, TileResources.Track1.Source, TileResources.Track1.Height);
                                    else
                                        AddTileElement(map_coords, TileResources.Road2_Track1.Source, TileResources.Road2_Track1.Height);
                                }
                                else
                                {
                                    // should show that red error dialog now :)
                                    "Cannot build tracks!".ToConsole();
                                    foreach (var v in Subject)
                                    {
                                        v.Source.ToConsole();
                                    }
                                }

                            }

                            #endregion

                            bool ActiveIsRoad1 = toolbar_btn_road1;
                            bool ActiveIsRoad2 = toolbar_btn_road2;

                            if (toolbar_btn_road1_road2)
                            {
                                ActiveIsRoad1 = CurrentMode == TileSelectorMode.Horizontal;
                                ActiveIsRoad2 = CurrentMode == TileSelectorMode.Vertical;
                            }



                            #region toolbar_btn_road1
                            if (ActiveIsRoad1)
                            {


                                #region Stats

                                Func<string, bool> IsOther = s =>
                                    !(IsRoad2(s) || IsTrack1(s) || IsGrass(s) || IsTree(s) || IsTileSelector(s));

                                var Stats = new
                                {
                                    Grass = StatsQuery.Any(IsGrass),
                                    Road2 = StatsQuery.Any(IsRoad2),
                                    Track1 = StatsQuery.Any(IsTrack1),
                                    Other = StatsQuery.Any(IsOther)
                                };
                                #endregion

                                if (!Stats.Other)
                                {
                                    RemoveAllTilesAt(map_coords);

                                    if (!Stats.Road2)
                                        AddTileElement(map_coords,
                                            TileResources.Road1.Source,
                                            TileResources.Road1.Height);
                                    else
                                        AddTileElement(map_coords, TileResources.Road1_Road2.Source, TileResources.Road1_Road2.Height);
                                }
                                else
                                {
                                    // should show that red error dialog now :)
                                    "Cannot build tracks!".ToConsole();

                                }

                            }

                            #endregion


                            #region toolbar_btn_road2
                            if (ActiveIsRoad2)
                            {


                                #region Stats

                                Func<string, bool> IsOther = s =>
                                    !(IsRoad1(s) || IsTrack1(s) || IsGrass(s) || IsTree(s) || IsTileSelector(s));




                                var Stats = new
                                {
                                    Grass = StatsQuery.Any(IsGrass),
                                    Road1 = StatsQuery.Any(IsRoad1),
                                    Track1 = StatsQuery.Any(IsTrack1),
                                    Other = StatsQuery.Any(IsOther)
                                };
                                #endregion

                                if (!Stats.Other)
                                {
                                    RemoveAllTilesAt(map_coords);

                                    if (Stats.Road1)
                                        AddTileElement(map_coords, TileResources.Road1_Road2.Source, TileResources.Road1_Road2.Height);
                                    else if (!Stats.Track1)
                                        AddTileElement(map_coords, TileResources.Road2.Source, TileResources.Road2.Height);
                                    else
                                        AddTileElement(map_coords, TileResources.Road2_Track1.Source, TileResources.Road2_Track1.Height);
                                }
                                else
                                {
                                    // should show that red error dialog now :)
                                    "Cannot build tracks!".ToConsole();

                                }

                            }

                            #endregion
                        };
                    #endregion

                    #region arena.ApplyPointsSelection - using current tool
                    arena.ApplyPointsSelection +=
                         (from, to, ev) =>
                         {
                             if (!ShowingTileSelector())
                                 return;

                             if (paused)
                                 return;

                             var map_coords = new
                             {
                                 from = GetNearestMapPosition(from),
                                 to = GetNearestMapPosition(to)
                             };



                             #region CurrentMode

                             var CurrentMode = GetActivatedTileSelectorMode();

                             if (CurrentMode == TileSelectorMode.Unknown)
                                 return;

                             if (CurrentMode == TileSelectorMode.HorizontalOrVertical)
                             {
                                 if ((map_coords.from.X - map_coords.to.X).Abs() > (map_coords.from.Y - map_coords.to.Y).Abs())
                                     CurrentMode = TileSelectorMode.Horizontal;
                                 else
                                     CurrentMode = TileSelectorMode.Vertical;
                             }

                             #endregion

                             Console.WriteLine("mode: " + CurrentMode);

                             if (CurrentMode == TileSelectorMode.Vertical)
                             {
                                 MultipleTileSelector_Clear();

                                 foreach (var y in map_coords.from.Y.ToInt32().RangeTo(map_coords.to.Y.ToInt32()))
                                 {
                                     UseCurrentToolAt(new Point<double> { X = map_coords.from.X, Y = y }, CurrentMode);

                                 }
                             }

                             if (CurrentMode == TileSelectorMode.Horizontal)
                             {
                                 MultipleTileSelector_Clear();

                                 foreach (var x in map_coords.from.X.ToInt32().RangeTo(map_coords.to.X.ToInt32()))
                                     UseCurrentToolAt(new Point<double> { X = x, Y = map_coords.from.Y }, CurrentMode);
                             }

                             if (CurrentMode == TileSelectorMode.Rectangle)
                             {
                                 MultipleTileSelector_Clear();

                                 foreach (var x in map_coords.from.X.ToInt32().RangeTo(map_coords.to.X.ToInt32()))
                                     foreach (var y in map_coords.from.Y.ToInt32().RangeTo(map_coords.to.Y.ToInt32()))
                                     {
                                         UseCurrentToolAt(new Point<double> { X = x, Y = y }, CurrentMode);
                                     }
                             }


                             MultipleTileSelector_Clear();
                             MultipleTileSelector_Add(map_coords.to);

                         };
                    #endregion

                    #region arena.SelectionClick - just use the current tool
                    arena.SelectionClick +=
                     (p, ev) =>
                     {
                         if (paused)
                             return;

                         if (!ShowingTileSelector())
                             return;



                         var map_coords = GetNearestMapPosition(p);

                         #region CurrentMode
                         var CurrentMode = GetActivatedTileSelectorMode();

                         if (CurrentMode == TileSelectorMode.Unknown)
                             return;

                         if (CurrentMode == TileSelectorMode.HorizontalOrVertical)
                         {
                             var map_coords0 = Translator.ConvertCanvasToMap(p.ToDouble()).Wrap(1).Abs();

                             if (map_coords0.X > map_coords0.Y)
                                 CurrentMode = TileSelectorMode.Horizontal;
                             else
                                 CurrentMode = TileSelectorMode.Vertical;
                         }
                         #endregion


                         UseCurrentToolAt(map_coords, CurrentMode);


                         MultipleTileSelector_Clear();
                         MultipleTileSelector_Add(map_coords);

                     };
                    #endregion




                    #region toolbar_btngroup.Clicked - a tool has been selected
                    toolbar_btngroup.Clicked +=
                        btn =>
                        {
                            arena.ShowSelectionRectangle = !ShowingTileSelector();

                            if (!ShowingTileSelector())
                                MultipleTileSelector_Clear();
                        };
                    #endregion



                    #endregion


                    #region arena.SelectionClick - move dudes
                    arena.SelectionClick +=
                        (p, ev) =>
                        {
                            if (paused)
                                return;

                            if (ShowingTileSelector())
                                return;

                            var selection = Dudes.Where(i => i.IsSelected).ToArray();


                            //KnownCanvasPosition = p;

                            var target = Translator.ConvertCanvasToMap(p.ToDouble()).BoundTo(MapSize);

                            if (selection.Length == 0)
                            {
                                // single select?

                                return;
                            }


                            if (selection.Length == 1)
                            {

                                var canvas = Translator.ConvertMapToCanvas(target);

                                //canvas.X += GetCenter().X;
                                //canvas.Y += GetCenter().Y;

                                //new
                                //{
                                //    target = new { target.X, target.Y },
                                //    canvas = new { canvas.X, canvas.Y }
                                //}.ToConsole(); ;

                                selection.ForEach(i => i.WalkTo(
                                    canvas
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
                                    from index in selection.Length.ToRange()
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
                                    selection[v.index].WalkTo(v.canvas.ToDouble());
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
                                    v.IsSelected |= r.Contains(v.CurrentLocation.ToInt32());
                                else
                                    v.IsSelected = r.Contains(v.CurrentLocation.ToInt32());
                            }
                        };
                    #endregion

                    #region grass growth
                    1500.AtInterval(
                        t =>
                        {
                            if (paused)
                                return;

                            "got dirt?".ToConsole();

                            #region build city
                            if (t.Counter % 3 == 0)
                            {
                                var TryBuildHereCounter = 0;

                                #region TryBuildHere
                                Func<Point<double>, bool> TryBuildHere =
                                    n =>
                                    {
                                        if (IsDefined(n.X, n.Y))
                                            if (GetTileElementsAt(n).All(i => IsGrassStrict(i.Source)))
                                            {
                                                ReplaceTileWithNewBuilding(n);

                                                TryBuildHereCounter++;

                                                return true;
                                            }

                                        return false;
                                    };
                                #endregion


                                foreach (var v in
                                            from i in KnownTileElements
                                            let Road2 = IsRoad2(i.Source)
                                            let Road1 = IsRoad1(i.Source)
                                            where Road2 || Road1
                                            where 0.1.ByChance()
                                            select new { i, Road1, Road2 }
                                        )
                                {
                                    if (TryBuildHereCounter == 2)
                                        break;

                                    if (v.Road2)
                                    {
                                        TryBuildHere(v.i.Position.Round().WithOffset(-1, 0));
                                        TryBuildHere(v.i.Position.Round().WithOffset(1, 0));
                                    }

                                    if (v.Road1)
                                    {
                                        TryBuildHere(v.i.Position.Round().WithOffset(0, -1));
                                        TryBuildHere(v.i.Position.Round().WithOffset(0, 1));
                                    }
                                }


                            }
                            #endregion

                            #region KnownDirtTileElements
                            foreach (var v in KnownDirtTileElements.ToArray())
                            {
                                new { v.DirtAge, v.Position.X, v.Position.Y }.ToConsole();

                                if (v.DirtAge == 100)
                                {
                                    v.RemoveFrom(KnownTileElements);


                                }

                                #region House5 building animation
                                if (v.Source == TileResources.House5a.Source)
                                    if (v.DirtAge > 3)
                                    {
                                        RemoveAllTilesAt(v.Position);
                                        AddTileElement(v.Position,
                                            TileResources.House5b.Source,
                                            TileResources.House5b.Height
                                            )
                                            .AddTo(KnownDirtTileElements);
                                    }

                                if (v.Source == TileResources.House5b.Source)
                                    if (v.DirtAge > 3)
                                    {
                                        RemoveAllTilesAt(v.Position);
                                        AddTileElement(v.Position,
                                            TileResources.House5c.Source,
                                            TileResources.House5c.Height
                                            )
                                            .AddTo(KnownDirtTileElements);
                                    }

                                if (v.Source == TileResources.House5c.Source)
                                    if (v.DirtAge > 3)
                                    {
                                        RemoveAllTilesAt(v.Position);
                                        AddTileElement(v.Position,
                                            TileResources.House5.Source,
                                            TileResources.House5.Height
                                            );

                                    }
                                #endregion

                                #region make that dirt grow into grass over time
                                if (v.Source == TileResources.Dirt.Source)
                                {
                                    if (v.DirtAge == -200)
                                    {
                                        RemoveAllTilesAt(v.Position);


                                        var NewHouse = new[]
                                            {
                                                TileResources.House1,
                                                TileResources.House2,
                                                TileResources.House3,
                                                TileResources.House4,
                                                TileResources.House5a,
                                                TileResources.House5a

                                            }.Random();

                                        AddTileElement(v.Position,
                                            NewHouse.Source,
                                            NewHouse.Height
                                            )
                                            .AddTo(KnownDirtTileElements);
                                    }

                                    if (v.DirtAge > 3)
                                    {
                                        RemoveAllTilesAt(v.Position);

                                        v.RemoveFrom(KnownDirtTileElements);

                                        AddTileElement(v.Position, TileResources.DirtDirtGrass.Source, TileResources.DirtDirtGrass.Height)
                                            .AddTo(KnownDirtTileElements);
                                    }
                                }

                                if (v.Source == TileResources.DirtDirtGrass.Source)
                                    if (v.DirtAge > 3)
                                    {
                                        RemoveAllTilesAt(v.Position);

                                        v.RemoveFrom(KnownDirtTileElements);

                                        AddTileElement(v.Position, TileResources.DirtGrassGrass.Source, TileResources.DirtGrassGrass.Height)
                                            .AddTo(KnownDirtTileElements);
                                    }

                                if (v.Source == TileResources.DirtGrassGrass.Source)
                                    if (v.DirtAge > 3)
                                    {
                                        RemoveAllTilesAt(v.Position);

                                        v.RemoveFrom(KnownDirtTileElements);

                                        AddTileElement(v.Position, TileResources.Grass.Source, TileResources.Grass.Height);
                                    }
                                #endregion

                                v.DirtAge++;
                            }
                            #endregion

                        }
                    );
                    #endregion

                    Native.Window.onblur +=
                        delegate
                        {
                            if (!toolbar_btn_pause.IsActivated)
                                toolbar_btn_pause.RaiseClicked();
                        };
                });



        }






        static Tycoon4()
        {
            typeof(Tycoon4).Spawn();
        }


    }

}
