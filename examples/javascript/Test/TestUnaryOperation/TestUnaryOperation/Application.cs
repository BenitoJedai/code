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
using TestUnaryOperation;
using TestUnaryOperation.Design;
using TestUnaryOperation.HTML.Pages;

namespace TestUnaryOperation
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
            // Uncaught Error: NotImplementedException: { binder = UnaryOperationBinder, operation = 83 }

            // Uncaught Error: NotImplementedException: __CallSite.Create 
            // script: error JSC1000: No implementation found for this native method, please implement 
            // [static Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(Microsoft.CSharp.RuntimeBinder.CSharpBinderFlags, System.Linq.Expressions.ExpressionType, System.Type, System.Collections.Generic.IEnumerable`1[[Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo, Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a]])]

            // script: error JSC1000: No implementation found for this native method, please implement 
            // [static Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(Microsoft.CSharp.RuntimeBinder.CSharpBinderFlags, System.Linq.Expressions.ExpressionType, System.Type, System.Collections.Generic.IEnumerable`1[[Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo, Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a]])]

            Native.document.title = "foo";
            dynamic doc = Native.document;

            if (doc.title == "foo")
            {
                Native.css.style.backgroundColor = "cyan";

            }
        }

    }
}
