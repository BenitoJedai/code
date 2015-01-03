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
using Test453WebMethodExpressionUsing;
using Test453WebMethodExpressionUsing.Design;
using Test453WebMethodExpressionUsing.HTML.Pages;

namespace Test453WebMethodExpressionUsing
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        //public void foo(IDisposable x, string e) => using(x) new IHTMLPre{new{e}}.AttatchToDocument();
        public Action<IDisposable, string> foo = (IDisposable x, string e) => { using (x) new IHTMLPre { new { e } }.AttachToDocument(); };


        class MyClass : IDisposable
        {
            public MyClass() { new IHTMLPre { "start" }.AttachToDocument(); }
            public void Dispose() => new IHTMLPre { "end" }.AttachToDocument();
        }
        public Application(IApp page)
        {

            foo(new MyClass(), "hello world");

        }


        //{ SourceMethod = Int32 Fill(System.Data.DataTable) }
        //    script: error JSC1000: invalid branch in block build - block skipped
        //current 00d1, first 001a, last 00d0
    }
}
