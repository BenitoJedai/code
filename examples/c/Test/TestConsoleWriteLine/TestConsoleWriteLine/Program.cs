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

        static void Main(string[] args)
        {
            ScriptCoreLibNative.SystemHeaders.stdio_h.puts("hello");
        }
    }
}
