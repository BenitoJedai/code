using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.BCLImplementation.GLSL;
using ScriptCoreLib.Shared.Lambda;
using starling.core;
using starling.display;
using starling.text;
using starling.textures;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using FlashHeatZeekerWithStarlingT26.ActionScript.Images;
using ScriptCoreLib.ActionScript.flash.geom;
using starling.filters;
using System.Xml.Linq;
using starling.utils;

namespace FlashHeatZeekerWithStarlingT26
{
    static class X
    {
        public static double GetLength(this __vec2 p)
        {
            return Math.Sqrt(p.x * p.x + p.y * p.y);
        }

        public static double DegreesToRadians(this double Degrees)
        {
            return (Math.PI * 2) * Degrees / 360;
        }

        public static double DegreesToRadians(this int Degrees)
        {
            return (Math.PI * 2) * Degrees / 360;
        }

        public static int RadiansToDegrees(this double Arc)
        {
            return (int)(360 * Arc / (Math.PI * 2));
        }

        public static double GetRotation(this __vec2 p)
        {
            var x = p.x;
            var y = p.y;

            const double _180 = System.Math.PI;
            const double _90 = System.Math.PI / 2;
            const double _270 = System.Math.PI * 3 / 2;

            if (x == 0)
                if (y < 0)
                    return _270;
                else if (y == 0)
                    return 0;
                else
                    return _90;

            if (y == 0)
                if (x < 0)
                    return _180;
                else
                    return 0;

            var a = System.Math.Atan(y / x);

            if (x < 0)
                a += _180;
            else if (y < 0)
                a += System.Math.PI * 2;


            return a;
        }
    }



    // HD
    [SWF(frameRate = 120, width = 1280, height = 720)]
    public sealed class ApplicationSprite : ScriptCoreLib.ActionScript.flash.display.Sprite
    {
        public ApplicationSprite()
        {
            __stage = this.stage;

            // there can only be one in this VM. :)
            __sprite = this;





            #region AtInitializeConsoleFormWriter

            var w = new __OutWriter();
            var o = Console.Out;
            var __reentry = false;

            var __buffer = new StringBuilder();

            w.AtWrite =
                x =>
                {
                    __buffer.Append(x);
                };

            w.AtWriteLine =
                x =>
                {
                    __buffer.AppendLine(x);
                };

            Console.SetOut(w);

            this.AtInitializeConsoleFormWriter = (
                Action<string> Console_Write,
                Action<string> Console_WriteLine
            ) =>
            {

                try
                {


                    w.AtWrite =
                        x =>
                        {
                            o.Write(x);

                            if (!__reentry)
                            {
                                __reentry = true;
                                Console_Write(x);
                                __reentry = false;
                            }
                        };

                    w.AtWriteLine =
                        x =>
                        {
                            o.WriteLine(x);

                            if (!__reentry)
                            {
                                __reentry = true;
                                Console_WriteLine(x);
                                __reentry = false;
                            }
                        };

                    Console.WriteLine("flash Console.WriteLine should now appear in JavaScript form!");
                    Console.WriteLine(__buffer.ToString());
                }
                catch
                {

                }
            };
            #endregion

            this.stage.keyUp +=
              e =>
              {
                  if (e.keyCode == (uint)System.Windows.Forms.Keys.F11)
                  {
                      this.stage.SetFullscreen(true);
                  }
              };



            // http://gamua.com/starling/first-steps/
            // http://forum.starling-framework.org/topic/starling-air-desktop-extendeddesktop-fullscreen-issue
            Starling.handleLostContext = true;

            var s = new Starling(
                typeof(Game).ToClassToken(),
                this.stage
            );


            s.start();

            #region resize
            // http://forum.starling-framework.org/topic/starling-stage-resizing
            this.stage.resize += delegate
            {
                // http://forum.starling-framework.org/topic/starling-stage-resizing

                s.viewPort = new ScriptCoreLib.ActionScript.flash.geom.Rectangle(
                    0, 0, this.stage.stageWidth, this.stage.stageHeight
                );

                s.stage.stageWidth = this.stage.stageWidth;
                s.stage.stageHeight = this.stage.stageHeight;
            };

            s.viewPort = new ScriptCoreLib.ActionScript.flash.geom.Rectangle(
                 0, 0, this.stage.stageWidth, this.stage.stageHeight
             );

            s.stage.stageWidth = this.stage.stageWidth;
            s.stage.stageHeight = this.stage.stageHeight;
            #endregion
        }

        public event Action<string> fps;

        public void raise_fps(string e)
        {
            if (fps != null)
                fps(e);
        }

        Action<Action<string>, Action<string>> AtInitializeConsoleFormWriter;


        #region InitializeConsoleFormWriter
        class __OutWriter : TextWriter
        {
            public Action<string> AtWrite;
            public Action<string> AtWriteLine;

            public override void Write(string value)
            {
                AtWrite(value);
            }

            public override void WriteLine(string value)
            {
                AtWriteLine(value);
            }

            public override Encoding Encoding
            {
                get { return Encoding.UTF8; }
            }
        }

        public void InitializeConsoleFormWriter(
            Action<string> Console_Write,
            Action<string> Console_WriteLine
        )
        {
            AtInitializeConsoleFormWriter(Console_Write, Console_WriteLine);
        }
        #endregion

        public static ApplicationSprite __sprite;
        public static ScriptCoreLib.ActionScript.flash.display.Stage __stage;

        public event Action<XElement> context_onmessage;
        public void context_postMessage(XElement e)
        {
            if (context_onmessage != null)
                context_onmessage(e);
        }

        public event Action<IRemoteGame> context_new_remotegame;
        public void raise_context_new_remotegame(IRemoteGame e)
        {
            if (context_new_remotegame != null)
                context_new_remotegame(e);
        }



        public event Action<XElement> game_onmessage;
        public void game_postMessage(XElement e)
        {
            if (game_onmessage != null)
                game_onmessage(e);
        }
    }

    class KineticEnergy
    {
        public DisplayObject Target;

        public __vec2 Energy;

        public int TTL;
    }


    static class StarlingExtensions
    {
        public static T AttachTo<T>(this T e, DisplayObjectContainer x) where T : DisplayObject
        {
            x.addChild(e);

            return e;
        }

        public static T Orphanize<T>(this T e) where T : DisplayObject
        {
            if (e != null)
                if (e.parent != null)
                    e.removeFromParent();


            return e;
        }

        public static T MoveTo<T>(this T e, double x, double y) where T : DisplayObject
        {
            e.x = x;
            e.y = y;


            return e;
        }


    }

    class GameMap
    {
        //    BBB GGG
        //  CCC AAA FFF
        //    DDD EEE

        public Image ground;


        public GameMap()
        {
            var bmd = new ScriptCoreLib.ActionScript.flash.display.BitmapData(4, 4, false, 0xB27D51);

            var tex = Texture.fromBitmapData(bmd);
            this.ground = new Image(tex);
            this.ground.scaleX = 512;
            this.ground.scaleY = 512;
        }

        public List<DisplayObject> doodads = new List<DisplayObject>();
        public string Name;


        public Point Location = new Point();
        public List<Point> VirtualLocations = new List<Point>();

