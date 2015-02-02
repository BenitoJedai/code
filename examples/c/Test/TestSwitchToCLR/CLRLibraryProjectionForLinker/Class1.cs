using ScriptCoreLib.C;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: Obfuscation(Feature = "script")]

namespace CLRLibraryProjectionForLinker
{
    public class Class1
    {
        // .export [1] as 'Export2'

        // dllExport?

        [DllExport(CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static void Export2() { }
        [DllExport(CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static long Export4(long u) => default(long);
        [DllExport(CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public unsafe static long ExportPointer(global::CLRLibraryDllExportDefinition.uvec3ptr* p) => default(long);
        //[DllExport(CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [DllExport(CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        // no decoration
        public unsafe static long Export196(CLRLibraryDllExportDefinition.uvec3* u) => default(long);

        //public unsafe static long Export196(ref CLRLibraryDllExportDefinition.uvec3 u) => CLRLibraryCSharp.Class1.Export196(ref u);

        //Additional information: The runtime has encountered a fatal error.The address of the error was at 0x6f94af0f, on thread 0x23e4. The error code is 0xc0000005. This error may be a bug in the CLR or in the unsafe or non-verifiable portions of user code.Common sources of this bug include user marshaling errors for COM-interop or PInvoke, which may corrupt the stack.


        //Additional information: Could not load file or assembly 'CLRLibraryDllExportDefinition, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null' or one of its dependencies.The system cannot find the file specified.

    }
}
