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
using TestDynamicGetIndex;
using TestDynamicGetIndex.Design;
using TestDynamicGetIndex.HTML.Pages;

namespace TestDynamicGetIndex
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
            // X:\jsc.svn\core\ScriptCoreLib\Shared\BCLImplementation\System\Dynamic\GetIndexBinder.cs


            dynamic x = new {foo = "goo" };

            // script: error JSC1000: No implementation found for this native method, please implement [static Microsoft.CSharp.RuntimeBinder.Binder.GetIndex(Microsoft.CSharp.RuntimeBinder.CSharpBinderFlags, System.Type, System.Collections.Generic.IEnumerable`1[[Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo, Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a]])]

            new IHTMLPre { new { foo = x["foo"] } }.AttachToDocument();

        }

    }
}
