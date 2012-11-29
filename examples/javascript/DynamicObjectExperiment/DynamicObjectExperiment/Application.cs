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
using DynamicObjectExperiment.Design;
using DynamicObjectExperiment.HTML.Pages;
using System.Dynamic;
using ScriptCoreLib.Shared.BCLImplementation.System.Dynamic;

namespace DynamicObjectExperiment
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
            var foo = new MyDynamic();

            dynamic bar = foo;

            bar.foo = "foo";
            Console.WriteLine("after foo");

            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }

    class MyDynamic : __DynamicObject
    {
        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            result = null;
            return true;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = null;
            return true;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            Console.WriteLine("TrySetMember");

            new IHTMLPre { innerText = "TrySetMember: " + new { value } }.AttachToDocument();
            return true;
        }
    }
}