        public bool hitTest(double hx, double hy)
        {
            var cx = ground.x + 2048;
            var cy = ground.y + 2048;

            //Console.WriteLine("hitTest " + new { this.ground.x, this.ground.y, cx, cy, hx, hy });


            //allow_unit_teleport, hittesting... { x = 880.6350114443436, y = -2026.816097834212 }
            //hitTest { x = 0, y = -4096, cx = 2048, cy = -2048, hx = 880.6350114443436, hy = -2026.816097834212 }
            //hy > cy where { hy = -2026.816097834212, cy = -2048 }
            //will NOT teleport
            //map teleport done in 1ms
            //allow_unit_teleport, hittesting... { x = 880.6350114443436, y = -2026.816097834212 }
            //hitTest { x = -2048, y = -4096, cx = 0, cy = -2048, hx = 880.6350114443436, hy = -2026.816097834212 }
            //hx > cx where { hx = 880.6350114443436, cx = 0 }
            //will NOT teleport



            if (hx < ground.x)
            {
                //Console.WriteLine("hx < ground.x where " + new { hx, ground.x });
                return false;
            }

            if (hy < ground.y)
            {
                //Console.WriteLine("hy < ground.y where " + new { hy, ground.y });
                return false;
            }

            if (hx >= cx)
            {
                //Console.WriteLine("hx > cx where " + new { hx, cx });
                return false;
            }

            if (hy >= cy)
            {
                //Console.WriteLine("hy > cy where " + new { hy, cy });
                return false;
            }

            return true;

        }
    }


    class GameUnit
    {
        public Sprite loc;
        public Sprite rot;

        public Image shape;

        public Sprite shadow_rot;

        public __vec2 prevframe_loc = new __vec2();
        public double prevframe_rot = 0;

        public Queue<Sprite> tracks = new Queue<Sprite>();

        public double rotation
        {
            get { return this.rot.rotation; }
            set
            {
                this.rot.rotation = value;

                if (this.shadow_rot != null)
                    this.shadow_rot.rotation = value;
            }
        }

        // make th unit look like team lead
        public Action AddRank;

        public Action<double> ScrollTracks;

        public DisplayObject guntower;
        public bool RemoteControlEnabled;
    }

