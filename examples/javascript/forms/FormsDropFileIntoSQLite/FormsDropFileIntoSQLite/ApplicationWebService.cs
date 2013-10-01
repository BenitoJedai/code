using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.ComponentModel;
using System.Linq;
using System.Xml.Linq;

namespace FormsDropFileIntoSQLite
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed partial class ApplicationWebService : Component
    {

//no implementation for System.IO.FileSystemInfo 1f0e8db5-8f52-3360-8a47-9d3dc3a5acaf
//script: error JSC1000: No implementation found for this native method, please implement [System.IO.FileSystemInfo.get_Extension()]
//script: warning JSC1000: Did you reference ScriptCoreLib via IAssemblyReferenceToken?
//script: error JSC1000: error at FormsDropFileIntoSQLite.ApplicationControl.<dataGridView1_DragDrop>b__2,
// assembly: V:\FormsDropFileIntoSQLite.Application.exe
// type: FormsDropFileIntoSQLite.ApplicationControl, FormsDropFileIntoSQLite.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// offset: 0x001f
//  method:Void <dataGridView1_DragDrop>b__2(System.String)
//*** Compiler cannot continue... press enter to quit.

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
