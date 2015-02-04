using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: System.Reflection.Obfuscation(Feature = "script")]


namespace TestExportDef
{
    // x:\jsc.svn\examples\c\test\testexportdef\testexportdef\bin\debug\web\TestExportDef.dll.h(39): error C2628: 'tag_unsigned' followed by 'short' is illegal (did you forget a ';'?)

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

        [ScriptCoreLib.C.DllExport(CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        unsafe static void DoCallback(Action y)
        {
            // https://msdn.microsoft.com/en-us/library/ektebyzx.aspx
            //var yy = (__Action)y;

            //yy();

            y();
        }

        [ScriptCoreLib.C.DllExport(CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        unsafe static void DoCallbackFoo8(TestExportDefSharedProject.FooSignal8* y, Action signal)
        {
            for (int i = 0; i < y->samplesLength; i++)
            {
                //  C : unable to emit stloc.2 at 'TestExportDef.Class1.DoCallbackFoo8'#0012: C : Opcode not implemented: ldind.u2 at TestExportDef.Class1.DoCallbackFoo8

                //var __value = y->samples[i];

            }
            y->value8 = 90;

            signal();

            var loc1 = y->value8;
            //y.value8++;
            loc1 = 88;
            y->value8 = loc1;
        }

        [ScriptCoreLib.C.DllExport(CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        //unsafe static void DoCallbackFoo(TestExportDefSharedProject.FooSignal* y)
        unsafe static void DoCallbackFoo(ref TestExportDefSharedProject.FooSignal y)
        {
            // X:\jsc.svn\examples\c\Test\TestByRefField\TestByRefField\Class1.cs

            // Show Details	Severity	Code	Description	Project	File	Line
            //Error CS0208  Cannot take the address of, get the size of, or declare a pointer to a managed type('FooSignal')   TestExportDef Class1.cs   44

            //TestExportDef.Class1
            //script: error JSC1000: C: Opcode not implemented: ldflda at TestExportDef.Class1.DoCallbackFoo

            // Opcode not implemented: ldind.i8 at TestExportDef.Class1.DoCallbackFoo
            //y.value8++;
            //  num0 = ((long long)y.value8);
            var loc1 = y.value8;
            //y.value8++;
            loc1 = 88;
            y.value8 = loc1;

            var loc2 = y.signal1;
            loc2();
            //y.signal1();
        }


        [ScriptCoreLib.C.DllExport(CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        unsafe static Action GetCallback()
        {
            // for now this is wont work correctly, and scope is lost
            // one option would be to allocate a new slot and bake the Target/this argument into it
            var scope = "scope";

            // we are to give a single pointer back to the external dll
            // how could we store the scope pointer for later lookup?
            return delegate
            {
                var u = "break!";
                var s = scope;

                //Debugger.Break();
            };
        }
    }

    [Script(IsNative = true, Implements = typeof(global::System.Action))]
    internal delegate void __Action();
}
