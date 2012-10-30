using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.ComponentModel;
using System.Linq;
using System.Xml.Linq;

namespace AssetsLibraryDesignerExperiment.Forms
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed partial class ApplicationWebService
    {
//         [javac] T:\src\ScriptCoreLibJava\BCLImplementation\System\ComponentModel\__Component.java:11: ScriptCoreLibJava.BCLImplementation.System.ComponentModel.__Component is not abstract and does not ove
//de abstract method System_IDisposable_Dispose() in ScriptCoreLib.Shared.BCLImplementation.System.__IDisposable
// [javac] public class __Component extends __MarshalByRefObject implements __IComponent, __IDisposable
// [javac]        ^

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
