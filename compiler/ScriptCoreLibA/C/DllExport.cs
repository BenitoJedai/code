using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ScriptCoreLib.C
{
    [AttributeUsage(AttributeTargets.Method)]
    public class DllExportAttribute : Attribute
    {
        // System.Runtime.InteropServices.DllImport

        // we need an attribute to 
        // mark methods we want to export in PE/C
        // name clash?

        // X:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Ultra\IL\ILAssembly.cs

        // X:\jsc.svn\examples\c\Test\TestConsoleWriteLine\TestConsoleWriteLine\Program.cs

        public CallingConvention CallingConvention;


        public DllExportAttribute()
        {

        }

        public DllExportAttribute(string dllName)
        {
            //DllImportAttribute

        }
    }
}
