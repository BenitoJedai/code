using com.abstractatech.gomoku.Design;
using com.abstractatech.gomoku.HTML.Pages;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using System;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Xml.Linq;

namespace com.abstractatech.gomoku
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        public readonly ApplicationCanvas content = new ApplicationCanvas();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefaultPage page)
        {
            content.AttachToContainer(page.Content);
            content.AutoSizeTo(page.ContentSize);
            // http://www.sounddogs.com/results.asp?Type=1&CategoryID=1004&SubcategoryID=27
            var click = new HTML.Audio.FromAssets.click { autobuffer = true };
            content.AtClick +=
                delegate
                {
                    click.play();
                    click = new HTML.Audio.FromAssets.click { autobuffer = true };
                };

            var win = new HTML.Audio.FromAssets.applause { autobuffer = true };
            content.AtWin +=
                delegate
                {
                    win.play();
                    win = new HTML.Audio.FromAssets.applause { autobuffer = true };
                };

            var loss = new HTML.Audio.FromAssets.aww { autobuffer = true };
            content.AtLoss +=
                delegate
                {
                    loss.play();
                    loss = new HTML.Audio.FromAssets.aww { autobuffer = true };
                };

            var applause = new HTML.Audio.FromAssets.applause { autobuffer = true };

            //#region AvalonPromotionBrandIntro
            //var canvas = new AvalonPromotionBrandIntro.ApplicationCanvas();

            //canvas.TriggerOnClick = false;
            //canvas.Background = Brushes.Transparent;

            //canvas.AnimationAllWhite +=
            //    delegate
            //    {
            //        applause.play();

            //        Native.Document.body.style.backgroundColor = JSColor.None;
            //    };

            //canvas.AnimationCompleted +=
            //    delegate
            //    {
            //        ScriptCoreLib.JavaScript.Extensions.AvalonExtensions.ToHTMLElement(
            //            canvas
            //        ).Orphanize();

            //    };

            //canvas.AttachToContainer(Native.Document.body);

            //canvas.AutoSizeTo(Native.Document.body);
            //#endregion


            @"Gomoku".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"Multiscreen Gomoku",
                value => value.ToDocumentTitle()
            );
        }

    }
}
