using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel;
using System.Diagnostics;

namespace ScriptCoreLibJava.BCLImplementation.System.Diagnostics
{
    // http://referencesource.microsoft.com/#System/services/monitoring/system/diagnosticts/Process.cs
    // https://github.com/dotnet/corefx/blob/master/src/System.Diagnostics.Process/src/System/Diagnostics/Process.cs


    [Script(Implements = typeof(global::System.Diagnostics.Process))]
    internal class __Process : __Component
    {
        // X:\jsc.svn\examples\java\async\test\JVMCLRTCPServerAsync\JVMCLRTCPServerAsync\Program.cs



        public static Process Start(string fileName)
        {
            Console.WriteLine(new { fileName });

            return default(Process);
        }
    }
}
