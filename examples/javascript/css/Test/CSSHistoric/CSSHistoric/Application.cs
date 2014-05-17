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
using CSSHistoric;
using CSSHistoric.Design;
using CSSHistoric.HTML.Pages;

namespace CSSHistoric
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
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140517





            //0:9ms HistoryExtensions enter view-source:35994
            //0:15ms Foo.Historic view-source:35994
            //0:16ms enter Historic: { domain = 192.168.1.91, baseURI = http://192.168.1.91:20443/#/bar, location = http://192.168.1.91:20443/#/bar, href = http://192.168.1.91:20443/#/foo } view-source:35994
            //0:17ms update { href = http://192.168.1.91:20443/#/foo, url = http://192.168.1.91:20443/#/bar } 

            Native.css.style.borderBottom = "1em solid yellow";

            new IHTMLPre { new { hello = "world" } }.AttachToDocument();

            // jsc, stop camel casing.
            Console.WriteLine("Foo.Historic");
            page.Foo.Historic(
                async scope =>
                {
                    "enter Foo Historic".ToDocumentTitle();

                    Native.css.style.borderTop = "1em solid yellow";
                    Native.css.before.content = "/#/foo";

                    await scope;
                    "undo Foo Historic".ToDocumentTitle();
                }
            );

            Console.WriteLine("Bar.Historic");
            page.Bar.Historic(
                async scope =>
                {
                    "enter Bar Historic".ToDocumentTitle();
                    Native.css.style.borderLeft = "1em solid yellow";
                    Native.css.after.content = "/#/bar";

                    await scope;
                    "undo Bar Historic".ToDocumentTitle();
                }
            );
        }

    }
}
