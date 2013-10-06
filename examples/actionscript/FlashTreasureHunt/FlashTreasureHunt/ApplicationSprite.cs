using net.hires.debug;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.Extensions;

namespace FlashTreasureHunt
{
    //[SWF(frameRate = 60)]
    public sealed class ApplicationSprite :

        FlashTreasureHunt.ActionScript.FlashTreasureHunt
    {
        public const int DefaultWidth = FlashTreasureHunt.ActionScript.FlashTreasureHunt.DefaultControlWidth;
        public const int DefaultHeight = FlashTreasureHunt.ActionScript.FlashTreasureHunt.DefaultControlHeight;

        public ApplicationSprite()
        {
            //this.doubleClickEnabled = true;
            //this.click +=
            //    e =>
            //    {
            //        e.stopImmediatePropagation();

            //        //e.stopImmediatePropagation();

            //        this.stage.fullScreenSourceRect = new ScriptCoreLib.ActionScript.flash.geom.Rectangle(
            //             0, 0, DefaultWidth, DefaultHeight
            //         );


            //        this.stage.displayState = StageDisplayState.FULL_SCREEN;
            //        //this.stage.displayState = StageDisplayState.FULL_SCREEN_INTERACTIVE;
            //    };



            // http://www.flare3d.com/support/index.php?topic=1101.0
            this.addChild(new Stats { alpha = 0.5 });

            #region FULL_SCREEN_INTERACTIVE
            this.stage.keyUp +=
                 e =>
                 {
                     if (e.keyCode == (uint)System.Windows.Forms.Keys.F11)
                     {
                         this.stage.displayState = ScriptCoreLib.ActionScript.flash.display.StageDisplayState.FULL_SCREEN_INTERACTIVE;
                     }

                     if (e.keyCode == (uint)System.Windows.Forms.Keys.F)
                     {
                         this.stage.displayState = ScriptCoreLib.ActionScript.flash.display.StageDisplayState.FULL_SCREEN_INTERACTIVE;
                     }
                 };
            #endregion
        }

        // internal causes a fault
        public void GoFullscreen()
        {
            this.stage.fullScreenSourceRect = new ScriptCoreLib.ActionScript.flash.geom.Rectangle(
                    0, 0, DefaultWidth, DefaultHeight
                );


            //this.stage.displayState = StageDisplayState.FULL_SCREEN_INTERACTIVE;
            this.stage.displayState = StageDisplayState.FULL_SCREEN;
        }
    }
}
