using FlashCubes;
using ScriptCoreLib;
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

[Script(IsNative = true)]
[SWCImport]
public class Main : Sprite
{
    // Carlo: generation was disabled due to a bug. re-enable once fixed. 
    // Arvo: JSC could make use of Roslyn to pick up such code comments :)
}