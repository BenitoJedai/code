using AIRAvalonBrowserLogos.HTML.Pages;
using AvalonBrowserLogos;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.ActionScript.flash.media;
using ScriptCoreLib.ActionScript.flash.system;
using ScriptCoreLib.Extensions;
using System.Xml.Linq;

namespace AIRAvalonBrowserLogos
{
    public sealed class ApplicationSprite : Sprite
    {
        // No Devices Detected
        // Installation Error: MissingBundleIdentifier.

        public readonly ApplicationCanvas content = new ApplicationCanvas();

        public ApplicationSprite()
        {
            this.InvokeWhenStageIsReady(
                () =>
                {
                    content.AttachToContainer(this);
                    content.AutoSizeTo(this.stage);

                    // add net.hires package from jsc store
                    this.addChild(new net.hires.debug.Stats());
                    // then do a rebuild to see it on ipad

                }
            );
        }

    }
}

