using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Components;
using System;
using System.Linq;
using System.Xml.Linq;

namespace WebGLCity
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    //[System.ComponentModel.DesignerCategory("Component")]
    public sealed class ApplicationWebService : ApplicationWebServiceComponent
    {
        // The designer could not be shown for this file because none of the classes within it can be designed. The designer inspected the following classes in the file: ApplicationWebService --- The base class 'System.Object' cannot be designed. 

        // http://stackoverflow.com/questions/399768/visual-studio-make-view-code-default
        // http://stackoverflow.com/questions/155810/how-do-i-configure-visual-studio-to-use-the-code-view-as-the-default-view-for-we

        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public void WebMethod2(string e, Action<string> y)
        {
            // Send it back to the caller.
            y(e);
        }

    }
}
