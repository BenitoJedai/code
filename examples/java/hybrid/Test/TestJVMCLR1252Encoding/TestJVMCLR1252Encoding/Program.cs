#define JVM

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
using System.Runtime.InteropServices;


namespace TestJVMCLR1252Encoding
{

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

#if JVM
            //10-27 12:11:22.882: I/EstEIDNFC(28826): Last name 4dc44e4e494b9000

            var enc = new byte[] { 0x4D, 0xC4, 0x4E, 0x4E, 0x49, 0x4B };

            var res = Encoding.GetEncoding(1252).GetString(enc);
            // x = 41
            Console.WriteLine(

                new { res }

                );


            CLRProgram.CLRMain();

#else
           
            var enc = new byte[] { 0x4D, 0xC4, 0x4E, 0x4E, 0x49, 0x4B };

            var encSbyte = (sbyte[])(object)enc;

            java.lang.String res = new java.lang.String(encSbyte, "Windows-1252");

            Console.WriteLine(

               new { res }

            );

            CLRProgram.CLRMain();
#endif

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
