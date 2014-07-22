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
using TestTemplateElement;
using TestTemplateElement.Design;
using TestTemplateElement.HTML.Pages;

namespace TestTemplateElement
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
            // example in spec is faulty!
            // http://www.w3.org/TR/html5/scripting-1.html#the-template-element


            // this is something different?
            // http://golang.org/pkg/html/template/

            // https://developer.mozilla.org/en-US/docs/Web/HTML/Element/template

            // how is this any different of the way jsc does it already?
            // perhaps its faster as we do DOM rebuild of html assets?

            var clone = (IHTMLElement)page.row.content.cloneNode(true);
            clone.querySelectorAll(IHTMLElement.HTMLElementEnum.td).First().innerText = "name1";
            clone.querySelectorAll(IHTMLElement.HTMLElementEnum.td).Last().innerText = "color1";
            page.row.parentNode.appendChild(clone);

            // next step is shadow DOM ?

            // is this faster?
            // will it help DataGridView?
        }

    }
}
