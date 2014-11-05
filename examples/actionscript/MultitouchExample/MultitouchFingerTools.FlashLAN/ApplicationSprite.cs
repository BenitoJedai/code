using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptCoreLib.Extensions.Avalon;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;

namespace MultitouchFingerTools.FlashLAN
{
    public sealed class ApplicationSprite : Sprite
    {
        // This small tile image has been rejected due to the following reasons:
        //Too much text
        //Text is too small
        //Too much detail
        // You need to use a different version code for your APK because you already have one with version code 1000001.

        public const int DefaultWidth = ApplicationCanvas.DefaultWidth;
        public const int DefaultHeight = ApplicationCanvas.DefaultHeight;

        ApplicationCanvas content = new ApplicationCanvas();

        public ApplicationSprite()
        {
            this.InvokeWhenStageIsReady(
                delegate
                {

                    #region context menu

                    //this.click +=
                    //    delegate
                    //    {
                    //        this.stage.SetFullscreen(true);
                    //    };

                    //var fullscreen = new ScriptCoreLib.ActionScript.flash.ui.ContextMenuItem("Go fullscreen!");

                    //fullscreen.menuItemSelect +=
                    //    delegate
                    //    {
                    //        this.stage.SetFullscreen(true);
                    //    };

                    //this.contextMenu = new ScriptCoreLib.ActionScript.flash.ui.ContextMenu
                    //{
                    //    customItems = new[] { fullscreen }
                    //};

                    #endregion

                    content.AttachToContainer(this).AutoSizeTo(this.stage);

                    // user code
                    content.ConnectToSession(ApplicationCanvasExtensionsForFlash.ConnectToSessionVariation.Flash);

                    this.addChild(new net.hires.debug.Stats());
                }
            );
        }

    }

}
