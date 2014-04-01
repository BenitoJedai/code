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
using TestTypeNameAndAssemblyName;
using TestTypeNameAndAssemblyName.Design;
using TestTypeNameAndAssemblyName.HTML.Pages;

namespace TestTypeNameAndAssemblyName
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
            // Error	2	Cannot implicitly convert type 'AnonymousType#1' to 'string'	X:\jsc.svn\examples\javascript\test\TestTypeNameAndAssemblyName\TestTypeNameAndAssemblyName\Application.cs	33	46	TestTypeNameAndAssemblyName

            // { Name = Application, Namespace = <Namespace>, Assembly = TestTypeNameAndAssemblyName.Application }

            Native.document.body.innerText = new
            {
                typeof(Application).Name,
                typeof(Application).Namespace,
                Assembly = typeof(Application).Assembly.GetName().Name,
            }.ToString();

        }

    }
}
