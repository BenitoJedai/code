using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.Extensions;
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

    public class Game : Sprite
    {
        public Game()
        {
            {
                var bmd = new ScriptCoreLib.ActionScript.flash.display.BitmapData(2048, 2048, false, 0xA26D41);
                var tex = Texture.fromBitmapData(bmd);
                var img = new Image(tex);
                addChild(img);
            }

            // our map
            {


                // ArgumentError: Error #3683: Texture too big (max is 2048x2048).
                var bmd = new ScriptCoreLib.ActionScript.flash.display.BitmapData(2048, 400, false, 0xB27D51);

                var tex = Texture.fromBitmapData(bmd);
                var img = new Image(tex);
                img.x = 100;
                img.y = 100;
                addChild(img);
            }



            KnownEmbeddedResources.Default["assets/FlashHeatZeekerWithStarling/touchdown.svg"].ToSprite().With(
                shape =>
                {
                    var bmd = new ScriptCoreLib.ActionScript.flash.display.BitmapData(400, 400, true, 0x00000000);

                    bmd.draw(shape);
                    var tex = Texture.fromBitmapData(bmd);
                    var img = new Image(tex);
                    img.x = 100;
                    img.y = 100;
                    addChild(img);
                }
            );


            KnownEmbeddedResources.Default["assets/FlashHeatZeekerWithStarling/tree0.svg"].ToSprite().With(
                shape =>
                {
                    var bmd = new ScriptCoreLib.ActionScript.flash.display.BitmapData(400, 400, true, 0x00000000);
                    bmd.draw(shape);
                    var tex = Texture.fromBitmapData(bmd);

                    for (int iy = 0; iy < 128; iy++)
                    {
                        var svg = new Image(tex);
                        svg.x = 300;
                        svg.y = 0;
                        addChild(svg);

                        svg.scaleX = 0.2;
                        svg.scaleY = 0.2;

                        if (iy % 3 == 0)
                            svg.y += 50;

                        if (iy % 3 == 1)
                            svg.y += 100;


                        svg.x += 15 * iy;
                    }
                }

            );

            // script: error JSC1000: ActionScript : failure at starling.display.Stage.add_keyDown : Object reference not set to an instance of an object.
            // there is something fron with flash natives gen. need to fix that.
            ApplicationSprite.__stage.keyDown +=
                e =>
                {
                    Console.WriteLine("keyDown " + new { e.keyCode });

                };


            var info = new TextField(100, 100, "Welcome to Starling!");
            addChild(info);


            var maxframe = new Stopwatch();
            var maxframe_elapsed = 0.0;

            #region fps
            var sw = new Stopwatch();

            sw.Start();

            var ii = 0;

            maxframe.Start();
            ApplicationSprite.__stage.enterFrame +=
                delegate
                {
                    maxframe.Stop();

                    //                    System.TimeSpan for Boolean op_GreaterThan(System.TimeSpan, System.TimeSpan) used at
                    //FlashHeatZeeker.ApplicationSprite+<>c__DisplayClass11.<.ctor>b__d at offset 001e.

                    //                TypeError: Error #1009: Cannot access a property or method of a null object reference.
                    //at FlashHeatZeeker::ApplicationSprite___c__DisplayClass11/__ctor_b__d_100663322()[U:\web\FlashHeatZeeker\ApplicationSprite___c__DisplayClass11.as:141]

                    if (maxframe.Elapsed.TotalMilliseconds > maxframe_elapsed)
                        maxframe_elapsed = maxframe.Elapsed.TotalMilliseconds;

                    if (sw.ElapsedMilliseconds < 1000)
                    {
                        ii++;

                        maxframe.Restart();

                        return;
                    }

                    info.text = new { fps = ii, maxframe_elapsed }.ToString();

                    ApplicationSprite.__sprite.raise_fps("" + ii);

                    ii = 0;
                    maxframe_elapsed = 0;
                    sw.Restart();
                };
            #endregion
        }
    }
}
