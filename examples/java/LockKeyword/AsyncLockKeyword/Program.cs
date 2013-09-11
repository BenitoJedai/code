using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using ScriptCoreLibJava.Extensions;

namespace AsyncLockKeyword
{

    public class Program
    {
        public static object sync = new object();
        public static int i = 0;

        public static void Invoke(Action yield)
        {
            //    switch (i)
            //    {
            //        case 1: return;
            //        case 12: return;
            //        case 13: return;
            //        case 14: return;
            //        case 15: return;
            //        case 16: return;
            //        case 17: return;
            //        case 18: return;
            //        case 19: return;
            //        case 20: return;

            //        case 22:
            //            yield();
            //            break;
            //    }

        }

        public static void Main(string[] args)
        {
            lock (sync)
                Invoke(
                    delegate
                    {
                        Console.WriteLine(new { args.Length });
                    }
                );




            //public static  void main(String[] args)
            //{
            //    boolean flag0;
            //    Object object1;

            //    flag0 = false;
            //    synchronized (Program.sync)
            //    {
            //        __Console.WriteLine();
            //    }

            //}

        }
    }

}
