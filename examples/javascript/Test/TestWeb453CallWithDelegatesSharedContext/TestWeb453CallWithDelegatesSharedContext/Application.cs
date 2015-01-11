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
using TestWeb453CallWithDelegatesSharedContext;
using TestWeb453CallWithDelegatesSharedContext.Design;
using TestWeb453CallWithDelegatesSharedContext.HTML.Pages;

namespace TestWeb453CallWithDelegatesSharedContext
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {

        public static void Foo(string e, Action<string> a, Action<string> b)
        {
            a(e);
            b(e);
        }

        public Application(IApp page)
        {

            Foo("hello!",

                a: e => new IHTMLPre { "a: " + new { e } }.AttachToDocument(),

                b: e => new IHTMLPre { "a: " + new { e } }.AttachToDocument()

            );
        }

    }
}
