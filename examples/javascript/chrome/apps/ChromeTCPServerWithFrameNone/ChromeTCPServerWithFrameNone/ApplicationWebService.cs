using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Linq;
using System.Xml.Linq;

namespace ChromeTCPServerWithFrameNone
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
    {
        //Error	4	The task factory "CodeTaskFactory" could not be loaded from the assembly "C:\Program Files (x86)\MSBuild\12.0\bin\Microsoft.Build.Tasks.v4.0.dll".
        // Could not load file or assembly 'file:///C:\Program Files (x86)\MSBuild\12.0\bin\Microsoft.Build.Tasks.v4.0.dll' or one of its dependencies. The system cannot find the file specified.	ChromeTCPServerWithFrameNone


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
