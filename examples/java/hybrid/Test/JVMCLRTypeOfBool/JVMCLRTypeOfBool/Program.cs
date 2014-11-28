using java.util.zip;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLibJava.BCLImplementation.System;
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

namespace JVMCLRTypeOfBool
{

    static class Program
    {
        // X:\jsc.svn\examples\java\Test\TestTypeOfBool\TestTypeOfBool\Class1.cs

        public static bool field1;

        [STAThread]
        public static void Main(string[] args)
        {
            // jsc needs to see args to make Main into main for javac..


            // see also>
            // X:\jsc.svn\examples\javascript\android\AndroidBroadcastLogger\AndroidBroadcastLogger\ApplicationWebService.cs

            System.Console.WriteLine(
               typeof(object).AssemblyQualifiedName
            );

            var t = typeof(Program);

            var f = t.GetField("field1");

            //{ FieldType = boolean, ElementType =  }
            //{ Tbool = java.lang.Boolean, ElementType =  }

            Console.WriteLine(new { f.FieldType, isBoolean = f.FieldType == typeof(bool) });

            __Type TBoolean = typeof(java.lang.Boolean);
            __Type TBoolean_primitive = java.lang.Boolean.TYPE;
            __Type Tbool = typeof(bool);

            // { TBoolean = java.lang.Boolean, TBoolean_primitive = boolean, Tbool = java.lang.Boolean, isPrimitive = false }

            //{ FieldType = boolean }
            //{ Tbool = java.lang.Boolean }

            // http://www.thecodingforums.com/threads/difference-between-boolean-and-java-lang-boolean.585702/
            // Boolean is a boolean
            //primitive wrapped up in an Object.

            //{ FieldType = boolean, ElementType =  }
            //{ TBoolean = java.lang.Boolean, Tbool = java.lang.Boolean, isPrimitive = false }

            // Each wrapper class contains a field named TYPE which is equal to the Class for the primitive type being wrapped.
            // The value of Double.TYPE is identical to that of double.class.
            // { Tbool = java.lang.Boolean, isPrimitive = false }
            Console.WriteLine(new { TBoolean, TBoolean_primitive, Tbool, isPrimitive = Tbool.InternalTypeDescription.isPrimitive() });


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
