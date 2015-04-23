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
using TestInsertionPoints;
using TestInsertionPoints.Design;
using TestInsertionPoints.HTML.Pages;

namespace TestInsertionPoints
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
            //         <element name="my-tabs">
            //    <template>
            //        <content select="h2"></content>
            //        <content select="section"></content>
            //    </template>
            //</element>

            // where should we define our custom elements?
            // class my-tabs {}

                // will this live longer than silverlight and mathml and gears, css filters, css regions?
            Native.document.registerElement("my-tabs",
                (IHTMLElement e) =>
                {
                    // when would we ever manually define a template? 
                    // would the assetslibary need to call createShadowRoot to dynamically rebuild it?
                    var s = e.createShadowRoot();

                    // cool. this allows us to reorder elements. like XAML allows?
                    // how will it implact data binding?
                    s.appendChild(new IHTMLContent { select = "h2" });
                    s.appendChild(new IHTMLContent { select = "section" });

                    // does shadowdom help against javascript third party library clashes?
                }
            );

            // http://www.html5rocks.com/en/tutorials/webcomponents/shadowdom-301/

            // http://acko.net/blog/shadow-dom/
            // http://blogs.adobe.com/webplatform/2012/06/25/working-with-css-regions-and-shadow-dom/

            // http://www.webcomponentsshift.com/#40
            new Tabs().AttachToDocument();

        }

    }
}
