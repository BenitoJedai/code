using AvalonPromotionBrandIntro;
using com.abstractatech.multiscreen.formsexample.Design;
using com.abstractatech.multiscreen.formsexample.HTML.Pages;
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
using System.Windows.Media;
using System.Xml.Linq;

namespace com.abstractatech.multiscreen.formsexample
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationXWebService service = new ApplicationXWebService();

        public readonly ApplicationControl content = new ApplicationControl();

        //public readonly ApplicationCanvas canvas = new ApplicationCanvas();


        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefaultPage page)
        {
            content.AttachControlTo(page.Content);
            content.AutoSizeControlTo(page.ContentSize);

            //canvas.TriggerOnClick = false;
            //canvas.Background = Brushes.Transparent;

  

            //canvas.AnimationCompleted +=
            //    delegate
            //    {
            //        ScriptCoreLib.JavaScript.Extensions.AvalonExtensions.ToHTMLElement(
            //            canvas
            //        ).Orphanize();
            //    };

            //canvas.AttachToContainer(page.Content);
            //canvas.AutoSizeTo(page.ContentSize);

            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }
}
