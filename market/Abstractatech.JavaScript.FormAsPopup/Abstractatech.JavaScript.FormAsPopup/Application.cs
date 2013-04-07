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
    // intellisense friendly :) discoverability
    public static class FormAsPopupExtensionsForConsoleFormPackage
    {
        public static T PopupInsteadOfClosing<T>(
            this T f,
            bool HandleFormClosing = true,
            Action SpecialCloseOnLeft = null,

            // as if we were docked
            bool SpecialNoMovement = false
            ) where T : Form
        {
            Abstractatech.JavaScript.FormAsPopup.FormAsPopupExtensions.PopupInsteadOfClosing(
                f,
                HandleFormClosing,
                SpecialCloseOnLeft,
                SpecialNoMovement
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

            bool SpecialNoMovement = false
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

                Native.Window.onbeforeunload +=
                    delegate
                    {
                        HTMLTarget_parent = null;
                        HTMLTargetContainer_parent = null;

                        w.close();
                    };

                content.f.GetHTMLTarget().Orphanize();


                w.onload +=
                    delegate
                    {

                        content.f.GetHTMLTargetContainer().Orphanize().AttachTo(
                            w.document.body
                        );


                        w.document.title = content.f.Text;


                        w.onresize +=
                            delegate
                            {
                                var cs = content.f.ClientSize;

                                cs.Width = w.Width;
                                cs.Height = w.Height;

                                content.f.ClientSize = cs;

                                //__f.InternalRaiseClientSizeChanged(
                                //    new EventArgs()
                                //);
                            };

                        w.onbeforeunload +=
                            delegate
                            {
                                if (HTMLTargetContainer_parent == null)
                                    return;

                                // undo
                                content.f.GetHTMLTargetContainer().Orphanize().AttachTo(
                                    HTMLTargetContainer_parent
                                );

                                content.f.GetHTMLTarget().Orphanize().AttachTo(
                                    HTMLTarget_parent
                                );
                            };
                    };
            };
            #endregion


            var undo_x = 0;
            var undo_y = 0;

            //var fOpacity = 1.0;

            __f.InternalCaptionDrag.DragStart +=
                delegate
                {
                    //fOpacity = __f.Opacity;
                    undo_x = __f.Left;


                    undo_y = __f.Top;
                };

            var SpecialClose = false;


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

            content.f.FormClosing +=
                (sender, e) =>
                {
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
