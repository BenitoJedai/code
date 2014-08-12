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
using TestShadowSelectByClass;
using TestShadowSelectByClass.Design;
using TestShadowSelectByClass.HTML.Pages;

namespace TestShadowSelectByClass
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
            // http://docs.webplatform.org/wiki/dom/shadowdom/ShadowRoot

            // https://www.w3.org/Bugs/Public/show_bug.cgi?id=24867

            //new ShadowLayout().AttachTo(Native.document.documentElement.shadow);
            var s = new ShadowLayout().AttachTo(Native.shadow);

            // http://w3c.github.io/webcomponents/spec/shadow/#dfn-content-element-select
            // http://caniuse.com/#feat=shadowdom
            new IHTMLButton { "select all buttons" }.AttachToDocument().onclick +=
                e =>
                {
                    // this aint working. why?
                    // spec says it should. but nothing shows up. 20140812
                    s.SelectedContent.select = ".what";

                };

            //new IHTMLButton { "select this button" }.AttachToDocument().onclick +=
            //    e =>
            //    {
            //        s.SelectedContent.select = "button";
            //    };
        }


    }
}
