using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using Microsoft.Win32;
using System.Reflection;

namespace JVMLauncher
{



    [Obfuscation(Exclude = true, ApplyToMembers = true)]
    public static class Program
    {

        static void Main(string[] args)
        {
            // http://java.sun.com/j2se/1.4.2/runtime_win32.html



            var f = Path.GetFullPath(Assembly.GetExecutingAssembly().Location);

            Console.WriteLine(f);


            JVMLauncher.Invoke(f, "CLRJVMConsole.Program",
                "hello",
                "world"
            );

        }
    }

}
