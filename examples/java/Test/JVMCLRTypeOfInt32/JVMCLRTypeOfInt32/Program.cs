using java.util.zip;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLibJava.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;

namespace JVMCLRTypeOfInt32
{
    enum XKey : long
{}
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {

            System.Console.WriteLine(
               typeof(object).AssemblyQualifiedName
            );

            int int32 =33;
            object int32box = int32;

            // why are we using the boxed version of the type?
            // jsc is giving us the primitive? 
            Console.WriteLine(
                int32.GetType().FullName
            );

            Console.WriteLine(
                  int32box.GetType().FullName
              );


            //java.lang.Object, rt
            //java.lang.Integer
            //java.lang.Integer
            //int

            Console.WriteLine(
               typeof(int).FullName
           );

            Action<int> intAction = i =>
                {
                    Console.WriteLine(new { i });
                };

            intAction(55);

            //            Caused by: java.lang.ClassCastException: java.lang.Integer cannot be cast to java.lang.Long
            // { Message = java.lang.Integer cannot be cast to java.lang.Long }

            try
            {

                var int32x = int32box is int;
                var int64x = int32box is long;
                Console.WriteLine(new { int32x, int64x });

                // +		ex	{"Specified cast is not valid."}	System.Exception {System.InvalidCastException}
                var int64 = (XKey)Convert.ToInt64(int32box);
                Console.WriteLine(new { int64 });
            }
            catch (Exception ex)
            {
                Console.WriteLine(new { ex.Message });
            }


            CLRProgram.CLRMain();
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
        public static void CLRMain()
        {
            System.Console.WriteLine(
                typeof(object).AssemblyQualifiedName
            );

            MessageBox.Show("click to close");

        }
    }


}
