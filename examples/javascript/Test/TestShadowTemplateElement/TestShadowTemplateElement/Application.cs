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
using TestShadowTemplateElement;
using TestShadowTemplateElement.Design;
using TestShadowTemplateElement.HTML.Pages;

namespace TestShadowTemplateElement
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        // what if we could reuse any other application as a document fragment?

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // http://www.inserthtml.com/2013/09/web-components/


            //var shadow = page.mydiv.createShadowRoot();
            var shadow = page.body.createShadowRoot();
            // appending document fragment actually moves all children?
            //shadow.appendChild(page.atemplate.content.cloneNode(true));
            shadow.appendChild(new FooTemplate().root);


            // http://abhishek-tiwari.com/post/webcomponents-web-s-polymer-future

            // http://samuli.hakoniemi.net/meet-shadow-dom-a-new-kid-in-town/
            // http://matthewphillips.info/posts/fun-with-shadow-dom.html
            // after we move xslx to script core lib, html gen als needs to end up there.
            //var shadow2 = page.mydiv2.createShadowRoot();
            //shadow2.appendChild(new FooTemplate().root.content.cloneNode(true));
            //shadow2.appendChild(new FooTemplate().root.content);

            // http://stackoverflow.com/questions/19483158/using-a-shadow-dom-today
            //shadow2.appendChild(new FooTemplate().root.childNodes);

            // template element cannot be rebuild from assetslibrary right now...
            // we now have scoped style dont we...

        }

    }
}
