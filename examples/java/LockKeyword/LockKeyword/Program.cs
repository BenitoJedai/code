using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using ScriptCoreLibJava.Extensions;

namespace LockKeyword
{

    public class Program
    {
        public static object sync = new object();

        public static void Main(string[] args)
        {
            lock (sync)
            {
                Console.WriteLine();
            }

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
