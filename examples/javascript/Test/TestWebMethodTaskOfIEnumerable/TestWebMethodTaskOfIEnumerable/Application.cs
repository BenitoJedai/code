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
using TestWebMethodTaskOfIEnumerable;
using TestWebMethodTaskOfIEnumerable.Design;
using TestWebMethodTaskOfIEnumerable.HTML.Pages;

namespace TestWebMethodTaskOfIEnumerable
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


            new IHTMLDiv().AttachToDocument().With(
                async div =>
                {
                    var s = await base.GetStrings();

                    //foreach (var item in s)

                    // jsc switch rewriter does not yet like finally blocks!
                    foreach (var item in s.ToArray())
                    {
                        new IHTMLPre { new { item } }.AttachToDocument();
                    }
                }
            );

        }

    }
}
