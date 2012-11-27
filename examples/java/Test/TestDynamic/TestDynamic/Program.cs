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
using System.Data;
using System.Dynamic;

namespace TestDynamic
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

            // generic parameter needs to be moved..
            //enumerable_10 = __Enumerable.AsEnumerable(__SZArrayEnumerator_1<String>.Of(stringArray3));

            Console.WriteLine("hi! vm:" + typeof(object).FullName);

            try
            {

                // will this compile?
                var x = default(DynamicDataReader);

                dynamic y = new MyDynamicObject();

                string foo = y.foo;

                Console.WriteLine(foo);
            }
            catch (Exception ex)
            {
                Console.WriteLine("error: " + new { ex.Message, ex.StackTrace });
            }

            CLRProgram.XML = new XElement("hello", "world");
            CLRProgram.CLRMain(
            );

        }


    }

    public class MyDynamicObject : DynamicObject
    {
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = "helo world for " + binder.Name;

            var retvalue = true;


            return retvalue;
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
