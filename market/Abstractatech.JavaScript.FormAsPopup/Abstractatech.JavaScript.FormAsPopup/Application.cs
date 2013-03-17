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


namespace ScriptCoreLib.Extensions
{
    // intellisense friendly :) discoverability
    public static class FormAsPopupExtensionsForConsoleFormPackage
    {
        public static T PopupInsteadOfClosing<T>(this T f) where T : Form
        {
            Abstractatech.JavaScript.FormAsPopup.FormAsPopupExtensions.PopupInsteadOfClosing(f);
            return f;

        }
    }
}


namespace ScriptCoreLib.JavaScript.Windows.Forms
{
    public static class FormAsPopupExtensions
    {
        public static T PopupInsteadOfClosing<T>(this T f) where T : Form
        {
            Abstractatech.JavaScript.FormAsPopup.FormAsPopupExtensions.PopupInsteadOfClosing(f);
            return f;

        }
    }
}
namespace Abstractatech.JavaScript.FormAsPopup
{
    public static class FormAsPopupExtensions
    {
        public static void PopupInsteadOfClosing(this Form f)
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

                //var w = Native.Window.open("about:blank", "_blank",
                //          content.f.Width,
                //          content.f.Height,
                //          false
                //      );

                var w = new IWindow();

                var HTMLTargetContainer_parent = content.f.GetHTMLTargetContainer().parentNode;
                var HTMLTarget_parent = content.f.GetHTMLTarget().parentNode;

                Native.Window.onbeforeunload +=
                    delegate
                    {
                        HTMLTarget_parent = null;
                        HTMLTargetContainer_parent = null;

                        w.close();
                    };

                w.onload +=
                    delegate
                    {

                        content.f.GetHTMLTargetContainer().Orphanize().AttachTo(
                            w.document.body
                        );

                        content.f.GetHTMLTarget().Orphanize();

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

            __f.InternalCaptionDrag.DragStart +=
                delegate
                {

                    undo_x = __f.Left;
                };

            __f.InternalCaptionDrag.DragStart +=
                 delegate
                 {

                     undo_y = __f.Top;
                 };

            __f.InternalCaptionDrag.DragStop +=
                delegate
                {
                    var z = new { f.Right, f.Left };

                    if (z.Right > 0)
                        if (z.Left < Native.Window.Width)
                        {
                            Console.WriteLine("still in window: " + z);
                            // still in the window!
                            // what about popups?
                            return;
                        }


                    Console.WriteLine("close to popup");
                    f.Left = undo_x;
                    f.Top = undo_y;
                    f.Close();
                };

            content.f.FormClosing +=
                (sender, e) =>
                {
                    Console.WriteLine("FormClosing!");

                    e.Cancel = true;

                    AtClose();
                };
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


            content.f.PopupInsteadOfClosing();
        }

    }
}
