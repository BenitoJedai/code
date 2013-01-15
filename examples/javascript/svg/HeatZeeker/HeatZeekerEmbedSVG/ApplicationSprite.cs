using ScriptCoreLib;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.utils;
using ScriptCoreLib.Extensions;
using System;
using System.Diagnostics;

namespace HeatZeekerEmbedSVG
{
    [SWF(frameRate = 100)]
    public sealed class ApplicationSprite : Sprite
    {

        public ApplicationSprite()
        {
            stage.align = StageAlign.TOP_LEFT;
            stage.scaleMode = StageScaleMode.NO_SCALE;

            var fill = new Sprite().AttachTo(this);

            fill.graphics.beginFill(0xB27D51);
            fill.graphics.drawRect(0, 0, this.stage.stageWidth, this.stage.stageHeight);

            this.stage.resize +=
                delegate
                {
                    fill.graphics.beginFill(0xB27D51);
                    fill.graphics.drawRect(0, 0, this.stage.stageWidth, this.stage.stageHeight);
                };


            // http://tournasdimitrios1.wordpress.com/2010/08/27/understanding-embedding-assets-in-flex3/
            // http://www.streamhead.com/how-to-use-vector-graphics-in-flashdevelop-svg-in-flash/s

            // Flex defines imgCls as a reference to a subclass of the mx.core.SpriteAsset class, which is a subclass of the flash.display.Sprite class. Therefore, you can manipulate the image by using the methods and properties of the SpriteAsset class.

            // V:\web\HeatZeekerEmbedSVG\ApplicationSprite.as: Warning: The use of SVG has been deprecated since Flex 4. Please use FXG.
            // http://www.kongregate.com/forums/4/topics/146594
            // The links to the FXG specification no longer work and the project seems to be abandoned.

            Action<double, double> CreateUnit =
                (x, y) =>
                {
                    var unit = new Sprite().AttachTo(this);

                    var shadow = new Sprite().AttachTo(unit);

                    shadow.MoveTo(32, 32);
                    shadow.alpha = 0.3;
                    KnownEmbeddedResources.Default["assets/HeatZeekerEmbedSVG/hind0_shadow.svg"].ToSprite().AttachTo(shadow).MoveTo(-200, -200);


                    KnownEmbeddedResources.Default["assets/HeatZeekerEmbedSVG/hind0_nowings.svg"].ToSprite().AttachTo(unit).MoveTo(-200, -200);

                    var wings = new Sprite().AttachTo(unit);

                    for (int i = 0; i < 5; i++)
                    {
                        var wingloc = new Sprite().AttachTo(wings);

                        KnownEmbeddedResources.Default["assets/HeatZeekerEmbedSVG/hind0_wing1.svg"].ToSprite().AttachTo(wingloc).MoveTo(-200, -200);

                        wingloc.rotation = i * (360 / 5);
                    }


                    var t = new Timer(1000 / 30);

                    t.timer +=
                        delegate
                        {
                            wings.rotation = t.currentCount * 9;
                        };

                    t.start();

                    unit.scaleX = 0.3;
                    unit.scaleY = 0.3;

                    unit.MoveTo(x, y);
                };


            this.click +=
                e =>
                {

                    CreateUnit(e.stageX, e.stageY);
                };

            CreateUnit(200, 200);

            #region fps
            var sw = new Stopwatch();

            sw.Start();

            var ii = 0;

            this.enterFrame +=
                delegate
                {


                    if (sw.ElapsedMilliseconds < 1000)
                    {
                        ii++;
                        return;
                    }


                    if (fps != null)
                        fps("" + ii);

                    ii = 0;

                    sw.Restart();

                };
            #endregion

        }

        public event Action<string> fps;

    }


}
