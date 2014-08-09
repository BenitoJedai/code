using java.util.zip;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLibJava.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;

namespace JVMCLRBase64
{

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            // jsc needs to see args to make Main into main for javac..


            System.Console.WriteLine(
               typeof(object).AssemblyQualifiedName
            );

            //24a8:02:01 after worker yield...
            //24a8:01:01 [jsc.meta] worker unloading... { Count = 0 }

            //Unhandled Exception: System.CannotUnloadAppDomainException: Error while unloading appdomain. (Exception from HRESULT: 0x80131015)
            //   at System.AppDomain.Unload(AppDomain domain)

            //enter ToBase64String
            //enter FromBase64String
            //{ w0ms = 136, w1ms = 59, bytes = 116512, bytes999 = 65536, string0 = 87384 }

            // why is using the MemoryStream so slow?
            // { w0ms = 11777, w1ms = 11619, bytes = 65536, bytes999 = 65536, string0 = 87384 }
            var len = 0x90000;
            //FromBase64String loop { FromBase64String_while_timeout = 0.00:00:03, i = 32768, Length = 32768, enc1 = 0, enc2 = 0, enc3 = 0, enc4 = 0 }
            //{ w0ms = 3590, w1ms = 3548, bytes = 24576, bytes999 = 24576, string0 = 32768 }

            //Implementation not found for type import :
            //type: System.Text.StringBuilder
            //method: Void set_Capacity(Int32)
            //Did you forget to add the [Script] attribute?
            //Please double check the signature!

            //assembly: C:\util\jsc\bin\ScriptCoreLib.dll

