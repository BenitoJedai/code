using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace CLRJVMConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (null == args)
            {
                Console.WriteLine("args is null");
            }
            else
            {
                Console.WriteLine("args: " + args.Length);

                for (int i = 0; i < args.Length; i++)
                {
                    Console.WriteLine("#" + i + " " + args[i]);
                }
            }

            Console.WriteLine("CLR has loaded jvm.dll and created a new JVM");
            Console.WriteLine("this code is running inside JVM");
            Console.WriteLine("code within JVM will PInvoke into CLR code");
            Console.WriteLine("JVM cannot PInvoke into exe and this we shall use .exports mirror");

            CLRProgram.CLRMain();
        }
    }

    [SwitchToCLRContext]
    static class CLRProgram
    {
        [STAThread]
        public static void CLRMain()
        {
            Console.WriteLine("running inside CLR");

            Console.WriteLine(Path.GetFileName(Assembly.GetExecutingAssembly().Location));

            foreach (var item in AppDomain.CurrentDomain.GetAssemblies())
            {
                Console.WriteLine(Path.GetFullPath(item.Location) + " types: " + item.GetTypes().Length);
            }

            //Debugger.Break();
        }
    }
}
