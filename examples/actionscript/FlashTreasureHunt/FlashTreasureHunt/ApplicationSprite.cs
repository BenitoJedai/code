using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.Extensions;

namespace FlashTreasureHunt
{
    public sealed class ApplicationSprite : 

        FlashTreasureHunt.ActionScript.FlashTreasureHunt
    {
        public const int DefaultWidth = FlashTreasureHunt.ActionScript.FlashTreasureHunt.DefaultControlWidth;
        public const int DefaultHeight = FlashTreasureHunt.ActionScript.FlashTreasureHunt.DefaultControlHeight;

        public ApplicationSprite()
        {
            //this.doubleClickEnabled = true;
            this.click +=
                e =>
                {
                    e.stopImmediatePropagation();

                    //e.stopImmediatePropagation();

                    this.stage.fullScreenSourceRect = new ScriptCoreLib.ActionScript.flash.geom.Rectangle(
                         0, 0, DefaultWidth, DefaultHeight
                     );


                    this.stage.displayState = StageDisplayState.FULL_SCREEN;
                    //this.stage.displayState = StageDisplayState.FULL_SCREEN_INTERACTIVE;
                };
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
