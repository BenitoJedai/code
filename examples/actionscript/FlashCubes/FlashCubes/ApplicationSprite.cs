using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.Extensions;

namespace FlashCubes
{
    public sealed class ApplicationSprite : Main
    {
        public const int DefaultWidth = 800;
        public const int DefaultHeight = 600;

        public ApplicationSprite()
        {
            this.doubleClickEnabled = true;
            this.doubleClick +=
                delegate
                {
                    this.stage.SetFullscreen(true);
                };
        }

    }
}
