using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using com.abstractatech.consoleworm.Design;
using com.abstractatech.consoleworm.HTML.Pages;
using System.Windows.Media;
using ScriptCoreLib.JavaScript.Runtime;
using AvalonPromotionBrandIntro;

namespace com.abstractatech.consoleworm
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefault page)
        {
            // JavaScript Warning: "HTTP "Content-Type" of "audio/mpeg3" is not supported.
            new global::com.abstractatech.consoleworm.HTML.Audio.FromAssets.applause().play();


            new com.abstractatech.consoleworm.js.Game();

            var canvas = new ApplicationCanvas();

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

            canvas.AttachToContainer(Native.Document.body);

            canvas.AutoSizeTo(Native.Document.body);

            @"Console Worm".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"Multiscreen Console Worm",
                value => value.ToDocumentTitle()
            );
        }

    }
}
