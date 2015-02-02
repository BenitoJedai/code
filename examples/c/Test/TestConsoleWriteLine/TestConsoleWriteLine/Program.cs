using ScriptCoreLib;
using ScriptCoreLib.C;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

[assembly: Obfuscation(Feature = "script")]


namespace TestConsoleWriteLine
{
    class Program
    {
        // can we compile a .lib into us?
        // can we compile us into .lib?

        // 1>CSC : error CS5001: Program does not contain a static 'Main' method suitable for an entry point
        static void Main(string[] args)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201502/20150201

            ScriptCoreLibNative.SystemHeaders.stdio_h.puts("hello. Enable native code debugging");

            // http://stackoverflow.com/questions/4532457/program-and-debugger-quit-without-indication-of-problem
            __debugbreak();

            //http://www.viva64.com/en/k/0035/

        }

        [Script(OptimizedCode = "__debugbreak();")]
        static void __debugbreak()
        {
        }

        // void TestConsoleWriteLine_Program_TheExport1(void);


        // [System.Runtime.InteropServices.DllImport]
        [DllExport]
        static long TheExport1()
        {
            Main(null);

            return 64;
        }
    }
}
