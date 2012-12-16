using SQLiteWithDataGridView.Design;
using SQLiteWithDataGridView.HTML.Pages;
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
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Windows.Media;
using ScriptCoreLib.JavaScript.Runtime;

namespace SQLiteWithDataGridView
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
        public Application(IDefaultPage page)
        {
            //FormStyler.AtFormCreated = FormStyler.LikeVisualStudioMetro;
            content.AttachControlTo(page.Content);
            content.AutoSizeControlTo(page.ContentSize);

            @"SQLite With DataGridView".ToDocumentTitle();



            var canvas = new AvalonPromotionBrandIntro.ApplicationCanvas();

            canvas.TriggerOnClick = false;
            canvas.Background = Brushes.Transparent;

            canvas.AnimationAllWhite +=
                delegate
                {
                    Native.Document.body.style.backgroundColor = JSColor.None;
                };

            canvas.AnimationCompleted +=
                delegate
                {
                    ScriptCoreLib.JavaScript.Extensions.AvalonExtensions.ToHTMLElement(
                        canvas
                    ).Orphanize();

                };

            canvas.PrepareAnimation()();

            canvas.AttachToContainer(Native.Document.body);

            canvas.AutoSizeTo(Native.Document.body);
        }

    }
}
