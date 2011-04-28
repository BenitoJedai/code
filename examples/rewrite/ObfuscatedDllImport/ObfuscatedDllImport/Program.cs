using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace ObfuscatedDllImport
{
    class Program
    {
        [DllImport("kernel32"/*, EntryPoint = "LoadLibrary", SetLastError = true*/)]
        static extern IntPtr LoadLibrary(string lpFileName);


        static void Main(string[] args)
        {
            var RUNTIME_DLL = "C:\\Program Files\\Java\\jdk1.6.0_24\\jre\\bin\\client\\jvm.dll";

            var handle = LoadLibrary(RUNTIME_DLL);
        }
    }
}