    public class Game : Sprite
    {
        public Game()
        {
            var r = new Random();
            var networkid = r.Next();


            #region screen bg
            {
                var bmd = new ScriptCoreLib.ActionScript.flash.display.BitmapData(2048, 2048, false, 0xA26D41);
                var tex = Texture.fromBitmapData(bmd);
                var img = new Image(tex);
                addChild(img);
            }
            #endregion

            var viewport_loc = new Sprite().AttachTo(this);
            var viewport_rot = new Sprite().AttachTo(viewport_loc);
            var viewport_content = new Sprite().AttachTo(viewport_rot);

            var viewport_content_layer0_ground = new Sprite().AttachTo(viewport_content);
            var viewport_content_layer1_tracks = new Sprite().AttachTo(viewport_content);
            var viewport_content_layer2_units = new Sprite().AttachTo(viewport_content);
            var viewport_content_layer3_trees = new Sprite().AttachTo(viewport_content);
            var viewport_content_layer4_clouds = new Sprite().AttachTo(viewport_content);

            viewport_rot.scaleX = 2.0;
            viewport_rot.scaleY = 2.0;

            var mapA = new GameMap { Name = "mapA" };

            // our map A
            {
                // ArgumentError: Error #3683: Texture too big (max is 2048x2048).
                //var bmd = new ScriptCoreLib.ActionScript.flash.display.BitmapData(2048, 2048, false, 0xB27D51);

                //var tex = Texture.fromBitmapData(bmd);
                //var img = new Image(tex);

                mapA.ground.AttachTo(viewport_content_layer0_ground);
            }

            //var mapB_offset_x = -2048 / 2;
            //var mapB_offset_y = -2048;
            var mapB = new GameMap { Name = "mapB" };

            mapB.Location.x = -2048 / 2;
            mapB.Location.y = -2048;

            mapB.VirtualLocations.Add(new Point(-2048, 2048 * 2));
            mapB.VirtualLocations.Add(new Point(2048 * 1.5, 2048));



            // our map B
            {
                // ArgumentError: Error #3683: Texture too big (max is 2048x2048).
                //var bmd = new ScriptCoreLib.ActionScript.flash.display.BitmapData(2048, 2048, false, 0xB27D51);

                //var tex = Texture.fromBitmapData(bmd);
                //mapB.ground = new Image(tex);
                mapB.ground.AttachTo(viewport_content_layer0_ground);
                mapB.ground.MoveTo(mapB.Location.x, mapB.Location.y);
            }









            var mapC = new GameMap { Name = "mapC" };
            mapC.Location = new Point(-2048, 0);



            mapC.VirtualLocations.Add(new Point(2048, 2048 * 2));
            mapC.VirtualLocations.Add(new Point(2048 * 1.5, -2048));
            // our map C
            {
                //// ArgumentError: Error #3683: Texture too big (max is 2048x2048).
                //var bmd = new ScriptCoreLib.ActionScript.flash.display.BitmapData(2048, 2048, false, 0xB27D51);

                //var tex = Texture.fromBitmapData(bmd);
                //mapC.ground = new Image(tex);
                mapC.ground.AttachTo(viewport_content_layer0_ground);
                mapC.ground.MoveTo(mapC.Location.x, mapC.Location.y);
            }


            var mapD = new GameMap { Name = "mapD" };

            mapD.Location = new Point(-2048 / 2, 2048);
            mapD.VirtualLocations.Add(new Point(0, -2048 * 2));
            mapD.VirtualLocations.Add(new Point(2048 * 2, 0));




            // our map C
            {
                //// ArgumentError: Error #3683: Texture too big (max is 2048x2048).
                //var bmd = new ScriptCoreLib.ActionScript.flash.display.BitmapData(2048, 2048, false, 0xB27D51);

                //var tex = Texture.fromBitmapData(bmd);

                //mapD.ground = new Image(tex);
                mapD.ground.AttachTo(viewport_content_layer0_ground);
                mapD.ground.MoveTo(mapD.Location.x, mapD.Location.y);
            }


            var mapE = new GameMap { Name = "mapE" };

            mapE.Location = new Point(2048 / 2, 2048);
            mapE.VirtualLocations.Add(new Point(2048 * -1.5, -2048));
            mapE.VirtualLocations.Add(new Point(2048, -2048 * 2));


            // our map C
            {
                // ArgumentError: Error #3683: Texture too big (max is 2048x2048).
                //var bmd = new ScriptCoreLib.ActionScript.flash.display.BitmapData(2048, 2048, false, 0xB27D51);

                //var tex = Texture.fromBitmapData(bmd);
                //mapE.ground = new Image(tex);
                mapE.ground.AttachTo(viewport_content_layer0_ground);
                mapE.ground.MoveTo(mapE.Location.x, mapE.Location.y);
            }

            var mapF = new GameMap { Name = "mapF" };

            mapF.Location = new Point(2048, 0);
            mapF.VirtualLocations.Add(new Point(-2048, -2048 * 2));
            mapF.VirtualLocations.Add(new Point(-2048 * 1.5, 2048));


            {
                // ArgumentError: Error #3683: Texture too big (max is 2048x2048).
                //var bmd = new ScriptCoreLib.ActionScript.flash.display.BitmapData(2048, 2048, false, 0xB27D51);

                //var tex = Texture.fromBitmapData(bmd);

                //mapF.ground = new Image(tex);
                mapF.ground.AttachTo(viewport_content_layer0_ground);
                mapF.ground.MoveTo(mapF.Location.x, mapF.Location.y);
            }

            var mapG = new GameMap { Name = "mapG" };


            mapG.Location = new Point(2048 / 2, -2048);
            mapG.VirtualLocations.Add(new Point(0, 2048 * 2));
            mapG.VirtualLocations.Add(new Point(2048 * -2, 0));




            {
                // ArgumentError: Error #3683: Texture too big (max is 2048x2048).
                //var bmd = new ScriptCoreLib.ActionScript.flash.display.BitmapData(2048, 2048, false, 0xB27D51);

                //var tex = Texture.fromBitmapData(bmd);

                //mapG.ground = new Image(tex);
                mapG.ground.AttachTo(viewport_content_layer0_ground);
                mapG.ground.MoveTo(mapG.Location.x, mapG.Location.y);
            }


            var maps = new[] { mapB, mapC, mapD, mapE, mapF, mapG };


            #region new_tex
            Func<string, Texture> new_tex =
               asset =>
               {
                   var shape = KnownEmbeddedResources.Default[asset].ToSprite();
                   var bmd = new ScriptCoreLib.ActionScript.flash.display.BitmapData(400, 400, true, 0x00000000);
                   bmd.draw(shape);
                   return Texture.fromBitmapData(bmd);
               };
            #endregion

            #region new_tex_512
            Func<string, Texture> new_tex_512 =
               asset =>
               {
                   var shape = KnownEmbeddedResources.Default[asset].ToSprite();


                   var bmd = new ScriptCoreLib.ActionScript.flash.display.BitmapData(512, 512, true, 0x00000000);

                   var m = new Matrix();
                   m.scale(512 / 400.0, 512 / 400.0);

                   bmd.draw(shape, m);
                   return Texture.fromBitmapData(bmd);
               };
            #endregion




            var textures_road0 = new_tex_512("assets/FlashHeatZeekerWithStarlingT26/road0.svg");
            var textures_touchdown = new_tex_512("assets/FlashHeatZeekerWithStarlingT26/touchdown.svg");

            {
                var img = new Image(textures_touchdown);
                img.x = 128;
                img.y = 128;

                img.scaleX = 0.5;
                img.scaleY = 0.5;

                img.AttachTo(viewport_content_layer1_tracks);
            }




            var textures_tree0 = new_tex_512("assets/FlashHeatZeekerWithStarlingT26/tree0.svg");




            var textures_hill0 = new_tex_512("assets/FlashHeatZeekerWithStarlingT26/hill0.svg");

            {
                var img = new Image(textures_hill0);
                img.x = 400;
                img.y = 400;

                img.scaleX = 0.4;
                img.scaleY = 0.4;

                img.AttachTo(viewport_content_layer1_tracks);
            }

            var textures_hill1 = new_tex_512("assets/FlashHeatZeekerWithStarlingT26/hill1.svg");

            {
                var img = new Image(textures_hill1);
                img.x = 800;
                img.y = 400;

                img.scaleX = 0.4;
                img.scaleY = 0.4;

                img.AttachTo(viewport_content_layer1_tracks);
            }

            var textures_watertower0 = new_tex_512("assets/FlashHeatZeekerWithStarlingT26/watertower0.svg");


            #region new_tree
            Func<Sprite> new_tree =
                delegate
                {
                    var unit_loc = new Sprite().AttachTo(viewport_content_layer3_trees);
                    var unit_scale = new Sprite().AttachTo(unit_loc);
                    var img = new Image(textures_tree0);
                    img.x = -256;
                    img.y = -256;

                    unit_scale.scaleX = 0.15;
                    unit_scale.scaleY = 0.15;

                    img.AttachTo(unit_scale);

                    return unit_loc;
                };
            #endregion

            #region new_hill1
            Func<Sprite> new_hill1 =
                delegate
                {
                    var unit_loc = new Sprite().AttachTo(viewport_content_layer1_tracks);
                    var unit_scale = new Sprite().AttachTo(unit_loc);
                    var img = new Image(textures_hill1);
                    img.x = -256;
                    img.y = -256;

                    unit_scale.scaleX = 0.15;
                    unit_scale.scaleY = 0.15;

                    img.AttachTo(unit_scale);

                    return unit_loc;
                };
            #endregion


            #region new_watertower
            Func<Sprite> new_watertower =
                delegate
                {
                    var unit_loc = new Sprite().AttachTo(viewport_content_layer3_trees);
                    var unit_scale = new Sprite().AttachTo(unit_loc);
                    var img = new Image(textures_watertower0);
                    img.x = -256;
                    img.y = -256;

                    unit_scale.scaleX = 0.3;
                    unit_scale.scaleY = 0.3;

                    img.AttachTo(unit_scale);

                    return unit_loc;
                };
            #endregion



            new_tree().MoveTo(128 * 1, 0);
            //new_tree().MoveTo(128 * 2, 0);
            //new_tree().MoveTo(128 * 3, 0);
            new_tree().MoveTo(128 * 4, 0);
            //new_tree().MoveTo(128 * 5, 0);
            //new_tree().MoveTo(128 * 6, 0);
            new_tree().MoveTo(128 * 7, 0);






            #region new_road
            Func<GameUnit> new_road = delegate
            {
                var _loc = new Sprite().AttachTo(viewport_content_layer1_tracks);
                var _rot = new Sprite().AttachTo(_loc);
                var img = new Image(textures_road0);
                img.x = -256;
                img.y = -256;
                _rot.scaleX = 0.5;
                _rot.scaleY = 0.5;

                img.AttachTo(_rot);

                return new GameUnit { loc = _loc, rot = _rot };
            };
            #endregion

            #region build_vroad
            Action<double, double, GameMap> build_vroad = (mapx, mapy, map) =>
            {
                for (int i = 0; i < 2048; i += 256)
                {
                    var rr = new_road();

                    rr.rotation = 90.DegreesToRadians();

                    var rrx = mapx + 2024 - 128;
                    var rry = mapy + 128 + (i);

                    rr.loc.MoveTo(rrx, rry);

                    if (map != null)
                        map.doodads.Add(rr.loc);
                }
            };
            #endregion

            #region build_yroad
            Action<double, double, GameMap> build_yroad = (mapx, mapy, map) =>
            {
                for (int i = 0; i < 2048; i += 256)
                {
                    var rr = new_road();

                    rr.rotation = 90.DegreesToRadians();

                    var rrx = mapx + 2024 - 128 - i / 2;
                    var rry = mapy + 128 + (i);

                    rr.loc.MoveTo(rrx, rry);

                    if (map != null)
                        map.doodads.Add(rr.loc);
                }
            };
            #endregion


            #region build_hroad
            Action<double, double, GameMap> build_hroad = (mapx, mapy, map) =>
            {
                for (int i = 0; i < 2048; i += 256)
                {
                    var rr = new_road();

                    //rr.rotation = 90.DegreesToRadians();

                    var rry = mapy + 2024 - 128;
                    var rrx = mapx + 128 + (i);

                    rr.loc.MoveTo(rrx, rry);

                    if (map != null)
                        map.doodads.Add(rr.loc);
                }
            };
            #endregion


            #region fill_empty_map
            Action<double, double, GameMap> fill_empty_map =
                (mapx, mapy, map) =>
                {

                    #region map c grow trees in the center
                    for (int i = 0; i < 128; i++)
                    {


                        var tree = new_tree().MoveTo(
                           mapx + (r.NextDouble() * 0.8 + 0.1) * 2048,
                           mapy + (r.NextDouble() * 0.8 + 0.1) * 2048
                           );

                        if (map != null)
                            map.doodads.Add(tree);

                    }

                    for (int i = 0; i < 64; i++)
                    {




                        var hill = new_hill1().MoveTo(
                         mapx + (r.NextDouble() * 0.8 + 0.1) * 2048,
                         mapy + (r.NextDouble() * 0.8 + 0.1) * 2048
                         );

                        if (map != null)
                            map.doodads.Add(hill);
                    }
                    #endregion

                };
            #endregion


            fill_empty_map(mapB.Location.x, mapB.Location.y, mapB);
            build_vroad(mapB.Location.x, mapB.Location.y, mapB);
            build_hroad(mapB.Location.x, mapB.Location.y, mapB);

            fill_empty_map(mapC.Location.x, mapC.Location.y, mapC);

            fill_empty_map(mapD.Location.x, mapD.Location.y, mapD);
            build_yroad(mapD.Location.x, mapD.Location.y, mapD);


            fill_empty_map(mapE.Location.x, mapE.Location.y, mapE);
            fill_empty_map(mapF.Location.x, mapF.Location.y, mapF);
            fill_empty_map(mapG.Location.x, mapG.Location.y, mapG);


            #region pin_doodad
            Action<DisplayObject> pin_doodad = cloc =>
                {
                    maps.Where(k => k.hitTest(cloc.x, cloc.y)).WithEach(
                        k =>
                        {
                            k.doodads.Add(cloc);
                        }
                    );
                };
            #endregion


            pin_doodad(new_watertower().MoveTo(2048 / 2, 0));
            pin_doodad(new_watertower().MoveTo(mapB.Location.x, mapB.Location.y));
            pin_doodad(new_watertower().MoveTo(mapB.Location.x + 2048, mapB.Location.y));
            pin_doodad(new_watertower().MoveTo(mapB.Location.x, mapB.Location.y + 2048));

            pin_doodad(new_watertower().MoveTo(400, 800));
            pin_doodad(new_watertower());

            var textures_tanktrackpattern = new_tex_512("assets/FlashHeatZeekerWithStarlingT26/tanktrackpattern.svg");

            #region pin_letter
            Action<string, double, double> pin_letter =
                (texasset, texx, texy) =>
                {
                    new Image(new_tex_512(texasset)).With(
                       img =>
                       {
                           img.AttachTo(viewport_content_layer1_tracks);

                           img.MoveTo(texx + 512, texy + 512);
                           pin_doodad(img);
                       }
                   );
                };
            #endregion


            pin_letter("assets/FlashHeatZeekerWithStarlingT26/letterB.svg", mapB.Location.x, mapB.Location.y);
            pin_letter("assets/FlashHeatZeekerWithStarlingT26/letterC.svg", mapC.Location.x, mapC.Location.y);
            pin_letter("assets/FlashHeatZeekerWithStarlingT26/letterD.svg", mapD.Location.x, mapD.Location.y);
            pin_letter("assets/FlashHeatZeekerWithStarlingT26/letterE.svg", mapE.Location.x, mapE.Location.y);
            pin_letter("assets/FlashHeatZeekerWithStarlingT26/letterF.svg", mapF.Location.x, mapF.Location.y);
            pin_letter("assets/FlashHeatZeekerWithStarlingT26/letterG.svg", mapG.Location.x, mapG.Location.y);



            textures_tanktrackpattern.repeat = true;

            var textures_bullet = new_tex("assets/FlashHeatZeekerWithStarlingT26/bullet.svg");
            var textures_tracks0 = new_tex("assets/FlashHeatZeekerWithStarlingT26/tracks0.svg");
            var textures_greentank = new_tex("assets/FlashHeatZeekerWithStarlingT26/greentank.svg");
            var textures_greentank_guntower = new_tex("assets/FlashHeatZeekerWithStarlingT26/greentank_guntower.svg");
            var textures_greentank_guntower_rank = new_tex("assets/FlashHeatZeekerWithStarlingT26/greentank_guntower_rank.svg");
            var textures_greentank_shadow = new_tex("assets/FlashHeatZeekerWithStarlingT26/greentank_shadow.svg");

            GameUnit current = null;


            #region new_gameunit
            Func<GameUnit> new_gameunit =
                delegate
                {
                    var unit_loc = new Sprite().AttachTo(viewport_content_layer2_units);

                    var unit_shadow_loc = new Sprite().AttachTo(unit_loc).MoveTo(8, 8);
                    var unit_shadow_rot = new Sprite().AttachTo(unit_shadow_loc);

                    // can we have wheels?



                    var shadow_shape = new Image(textures_greentank_shadow) { x = -200, y = -200 }.AttachTo(unit_shadow_rot);
                    shadow_shape.alpha = 0.2;

                    var unit_rot = new Sprite().AttachTo(unit_loc);

                    var trackpattern = new Sprite().AttachTo(unit_rot);
                    var trackpattern_img = new Image(textures_tanktrackpattern)
                    {
                        x = -512 / 2 + 6, // got a centering issue?
                        y = -512 / 2
                    }.AttachTo(trackpattern);
                    var trackpattern_x = 0.0;

                    trackpattern.scaleX = 0.13;
                    trackpattern.scaleY = 0.18;

                    #region setOffset
                    var hRatio = 1.0;
                    var vRatio = 1.0;

                    Action<Image, double, double> setOffset = (image, xx, yy) =>
                    {
                        yy = ((yy / image.height % 1) + 1);
                        xx = ((xx / image.width % 1) + 1);
                        image.setTexCoords(0, new Point(xx, yy));
                        image.setTexCoords(1, new Point(xx + hRatio, yy));
                        image.setTexCoords(2, new Point(xx, yy + vRatio));
                        image.setTexCoords(3, new Point(xx + hRatio, yy + vRatio));

                    };
                    #endregion

                    var shape = new Image(textures_greentank) { x = -200, y = -200 }.AttachTo(unit_rot);


                    var guntower = new Sprite().AttachTo(unit_rot);


                    new Image(textures_greentank_guntower) { x = -200, y = -200 }.AttachTo(guntower);


                    return new GameUnit
                    {
                        loc = unit_loc,
                        rot = unit_rot,
                        shape = shape,
                        shadow_rot = unit_shadow_rot,

                        AddRank = delegate
                        {

                            new Image(textures_greentank_guntower_rank) { x = -200, y = -200 }.AttachTo(guntower);

                        },

                        guntower = guntower,

                        ScrollTracks = dx =>
                        {
                            trackpattern_x += dx;

                            setOffset(trackpattern_img, 0, trackpattern_x * 1.2);
                        }
                    };
                };
            #endregion

            #region robo1
            // this is where unit 1 is based on network intel

            // each unit needs a recon shadow
            var unit1_recon = new_gameunit();

            // there is a man in robo1, NPC man
            // lets remove his weapon
            unit1_recon.guntower.Orphanize();
            {
                var filter = new ColorMatrixFilter();
                filter.adjustSaturation(-1);
                unit1_recon.shape.filter = filter;
            }


            unit1_recon.loc.MoveTo(
                256, 256
            );

            #endregion


            var unit1 = new_gameunit();
            unit1.loc.MoveTo(256, 256);
            unit1.AddRank();

            var unit2 = new_gameunit();
            unit2.loc.MoveTo(200 + 400, 200 + 400);


            var unit3 = new_gameunit();
            unit3.loc.MoveTo(200 + 400 + 200, 200 + 400);

            #region robo1
            var robo1 = new_gameunit();

            // there is a man in robo1, NPC man
            // lets remove his weapon
            robo1.guntower.Orphanize();
            {
                var filter = new ColorMatrixFilter();
                filter.adjustHue(0.5);
                robo1.shape.filter = filter;
            }
            robo1.loc.MoveTo(
                mapB.Location.x + 2048 - 128,
                mapB.Location.y + 2048 - 128
            );

            robo1.rotation = 270.DegreesToRadians();



            #endregion


            var controllable = new[] { unit1, unit2, unit3 };

            current = unit1;

            #region tree0

            for (int iy = 0; iy < 128; iy++)
            {
                {
                    var svg = new Image(textures_tree0);
                    svg.x = 400;
                    svg.y = 0;
                    svg.AttachTo(viewport_content);

                    svg.scaleX = 0.15;
                    svg.scaleY = 0.15;

                    if (iy % 3 == 0)
                        svg.y += 50;

                    if (iy % 3 == 1)
                        svg.y += 100;


                    svg.x += 15 * iy;
                }


                {
                    var svg = new Image(textures_tree0);
                    svg.x = 0;
                    svg.y = 400;
                    svg.AttachTo(viewport_content);

                    svg.scaleX = 0.15;
                    svg.scaleY = 0.15;

                    if (iy % 3 == 0)
                        svg.x += 50;

                    if (iy % 3 == 1)
                        svg.x += 100;


                    svg.y += 15 * iy;
                }
            }
            #endregion

            var frameid = 0L;


            var move_speed_default = 0.09;
            var move_speed = move_speed_default;

            var move_forward = 0.0;
            var move_backward = 0.0;

            var rot_left = 0.0;
            var rot_right = 0.0;

            var rot_sw = new Stopwatch();
            rot_sw.Start();

            var move_zoom = 1.0;

            var diesel2 = KnownEmbeddedResources.Default["assets/FlashHeatZeekerWithStarlingT26/diesel4.mp3"].ToSoundAsset().ToMP3PitchLoop();


            //diesel2.Sound.vol
            var profile_map_teleportcheck = 0L;

            Func<double, double> zoomer_default = y => 1 + (1 - y) * 0.2;
            Func<double, double> zoomer = zoomer_default;


            #region lookat
            Action<double, double, double> lookat = null;

            lookat = (rot, x, y) =>
            {
                viewport_rot.rotation = -rot;

                viewport_content.x = -x;
                viewport_content.y = -y;

                var __profile_map_teleportcheck = new Stopwatch();
                __profile_map_teleportcheck.Start();

                Action later = delegate { };

                #region map_teleport
                Action<GameMap, double, double, bool> map_teleport =
                    (map, mapx, mapy, allow_unit_teleport) =>
                    {
                        var dx = mapx - map.ground.x;
                        var dy = mapy - map.ground.y;

                        if (dx == 0)
                            if (dy == 0)
                                return;

                        if (allow_unit_teleport)
                        {
                            //Console.WriteLine("allow_unit_teleport, hittesting... " + new { locx = current.loc.x, locy = current.loc.y, x, y });

                            if (map.hitTest(x, y))
                            {
                                Console.WriteLine("will teleport to " + map.Name);

                                // ourself and everybody around us?
                                current.loc.x += dx;
                                current.loc.y += dy;

                                later = delegate
                                {
                                    // infinite loop?
                                    lookat(current.rotation, current.loc.x, current.loc.y);
                                };
                            }
                            else
                            {
                                //Console.WriteLine("will NOT teleport");
                            }
                        }

                        map.ground.MoveTo(
                          map.ground.x + dx,
                          map.ground.y + dy
                        );

                        var teleportsw = new Stopwatch();

                        teleportsw.Start();

                        map.doodads.WithEach(
                            k => k.MoveTo(k.x + dx, k.y + dy)
                        );


                        teleportsw.Stop();

                        Console.WriteLine(map.Name + " teleport done in " + teleportsw.ElapsedMilliseconds + "ms");

                    };
                #endregion


                // am I in a virtual position? teleport to original
                // am I near a virtual position? show it
                // otherwise revert


                maps.WithEach(
                    map =>
                    {
                        #region in_virtual
                        var in_virtual = map.VirtualLocations.Where(
                            v =>
                            {
                                if (x < v.x)
                                    return false;

                                if (y < v.y)
                                    return false;

                                if (x >= v.x + 2048)
                                    return false;

                                if (y >= v.y + 2048)
                                    return false;

                                return true;
                            }
                        ).FirstOrDefault();

                        if (in_virtual != null)
                        {
                            map_teleport(map,
                                map.Location.x,
                                map.Location.y,
                                true
                            );
                            return;
                        }
                        #endregion

                        #region near_virtual
                        var near_virtual = map.VirtualLocations.Where(
                                v =>
                                {
                                    if (x < v.x - 1024)
                                        return false;

                                    if (y < v.y - 1024)
                                        return false;

                                    if (x >= v.x + 2048 + 1024)
                                        return false;

                                    if (y >= v.y + 2048 + 1024)
                                        return false;

                                    return true;
                                }
                            ).FirstOrDefault();

                        if (near_virtual != null)
                        {
                            map_teleport(map,
                                near_virtual.x,
                                near_virtual.y,
                                false
                            );
                            return;
                        }
                        #endregion

                        map_teleport(map,
                            map.Location.x,
                            map.Location.y,
                            false
                        );

                    }
                );

                __profile_map_teleportcheck.Stop();
                profile_map_teleportcheck += __profile_map_teleportcheck.ElapsedMilliseconds;

                later();

            };
            #endregion

            bool user_pause = false;


            var KineticEnergy = new List<KineticEnergy>();

            ApplicationSprite.__stage.enterFrame +=
                delegate
                {
                    if (user_pause)
                        return;

                    rot_sw.Stop();

                    // which is it, do we need to zoom out or in?

                    #region KineticEnergy
                    foreach (var item in KineticEnergy)
                    {
                        item.Target.With(
                            t =>
                            {
                                if (item.TTL == 0)
                                {
                                    t.Orphanize();

                                    item.Target = null;
                                    return;
                                }

                                t.x += rot_sw.ElapsedMilliseconds * item.Energy.x;
                                t.y += rot_sw.ElapsedMilliseconds * item.Energy.y;

                                item.TTL--;


                            }
                        );
                    }
                    #endregion


                    var any_movement = Math.Sign(
                        Math.Abs(move_forward)
                        + Math.Abs(move_backward)
                        + Math.Abs(rot_left)
                        + Math.Abs(rot_right)
                    ) - 0.5;

                    move_zoom +=
                        any_movement *
                          rot_sw.ElapsedMilliseconds * 0.004;

                    move_zoom = move_zoom.Max(0.0).Min(1.0);

                    diesel2.LeftVolume = 0.3 + move_zoom * 0.7;
                    diesel2.Rate = 0.9 + move_zoom;

                    // show only % of the zoom/speed boost
                    viewport_rot.scaleX = zoomer(move_zoom);
                    viewport_rot.scaleY = zoomer(move_zoom);


                    var drot = rot_sw.ElapsedMilliseconds
                        * (1 + move_zoom)
                        * (rot_left + rot_right)
                        * (Math.Abs(move_forward + move_backward).Max(0.5) * 0.09).DegreesToRadians();



                    #region remotecontrol
                    Action<GameUnit> remotecontrol =
                        c =>
                        {
                            c.rotation += drot;

                            var dx = rot_sw.ElapsedMilliseconds
                                * (1 + move_zoom)
                                * (move_forward + move_backward)
                                * move_speed
                                * Math.Cos(c.rotation + (270).DegreesToRadians());

                            var dy = rot_sw.ElapsedMilliseconds
                               * (1 + move_zoom)
                               * (move_forward + move_backward)
                               * move_speed
                               * Math.Sin(c.rotation + (270).DegreesToRadians());


                            c.ScrollTracks(
                                rot_sw.ElapsedMilliseconds
                                * (1 + move_zoom)
                               * (move_forward + move_backward)
                               * move_speed
                            );
                            var prevframe_loc = new __vec2();

                            prevframe_loc.x = (float)c.loc.x;
                            prevframe_loc.y = (float)c.loc.y;

                            c.loc.x += dx;
                            c.loc.y += dy;

                            var prevframe_loc_length = new __vec2(
                                c.prevframe_loc.x - (float)c.loc.x,
                                c.prevframe_loc.y - (float)c.loc.y
                            ).GetLength();

                            var changed_prevframe_rot =
                                Math.Abs(c.rot.rotation - c.prevframe_rot) > 25.DegreesToRadians();

                            if (prevframe_loc_length > 80 || changed_prevframe_rot)
                            {
                                // unit draws tracks..
                                c.prevframe_loc = prevframe_loc;
                                c.prevframe_rot = c.rot.rotation;

                                var unit_loc = new Sprite().AttachTo(viewport_content_layer1_tracks);
                                var unit_rot = new Sprite().AttachTo(unit_loc);

                                var img = new Image(textures_tracks0);
                                img.x = -200;
                                img.y = -200;
                                img.alpha = 0.2;

                                img.AttachTo(unit_rot);


                                unit_rot.rotation = c.rotation;
                                unit_loc.MoveTo(c.loc.x, c.loc.y);
                                // lets add our new tracks to the map, to teleport them later

                                pin_doodad(unit_loc);

                                c.tracks.Enqueue(unit_loc);

                                if (c.tracks.Count > 256)
                                    c.tracks.Dequeue().Orphanize();

                            }
                        };
                    #endregion



                    if (robo1.RemoteControlEnabled)
                    {
                        remotecontrol(robo1);


                        lookat(
                            current.rot.rotation,
                            (current.loc.x + (robo1.loc.x - current.loc.x) / 2),
                            (current.loc.y + (robo1.loc.y - current.loc.y) / 2)
                            );

                    }
                    else
                    {
                        remotecontrol(current);

                        lookat(current.rot.rotation, current.loc.x, current.loc.y);
                    }

                    rot_sw.Restart();



                };



            #region switchto
            Action<GameUnit> switchto =
                nextunit =>
                {
                    KnownEmbeddedResources.Default["assets/FlashHeatZeekerWithStarlingT26/letsgo.mp3"].ToSoundAsset().play();

                    move_zoom = 1;

                    current = nextunit;
                    lookat(current.rotation, current.loc.x, current.loc.y);
                };
            #endregion

            var postMessage_tracker = 0;
            var postMessage_xtracker = 1000;

            var sync_postMessage_for_context = new List<XElement>();

            #region postMessage
            Action<XElement> sync_postMessage =
                e =>
                {
                    postMessage_tracker++;
                    postMessage_xtracker++;

                    var ee = XElement.Parse(e.ToString());
                    ee.Add(new XAttribute("t", "" + postMessage_tracker));

                    // send it to outside world
                    //ApplicationSprite.__sprite.context_postMessage(ee);
                    sync_postMessage_for_context.Add(ee);

                    var eee = XElement.Parse(e.ToString());
                    eee.Add(new XAttribute("t", "" + postMessage_xtracker));

                    // send it to ourselves
                    ApplicationSprite.__sprite.game_postMessage(eee);
                };
            #endregion

            // script: error JSC1000: ActionScript : failure at starling.display.Stage.add_keyDown : Object reference not set to an instance of an object.
            // there is something fron with flash natives gen. need to fix that.

            var disable_keyDown_Up = false;
            var disable_keyDown_Down = false;
            var disable_keyDown_Left = false;
            var disable_keyDown_Right = false;

            #region keyDown
            ApplicationSprite.__stage.keyDown +=
                e =>
                {
                    Console.WriteLine("keyDown " + new { e.keyCode });

                    if (e.keyCode == (uint)System.Windows.Forms.Keys.Up)
                    {

                        if (!disable_keyDown_Up)
                        {
                            disable_keyDown_Up = true;

                            move_forward = 1;

                            sync_postMessage(
                                  new XElement("move_forward",
                                      new XAttribute("i", "" + networkid),
                                      new XAttribute("f", "" + (networkframe + 2))
                                  )
                            );
                        }
                    }

                    if (e.keyCode == (uint)System.Windows.Forms.Keys.Down)
                    {


                        if (!disable_keyDown_Down)
                        {
                            disable_keyDown_Down = true;

                            // move slower while backwards?
                            move_backward = -0.5;

                            sync_postMessage(
                                  new XElement("move_backward",
                                      new XAttribute("i", "" + networkid),
                                      new XAttribute("f", "" + (networkframe + 2))
                                  )
                            );
                        }
                    }

                    if (e.keyCode == (uint)System.Windows.Forms.Keys.Left)
                    {
                        if (!disable_keyDown_Left)
                        {
                            disable_keyDown_Left = true;

                            rot_left = -1;

                            sync_postMessage(
                                  new XElement("rot_left",
                                      new XAttribute("i", "" + networkid),
                                      new XAttribute("f", "" + (networkframe + 2))
                                  )
                            );
                        }
                    }

                    if (e.keyCode == (uint)System.Windows.Forms.Keys.Right)
                    {

                        if (!disable_keyDown_Right)
                        {
                            disable_keyDown_Right = true;

                            rot_right = 1;

                            sync_postMessage(
                                  new XElement("rot_right",
                                      new XAttribute("i", "" + networkid),
                                      new XAttribute("f", "" + (networkframe + 2))
                                  )
                            );
                        }
                    }


                };
            #endregion


            #region keyUp
            ApplicationSprite.__stage.keyUp +=
              e =>
              {
                  Console.WriteLine("keyUp " + new { e.keyCode });

                  if (e.keyCode == (uint)System.Windows.Forms.Keys.CapsLock)
                  {
                      robo1.RemoteControlEnabled = !robo1.RemoteControlEnabled;
                  }


                  if (e.keyCode == (uint)System.Windows.Forms.Keys.Up)
                  {
                      move_forward = 0;

                      disable_keyDown_Up = false;

                      sync_postMessage(
                           new XElement("move_forward",
                               new XAttribute("i", "" + networkid),
                               new XAttribute("f", "" + (networkframe + 2))
                           )
                     );
                  }

                  if (e.keyCode == (uint)System.Windows.Forms.Keys.Down)
                  {
                      move_backward = 0;

                      disable_keyDown_Down = false;

                      sync_postMessage(
                           new XElement("move_backward",
                               new XAttribute("i", "" + networkid),
                               new XAttribute("f", "" + (networkframe + 2))
                           )
                     );
                  }

                  if (e.keyCode == (uint)System.Windows.Forms.Keys.Left)
                  {
                      rot_left = 0;
                      disable_keyDown_Left = false;

                      sync_postMessage(
                            new XElement("rot_left",
                                new XAttribute("i", "" + networkid),
                                new XAttribute("f", "" + (networkframe + 2))
                            )
                      );
                  }

                  if (e.keyCode == (uint)System.Windows.Forms.Keys.Right)
                  {
                      rot_right = 0;
                      disable_keyDown_Right = false;

                      sync_postMessage(
                           new XElement("rot_right",
                               new XAttribute("i", "" + networkid),
                               new XAttribute("f", "" + (networkframe + 2))
                           )
                     );
                  }



                  if (e.keyCode == (uint)System.Windows.Forms.Keys.D1)
                  {
                      switchto(unit1);
                  }

                  if (e.keyCode == (uint)System.Windows.Forms.Keys.D2)
                  {
                      switchto(unit2);
                  }

                  if (e.keyCode == (uint)System.Windows.Forms.Keys.D3)
                  {
                      switchto(unit3);
                  }

                  if (e.keyCode == (uint)System.Windows.Forms.Keys.P)
                  {
                      user_pause = !user_pause;
                  }

                  if (e.keyCode == (uint)System.Windows.Forms.Keys.Tab)
                  {
                      //                      System.Linq.Enumerable for System.Collections.Generic.IEnumerable`1[FlashHeatZeekerWithStarlingT26.GameUnit] Skip[GameUnit](System.Collections.Generic.IEnumerable`1[FlashHeatZeekerWithStarlingT26.GameUnit], Int32) used at
                      //FlashHeatZeekerWithStarlingT26.Game+<>c__DisplayClass10.<.ctor>b__a at offset 014b.
                      //If the use of this method is intended, an implementation should be provided with the attribute [Script(Implements=typeof(...)] set. You may have mistyped it.



                      var nextunit = controllable.AsCyclicEnumerable().SkipWhile(k => k != current).Take(2).Last();
                      switchto(nextunit);
                  }

                  // flash fullscreen allows space, tab and arrows!
                  if (e.keyCode == (uint)System.Windows.Forms.Keys.Space)
                  {
                      Console.WriteLine("fire!");
                      // http://www.sounddogs.com/results.asp?Type=1&CategoryID=1027&SubcategoryID=11
                      KnownEmbeddedResources.Default["assets/FlashHeatZeekerWithStarlingT26/cannon1.mp3"].ToSoundAsset().play();

                      var unit_bullet = new Sprite().AttachTo(viewport_content);

                      var shape = new Image(textures_bullet) { x = -200, y = -200 }.AttachTo(unit_bullet);

                      unit_bullet.MoveTo(
                          current.loc.x + 100 * Math.Cos(current.rotation + 270.DegreesToRadians()),
                          current.loc.y + 100 * Math.Sin(current.rotation + 270.DegreesToRadians())
                      );

                      KineticEnergy.Add(
                          new FlashHeatZeekerWithStarlingT26.KineticEnergy
                          {
                              Target = unit_bullet,
                              Energy = new __vec2(
                                  (float)(2 * Math.Cos(current.rotation + 270.DegreesToRadians())),
                                  (float)(2 * Math.Sin(current.rotation + 270.DegreesToRadians()))
                              ),
                              TTL = 30
                          }
                        );

                      unit_bullet.scaleX = 0.8;
                      unit_bullet.scaleY = 0.8;
                  }

                  if (e.keyCode == 192)
                  {
                      //                      cript: error JSC1000: ActionScript :
                      //BCL needs another method, please define it.
                      //Cannot call type without script attribute :
                      //System.Delegate for Boolean op_Equality(System.Delegate, System.Delegate) used at
                      //FlashHeatZeekerWithStarlingT26.Game+<>c__DisplayClass22.<.ctor>b__19 at offset 035c.
                      //If the use of this method is intended, an implementation should be provided with the attribute [Script(Implements=typeof(...)] set. You may have mistyped it.

                      if ((object)zoomer == (object)zoomer_default)
                      {
                          zoomer = y => 0.10 + (1 - y) * 0.02;
                          move_speed = move_speed_default * 10;
                      }
                      else
                      {
                          zoomer = zoomer_default;
                          move_speed = move_speed_default;
                      }
                  }
              };
            #endregion



            // where is our ego? center of touchdown?
            switchto(unit1);

            var info = new TextField(800, 100, "Welcome to Starling!") { hAlign = HAlign.LEFT };

            addChild(info);

            var __bmd = new ScriptCoreLib.ActionScript.flash.display.BitmapData(96, 96, true, 0x00000000);
            __bmd.draw(new white_jsc());
            var __img = Texture.fromBitmapData(__bmd);

            var logo = new Image(__img) { alpha = 0.3 }.AttachTo(this);

            #region viewport_loc, resize all you want
            Action centerize = delegate
            {
                logo.MoveTo(
                    ApplicationSprite.__stage.stageWidth - 96,
                    ApplicationSprite.__stage.stageHeight - 96
                );

                viewport_loc.x = ApplicationSprite.__stage.stageWidth * 0.5;
                viewport_loc.y = ApplicationSprite.__stage.stageHeight * 0.7;
            };

            ApplicationSprite.__stage.resize +=
                delegate
                {
                    centerize();
                };

            centerize();
            #endregion


            var maxframe = new Stopwatch();
            var maxframe_elapsed = 0.0;


            #region fps
            var sw = new Stopwatch();

            sw.Start();

            var fps = 1;

            var ii = 0;

            maxframe.Start();


            var network_rx_last_second = 0;

            ApplicationSprite.__stage.enterFrame +=
                delegate
                {
                    frameid++;

                    maxframe.Stop();

                    //                    System.TimeSpan for Boolean op_GreaterThan(System.TimeSpan, System.TimeSpan) used at
                    //FlashHeatZeeker.ApplicationSprite+<>c__DisplayClass11.<.ctor>b__d at offset 001e.

                    //                TypeError: Error #1009: Cannot access a property or method of a null object reference.
                    //at FlashHeatZeeker::ApplicationSprite___c__DisplayClass11/__ctor_b__d_100663322()[U:\web\FlashHeatZeeker\ApplicationSprite___c__DisplayClass11.as:141]

                    if (maxframe.Elapsed.TotalMilliseconds > maxframe_elapsed)
                        maxframe_elapsed = maxframe.Elapsed.TotalMilliseconds;

                    ii++;

                    var now = DateTime.Now;
                    var network_rx_per_second = network_rx - network_rx_last_second;

                    info.text = new
                    {
                        networkid,
                        networkframe,
                        network_rx,
                        network_rx_per_second,
                        fps,
                        frameid,
                        maxframe_elapsed,
                        now
                        //, 
                        //profile_map_teleportcheck 
                    }.ToString();

                    if (fps < 25)
                        info.color = 0xff0000;
                    else if (fps < 30)
                        info.color = 0xaf0000;
                    else if (fps < 40)
                        info.color = 0x7f0000;
                    else
                        info.color = 0x0;

                    if (sw.ElapsedMilliseconds < 1000)
                    {
                        maxframe.Restart();
                        return;
                    }

                    fps = ii;

                    ApplicationSprite.__sprite.raise_fps("" + fps);

                    network_rx_last_second = network_rx;

                    ii = 0;
                    maxframe_elapsed = 0;
                    sw.Restart();
                };
            #endregion


            var networktimer = new ScriptCoreLib.ActionScript.flash.utils.Timer(1000 / 5);
            networktimer.timer +=
                delegate
                {
                    if (user_pause)
                        return;

                    networkframe++;




                    //what did we plan to do today, at this frame?
                    // lets look our calendar

                    networksync_actions[networkframe].With(
                        tasks =>
                        {
                            tasks();
                        }
                    );

                    // garbage collect
                    networksync_actions[networkframe] = null;

                    // how much bandwidth can we use? are we throttled if we exceed a limit per sec?

                    #region context_postMessage
                    var message = new XElement("s",
                            new XAttribute("i", "" + networkid),
                            new XAttribute("f", "" + networkframe)
                        );


                    // if we send it at once is it better?
                    foreach (var item in sync_postMessage_for_context)
                    {
                        message.Add(item);
                    }
                    sync_postMessage_for_context.Clear();

                    ApplicationSprite.__sprite.context_postMessage(message);
                    #endregion


                    this.remotegames.WithEach(
                        remotegame =>
                        {
                            if (remotegame.networkid == networkid)
                                return;

                            // if dx is more than 4 we might need to wait for the other client!
                            // they mght be on pause!

                            remotegame.RaiseTitleChange(
                                remotegame.networkid + " " + new
                                {
                                    remotegame.networkframe,
                                    remotegame.networkframe_dx,
                                    remotegame.rx_messagecount
                                    //, remotegame.initial_networkframe_delta
                                }.ToString());
                        }
                    );
                };
            networktimer.start();

            Action<XElement> __sprite_game_onmessage = null;

            __sprite_game_onmessage =
                data =>
                {
                    network_rx += data.ToString().Length;

                    var __networkid = data.Attribute("networkid");
                    if (__networkid == null)
                        __networkid = data.Attribute("i");

                    var __networkframe = data.Attribute("networkframe");
                    if (__networkframe == null)
                        __networkframe = data.Attribute("f");


                    var remotegame_networkid = int.Parse(__networkid.Value);
                    var remotegame_networkframe = int.Parse(__networkframe.Value);

                    #region remotegame networkid


                    // do we know this remote game already?

                    var remotegame = remotegames.FirstOrDefault(k => k.networkid == remotegame_networkid);

                    if (remotegame == null)
                    {
                        remotegame = new RemoteGame
                        {
                            Context = this,

                            networkid = remotegame_networkid,
                            initial_networkframe_delta = remotegame_networkframe - this.networkframe
                        };

                        remotegames.Add(remotegame);

                        // 
                        var remotegame_info = new TextField(800, 100, "") { hAlign = HAlign.LEFT }.MoveTo(
                            8, remotegames.Count * 32 + 72
                        ).AttachTo(this);


                        remotegame.AtTitleChange +=
                            e =>
                            {
                                if (remotegame.networkid == networkid)
                                {
                                    remotegame_info.color = 0xff;
                                }
                                else if (Math.Abs(remotegame.networkframe_dx) > 4)
                                {
                                    remotegame_info.color = 0xff0000;
                                }
                                else
                                {
                                    remotegame_info.color = 0;
                                }

                                remotegame_info.text = e;
                            };


                        // let context (html app) know about what we know
                        ApplicationSprite.__sprite.raise_context_new_remotegame(remotegame);
                        remotegame.RaiseTitleChange(new { remotegame.networkid }.ToString());
                    }
                    #endregion


                    remotegame.rx_messagecount++;

                    //if (sync_networkid == networkid)
                    //{
                    // we are looking at a message from ourself!

                    //if (sync.Name.LocalName == "recon_keyCode_Left")
                    //{

                    //    var tasks = networksync_actions[sync_networkframe];

                    //    tasks += delegate
                    //    {
                    //        unit1_recon.rotation -= 5.DegreesToRadians();
                    //    };

                    //    networksync_actions[sync_networkframe] = tasks;

                    //    if (sync_networkid == networkid)
                    //        return;
                    //}
                    //}

                    #region sync
                    if (data.Name.LocalName == "s")
                    {

                        // this tells us the remote game has completed this frame
                        remotegame.networkframe = remotegame_networkframe;

                        data.Elements().WithEach(
                            submessage =>
                            {
                                __sprite_game_onmessage(submessage);
                            }
                        );

                        return;
                    }
                    #endregion


                    if (data.Name.LocalName == "move_forward")
                    {
                        if (remotegame.networkid == networkid)
                        {
                            // move ghost instead
                        }
                        else
                        {
                            if (move_forward == 0)
                                move_forward = 1;
                            else
                                move_forward = 0;
                        }
                    }

                    if (data.Name.LocalName == "move_backward")
                    {
                        if (remotegame.networkid == networkid)
                        {
                            // move ghost instead
                        }
                        else
                        {
                            if (move_backward == 0)
                                move_backward = -0.5;
                            else
                                move_backward = 0;
                        }
                    }

                    if (data.Name.LocalName == "rot_left")
                    {
                        if (remotegame.networkid == networkid)
                        {
                            // move ghost instead
                        }
                        else
                        {
                            if (rot_left == 0)
                                rot_left = -1;
                            else
                                rot_left = 0;
                        }
                    }

                    if (data.Name.LocalName == "rot_right")
                    {
                        if (remotegame.networkid == networkid)
                        {
                            // move ghost instead
                        }
                        else
                        {
                            if (rot_right == 0)
                                rot_right = 1;
                            else
                                rot_right = 0;
                        }
                    }

                    // show an event we did not process and when the remote client scheduled it

                    remotegame.RaiseWriteLine(
                         data.Name.LocalName + " " + data
                     );
                };


            // preventing a bug?
            ApplicationSprite.__sprite.game_onmessage += __sprite_game_onmessage;

        }

        public int network_rx = 0;

        public int networkframe = 0;

        // world stops at frame 0xffff! should wrap actually!
        public Action[] networksync_actions = new Action[0xffff];

        public List<RemoteGame> remotegames = new List<RemoteGame>();
    }

    public interface IRemoteGame
    {
        event Action<string> AtTitleChange;
        event Action<string> AtWriteLine;
    }

    public class RemoteGame : IRemoteGame
    {
        public Game Context;

        public int networkframe_dx
        {
            get
            {
                return Context.networkframe - this.networkframe + this.initial_networkframe_delta;
            }
        }

        public int rx_messagecount;

        public int networkid { get; set; }
        public int networkframe { get; set; }

        public int initial_networkframe_delta { get; set; }

        public event Action<string> AtTitleChange;
        public void RaiseTitleChange(string e)
        {
            if (AtTitleChange != null)
                AtTitleChange(e);
        }

        public event Action<string> AtWriteLine;
        public void RaiseWriteLine(string e)
        {
            if (AtWriteLine != null)
                AtWriteLine(e);
        }
    }
}
