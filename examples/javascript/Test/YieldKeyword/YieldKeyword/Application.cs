using YieldKeyword.HTML.Pages;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Collections.Generic;

namespace YieldKeyword
{
    /// <summary>
    /// This type will run as JavaScript.
    /// </summary>
    internal sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefault page)
        {
            var q = new[] { "foo", "bar" };

            Foo(q).WithEach(
                k => new IHTMLDiv { innerText = k }.AttachTo(page.Content)
            );

            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }




        //02000006 YieldKeyword.Application+<Foo>d__5
        //{ SourceMethod = Void System.IDisposable.Dispose() }
        //{ SourceMethod = Boolean MoveNext() }
        //{ SourceMethod = System.String System.Collections.Generic.IEnumerator<System.String>.get_Current() }
        //{ SourceMethod = Void System.Collections.IEnumerator.Reset() }
        //{ SourceMethod = System.Object System.Collections.IEnumerator.get_Current() }
        //{ SourceMethod = System.Collections.Generic.IEnumerator`1[System.String] System.Collections.Generic.IEnumerable<System.String>.GetEnumerator() }
        //script: error JSC1000: if block not detected correctly, opcode was { Branch = [0x0008] bne.un.s   +0 -2{[0x0001] ldfld      +1 -1{[0x0000] ldarg.0    +1 -0} } {[0x0006] ldc.i4.s   +1 -0} , Location =
        // assembly: U:\YieldKeyword.Application.exe
        // type: YieldKeyword.Application+<Foo>d__5, YieldKeyword.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
        // offset: 0x0008
        //  method:System.Collections.Generic.IEnumerator`1[System.String] System.Collections.Generic.IEnumerable<System.String>.GetEnumerator() }



        public
            //static 
            IEnumerable<string> Foo(IEnumerable<string> content)
        {
            yield return "hello";

            var e = content.AsEnumerable().GetEnumerator();

            while (e.MoveNext())
            {
                var item = e.Current;

                yield return "content " + item;
            }

            yield return "world";
        }
    }
}
