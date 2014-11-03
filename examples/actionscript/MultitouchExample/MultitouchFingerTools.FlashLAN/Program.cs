#define xFCHROME

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
using ScriptCoreLib.JavaScript.Windows.Forms;
using ScriptCoreLib.Shared.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Xml.Linq;
using ScriptCoreLib.Shared.Avalon.Extensions;

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

                            // where is this function documented?
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
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201403/20140301
#if FCHROME
            FormStyler.AtFormCreated =
               s =>
               {
                   s.Context.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

                   //var x = new ChromeTCPServerWithFrameNone.HTML.Pages.AppWindowDrag().AttachTo(s.Context.GetHTMLTarget());
                   var x = new ChromeTCPServerWithFrameNone.HTML.Pages.AppWindowDragWithShadow().AttachTo(s.Context.GetHTMLTarget());
               };

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
                    FormStyler.AtFormCreated
                );





                return;
            }
            #endregion

#endif

            #region += Launched chrome.app.window
            // X:\jsc.svn\examples\javascript\chrome\apps\ChromeTCPServerAppWindow\ChromeTCPServerAppWindow\Application.cs
            dynamic self = Native.self;
            dynamic self_chrome = self.chrome;
            object self_chrome_socket = self_chrome.socket;

            if (self_chrome_socket != null)
            {
                //chrome.Notification.DefaultTitle = "Audi Visualization";
                //chrome.Notification.DefaultIconUrl = new x128().src;

                ChromeTCPServer.TheServerWithAppWindow.Invoke(
                    AppSource.Text
                    );

                return;
            }
            #endregion

            var sprite = new ApplicationSprite();
            sprite.AttachSpriteToDocument().With(
                   embed =>
                   {
                       embed.style.SetLocation(0, 0);
                       embed.style.SetSize(Native.window.Width, Native.window.Height);

                       Native.window.onresize +=
                           delegate
                           {
                               embed.style.SetSize(Native.window.Width, Native.window.Height);
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
