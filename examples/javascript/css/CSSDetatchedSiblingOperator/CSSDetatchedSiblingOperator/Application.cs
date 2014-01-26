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
using CSSDetatchedSiblingOperator;
using CSSDetatchedSiblingOperator.Design;
using CSSDetatchedSiblingOperator.HTML.Pages;

namespace CSSDetatchedSiblingOperator
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
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201401/20140126

            var page_Content = page.Content;
            var o = page.PageContainer.Orphanize();


            //21ms  either a sibling or a decendant. our task is to find the location and remember it view-source:34816
            //Uncaught TypeError: Cannot call method 'EQEABmASXzSNfpl_bHJLUOA' of undefined 

            o.css[
                // we cannot use this as id cannot be found anymore
                //page.Content
                page_Content

                // 49ms css.style { selectorText = div[style-id="0"] > p:nth-of-type(1) } 
            ].style.backgroundColor = "yellow";

            o.AttachToDocument();

        }

    }
}
