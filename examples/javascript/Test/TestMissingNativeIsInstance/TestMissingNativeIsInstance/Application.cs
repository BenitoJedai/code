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
using TestMissingNativeIsInstance;
using TestMissingNativeIsInstance.Design;
using TestMissingNativeIsInstance.HTML.Pages;

namespace TestMissingNativeIsInstance
{
    [Script(HasNoPrototype = true,
        ExternalTarget = "xMissing")]
    public class xMissing
    {
    }

    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        // X:\jsc.svn\examples\javascript\Test\TestIsInstNestedExternalTarget\TestIsInstNestedExternalTarget\Class1.cs

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            var z = new object();

            var u = z as xMissing;
            var i = z is xMissing;
            // Uncaught ReferenceError: xMissing is not defined 
            //d = ('xMissing' in __this && c instanceof xMissing? c : null);
            //e = ('xMissing' in __this && c instanceof xMissing);

        }

    }
}
