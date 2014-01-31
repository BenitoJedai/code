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
using CSSConditionalHover;
using CSSConditionalHover.Design;
using CSSConditionalHover.HTML.Pages;

namespace CSSConditionalHover
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
            //16ms __get_item IStyleSheet { selectorText = div[style-id='0'] } view-source:35239
            //18ms __get_item IStyleSheet { selectorText = null:hover } view-source:35239
            //22ms __get_item IStyleSheet { selectorText = null>p } view-source:35239
            //24ms __get_item IStyleSheet { selectorText = null:nth-of-type(1) } 

            new IHTMLPre { new { IStyleSheet.all.Rules.Length } }.AttachToDocument();

            // 22ms AddRule { selectorText = div[style-id='0']:hover>p:nth-of-type(1),div[style-id='0']:hover~p:nth-of-type(1), Length = 0 } 
            page.PageContainer.css.hover[page.Content, page.Content2].style.backgroundColor = "yellow";


            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201401/20140131
            // 4 ?
            new IHTMLPre { new { IStyleSheet.all.Rules.Length } }.AttachToDocument();
        }

    }
}
