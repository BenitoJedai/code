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

namespace TestJavaFinalIntegerField
{

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            // jsc needs to see args to make Main into main for javac..


            // see also>
            // X:\jsc.svn\examples\javascript\android\AndroidBroadcastLogger\AndroidBroadcastLogger\ApplicationWebService.cs
            // X:\jsc.svn\examples\javascript\android\com.abstractatech.battery\com.abstractatech.battery\ApplicationWebService.cs

            System.Console.WriteLine(
                new
                {
                    typeof(object).AssemblyQualifiedName,

                    // public const int BATTERY_HEALTH_COLD = 0;
                    Foo.Bar.BATTERY_HEALTH_COLD,
                    Foo.Bar.XLong,
                    Foo.Bar.XShort
                    ,
                    Foo.Bar.XSByte
                    , Foo.Bar.XFloat
                }
            );

            var t = typeof(Foo.Bar);


            // { item = Int32 BATTERY_HEALTH_COLD, constant = 0, ctype = System.Int32 }

            foreach (var item in t.GetFields())
            {
                // Operation is not valid due to the current state of the object.

                if (item.IsLiteral)
                {
                    //{ item = int BATTERY_HEALTH_COLD, constant = 7, ctype = java.lang.Integer }
                    //{ item = long XLong, constant = 77, ctype = java.lang.Long }
                    //{ item = short XShort, constant = 77, ctype = java.lang.Short }
                    //{ item = byte XSByte, constant = 77, ctype = java.lang.Byte }

                    var constant = item.GetRawConstantValue();
                    var ctype = constant.GetType();

                    var isInteger = ctype == typeof(int);
                    var isLong = ctype == typeof(long);
                    var isShort = ctype == typeof(short);
                    var isByte = ctype == typeof(byte);
                    //{ item = int BATTERY_HEALTH_COLD, constant = 7, ctype = java.lang.Integer, isInteger = true, isLong = false, isShort = false, isByte = false }
                    //{ item = long XLong, constant = 77, ctype = java.lang.Long, isInteger = false, isLong = true, isShort = false, isByte = false }
                    //{ item = short XShort, constant = 77, ctype = java.lang.Short, isInteger = false, isLong = false, isShort = true, isByte = false }
                    //{ item = byte XSByte, constant = 77, ctype = java.lang.Byte, isInteger = false, isLong = false, isShort = false, isByte = true }

                    Console.WriteLine(new { item, constant, ctype, isInteger, isLong, isShort, isByte });
                }


            }

            // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Reflection\FieldInfo.cs

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
