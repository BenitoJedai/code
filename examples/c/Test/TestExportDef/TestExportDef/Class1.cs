using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: System.Reflection.Obfuscation(Feature = "script")]


namespace TestExportDef
{
    public class Class1
    {
        // __declspec(dllexport) void InvokeCLRFromCRT(LPTestExportDefSharedProject_Foo);
        // __declspec(dllexport) void __stdcall InvokeCLRFromCRT(TestExportDefSharedProject_Foo);

        // TestExportDef.exp : error LNK2001: unresolved external symbol InvokeCLRFromCRT


        //[ScriptCoreLib.C.DllExport]
        [ScriptCoreLib.C.DllExport(CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        unsafe static void InvokeCLRFromCRT(TestExportDefSharedProject.Foo3 f)
        {

        }

        [ScriptCoreLib.C.DllExport(CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        unsafe static long sizeof_Foo3() => sizeof(TestExportDefSharedProject.Foo3);
    }
}
