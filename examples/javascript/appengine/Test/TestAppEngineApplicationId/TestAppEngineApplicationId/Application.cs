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
using TestAppEngineApplicationId;
using TestAppEngineApplicationId.Design;
using TestAppEngineApplicationId.HTML.Pages;

namespace TestAppEngineApplicationId
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
            //Native.css[

            // 1:22ms IStyleSheetRule.AddRule error { text = html head,{/**/} } 
            // 2:21ms IStyleSheetRule.AddRule error { text = ,{/**/} } 
            // 2:21ms IStyleSheetRule.AddRule error { text = ,{/**/} }

            // why wont this work?
            //new IStyle(IStyleSheet.all[IHTMLElement.HTMLElementEnum.head] | IStyleSheet.all[IHTMLElement.HTMLElementEnum.title])

            //{ css1 = { selectorElement = , rule = { selectorText = , type = 1 } } }
            //{ css2 = { selectorElement = , rule = { selectorText = , type = 1 } } }
            //{ css3 = { selectorElement = , rule = { selectorText = ,, rule = null } } }

            var css1 = IStyleSheet.all[IHTMLElement.HTMLElementEnum.head];

            new IHTMLPre { new { css1 } }.AttachToDocument();

            var css2 = IStyleSheet.all[IHTMLElement.HTMLElementEnum.title];

            new IHTMLPre { new { css2 } }.AttachToDocument();

            var css3 = css1 | css2;

            new IHTMLPre { new { css3 } }.AttachToDocument();

            var css4 = Native.css[IHTMLElement.HTMLElementEnum.head] | IHTMLElement.HTMLElementEnum.title;

            new IHTMLPre { new { css4 } }.AttachToDocument();

            new IStyle(IHTMLElement.HTMLElementEnum.head)
            {
                display = IStyle.DisplayEnum.block
            };

            new IStyle(IHTMLElement.HTMLElementEnum.title)
            {
                display = IStyle.DisplayEnum.block,
                margin = "2em",
                color = "red",
                fontSize = "large"
            };



            //this.title = Native.document.head.AsXElement().Attribute("title");
            this.title = page.title;


            // do we have async for java just yet?
            this.yield();
        }

    }
}
