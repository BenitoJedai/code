// For more information visit:
// http://studio.jsc-solutions.net/

using System;
using System.Text;
using System.Linq;
using System.Xml.Linq;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Avalon.Extensions;
using MultitouchFingerTools.HTML.Pages;
using MultitouchFingerTools;
using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using ScriptCoreLib.CSharp.Avalon.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.Extensions;
using System.Windows;

namespace MultitouchFingerTools
{


    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// In debug build you can just hit F5 and debug the server side code.
        /// </summary>
        /// <param name="args">Commandline arguments</param>
        [STAThread]
        public static void Main(string[] args)
        {
#if DEBUG
            var c = new ApplicationCanvas();
            var w = c.ToWindow();

            

            Action Update =
                delegate
                {
                    // http://stackoverflow.com/questions/1081580/how-to-set-wpf-windows-startup-clientsize
                    var horizontalBorderHeight = SystemParameters.ResizeFrameHorizontalBorderHeight;
                    var verticalBorderWidth = SystemParameters.ResizeFrameVerticalBorderWidth;
                    var captionHeight = SystemParameters.CaptionHeight;

                    var Width = w.Width - 2 * verticalBorderWidth;
                    var Height = w.Height - captionHeight - 2 * horizontalBorderHeight;

                    c.SizeContentTo(Width, Height);
                };

    
            w.SizeChanged +=
                (s, e) =>
                {
                    if (e.PreviousSize.Width == 0)
                        return;

                    // Content dictates its size. Yet when we resize the window we want the content to know it has more room.
                    Update();
                    
                };

            w.ShowDialog();
#else

            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
#endif
        }

    }

    #region default Browser Application implementation for release build testing
    /// <summary>
    /// This type can be used from javascript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class Application
    {
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefaultPage page)
        {
            var s = new ApplicationSprite();
            var e = s.ToHTMLElement();

            s.AttachSpriteTo(page.PageContainer);

          
            Native.Window.onresize +=
                delegate
                {
                    var w = page.SizeShadow.scrollWidth;
                    var h = page.SizeShadow.scrollHeight;

                    //e.style.SetLocation(32, 32, w, h);


                    //s.RaiseSizeContentTo("" + w,  "" + h);
                    //Native.Document.title = new { Native.Document.body.clientWidth, Native.Document.body.clientHeight }.ToString();

                    Native.Document.title = new { w, h}.ToString();

                    e.style.SetSize(w, h);
                };
        }

    }

    public sealed class ApplicationSprite : Sprite
    {
        public const int DefaultWidth = ApplicationCanvas.DefaultWidth;
        public const int DefaultHeight = ApplicationCanvas.DefaultHeight;

        public ApplicationSprite()
        {
            this.InvokeWhenStageIsReady(
                delegate
                {
                    this.stage.align = StageAlign.TOP_LEFT;
                    this.stage.scaleMode = StageScaleMode.NO_SCALE;

                    var fullscreen = new ScriptCoreLib.ActionScript.flash.ui.ContextMenuItem("Go fullscreen!");

                    fullscreen.menuItemSelect +=
                        delegate
                        {
                            this.stage.SetFullscreen(true);
                        };

                    this.contextMenu = new ScriptCoreLib.ActionScript.flash.ui.ContextMenu
                    {
                        customItems = new[] { fullscreen }
                    };

                    var c = new ApplicationCanvas();
                    c.AttachToContainer(this);

                    this.stage.resize +=
                        e =>
                        {

                            c.SizeContentTo(this.stage.stageWidth, this.stage.stageHeight);
                        };
                }
            );
        }



    }

    /// <summary>
    /// This type can be used from javascript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
    {

    }

    #endregion
}
