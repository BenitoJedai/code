using AsyncOrderByExpression;
using AsyncOrderByExpression.Design;
using AsyncOrderByExpression.HTML.Pages;
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
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace AsyncOrderByExpression
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        public readonly ApplicationControl content = new ApplicationControl();

        //0200004f ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms.__Control
        //script: error JSC1000: unsupported flow detected, try to simplify.
        // Assembly X:\jsc.svn\examples\javascript\future\AsyncOrderByExpression\AsyncOrderByExpression\bin\Release\ScriptCoreLib.Windows.Forms.dll
        // DeclaringType ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms.__Control, ScriptCoreLib.Windows.Forms, Version= 1.0.0.0, Culture= neutral, PublicKeyToken= null
        // OwnerMethod add_TextChanged
        // Offset 002d
        // . Try ommiting the return, break or continue instruction.

        // roslyn does not like events?

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            content.AttachControlToDocument();

        }

    }
}
