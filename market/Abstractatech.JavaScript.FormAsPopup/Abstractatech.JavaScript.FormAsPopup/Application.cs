using Abstractatech.JavaScript.FormAsPopup.Design;
using Abstractatech.JavaScript.FormAsPopup.HTML.Pages;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Windows.Forms;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms;


//namespace Abstractatech.JavaScript.FormAsPopup
namespace ScriptCoreLib.Extensions
{

    public class FormAsPopupExtensionsForConsoleFormPackageMediator
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/20/2013
        // tested by X:\jsc.svn\examples\javascript\android\com.abstractatech.adminshell\com.abstractatech.adminshell\Application.cs
        public static bool InternalPopupHasFrame
        {
            get
            {
                // does the value exist?
                bool value = (Native.Window as dynamic).__FormAsPopupExtensions_InternalPopupHasFrame;

                return !!value;
            }

            set
            {
                // this value should stay for app inline reloads
                (Native.Window as dynamic).__FormAsPopupExtensions_InternalPopupHasFrame = value;
            }
        }

        #region postMessage
        public static Action<IWindow, XElement> postMessage =
          (w, x) =>
          {
              Console.WriteLine(new { x });

              var data = w.escape(x.ToString());

              var ww = w.open("http://hack-wtf-postmessage/" + data);
          };
        #endregion

        static FormAsPopupExtensionsForConsoleFormPackageMediator()
        {
            // X:\jsc.internal.svn\examples\javascript\chrome\ChromeMyJscSolutionsNet\ChromeMyJscSolutionsNet\Application.cs

            // each inline app has its own version of this yet we need to keep sngle variable?
            Console.WriteLine("Can we pop with our own frame? " + new { FormAsPopupExtensionsForConsoleFormPackageMediator.InternalPopupHasFrame });




            Native.Window.onmessage +=
                m =>
                {
                    try
                    {
                        var xml = XElement.Parse((string)m.data);

                        if (xml.Value == "Do you want to pop with your own frame?")
                        {
                            FormAsPopupExtensionsForConsoleFormPackageMediator.InternalPopupHasFrame = true;

                            postMessage(Native.Window, new XElement("re", "yes i have my own frame!"));
                        }
                    }
                    catch
                    {
                    }
                };
        }
    }

    // intellisense friendly :) discoverability
    public static class FormAsPopupExtensionsForConsoleFormPackage
    {
        public static T PopupInsteadOfClosing<T>(
            this T f,
            bool HandleFormClosing = true,
            Action SpecialCloseOnLeft = null,

            // as if we were docked
            bool SpecialNoMovement = false,


            Action NotifyDocked = null
            ) where T : Form
        {
            Abstractatech.JavaScript.FormAsPopup.FormAsPopupExtensions.PopupInsteadOfClosing(
                f,
                HandleFormClosing,
                SpecialCloseOnLeft,
                SpecialNoMovement,
                NotifyDocked
            );

            return f;

        }
    }
}



namespace Abstractatech.JavaScript.FormAsPopup
{

    public static class FormAsPopupExtensions
    {





