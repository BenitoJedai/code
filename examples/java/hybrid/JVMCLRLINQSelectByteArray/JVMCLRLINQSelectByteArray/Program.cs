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
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace JVMCLRLINQSelectByteArray
{


    static class Program
    {
        #region hex
        static Func<byte[], string> hex =
            bytes =>
            {
                if (bytes == null)
                    return "null";

                var v = "";


                for (int i = 0; i < bytes.Length; i++)
                {
                    v += bytes[i].ToString("x2");

                    if (i % 16 == 15)
                        v += "\n";
                    else
                        if (i % 16 == 7)
                            v += "  ";

                    // tab wont show in debug monitor
                        //v += "\t";
                        else
                            v += " ";
                }

                return v;
            };
        #endregion


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {

            System.Console.WriteLine(
               typeof(object).AssemblyQualifiedName
            );

            // X:\jsc.svn\examples\javascript\async\Test\TestBytesToSemaphore\TestBytesToSemaphore\Application.cs

            var bytes1 = new byte[] { 0, 1, 2, 3 };

            //  required: T#1[]
            //  found: byte[]
            //  reason: actual argument byte[] cannot be converted to Byte[] by method invocation conversion
            //  where T#1,T#2 are type-variables:
            //    T#1 extends Object declared in method <T#1>Of(T#1[])
            //    T#2 extends Object declared in class __SZArrayEnumerator_1
            //java\JVMCLRLINQSelectByteArray\Program.java:164: error: possible loss of precision
            //            x[i] = ((Short)e[i]).shortValue();
            //                                           ^

            var bytes2 = Enumerable.ToArray(
                 from x in bytes1
                 let y = (byte)(x ^ 0xff)
                 select y
             );

            Console.WriteLine(hex(bytes2));

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
