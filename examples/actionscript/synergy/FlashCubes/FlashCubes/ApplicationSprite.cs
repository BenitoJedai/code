using FlashCubes;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.Extensions;

namespace FlashCubes
{
    public sealed class ApplicationSprite : ApplicationSpriteX
    { 
    
    }
    
    public class ApplicationSpriteX : Main
    {
        public const int DefaultWidth = 800;
        public const int DefaultHeight = 600;

        public ApplicationSpriteX()
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

#if DisableActionScriptNatives
[Script(IsNative = true)]
[SWCImport]
public class Main : Sprite
{
}
#endif