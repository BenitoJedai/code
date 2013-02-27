using Box2D.Common.Math;
using Box2D.Dynamics;
using FlashHeatZeeker.StarlingSetup.Library;
using FlashHeatZeeker.UnitPed.Library;
using FlashHeatZeeker.UnitPedControl.Library;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.Extensions;
using starling.core;
using starling.text;
using starling.utils;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace FlashHeatZeeker.UnitPedControl
{
    [SWF(backgroundColor = 0x006D00)]
    public sealed class ApplicationSprite : Sprite
    {




        public ApplicationSprite()
        {
            this.InvokeWhenStageIsReady(
                delegate
                {
                    // http://gamua.com/starling/first-steps/
                    // http://forum.starling-framework.org/topic/starling-air-desktop-extendeddesktop-fullscreen-issue
                    Starling.handleLostContext = true;

                    var s = new Starling(
                        typeof(StarlingGameSpriteWithPedControl).ToClassToken(),
                        this.stage
                    );


                    //Starling.current.showStats

                    s.showStats = true;

                    #region atresize
                    Action atresize = delegate
                    {
                        // http://forum.starling-framework.org/topic/starling-stage-resizing

                        s.viewPort = new ScriptCoreLib.ActionScript.flash.geom.Rectangle(
                            0, 0, this.stage.stageWidth, this.stage.stageHeight
                        );

                        s.stage.stageWidth = this.stage.stageWidth;
                        s.stage.stageHeight = this.stage.stageHeight;


                        //b2stage_centerize();
                    };

                    atresize();
                    #endregion

                    StarlingGameSpriteBase.onresize =
                        yield =>
                        {
                            this.stage.resize += delegate
                            {
                                atresize();

                                yield(this.stage.stageWidth, this.stage.stageHeight);
                            };

                            yield(this.stage.stageWidth, this.stage.stageHeight);
                        };


              

                    this.stage.enterFrame +=
                        delegate
                        {
                          

                          

                            StarlingGameSpriteBase.onframe(this.stage, s);
                        };

                    s.start();

                }
            );
        }
    }
}
