using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.Extensions;

namespace FlashTowerDefenseApp.Components
{
    internal sealed class MySprite1 : Sprite
    {
        public const int DefaultWidth = 560;

        public const int DefaultHeight = 480;

        public MySprite1()
        {
            // http://stackoverflow.com/questions/4003286/error-1030-stack-depth-is-unbalanced

            var g = new FlashTowerDefense.ActionScript.FlashTowerDefense();

            g.AttachTo(this);
        }

    }
}
