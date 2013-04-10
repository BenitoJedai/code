using FormLockedAsSidebarTab.Design;
using FormLockedAsSidebarTab.HTML.Pages;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace FormLockedAsSidebarTab
{
    public static class XXX
    {
        public static void MoveNodeToFirst(this INode e)
        {
            var p = e.parentNode;
            e.Orphanize();

            p.insertBefore(
                e, p.firstChild);

        }
    }
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
            //content.AttachControlTo(page.Content);
            //content.AutoSizeControlTo(page.ContentSize);

            FormStyler.AtFormCreated = FormStylerLikeFloat.LikeFloat;

            // I want animated background!
            new WebGLClouds.Application();

            //Native.Document.body.lastChild.MoveNodeToFirst();

            var f = new Form();

            content.BackColor = Color.Transparent;
            content.Dock = DockStyle.Fill;
            content.AttachTo(f);

            f.Show();


            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );


            Action ResizeMargin = delegate
            {
                if (f.GetHTMLTarget().parentNode == null)
                    Native.Document.body.style.marginLeft = 16 + "px";
                else
                    Native.Document.body.style.marginLeft = (f.Width + 32) + "px";
            };

            Action AtResize = delegate
            {
                ResizeMargin();

                f.MoveTo(16, 16);
                f.SizeTo(
                    f.Width,
                    f.Height.Min(Native.Window.Height - 32)
                );
            };

            // why this not working?
            //f.SizeChanged +=
            content.ClientSizeChanged +=
                delegate
                {
                    ResizeMargin();
                };


            //new ScriptCoreLib.JavaScript.Runtime.Timer(
            //    delegate
            //    {

            //    }
            //).StartInterval();

            Native.Window.onresize +=
                delegate
                {
                    AtResize();
                };

            AtResize();


            f.PopupInsteadOfClosing(SpecialNoMovement: true);
        }

    }
}
