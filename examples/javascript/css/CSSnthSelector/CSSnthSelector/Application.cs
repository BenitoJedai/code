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
using CSSnthSelector;
using CSSnthSelector.Design;
using CSSnthSelector.HTML.Pages;

namespace CSSnthSelector
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
            var spans = page.Header.innerText.ToCharArray().Select(
                c =>
                {
                    var span = (IHTMLSpan)c;
                    return span;
                }
            ).ToArray();

            page.Header.Clear();
            spans.AsEnumerable().AttachTo(page.Header);


            var css3 = page.Header.css.nthChild[3];

            var css5 = page.Header.css[5];

            css5.style.backgroundColor = "cyan";

            // [style-id="0"] > :nth-child(5)
            page.Content.innerText = css5.selectorText;

            new IHTMLButton { "change index " }.AttachToDocument().WhenClicked(
                delegate
                {
                    // wow this works!
                    css5.selectorText = css3.selectorText;
                }
            );

        }

    }
}
