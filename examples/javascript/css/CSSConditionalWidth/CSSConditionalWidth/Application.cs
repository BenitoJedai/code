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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CSSConditionalWidth;
using CSSConditionalWidth.Design;
using CSSConditionalWidth.HTML.Pages;

namespace CSSConditionalWidth
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // https://developer.chrome.com/multidevice/webview/pixelperfect

            //Native.css.screen
            // jsc, haw can i do conditional css?

            // Uncaught SyntaxError: Failed to execute 'insertRule' on 'CSSStyleSheet': Failed to parse the rule 'html@media screen and (min-width: 1000px){/**/}'.

            //Native.css["@media screen and (min-width: 1000px)"].style.backgroundColor = "yellow";

            // Uncaught SyntaxError: Failed to execute 'insertRule' on 'CSSMediaRule': the rule 'and (min-width: 1000px)  { /**/ }' is invalid and cannot be parsed. 


            //var s = new CSSStyleSheet();
            var s = new IStyleSheet();

            // android webview does not respect this

                // how does it work with shadowdom?
            s.Owner.media = "screen and (min-width: 1000px)";

            // jsc should have an orientation example for it
            // how would this look like as LINQ expression?
            // css + linq to sql knowledge needed
            s[IHTMLElement.HTMLElementEnum.body].style.backgroundColor = "yellow";

            Native.body.css.before.With(
                async ss =>
                {
                    while (true)
                    {
                        ss.contentText = new { Native.window.Width }.ToString();

                        await Native.window.async.onresize;

                    }
                }
            );
            //IStyleSheet.all[CSSMediaTypes.screen]["and (min-width: 1000px) "].style.backgroundColor = "yellow";


        }

    }
}
