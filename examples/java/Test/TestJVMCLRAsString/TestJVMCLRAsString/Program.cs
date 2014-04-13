using java.util.zip;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.BCLImplementation.System.Data;
using ScriptCoreLibJava.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;

namespace TestJVMCLRAsString
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


            System.Console.WriteLine(
               typeof(object).AssemblyQualifiedName
            );

            // X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Shared\Data\Diagnostics\WithConnectionLambda.cs

            try
            {

                {
                    object u = null;
                    var s = u as string;
                    Console.WriteLine(new { s });
                }

                {
                    //- javac
                    //"C:\Program Files (x86)\Java\jdk1.7.0_45\bin\javac.exe" -classpath "Y:\staging\web\java";release -d release java\TestJVMCLRAsString\Program.java
                    //java\TestJVMCLRAsString\Program.java:35: error: incompatible types
                    //        string1 = _Main_Isinst_0050(row2.get_Item("c"));
                    //                                   ^
                    //  required: String
                    //  found:    __String
                    //java\TestJVMCLRAsString\Program.java:42: error: incompatible types
                    //        return ((((Object)_0050) instanceof  String) ? (String)((Object)_0050) : (String)null);
                    //                                                     ^
                    //  required: __String
                    //  found:    String

                    var t = new DataTable();

                    t.Columns.Add("c");

                    var r = t.NewRow();

                    r["c"] = "hi";

                    //{ Message = , StackTrace = java.lang.NullPointerException
                    //        at ScriptCoreLib.Shared.BCLImplementation.System.Data.__DataRow.set_Item(__DataRow.java:27)
                    //        at TestJVMCLRAsString.Program.main(Program.java:39)
                    // }

                    //                   { Message = -1, StackTrace = java.lang.ArrayIndexOutOfBoundsException: -1
                    //       at ScriptCoreLib.Shared.BCLImplementation.System.Data.__DataRow.set_Item(__DataRow.java:39)
                    //       at ScriptCoreLib.Shared.BCLImplementation.System.Data.__DataRow.set_Item(__DataRow.java:27)
                    //       at TestJVMCLRAsString.Program.main(Program.java:40)
                    //}

                    var z = r["c"];
                    var s = z as string;
                    //var s = r["c"] as string;
                    Console.WriteLine(new { s });

                    //java.lang.Object, rt
                    //{ s =  }
                    //{ s = hi }
                    //System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(new { ex.Message, ex.StackTrace });

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
