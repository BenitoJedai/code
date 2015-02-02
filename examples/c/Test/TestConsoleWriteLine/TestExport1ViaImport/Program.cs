using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TestExport1ViaImport
{
    class Program
    {
        //---------------------------
        //Microsoft Visual Studio
        //---------------------------
        //A reference to 'X:\jsc.svn\examples\c\Test\TestConsoleWriteLine\TestConsoleWriteLine\bin\Release\web\TestConsoleWriteLine.exe' could not be added.Please make sure that the file is accessible, and that it is a valid assembly or COM component.
        //---------------------------
        //OK
        //---------------------------


        // https://social.msdn.microsoft.com/Forums/vstudio/en-US/c7fd652c-7875-43da-bfd9-b8c02201ad84/dllimport-exe-how-to-initialize-runtime-library

        // http://blogs.msdn.com/b/freik/archive/2006/03/07/x64-hotpatchability.aspx

        [DllImport("TestConsoleWriteLine.exe"
            // , CallingConvention = CallingConvention.StdCall
            )]
        extern static long TheExport1();
        // long long TheExport1(void)

        static void Main(string[] args)
        {
            Console.WriteLine(new { Environment.Is64BitProcess });


            // Additional information: An attempt was made to load a program with an incorrect format. (Exception from HRESULT: 0x8007000B)

            // Additional information: Unable to load DLL 'TestConsoleWriteLine.dll': The specified module could not be found. (Exception from HRESULT: 0x8007007E)
            // http://www.syndicateofideas.com/posts/fighting-the-msvcrt-dll-hell
            // https://www.wireshark.org/docs/wsdg_html_chunked/ChToolsMSChain.html
            // http://stackoverflow.com/questions/2727187/creating-dll-and-lib-files-with-the-vc-command-line
            // http://www.dotnetperls.com/dllimport

            ///dll
            ///implib:TestConsoleWriteLine.lib
            ///out:TestConsoleWriteLine.dll
            //TestConsoleWriteLine.dll.obj
            //   Creating library TestConsoleWriteLine.lib and object TestConsoleWriteLine.exp

            // http://stackoverflow.com/questions/24297616/dllimport-an-attempt-was-made-to-load-a-program-with-an-incorrect-format
            //{ Is64BitProcess = True }
            //hello

            // The program '[9300] TestExport1ViaImport.vshost.exe' has exited with code 1073741855 (0x4000001f).
            // 'TestExport1ViaImport.exe' (Win32): Loaded 'C:\Windows\SysWOW64\msvcr100.dll'. Cannot find or open the PDB file.
            // Additional information: Attempted to read or write protected memory. This is often an indication that other memory is corrupt.
            var value = TheExport1();

            Console.WriteLine(new { value });

            //            hello.Enable native code debugging
            //{ value = 64 }

        }
    }
}
