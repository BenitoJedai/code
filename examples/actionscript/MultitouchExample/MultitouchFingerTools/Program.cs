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
using System.Windows.Input;

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

            w.KeyDown +=
                (s, e) =>
                {


                    if (e.Key == Key.Escape || e.Key == Key.F11)
                        if (w.WindowState == WindowState.Maximized)
                        {
                            w.WindowState = WindowState.Normal;
                            return;
                        }

                    if (e.Key == Key.F11)
                        if (w.WindowState == WindowState.Normal)
                        {
                            w.WindowState = WindowState.Maximized;
                            return;
                        }
                };

            w.StateChanged +=
                (s, e) =>
                {
                    var n = w.WindowState == WindowState.Maximized ? WindowStyle.None : WindowStyle.SingleBorderWindow;

                    if (w.WindowStyle != n)
                        w.WindowStyle = n;
                };


            w.Loaded +=
                delegate
                {
                    w.SizeToContent = SizeToContent.Manual;
                    w.Focus();
                };

            w.SizeChanged +=
                (s, e) =>
                {
                    if (e.PreviousSize.Width == 0)
                        return;

                    // Content dictates its size. Yet when we resize the window we want the content to know it has more room.
                    // http://stackoverflow.com/questions/1081580/how-to-set-wpf-windows-startup-clientsize
                    var horizontalBorderHeight = SystemParameters.ResizeFrameHorizontalBorderHeight;
                    var verticalBorderWidth = SystemParameters.ResizeFrameVerticalBorderWidth;
                    var captionHeight = SystemParameters.CaptionHeight;

                    var Width = e.NewSize.Width - 2 * verticalBorderWidth;
                    var Height = e.NewSize.Height - captionHeight - 2 * horizontalBorderHeight;

                    c.SizeTo(Width, Height);

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

            Action Update =
                delegate
                {
                    var w = page.SizeShadow.scrollWidth;
                    var h = page.SizeShadow.scrollHeight;


                    e.style.SetSize(w, h);
                };


            Native.Window.onresize +=
                delegate
                {
                    Update();
                };

            Update();
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

                            c.SizeTo(this.stage.stageWidth, this.stage.stageHeight);
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
