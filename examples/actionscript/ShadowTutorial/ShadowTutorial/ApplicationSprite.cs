using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.Extensions;

namespace ShadowTutorial
{
    public sealed class ApplicationSprite : Sprite
    {
        public const int DefaultWidth = 300;
        public const int DefaultHeight = 300;

        public ApplicationSprite()
        {
            var myMain = new Main(20);
            
            myMain.blendMode = BlendMode.LAYER;

            addChild(myMain);
        }

    }
}
