using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLibJava.Extensions;
using System.Xml.Linq;
using java.net;
using java.util.zip;
using System.Collections;
using System.IO;
using ScriptCoreLib.GLSL;

namespace TestTypeOfPrimitive
{
    class x
    {
        public string Name;
        public string Value;
    }

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            //            java.lang.Character
            //char
            //java.lang.Integer
            //int
            //java.lang.Long
            //long
            //jvm
            //<hello>world</hello>

            char c = (char)0;
            Console.WriteLine(((object)c).GetType());
            Console.WriteLine(typeof(char));

            //byte bc = (byte)0;
            //Console.WriteLine(((object)bc).GetType());
            //Console.WriteLine(typeof(byte));

            int ibc = (int)0;
            Console.WriteLine(((object)ibc).GetType());
            Console.WriteLine(typeof(int));

            long libc = (long)0;
            Console.WriteLine(((object)libc).GetType());
            Console.WriteLine(typeof(long));

            System.Console.WriteLine("jvm");


            CLRProgram.XML = new XElement("hello", "world");
            CLRProgram.CLRMain(
            );

        }


    }

    static class XX
    {
        public static IEnumerable<int> GetIndecies(this string e, string f)
        {
            var a = new List<int>();

            var p = 0;
            var i = e.IndexOf(f, p);
            while (i >= 0)
            {
                p = i + 1;

                a.Add(i);

                i = e.IndexOf(f, p);
            }

            return a;
        }
    }

    public delegate XElement XElementFunc();

    [SwitchToCLRContext]
    static class CLRProgram
    {
        public static XElement XML { get; set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void CLRMain(
             StringAction ListMethods = null
            )
        {
            System.Console.WriteLine(XML);

            MessageBox.Show("it works?!?");
        }
    }

}
