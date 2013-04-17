using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.Extensions;

namespace net.hires.debug
{
    public sealed class ApplicationSprite : Sprite
    {
        public ApplicationSprite()
        {

            // http://www.flare3d.com/support/index.php?topic=1101.0
            this.addChild(new Stats());
        }

    }
}
