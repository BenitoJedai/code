using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.Shared.BCLImplementation.System.Dynamic;
using System;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using TestDynamicSetIndexer.Design;
using TestDynamicSetIndexer.HTML.Pages;

namespace TestDynamicSetIndexer
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // X:\jsc.internal.svn\examples\javascript\chrome\ChromeMyJscSolutionsNet\ChromeMyJscSolutionsNet\Application.cs

            object binder = new __SetIndexBinder();

            var a = binder as __SetIndexBinder;
            var isa = binder is __SetIndexBinder;
            Console.WriteLine(new { a, isa });

            var b = binder as SetIndexBinder;
            var isb = binder is SetIndexBinder;
            Console.WriteLine(new { b, isb });


            var n = "App";
            var iconUrl = "data:x";



            dynamic expando = new object();

            // does this work? refactor to test:
            // X:\jsc.svn\examples\javascript\Test\TestDynamicSetIndexer\TestDynamicSetIndexer\Application.cs
            expando["preview:" + n] = iconUrl;

            //            Object
            //preview:App: "data:x"
            //__proto__: Object


            Console.WriteLine((object)expando);


            //            script: error JSC1000: No implementation found for this native method, please implement [static Microsoft.CSharp.RuntimeBinder.Binder.SetIndex(Microsoft.CSharp.RuntimeBinder.CSharpBinderFlags, System.Type, System.Collections.Generic.IEnumerable`1[[Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo, Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a]])]
            //script: warning JSC1000: Did you reference ScriptCoreLib via IAssemblyReferenceToken?
            //script: error JSC1000: error at TestDynamicSetIndexer.Application..ctor,
            // type: TestDynamicSetIndexer.Application
            // offset: 0x005d
            //  method:Void .ctor(TestDynamicSetIndexer.HTML.Pages.IApp)
            //*** Compiler cannot continue... press enter to quit.



            //@"Hello world".ToDocumentTitle();
            //// Send data from JavaScript to the server tier
            //service.WebMethod2(
            //    @"A string from JavaScript.",
            //    value => value.ToDocumentTitle()
            //);
        }

    }
}
