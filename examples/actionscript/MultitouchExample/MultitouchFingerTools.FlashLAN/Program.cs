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
using MultitouchFingerTools.FlashLAN.HTML.Pages;
using MultitouchFingerTools.FlashLAN;
using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using ScriptCoreLib.CSharp.Avalon.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.Extensions;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Input;

namespace MultitouchFingerTools.FlashLAN
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
            System.Diagnostics.Debug.WriteLine(
                typeof(Microsoft.FSharp.Core.Operators).AssemblyQualifiedName
            );

#if DEBUG
            var Spawn = default(Action<Action<Window>>);
            var Session = new List<ApplicationCanvas>();

            Spawn = WindowCreated =>
                {
                    var c = new ApplicationCanvas();

                    Session.Add(c);

                    c.ConnectToSession(Session.Except(new[] { c }));



                    var w = c.ToWindow();

                    w.Title += " | F2 - Spawn, F11 - Fullscreen";

                    w.Closing +=
                        delegate
                        {
                            Session.Remove(c);
                        };

                    w.KeyUp +=
                        (s, e) =>
                        {
                            if (e.Key == Key.F2)
                                Spawn(k => k.Show());
                        };

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
                                    w.WindowStyle = WindowStyle.None;
                                    w.WindowState = WindowState.Maximized;
                                    return;
                                }
                        };

                    w.StateChanged +=
                        (s, e) =>
                        {
                            w.WindowStyle = w.WindowState == WindowState.Maximized ? WindowStyle.None : WindowStyle.SingleBorderWindow;
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

                            var Width = e.NewSize.Width;

                            var Height = e.NewSize.Height;


                            Width -= 2 * verticalBorderWidth;
                            Height -= 2 * horizontalBorderHeight;


                            if (w.WindowStyle != WindowStyle.None)
                            {
                                Height -= captionHeight;
                            }

                            c.SizeTo(Width, Height);

                        };

                    if (WindowCreated != null)
                        WindowCreated(w);
                };

            Spawn(w => w.ShowDialog());
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

            s.AttachSpriteTo(page.PageContainer);
            s.AutoSizeSpriteTo(page.SizeShadow);
        }

    }

    public sealed class ApplicationSprite : Sprite
    {
        public const int DefaultWidth = ApplicationCanvas.DefaultWidth;
        public const int DefaultHeight = ApplicationCanvas.DefaultHeight;

        ApplicationCanvas content = new ApplicationCanvas();

        public ApplicationSprite()
        {
            this.InvokeWhenStageIsReady(
                delegate
                {

                    #region context menu

                    this.click +=
                        delegate
                        {
                            this.stage.SetFullscreen(true);
                        };

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
