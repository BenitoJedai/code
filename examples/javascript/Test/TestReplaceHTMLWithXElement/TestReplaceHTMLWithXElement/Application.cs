using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestReplaceHTMLWithXElement;
using TestReplaceHTMLWithXElement.Design;
using TestReplaceHTMLWithXElement.HTML.Pages;

namespace TestReplaceHTMLWithXElement
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
            //page.output.stylerule.

            page.foo.css.firstChild.style.color = "red";

            page.output = this.Content();



            // red { nodeName = #text }
            page.output.innerText = "red " + new { page.foo.firstChild.nodeName };



            page.a = "hello";

            page.b = new XElement("div", "hello world");


            page.specialcontent.css.before.style.contentAsync = SpecialContent();
        }

    }
}