        // { ExceptionObject = System.MissingMethodException: Method not found: 'Void Abstractatech.JavaScript.FormAsPopup.FormAsPopupExtensions.PopupInsteadOfClosing(System.Windows.Forms.Form, Boolean, System.Action)'.
        public static void PopupInsteadOfClosing(
            this Form f,
            bool HandleFormClosing,
            Action SpecialCloseOnLeft,

            bool SpecialNoMovement = false,
            Action NotifyDocked = null

            )
        {
            __Form __f = f;

            __f.CloseButtonContent.title = "Popup";

            var content = new { f };


            #region AtClose
            Action AtClose = delegate
            {
                Console.WriteLine("AtClose!");


                // cant be minimized
                content.f.WindowState = FormWindowState.Normal;

                var w = Native.Window.open("about:blank", "_blank",
                          content.f.Width,
                          content.f.Height,
                          false
                      );

                //var w = new IWindow();

                var HTMLTargetContainer_parent = content.f.GetHTMLTargetContainer().parentNode;
                var HTMLTarget_parent = content.f.GetHTMLTarget().parentNode;

                //Native.Window.onbeforeunload +=
                // chrome webview only supports onunload
                #region Native.Window.onunload
                Native.Window.onunload +=
                    delegate
                    {
                        HTMLTarget_parent = null;
                        HTMLTargetContainer_parent = null;

                        if (w == null)
                            return;

                        w.close();
                    };
                #endregion


                content.f.GetHTMLTarget().Orphanize();

                if (NotifyDocked != null)
                    NotifyDocked();

                w.onload +=
                    delegate
                    {
                        // keep relative links working..
                        new IHTMLBase { href = Native.Document.location.href }.AttachTo(w.document.body);

                        __Form ff = content.f;

                        var ff_TargetOuterBorder_parent = ff.TargetOuterBorder.parentNode;

                        if (FormAsPopupExtensionsForConsoleFormPackageMediator.InternalPopupHasFrame)
                        {
                            ff.TargetOuterBorder.Orphanize().AttachTo(
                                w.document.body
                            );
                        }
                        else
                        {
                            content.f.GetHTMLTargetContainer().Orphanize().AttachTo(
                                w.document.body
                            );
                        }




                        #region title
                        w.document.title = content.f.Text;

                        f.TextChanged +=
                            delegate
                            {
                                if (w == null)
                                    return;


                                w.document.title = content.f.Text;
                            };
                        #endregion

                        #region onresize
                        // does this work for chrome?
                        w.onresize +=
                            delegate
                            {
                                if (w == null)
                                    return;

                                var cs = content.f.ClientSize;

                                cs.Width = w.Width;
                                cs.Height = w.Height;

                                content.f.ClientSize = cs;

                                //__f.InternalRaiseClientSizeChanged(
                                //    new EventArgs()
                                //);
                            };
                        #endregion


                        ff.InternalBeforeFormClosing +=
                           (e) =>
                           {
                               if (w != null)
                               {
                                   e.Cancel = true;

                                   Console.WriteLine("InternalBeforeFormClosing!");

                                   if (FormAsPopupExtensionsForConsoleFormPackageMediator.InternalPopupHasFrame)
                                   {
                                       FormAsPopupExtensionsForConsoleFormPackageMediator.postMessage(Native.Window, new XElement("re", "close this window!"));
                                   }

                                   w.close();
                               }
                           };
                        //w.onbeforeunload +=

                        #region w.onunload
                        w.onunload +=
                            delegate
                            {

                                if (HTMLTargetContainer_parent == null)
                                    return;

                                // undo


                                if (FormAsPopupExtensionsForConsoleFormPackageMediator.InternalPopupHasFrame)
                                {
                                    ff.TargetOuterBorder.Orphanize().AttachTo(
                                        ff_TargetOuterBorder_parent
                                    );
                                }
                                else
                                {
                                    content.f.GetHTMLTargetContainer().Orphanize().AttachTo(
                                        HTMLTargetContainer_parent
                                    );
                                }

                                content.f.GetHTMLTarget().Orphanize().AttachTo(
                                    HTMLTarget_parent
                                );

                                w = null;

                                if (NotifyDocked != null)
                                    NotifyDocked();
                            };
                        #endregion

                    };
            };
            #endregion


            var undo_x = 0;
            var undo_y = 0;

            //var fOpacity = 1.0;

            #region DragStart
            __f.InternalCaptionDrag.DragStart +=
                delegate
                {
                    //fOpacity = __f.Opacity;
                    undo_x = __f.Left;


                    undo_y = __f.Top;
                };
            #endregion


            var SpecialClose = false;


            #region DragMove
            __f.InternalCaptionDrag.DragMove +=
                delegate
                {
                    var z = new { f.Right, f.Left };

                    if (SpecialNoMovement)
                    {
                        var dx = Math.Abs(undo_x - f.Left);
                        var dy = Math.Abs(undo_y - f.Top);


                        if (dx < 8)
                            if (dy < 8)
                            {
                                __f.Opacity = 1;

                                return;

                            }


                    }
                    else
                    {
                        if ((z.Right - f.Width / 2) > 0)
                            if ((z.Left + f.Width / 2) < Native.Window.Width)
                            {
                                __f.Opacity = 1;

                                return;
                            }

                    }

                    __f.Opacity = 0.5;
                };
            #endregion


            #region DragStop
            __f.InternalCaptionDrag.DragStop +=
                delegate
                {
                    __f.Opacity = 1;

                    var z = new { f.Right, f.Left };

                    var IsNotOnLeft = (z.Right - f.Width / 2) > 0;
                    var IsNotOnRight = (z.Left + f.Width / 2) < Native.Window.Width;

                    if (SpecialNoMovement)
                    {
                        var dx = Math.Abs(undo_x - f.Left);
                        var dy = Math.Abs(undo_y - f.Top);

                        Console.WriteLine("SpecialNoMovement snap? " + new { dx, dy });

                        if (dx < 8)
                            if (dy < 8)
                            {
                                f.Left = undo_x;
                                f.Top = undo_y;
                                return;

                            }


                    }
                    else
                    {


                        if (IsNotOnLeft)
                            if (IsNotOnRight)
                            {
                                Console.WriteLine("still in window: " + z);
                                // still in the window!
                                // what about popups?
                                return;
                            }

                        if (!IsNotOnLeft)
                            if (SpecialCloseOnLeft != null)
                            {
                                SpecialCloseOnLeft();

                                f.Close();

                                return;
                            }

                    }

                    Console.WriteLine("close to popup");
                    f.Left = undo_x;
                    f.Top = undo_y;

                    SpecialClose = true;
                    f.Close();
                };
            #endregion

            #region FormClosing
            content.f.FormClosing +=
                (sender, e) =>
                {
                    //if (FormClosingMeansDock)
                    //{
                    //    return;
                    //}

                    if (!HandleFormClosing)
                    {
                        if (!SpecialClose)
                        {
                            if (e.CloseReason == CloseReason.UserClosing)
                                return;
                        }

                        SpecialClose = false;
                    }

                    Console.WriteLine("FormClosing!");

                    e.Cancel = true;

                    AtClose();
                };
            #endregion

        }

        // error: System.MissingMethodException: Method not found: 'Void Abstractatech.JavaScript.FormAsPopup.FormAsPopupExtensions.PopupInsteadOfClosing(System.Windows.Forms.Form, Boolean, System.Action)'.
        public static void PopupInsteadOfClosing(
            this Form f,
            bool HandleFormClosing = true
            )
        {
            PopupInsteadOfClosing(f, HandleFormClosing, null);
        }

    }
}

namespace Abstractatech.JavaScript.FormAsPopup
{


    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        public readonly ApplicationControl content = new ApplicationControl();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            content.AttachControlTo(page.Content);
            content.AutoSizeControlTo(page.ContentSize);


            content.f.PopupInsteadOfClosing(HandleFormClosing: false, SpecialNoMovement: true);
        }

    }
}
