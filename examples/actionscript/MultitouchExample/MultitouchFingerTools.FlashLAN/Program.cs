// For more information visit:
// http://studio.jsc-solutions.net/

using chrome;
using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using MultitouchFingerTools.FlashLAN;
using MultitouchFingerTools.FlashLAN.HTML.Pages;
using ScriptCoreLib;
using ScriptCoreLib.CSharp.Avalon.Extensions;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.Shared.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Xml.Linq;

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
        public Application(IApp page)
        {
            #region TheServer
            dynamic self = Native.self;
            dynamic self_chrome = self.chrome;
            object self_chrome_socket = self_chrome.socket;

            if (self_chrome_socket != null)
            {
                chrome.Notification.DefaultIconUrl = new HTML.Images.FromAssets.Preview().src;
                chrome.Notification.DefaultTitle = "Multitouch";


                ChromeTCPServer.TheServerWithStyledForm.Invoke(
                    AppSource.Text,
                    ApplicationSprite.DefaultWidth,
                    ApplicationSprite.DefaultHeight,
                    AtFormCreated:
                        s =>
                        {
                            // X:\jsc.svn\examples\javascript\IsometricTycoonViewWithToolbar\IsometricTycoonViewWithToolbar\Application.cs
                            // X:\jsc.internal.svn\core\com.abstractatech.web\com.abstractatech.web\Domains\discover.xavalon.net\discover_xavalon_net.cs

                            // browser popup will use this color
                            ((__Form)s.Context).HTMLTargetContainerRef.style.backgroundColor = JSColor.FromRGB(154, 108, 70);

                            s.Caption.style.backgroundColor = JSColor.FromRGB(154, 108, 70);
                            s.TargetOuterBorder.style.boxShadow = "rgba(154, 108, 70, 0.3) 0px 0px 6px 3px";
                            s.TargetOuterBorder.style.borderColor = JSColor.FromRGB(154, 108, 70);

                            s.TargetInnerBorder.style.borderWidth = "0px";

                            s.CloseButton.style.color = JSColor.White;
                            s.CloseButton.style.backgroundColor = JSColor.None;
                            s.CloseButton.style.borderWidth = "0px";
                            s.CloseButtonContent.style.borderWidth = "0px";

                            s.TargetResizerPadding.style.left = "0px";
                            s.TargetResizerPadding.style.top = "0px";
                            s.TargetResizerPadding.style.right = "0px";
                            s.TargetResizerPadding.style.bottom = "0px";

                        }
                );





                return;
            }
            #endregion


            var sprite = new ApplicationSprite();

            sprite.AttachSpriteTo(page.PageContainer);
            sprite.AutoSizeSpriteTo(page.SizeShadow);
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
