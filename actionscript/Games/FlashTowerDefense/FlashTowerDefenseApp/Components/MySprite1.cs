using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.Extensions;

namespace FlashTowerDefenseApp.Components
{
    internal sealed class MySprite1 : Sprite
    {
        public MySprite1()
        {
            var g = new FlashTowerDefense.ActionScript.FlashTowerDefense();

            g.AttachTo(this);
        }

    }
}
