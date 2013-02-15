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

namespace Abstractatech.JavaScript.FormAsPopup
{
    public static class FormAsPopupExtensions
    {
        public static void PopupInsteadOfClosing(this Form f)
        {
            __Control __f = f;

            var content = new { f };

            Action AtClose = delegate
            {
                // cant be minimized
                content.f.WindowState = FormWindowState.Normal;

                var w = Native.Window.open("about:blank", "_blank",
                          content.f.Width,
                          content.f.Height,
                          false
                      );

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


            content.f.FormClosing +=
                (sender, e) =>
                {
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
