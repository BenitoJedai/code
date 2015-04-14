using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.ActionScript.flash.utils;
using ScriptCoreLib.Extensions;
using starling.core;
using starling.display;
using starling.text;
using starling.textures;
using System;
using System.Diagnostics;

namespace StarlingTextureScrollExperiment
{
    // HD
    [SWF(frameRate = 120, width = 1280, height = 720)]
    public sealed class ApplicationSprite : ScriptCoreLib.ActionScript.flash.display.Sprite
    {

        public ApplicationSprite()
        {
            // http://gamua.com/starling/first-steps/

            __stage = this.stage;

            //stage.align = ScriptCoreLib.ActionScript.flash.display.StageAlign.TOP_LEFT;
            //stage.scaleMode = ScriptCoreLib.ActionScript.flash.display.StageScaleMode.NO_SCALE;

            var s = new Starling(
                typeof(Game).ToClassToken(),
                this.stage
            );

            //s.enableErrorChecking = true;
            s.start();

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
        }

        public static ScriptCoreLib.ActionScript.flash.display.Stage __stage;
    }

    public class Game : Sprite
    {


        public Game()
        {

            // http://forum.starling-framework.org/topic/framerate-drops-a-lot
            // http://forum.starling-framework.org/topic/30-fps-in-chrome-but-60-fps-in-internet-explorer-huh
            // http://forum.starling-framework.org/topic/frame-rate-oddities
            // http://forums.tigsource.com/index.php?topic=23953.0
            // IE 59
            // FF 56
            // Chrome, 62? after a restart of chrome!

            var info = new TextField(100, 100, "Welcome to StarlingTextureScrollExperiment!");
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

                    //if (fps != null)
                    //    fps("" + ii);

                    ii = 0;
                    maxframe_elapsed = 0;
                    sw.Restart();
                };
            #endregion

            var xinfo = new TextField(400, 300, "Welcome to StarlingTextureScrollExperiment!");
            var xsw = new Stopwatch();
            xsw.Start();

            var content_rot = new Sprite();

            //var texture0 = Texture.fromBitmap(new ActionScript.Images.jsc128());
            var texture0 = Texture.fromBitmap(
                new ActionScript.Images.jsc128(), repeat: true
                );


            var img0 = new Image(texture0);
            // http://forum.starling-framework.org/topic/best-way-to-do-a-scroll-background

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

            // wtf?
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140504

            ///** Indicates if the texture should repeat like a wallpaper or stretch the outermost pixels.
            // *  Note: this only works in textures with sidelengths that are powers of two and 
            // *  that are not loaded from a texture atlas (i.e. no subtextures). @default false */
            //public function get repeat():Boolean { return false; }

            // X:\opensource\github\Starling-Framework\starling\src\starling\textures\ConcreteTexture.as
            //texture0.repeat = true;


            content_rot.addChild(img0);



            var scrollx = 0.0;

            // do events from flash native actually work?
            ApplicationSprite.__stage.enterFrame +=
                delegate
                {
                    content_rot.rotation += 0.0001 * xsw.ElapsedMilliseconds;

                    scrollx += 0.1 * xsw.ElapsedMilliseconds;
                    setOffset(img0, scrollx, 32);

					//xsw.Restart();
					xsw = Stopwatch.StartNew();
                };


            var loc = new Sprite();

            content_rot.addChild(xinfo);
            loc.addChild(content_rot);

            loc.x = 200;
            loc.y = 200;

            addChild(loc);
        }
    }
}
