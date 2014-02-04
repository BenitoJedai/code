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
using CSSSelectorReuse;
using CSSSelectorReuse.Design;
using CSSSelectorReuse.HTML.Pages;

namespace CSSSelectorReuse
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
            var x = new XAttribute("foo", "bar");

            //// should the intellisense say that we have .body available
            //// for html element
            ////Native.css.style.backgroundColor = "red";
            Native.css[x].style.backgroundColor = "red";

            x.AttachTo(Native.document.documentElement);

            //IStyleSheet.all["html[zoo]>body::after"].style.content = "h!";
            //IStyleSheet.all["body:after"].style.content = "'h!'";

            Console.WriteLine("we should have 1 rule");

            //// add it as is? what if we change the attributes value after?
            //// css selector shall use a snapshot of the [foo='bar']

            //// html cannot have :before?
            var z = Native.css[x][IHTMLElement.HTMLElementEnum.body];

            Console.WriteLine("we should still have 1 rule, the other is pending");

            var before1 = z.before;

            before1.content = "should have 2 styles";
            Console.WriteLine(new { before1 });

            //// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201402/20140204
            var before2 = z.before;

            before2.content = "should still have 2 styles. ?";
            Console.WriteLine(new { before2 });

            var after3 = z.after;
            after3.content = "this should be the 3rd style. next we will apply the or operator";

            var all = before1 | before2;
            all.style.color = "blue";
            // 4:21ms { all = { selectorElement = , rule = { selectorText = html[foo='bar']>body:before,html[foo='bar']>body:before, type = 1 } } } 
            Console.WriteLine(new { all });
            // 3:24ms { all = { selectorElement = , rule = { selectorText = html[foo='bar']>body:before, type = 1 } } } 

            var all1 = before1 | after3;
            all1.style.borderLeft = "1em solid yellow";
            Console.WriteLine("was this rul 4?");

            var all2 = before1 | after3;
            all2.style.borderRight = "1em solid yellow";
            Console.WriteLine("was this also rule 4?");


            //4:26ms was this rul 4? view-source:35502
            //5:26ms was this also rule 4? 

            //            4:29ms was this rul 4? view-source:35523
            //4:29ms was this also rule 4? view-source:35523


            //1:18ms we should still have 1 rule, the other is pending view-source:35482
            //2:18ms { before1 = { selectorElement = , rule = { selectorText = html[foo='bar']>body:before, type = 1 } } } view-source:35482
            //3:20ms { before2 = { selectorElement = , rule = { selectorText = html[foo='bar']>body:before, type = 1 } } } 

            // vs

            //            2:30ms { before1 = { selectorElement = , rule = { selectorText = html[foo='bar']>body:before, type = 1 } } } view-source:35494
            //2:31ms { before2 = { selectorElement = , rule = { selectorText = html[foo='bar']>body:before, type = 1 } } } 


        }

    }
}
