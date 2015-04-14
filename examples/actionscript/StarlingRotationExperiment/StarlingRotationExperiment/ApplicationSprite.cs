using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.utils;
using ScriptCoreLib.Extensions;
using starling.core;
using starling.display;
using starling.text;
using starling.textures;
using System.Diagnostics;

namespace StarlingRotationExperiment
{
    // HD
    [SWF(frameRate = 120, width = 1280, height = 720)]
    public sealed class ApplicationSprite : ScriptCoreLib.ActionScript.flash.display.Sprite
    {
        // XAttribute via shadowdom?

        public ApplicationSprite()
        {
            // http://gamua.com/starling/first-steps/

            __stage = this.stage;

            //stage.align = ScriptCoreLib.ActionScript.flash.display.StageAlign.TOP_LEFT;
            //stage.scaleMode = ScriptCoreLib.ActionScript.flash.display.StageScaleMode.NO_SCALE;

            //var xsw = new Stopwatch();
            //xsw.Start();
            //var v = 0.0001 * xsw.ElapsedMilliseconds;


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

            var info = new TextField(100, 100, "Welcome to StarlingRotationExperiment!");
            addChild(info);

            var maxframe = new Stopwatch();
            var maxframe_elapsed = 0.0;

            // jsc analyzer, suggest net stats instead?
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

                    // Nan?
                    info.text = new { fps = ii, maxframe_elapsed, sw.ElapsedMilliseconds }.ToString();

                    //if (fps != null)
                    //    fps("" + ii);

                    ii = 0;
                    maxframe_elapsed = 0;
                    sw.Restart();
                };
            #endregion

            var xinfo = new TextField(400, 300, "Welcome to StarlingRotationExperiment!");
            var xsw = new Stopwatch();
            xsw.Start();

            var content_rot = new Sprite();

            var texture0 = Texture.fromBitmap(new ActionScript.Images.jsc());


            content_rot.rotation = 0.2;


            //var cc = 128; // 10 FPS
            var cc = 64; // 33 FPS
            // fps 42

            for (int iy = -cc; iy <= cc; iy++)
                for (int ix = -cc; ix <= cc; ix++)
                {
                    var img0 = new Image(texture0);

                    img0.scaleX = 0.5;
                    img0.scaleY = 0.5;

                    img0.x = ix * 32;
                    img0.y = iy * 32;

                    content_rot.addChild(img0);
                }


            //            StarlingRotationExperiment.ApplicationSprite+__in_Delegate__in_Method
            //System.NullReferenceException: Object reference not set to an instance of an object.
            //   at jsc.Languages.ActionScript.ActionScriptCompiler.WriteMethodCallVerified(Prestatement p, ILInstruction i, MethodBase m) in x:\jsc.internal.svn\compiler\jsc\Languages\ActionScript\ActionScriptCompiler.WriteMethodCallVerified.cs:line 99
            //   at jsc.Script.CompilerBase.WriteMethodCall(Prestatement p, ILInstruction i, MethodBase m) in x:\jsc.internal.svn\compiler\jsc\Languages\CompilerBase.cs:line 1307
            //script: error JSC1000: ActionScript : failure at starling.display.DisplayObject.add_enterFrame : Object reference not set to an instance of an object.



            // do events from flash native actually work?
            ApplicationSprite.__stage.enterFrame +=
                delegate
                {
                    // https://www.adobe.com/support/flashplayer/downloads.html#fp15

                    //content_rot.rotation += 0.0001 * xsw.ElapsedMilliseconds;


                    //var v = 0.0001 * xsw.ElapsedMilliseconds;
                    content_rot.rotation += 0.02;


                    xsw.Restart();
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


//0001 02000026 StarlingRotationExperiment.ApplicationSprite::StarlingRotationExperiment.Game+<>c__DisplayClass0_0
//script: error JSC1000: ActionScript :
// BCL needs another method, please define it.
// Cannot call type without script attribute :
// System.String for System.String Format(System.IFormatProvider, System.String, System.Object[]) used at
// <>f__AnonymousType$134$0`2.ToString at offset 002e.
// If the use of this method is intended, an implementation should be provided with the attribute [Script(Implements = typeof(...)] set.You may have mistyped it.