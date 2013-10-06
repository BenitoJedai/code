using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.Extensions;

namespace FlashTowerDefenseApp.Components
{
    internal sealed class ApplicationSprite : Sprite
    {
        public const int DefaultWidth = 560;

        public const int DefaultHeight = 480;

        public ApplicationSprite()
        {
            // http://stackoverflow.com/questions/4003286/error-1030-stack-depth-is-unbalanced

            var g = new FlashTowerDefense.ActionScript.FlashTowerDefense();

            g.AttachTo(this);
        }

    }
}
