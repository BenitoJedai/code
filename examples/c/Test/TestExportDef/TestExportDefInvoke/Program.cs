using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TestExportDefInvoke
{
    static class Program
    {
        [DllImport("TestExportDef.dll", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        extern static void InvokeCLRFromCRT(this TestExportDefSharedProject.Foo3 f);

        [DllImport("TestExportDef.dll", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        extern unsafe static long sizeof_Foo3();// => sizeof(TestExportDefSharedProject.Foo3);

        unsafe static void Main(string[] args)
        {
            // Show Details	Severity	Code	Description	Project	File	Line
            //Error CS1708  Fixed size buffers can only be accessed through locals or fields TestExportDefInvoke Program.cs  21

            // Additional information: Unable to find an entry point named 'sizeof_Foo3' in DLL 'TestExportDef.dll'.

            var s = sizeof_Foo3();

            // http://www.dotnetperls.com/fixed-buffer

            var f = new TestExportDefSharedProject.Foo
            {
                value8 = 66,

                //EmailID = EmailID
            };

            //Error CS0821  Implicitly - typed local variables cannot be fixed    TestExportDefInvoke Program.cs  32
            //Error CS0213  You cannot use the fixed statement to take the address of an already fixed expression TestExportDefInvoke Program.cs  34

            f.EmailID[0] = (byte)'1';
            f.EmailID[1] = (byte)'A';
            f.EmailID[2] = (byte)'C';

            //fixed (var e = f.EmailID)
            //{
            //    e = "hello";

            //}

            var f3 = new TestExportDefSharedProject.Foo3 { z = f };
            f3.x.value8 = 3;

            f3.InvokeCLRFromCRT();

        }
    }
}
