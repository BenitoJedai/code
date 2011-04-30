using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using Microsoft.Win32;

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


            NativeProgram.JVMMain(
                RUNTIME_DLL: jvm,

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
