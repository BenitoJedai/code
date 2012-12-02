using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.Extensions;

namespace FlashTreasureHunt
{
    public sealed class ApplicationSprite : FlashTreasureHunt.ActionScript.FlashTreasureHunt
    {
        public const int DefaultWidth = DefaultControlWidth;
        public const int DefaultHeight = DefaultControlHeight;

        public ApplicationSprite()
        {
            this.doubleClickEnabled = true;
            this.doubleClick +=
                delegate
                {
                    this.stage.displayState = StageDisplayState.FULL_SCREEN;
                    //this.stage.displayState = StageDisplayState.FULL_SCREEN_INTERACTIVE;
                };
        }

        // internal causes a fault
        public void GoFullscreen()
        {
            //this.stage.displayState = StageDisplayState.FULL_SCREEN_INTERACTIVE;
            this.stage.displayState = StageDisplayState.FULL_SCREEN;
        }
    }
}
