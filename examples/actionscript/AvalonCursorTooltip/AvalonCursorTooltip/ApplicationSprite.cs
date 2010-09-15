using ScriptCoreLib.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.Extensions;

namespace BrowserAvalonApplicationWithAdobeFlash2
{
    internal sealed class ApplicationSprite : Sprite
    {
        public readonly ApplicationCanvas content = new ApplicationCanvas();

        public ApplicationSprite()
        {
            this.InvokeWhenStageIsReady(
                delegate()
                {
                    content.AttachToContainer(this);
                    content.AutoSizeTo(this.stage);
                }
            );
        }

    }
}
