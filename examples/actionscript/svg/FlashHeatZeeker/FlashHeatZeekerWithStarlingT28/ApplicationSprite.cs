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
using FlashHeatZeekerWithStarlingT28.ActionScript.Images;
using ScriptCoreLib.ActionScript.flash.geom;
using starling.filters;

namespace FlashHeatZeekerWithStarlingT28
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

            viewport_rot.scaleX = 2.0;
            viewport_rot.scaleY = 2.0;

            // our map A
            {
                // ArgumentError: Error #3683: Texture too big (max is 2048x2048).
                var bmd = new ScriptCoreLib.ActionScript.flash.display.BitmapData(2048, 2048, false, 0xB27D51);

                var tex = Texture.fromBitmapData(bmd);
                var img = new Image(tex);

                img.AttachTo(viewport_content);
            }

            var mapB_offset_x = -2048 / 2;
            var mapB_offset_y = -2048;

            // our map B
            {
                // ArgumentError: Error #3683: Texture too big (max is 2048x2048).
                var bmd = new ScriptCoreLib.ActionScript.flash.display.BitmapData(2048, 2048, false, 0xB27D51);

                var tex = Texture.fromBitmapData(bmd);
                var img = new Image(tex);

                img.AttachTo(viewport_content);

                img.MoveTo(mapB_offset_x, mapB_offset_y);
            }





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

            var viewport_content_layer_tracks = new Sprite().AttachTo(viewport_content);
            var viewport_content_layer_units = new Sprite().AttachTo(viewport_content);
            var viewport_content_layer_trees = new Sprite().AttachTo(viewport_content);


            var textures_road0 = new_tex_512("assets/FlashHeatZeekerWithStarlingT28/road0.svg");
            var textures_touchdown = new_tex_512("assets/FlashHeatZeekerWithStarlingT28/touchdown.svg");

            {
                var img = new Image(textures_touchdown);
                img.x = 128;
                img.y = 128;

                img.scaleX = 0.5;
                img.scaleY = 0.5;

                img.AttachTo(viewport_content_layer_tracks);
            }




            var textures_tree0 = new_tex_512("assets/FlashHeatZeekerWithStarlingT28/tree0.svg");




            var textures_hill0 = new_tex_512("assets/FlashHeatZeekerWithStarlingT28/hill0.svg");

            {
                var img = new Image(textures_hill0);
                img.x = 400;
                img.y = 400;

                img.scaleX = 0.4;
                img.scaleY = 0.4;

                img.AttachTo(viewport_content_layer_tracks);
            }

            var textures_hill1 = new_tex_512("assets/FlashHeatZeekerWithStarlingT28/hill1.svg");

            {
                var img = new Image(textures_hill1);
                img.x = 800;
                img.y = 400;

                img.scaleX = 0.4;
                img.scaleY = 0.4;

                img.AttachTo(viewport_content_layer_tracks);
            }

            var textures_watertower0 = new_tex_512("assets/FlashHeatZeekerWithStarlingT28/watertower0.svg");


            #region new_tree
            Func<Sprite> new_tree =
                delegate
                {
                    var unit_loc = new Sprite().AttachTo(viewport_content_layer_trees);
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


            #region new_watertower
            Func<Sprite> new_watertower =
                delegate
                {
                    var unit_loc = new Sprite().AttachTo(viewport_content);
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


            new_watertower().MoveTo(400, 800);

            new_watertower();

            new_tree().MoveTo(128 * 1, 0);
            //new_tree().MoveTo(128 * 2, 0);
            //new_tree().MoveTo(128 * 3, 0);
            new_tree().MoveTo(128 * 4, 0);
            //new_tree().MoveTo(128 * 5, 0);
            //new_tree().MoveTo(128 * 6, 0);
            new_tree().MoveTo(128 * 7, 0);


            var r = new Random();

            // grow trees in the center
            for (int i = 0; i < 1024; i++)
            {


                new_tree().MoveTo(
                   mapB_offset_x + (r.NextDouble() * 0.8 + 0.1) * 2048,
                   mapB_offset_y + (r.NextDouble() * 0.8 + 0.1) * 2048
                   );
            }

            for (int i = 0; i < 2048; i += 256)
            {


                var img = new Image(textures_road0);
                img.x = mapB_offset_x + i;
                img.y = -256;

                img.scaleX = 0.5;
                img.scaleY = 0.5;

                img.AttachTo(viewport_content_layer_tracks);
            }

            #region new_road
            Func<GameUnit> new_road = delegate
            {
                var _loc = new Sprite().AttachTo(viewport_content_layer_tracks);
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



            Console.WriteLine("will build vroad");
            for (int i = 0; i < 2048; i += 256)
            {
                var rr = new_road();

                rr.rotation = 90.DegreesToRadians();

                var rrx = mapB_offset_x + 2024 - 128;
                var rry = mapB_offset_y + 128 + (i);

                Console.WriteLine("vroad " + new { i, rrx, rry });
                rr.loc.MoveTo(rrx, rry);
            }
            Console.WriteLine("will build vroad. done.");

            new_watertower().MoveTo(2048 / 2, 0);
            new_watertower().MoveTo(mapB_offset_x, mapB_offset_y);
            new_watertower().MoveTo(mapB_offset_x + 2048, mapB_offset_y);

            var textures_tanktrackpattern = new_tex_512("assets/FlashHeatZeekerWithStarlingT28/tanktrackpattern.svg");
            textures_tanktrackpattern.repeat = true;

            var textures_bullet = new_tex("assets/FlashHeatZeekerWithStarlingT28/bullet.svg");
            var textures_tracks0 = new_tex("assets/FlashHeatZeekerWithStarlingT28/tracks0.svg");
            var textures_greentank = new_tex("assets/FlashHeatZeekerWithStarlingT28/greentank.svg");
            var textures_greentank_guntower = new_tex("assets/FlashHeatZeekerWithStarlingT28/greentank_guntower.svg");
            var textures_greentank_guntower_rank = new_tex("assets/FlashHeatZeekerWithStarlingT28/greentank_guntower_rank.svg");
            var textures_greentank_shadow = new_tex("assets/FlashHeatZeekerWithStarlingT28/greentank_shadow.svg");

            GameUnit current = null;


            #region new_gameunit
            Func<GameUnit> new_gameunit =
                delegate
                {
                    var unit_loc = new Sprite().AttachTo(viewport_content_layer_units);

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
            var filter = new ColorMatrixFilter();
            filter.adjustHue(0.5);
            robo1.shape.filter = filter;

            robo1.loc.MoveTo(
                mapB_offset_x + 2048 - 128,
                mapB_offset_y + 2048 - 128
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


            var move_speed = 0.09;

            var move_forward = 0.0;
            var move_backward = 0.0;

            var rot_left = 0.0;
            var rot_right = 0.0;

            var rot_sw = new Stopwatch();
            rot_sw.Start();

            var move_zoom = 1.0;

            var diesel2 = KnownEmbeddedResources.Default["assets/FlashHeatZeekerWithStarlingT28/diesel4.mp3"].ToSoundAsset().ToMP3PitchLoop();


            //diesel2.Sound.vol

            Func<double, double> zoomer_default = y => 1 + (1 - y) * 0.2;
            Func<double, double> zoomer = zoomer_default;


            var KineticEnergy = new List<KineticEnergy>();

            ApplicationSprite.__stage.enterFrame +=
                delegate
                {
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

                                var unit_loc = new Sprite().AttachTo(viewport_content_layer_tracks);
                                var unit_rot = new Sprite().AttachTo(unit_loc);

                                var img = new Image(textures_tracks0);
                                img.x = -200;
                                img.y = -200;
                                img.alpha = 0.2;

                                img.AttachTo(unit_rot);


                                unit_rot.rotation = c.rotation;
                                unit_loc.MoveTo(c.loc.x, c.loc.y);

                                c.tracks.Enqueue(unit_loc);

                                if (c.tracks.Count > 96)
                                    c.tracks.Dequeue().Orphanize();

                            }
                        };
                    #endregion



                    if (robo1.RemoteControlEnabled)
                    {
                        remotecontrol(robo1);


                        viewport_rot.rotation = -current.rot.rotation;

                        viewport_content.x = -(current.loc.x + (robo1.loc.x - current.loc.x) / 2);
                        viewport_content.y = -(current.loc.y + (robo1.loc.y - current.loc.y) / 2);
                    }
                    else
                    {
                        remotecontrol(current);


                        viewport_rot.rotation = -current.rot.rotation;

                        viewport_content.x = -current.loc.x;
                        viewport_content.y = -current.loc.y;
                    }

                    rot_sw.Restart();



                };

            // script: error JSC1000: ActionScript : failure at starling.display.Stage.add_keyDown : Object reference not set to an instance of an object.
            // there is something fron with flash natives gen. need to fix that.
            ApplicationSprite.__stage.keyDown +=
                e =>
                {
                    Console.WriteLine("keyDown " + new { e.keyCode });

                    if (e.keyCode == (uint)System.Windows.Forms.Keys.Up)
                    {
                        move_forward = 1;
                    }

                    if (e.keyCode == (uint)System.Windows.Forms.Keys.Down)
                    {
                        // move slower while backwards?
                        move_backward = -0.5;
                    }

                    if (e.keyCode == (uint)System.Windows.Forms.Keys.Left)
                    {
                        rot_left = -1;
                    }

                    if (e.keyCode == (uint)System.Windows.Forms.Keys.Right)
                    {
                        rot_right = 1;
                    }


                };

            #region switchto
            Action<GameUnit> switchto =
                nextunit =>
                {
                    KnownEmbeddedResources.Default["assets/FlashHeatZeekerWithStarlingT28/letsgo.mp3"].ToSoundAsset().play();

                    move_zoom = 1;

                    current = nextunit;

                    viewport_rot.rotation = -current.rot.rotation;

                    viewport_content.x = -current.loc.x;
                    viewport_content.y = -current.loc.y;
                };
            #endregion

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
                  }

                  if (e.keyCode == (uint)System.Windows.Forms.Keys.Down)
                  {
                      move_backward = 0;
                  }

                  if (e.keyCode == (uint)System.Windows.Forms.Keys.Left)
                  {
                      rot_left = 0;
                  }

                  if (e.keyCode == (uint)System.Windows.Forms.Keys.Right)
                  {
                      rot_right = 0;
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

                  if (e.keyCode == (uint)System.Windows.Forms.Keys.Tab)
                  {
                      //                      System.Linq.Enumerable for System.Collections.Generic.IEnumerable`1[FlashHeatZeekerWithStarlingT28.GameUnit] Skip[GameUnit](System.Collections.Generic.IEnumerable`1[FlashHeatZeekerWithStarlingT28.GameUnit], Int32) used at
                      //FlashHeatZeekerWithStarlingT28.Game+<>c__DisplayClass10.<.ctor>b__a at offset 014b.
                      //If the use of this method is intended, an implementation should be provided with the attribute [Script(Implements=typeof(...)] set. You may have mistyped it.



                      var nextunit = controllable.AsCyclicEnumerable().SkipWhile(k => k != current).Take(2).Last();
                      switchto(nextunit);
                  }

                  if (e.keyCode == (uint)System.Windows.Forms.Keys.ControlKey)
                  {
                      Console.WriteLine("fire!");
                      // http://www.sounddogs.com/results.asp?Type=1&CategoryID=1027&SubcategoryID=11
                      KnownEmbeddedResources.Default["assets/FlashHeatZeekerWithStarlingT28/cannon1.mp3"].ToSoundAsset().play();

                      var unit_bullet = new Sprite().AttachTo(viewport_content);

                      var shape = new Image(textures_bullet) { x = -200, y = -200 }.AttachTo(unit_bullet);

                      unit_bullet.MoveTo(
                          current.loc.x + 100 * Math.Cos(current.rotation + 270.DegreesToRadians()),
                          current.loc.y + 100 * Math.Sin(current.rotation + 270.DegreesToRadians())
                      );

                      KineticEnergy.Add(
                          new FlashHeatZeekerWithStarlingT28.KineticEnergy
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
                      //FlashHeatZeekerWithStarlingT28.Game+<>c__DisplayClass22.<.ctor>b__19 at offset 035c.
                      //If the use of this method is intended, an implementation should be provided with the attribute [Script(Implements=typeof(...)] set. You may have mistyped it.

                      if ((object)zoomer == (object)zoomer_default)
                      {
                          zoomer = y => 0.10 + (1 - y) * 0.02;
                      }
                      else
                      {
                          zoomer = zoomer_default;
                      }
                  }
              };



            // where is our ego? center of touchdown?
            switchto(unit1);

            var info = new TextField(100, 100, "Welcome to Starling!");
            info.width = 400;

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

                    info.text = new { fps, frameid, maxframe_elapsed, now }.ToString();

                    if (sw.ElapsedMilliseconds < 1000)
                    {
                        maxframe.Restart();
                        return;
                    }

                    fps = ii;

                    ApplicationSprite.__sprite.raise_fps("" + fps);


                    ii = 0;
                    maxframe_elapsed = 0;
                    sw.Restart();
                };
            #endregion




        }
    }
}
