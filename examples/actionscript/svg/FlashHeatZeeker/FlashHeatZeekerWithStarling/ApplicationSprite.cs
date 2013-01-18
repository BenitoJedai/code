using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.BCLImplementation.GLSL;
using starling.core;
using starling.display;
using starling.text;
using starling.textures;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace FlashHeatZeekerWithStarling
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



            #region AtInitializeConsoleFormWriter
            this.AtInitializeConsoleFormWriter = (
                Action<string> Console_Write,
                Action<string> Console_WriteLine
            ) =>
            {

                try
                {
                    var w = new __OutWriter();

                    var o = Console.Out;

                    var __reentry = false;

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

                    Console.SetOut(w);

                    Console.WriteLine("flash Console.WriteLine should now appear in JavaScript form!");
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

    static class StarlingExtensions
    {
        public static T AttachTo<T>(this T e, DisplayObjectContainer x) where T : DisplayObject
        {
            x.addChild(e);

            return e;
        }
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

            // our map
            {


                // ArgumentError: Error #3683: Texture too big (max is 2048x2048).
                var bmd = new ScriptCoreLib.ActionScript.flash.display.BitmapData(2048, 2048, false, 0xB27D51);

                var tex = Texture.fromBitmapData(bmd);
                var img = new Image(tex);

                img.AttachTo(viewport_content);
            }



            KnownEmbeddedResources.Default["assets/FlashHeatZeekerWithStarling/touchdown.svg"].ToSprite().With(
                shape =>
                {
                    var bmd = new ScriptCoreLib.ActionScript.flash.display.BitmapData(400, 400, true, 0x00000000);

                    bmd.draw(shape);
                    var tex = Texture.fromBitmapData(bmd);
                    var img = new Image(tex);
                    img.x = 0;
                    img.y = 0;

                    img.AttachTo(viewport_content);
                }
            );

            KnownEmbeddedResources.Default["assets/FlashHeatZeekerWithStarling/hill0.svg"].ToSprite().With(
                shape =>
                {
                    var bmd = new ScriptCoreLib.ActionScript.flash.display.BitmapData(400, 400, true, 0x00000000);

                    bmd.draw(shape);
                    var tex = Texture.fromBitmapData(bmd);
                    var img = new Image(tex);
                    img.x = 400;
                    img.y = 400;

                    img.AttachTo(viewport_content);
                }
            );


            #region tree0
            KnownEmbeddedResources.Default["assets/FlashHeatZeekerWithStarling/tree0.svg"].ToSprite().With(
                shape =>
                {
                    var bmd = new ScriptCoreLib.ActionScript.flash.display.BitmapData(400, 400, true, 0x00000000);
                    bmd.draw(shape);
                    var tex = Texture.fromBitmapData(bmd);

                    for (int iy = 0; iy < 128; iy++)
                    {
                        {
                            var svg = new Image(tex);
                            svg.x = 400;
                            svg.y = 0;
                            svg.AttachTo(viewport_content);

                            svg.scaleX = 0.2;
                            svg.scaleY = 0.2;

                            if (iy % 3 == 0)
                                svg.y += 50;

                            if (iy % 3 == 1)
                                svg.y += 100;


                            svg.x += 15 * iy;
                        }


                        {
                            var svg = new Image(tex);
                            svg.x = 0;
                            svg.y = 400;
                            svg.AttachTo(viewport_content);

                            svg.scaleX = 0.2;
                            svg.scaleY = 0.2;

                            if (iy % 3 == 0)
                                svg.x += 50;

                            if (iy % 3 == 1)
                                svg.x += 100;


                            svg.y += 15 * iy;
                        }
                    }
                }

            );
            #endregion

            var move_speed = 0.5;

            var move_forward = 0.0;
            var move_backward = 0.0;

            var rot_left = 0.0;
            var rot_right = 0.0;

            var rot_sw = new Stopwatch();
            rot_sw.Start();

            var move_zoom = 1.0;

            ApplicationSprite.__stage.enterFrame +=
                delegate
                {
                    // which is it, do we need to zoom out or in?

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

                    // show only % of the zoom/speed boost
                    viewport_rot.scaleX = 1 + (1 - move_zoom) * 0.2;
                    viewport_rot.scaleY = 1 + (1 - move_zoom) * 0.2;

                    viewport_rot.rotation +=
                        rot_sw.ElapsedMilliseconds
                        * (1 + move_zoom)
                        * (rot_left + rot_right)
                        * (0.15).DegreesToRadians();

                    viewport_content.x -=
                        rot_sw.ElapsedMilliseconds
                        * (1 + move_zoom)
                        * (move_forward + move_backward)
                        * move_speed
                        * Math.Cos(-viewport_rot.rotation + (270).DegreesToRadians());

                    viewport_content.y -=
                       rot_sw.ElapsedMilliseconds
                        * (1 + move_zoom)
                       * (move_forward + move_backward)
                       * move_speed
                       * Math.Sin(-viewport_rot.rotation + (270).DegreesToRadians());

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
                        rot_left = 1;
                    }

                    if (e.keyCode == (uint)System.Windows.Forms.Keys.Right)
                    {
                        rot_right = -1;
                    }
                };

            ApplicationSprite.__stage.keyUp +=
              e =>
              {
                  Console.WriteLine("keyUp " + new { e.keyCode });

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
              };



            // where is our ego? center of touchdown?
            viewport_content.x = -200;
            viewport_content.y = -200;

            var info = new TextField(100, 100, "Welcome to Starling!");
            info.width = 400;

            addChild(info);

            #region viewport_loc, resize all you want
            viewport_loc.x = ApplicationSprite.__stage.stageWidth / 2;
            viewport_loc.y = ApplicationSprite.__stage.stageHeight / 2;

            ApplicationSprite.__stage.resize +=
                delegate
                {
                    viewport_loc.x = ApplicationSprite.__stage.stageWidth / 2;
                    viewport_loc.y = ApplicationSprite.__stage.stageHeight / 2;
                };
            #endregion

            var frameid = 0L;

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

                    info.text = new { fps, frameid, maxframe_elapsed }.ToString();

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