            for (int i = 0; i < 1; i++)
            {
                Console.WriteLine(new { i });

                var bytes999 = new byte[len];

                var w0 = System.Diagnostics.Stopwatch.StartNew();

                var string0 = Convert.ToBase64String(bytes999);
                w0.Stop();
                var w1 = System.Diagnostics.Stopwatch.StartNew();

                var bytes = Convert.FromBase64String(string0);

                //Implementation not found for type import :
                //type: System.Diagnostics.Stopwatch
                //method: Int64 get_ElapsedTicks()
                //Did you forget to add the [Script] attribute?
                //Please double check the signature!


                //System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
                //{ w0ms = 0, w1ms = 0, w0ticks = 78, w1ticks = 24, bytes = 999, bytes999 = 999, string0 = 1332 }
                //{ w0ms = 0, w1ms = 0, w0ticks = 24, w1ticks = 15, bytes = 999, bytes999 = 999, string0 = 1332 }
                //{ w0ms = 0, w1ms = 0, w0ticks = 36, w1ticks = 15, bytes = 999, bytes999 = 999, string0 = 1332 }
                //{ w0ms = 0, w1ms = 0, w0ticks = 35, w1ticks = 26, bytes = 999, bytes999 = 999, string0 = 1332 }
                //{ w0ms = 0, w1ms = 0, w0ticks = 21, w1ticks = 14, bytes = 999, bytes999 = 999, string0 = 1332 }
                //{ w0ms = 0, w1ms = 0, w0ticks = 19, w1ticks = 12, bytes = 999, bytes999 = 999, string0 = 1332 }
                //{ w0ms = 0, w1ms = 0, w0ticks = 33, w1ticks = 14, bytes = 999, bytes999 = 999, string0 = 1332 }
                //{ w0ms = 0, w1ms = 0, w0ticks = 19, w1ticks = 12, bytes = 999, bytes999 = 999, string0 = 1332 }


                //System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
                //{ w0ms = 0, w1ms = 0, w0ticks = 165, w1ticks = 71, bytes = 4096, bytes999 = 4096, string0 = 5464 }
                //{ w0ms = 0, w1ms = 0, w0ticks = 89, w1ticks = 50, bytes = 4096, bytes999 = 4096, string0 = 5464 }
                //{ w0ms = 0, w1ms = 0, w0ticks = 92, w1ticks = 50, bytes = 4096, bytes999 = 4096, string0 = 5464 }
                //{ w0ms = 0, w1ms = 0, w0ticks = 75, w1ticks = 44, bytes = 4096, bytes999 = 4096, string0 = 5464 }
                //{ w0ms = 0, w1ms = 0, w0ticks = 83, w1ticks = 44, bytes = 4096, bytes999 = 4096, string0 = 5464 }
                //{ w0ms = 0, w1ms = 0, w0ticks = 96, w1ticks = 56, bytes = 4096, bytes999 = 4096, string0 = 5464 }
                //{ w0ms = 0, w1ms = 0, w0ticks = 89, w1ticks = 56, bytes = 4096, bytes999 = 4096, string0 = 5464 }
                //{ w0ms = 0, w1ms = 0, w0ticks = 84, w1ticks = 48, bytes = 4096, bytes999 = 4096, string0 = 5464 }


                //System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
                //{ w0ms = 0, w1ms = 0, w0ticks = 204, w1ticks = 98, bytes = 8192, bytes999 = 8192, string0 = 10924 }
                //{ w0ms = 0, w1ms = 0, w0ticks = 160, w1ticks = 83, bytes = 8192, bytes999 = 8192, string0 = 10924 }
                //{ w0ms = 0, w1ms = 0, w0ticks = 161, w1ticks = 89, bytes = 8192, bytes999 = 8192, string0 = 10924 }
                //{ w0ms = 0, w1ms = 0, w0ticks = 143, w1ticks = 81, bytes = 8192, bytes999 = 8192, string0 = 10924 }
                //{ w0ms = 0, w1ms = 0, w0ticks = 140, w1ticks = 81, bytes = 8192, bytes999 = 8192, string0 = 10924 }
                //{ w0ms = 0, w1ms = 0, w0ticks = 158, w1ticks = 81, bytes = 8192, bytes999 = 8192, string0 = 10924 }
                //{ w0ms = 0, w1ms = 0, w0ticks = 153, w1ticks = 87, bytes = 8192, bytes999 = 8192, string0 = 10924 }
                //{ w0ms = 0, w1ms = 0, w0ticks = 156, w1ticks = 88, bytes = 8192, bytes999 = 8192, string0 = 10924 }



                //java.lang.Object, rt
                //{ w0ms = 16, w1ms = 14, bytes = 999, bytes999 = 999, string0 = 1332 }
                //{ w0ms = 4, w1ms = 4, bytes = 999, bytes999 = 999, string0 = 1332 }
                //{ w0ms = 3, w1ms = 2, bytes = 999, bytes999 = 999, string0 = 1332 }
                //{ w0ms = 1, w1ms = 2, bytes = 999, bytes999 = 999, string0 = 1332 }
                //{ w0ms = 2, w1ms = 2, bytes = 999, bytes999 = 999, string0 = 1332 }
                //{ w0ms = 4, w1ms = 4, bytes = 999, bytes999 = 999, string0 = 1332 }
                //{ w0ms = 2, w1ms = 2, bytes = 999, bytes999 = 999, string0 = 1332 }
                //{ w0ms = 2, w1ms = 1, bytes = 999, bytes999 = 999, string0 = 1332 }

                //java.lang.Object, rt
                //{ w0ms = 53, w1ms = 48, bytes = 4096, bytes999 = 4096, string0 = 5464 }
                //{ w0ms = 34, w1ms = 33, bytes = 4096, bytes999 = 4096, string0 = 5464 }
                //{ w0ms = 33, w1ms = 31, bytes = 4096, bytes999 = 4096, string0 = 5464 }
                //{ w0ms = 28, w1ms = 28, bytes = 4096, bytes999 = 4096, string0 = 5464 }
                //{ w0ms = 28, w1ms = 27, bytes = 4096, bytes999 = 4096, string0 = 5464 }
                //{ w0ms = 43, w1ms = 42, bytes = 4096, bytes999 = 4096, string0 = 5464 }
                //{ w0ms = 30, w1ms = 30, bytes = 4096, bytes999 = 4096, string0 = 5464 }
                //{ w0ms = 36, w1ms = 35, bytes = 4096, bytes999 = 4096, string0 = 5464 }

                //java.lang.Object, rt
                //{ w0ms = 153, w1ms = 147, bytes = 8192, bytes999 = 8192, string0 = 10924 }
                //{ w0ms = 158, w1ms = 154, bytes = 8192, bytes999 = 8192, string0 = 10924 }
                //{ w0ms = 163, w1ms = 162, bytes = 8192, bytes999 = 8192, string0 = 10924 }
                //{ w0ms = 156, w1ms = 155, bytes = 8192, bytes999 = 8192, string0 = 10924 }
                //{ w0ms = 152, w1ms = 150, bytes = 8192, bytes999 = 8192, string0 = 10924 }
                //{ w0ms = 137, w1ms = 136, bytes = 8192, bytes999 = 8192, string0 = 10924 }
                //{ w0ms = 128, w1ms = 127, bytes = 8192, bytes999 = 8192, string0 = 10924 }
                //{ w0ms = 133, w1ms = 133, bytes = 8192, bytes999 = 8192, string0 = 10924 }


                Console.WriteLine(
                    new
                    {
                        w0ms = w0.ElapsedMilliseconds,

                        w1ms = w1.ElapsedMilliseconds,

#if DEBUG
                        // CLR too fast?

                        w0ticks = w0.ElapsedTicks,
                        w1ticks = w1.ElapsedTicks,
#endif
                        bytes = bytes.Length,
                        bytes999 = bytes999.Length,
                        string0 = string0.Length
                    }
                    );
                // clr:
                //{ ElapsedMilliseconds = 0, ElapsedTicks = 2190, bytes999 = 999, string0 = 1332 }
                //{ ElapsedMilliseconds = 0, ElapsedTicks = 8, bytes999 = 999, string0 = 1332 }
                //{ ElapsedMilliseconds = 0, ElapsedTicks = 5, bytes999 = 999, string0 = 1332 }
                //{ ElapsedMilliseconds = 0, ElapsedTicks = 5, bytes999 = 999, string0 = 1332 }
                //{ ElapsedMilliseconds = 0, ElapsedTicks = 21, bytes999 = 999, string0 = 1332 }
                //{ ElapsedMilliseconds = 0, ElapsedTicks = 6, bytes999 = 999, string0 = 1332 }
                //{ ElapsedMilliseconds = 0, ElapsedTicks = 18, bytes999 = 999, string0 = 1332 }
                //{ ElapsedMilliseconds = 0, ElapsedTicks = 6, bytes999 = 999, string0 = 1332 }

                // jvm
                //java.lang.Object, rt
                //{ ElapsedMilliseconds = 4, bytes999 = 999, string0 = 1332 }
                //{ ElapsedMilliseconds = 1, bytes999 = 999, string0 = 1332 }
                //{ ElapsedMilliseconds = 0, bytes999 = 999, string0 = 1332 }
                //{ ElapsedMilliseconds = 0, bytes999 = 999, string0 = 1332 }
                //{ ElapsedMilliseconds = 1, bytes999 = 999, string0 = 1332 }
                //{ ElapsedMilliseconds = 0, bytes999 = 999, string0 = 1332 }
                //{ ElapsedMilliseconds = 0, bytes999 = 999, string0 = 1332 }
                //{ ElapsedMilliseconds = 0, bytes999 = 999, string0 = 1332 }


            }



            CLRProgram.CLRMain();
        }


    }



    [SwitchToCLRContext]
    static class CLRProgram
    {

        [STAThread]
        public static void CLRMain()
        {
            System.Console.WriteLine(
                typeof(object).AssemblyQualifiedName
            );

            MessageBox.Show("click to close");

        }
    }


}
