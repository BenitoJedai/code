using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        [DllImport("TestExportDef.dll", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        //extern unsafe static void DoCallback(IntPtr y);
        //extern unsafe static void DoCallback(IntPtr y);
        extern unsafe static void DoCallback(Action y);

        [DllImport("TestExportDef.dll", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        extern unsafe static Action GetCallback();

        [DllImport("TestExportDef.dll", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        //unsafe static void DoCallbackFoo(TestExportDefSharedProject.FooSignal* y)
        extern unsafe static void DoCallbackFoo(ref TestExportDefSharedProject.FooSignal y);


        [DllImport("TestExportDef.dll", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        //extern unsafe static void DoCallbackFoo8(this TestExportDefSharedProject.FooSignal8* y, Action signal);
        extern unsafe static void DoCallbackFoo8(TestExportDefSharedProject.FooSignal8* y, Action signal);

        // Show Details	Severity	Code	Description	Project	File	Line
        //Error CS1103  The first parameter of an extension method cannot be of type 'FooSignal8*'	TestExportDefInvoke Program.cs  33



        unsafe static void Foo(TestExportDefSharedProject.FooSignal8* aa)
        {

            DoCallbackFoo8(aa,

                delegate
                {
                    var __a = aa;

                }
            );
        }

        unsafe static void Main(string[] args)
        {
            // Show Details	Severity	Code	Description	Project	File	Line
            //Error CS1708  Fixed size buffers can only be accessed through locals or fields TestExportDefInvoke Program.cs  21

            // Additional information: Unable to find an entry point named 'sizeof_Foo3' in DLL 'TestExportDef.dll'.

            //var a = Marshal.GetFunctionPointerForDelegate(
            //    new Action(
            //        delegate
            //        {
            //            // y = 0x00000016
            //        }
            //    )
            //);

            var aa = new TestExportDefSharedProject.FooSignal8 { value8 = 7 };

            //        Show Details    Severity Code    Description Project File Line
            //Error CS1686  Local 'aa' or its members cannot have their address taken and be used inside an anonymous method or lambda expression   TestExportDefInvoke Program.cs  58

            Foo(&aa);


            var a = new TestExportDefSharedProject.FooSignal { };

            a.value8 = 8;

            a.signal1 = delegate
            {

                // can CLR keep scope? yes. but byref is out of sync.


                var __a = a;
                // old memory. if we use byref

                Debugger.Break();
            };

            DoCallbackFoo(ref a);
            // only now we get an update? framesync



            //DoCallback(a);
            DoCallback(
               delegate
               {
                   // y = 0x00000016
               }
            );

            var s = sizeof_Foo3();

            // https://limbioliong.wordpress.com/2011/06/19/delegates-as-callbacks-part-2/


            var yy = GetCallback();

            yy();


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
