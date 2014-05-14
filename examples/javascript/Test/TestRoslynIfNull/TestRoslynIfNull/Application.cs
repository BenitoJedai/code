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
using TestRoslynIfNull;
using TestRoslynIfNull.Design;
using TestRoslynIfNull.HTML.Pages;

using ScriptCoreLib.JavaScript.Native;

namespace TestRoslynIfNull
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

            // X:\jsc.svn\examples\java\test\TestRoslynIfNull\TestRoslynIfNull\Class1.cs

            //var f = default(object);
            var f = new object();

            css.style.backgroundColor = "yellow";

            // 20140514. this will need attention at some point
            // javascript seems to be happy with roslyn causing a fault cmp? :P
            if (f == null)
                css.style.backgroundColor = "red";

        }

    }
}
