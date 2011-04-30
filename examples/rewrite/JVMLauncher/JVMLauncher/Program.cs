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



    static class Program
    {

        static void Main(string[] args)
        {
            // http://java.sun.com/j2se/1.4.2/runtime_win32.html

            var jre = RegistryKey.OpenBaseKey(
                RegistryHive.LocalMachine,
                RegistryView.Registry32
            ).OpenSubKey(@"SOFTWARE\JavaSoft\Java Runtime Environment");

            var jvm = (string)jre.OpenSubKey((string)jre.GetValue("CurrentVersion")).GetValue("RuntimeLib");

            var f = Path.GetFileName(Assembly.GetExecutingAssembly().Location);

            Console.WriteLine(f);

            
            var CLASS_PATH = @"-Djava.class.path=Z:\jsc.svn\examples\java\CLRJVMConsole\CLRJVMConsole\bin\Debug\staging\web\bin\CLRJVMConsole.dll";

            if (f == "JVMLauncher.exe.bar.exe")
                CLASS_PATH = @"-Djava.class.path=" + Path.GetFullPath(Assembly.GetExecutingAssembly().Location); 

            NativeProgram.JVMMain(
                RUNTIME_DLL: jvm,
                CLASS_PATH: CLASS_PATH,
                args:
                new[]
                {
                    "foo",
                    "bar"
                }
            );

        }
    }

}
