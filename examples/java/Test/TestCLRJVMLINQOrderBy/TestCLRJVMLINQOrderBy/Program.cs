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

namespace TestCLRJVMLINQOrderBy
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

            Console.WriteLine("hi!");

            try
            {
                Test();
            }
            catch (Exception ex)
            {
                // toString will return JVM style value.

                Console.WriteLine("error!\n\n" + new { ex.Message, ex.StackTrace } + "\n\n");

                // the old way
                //var x = (object)ex as java.lang.Throwable;
                //if (x != null)
                //{
                //    x.printStackTrace();
                //}

            }
            /*
{ isspecial = False, length = 11, name = Agent Smith }
{ isspecial = False, length = 7, name = _martin }
{ isspecial = False, length = 3, name = jay }
{ isspecial = True, length = 3, name = mac }
             * 
             * 
{ isspecial = false, length = 11, name = Agent Smith }
{ isspecial = false, length = 7, name = _martin }
{ isspecial = false, length = 3, name = jay }
{ isspecial = true, length = 3, name = mac }              
        */
            System.Console.WriteLine("done");





            System.Console.WriteLine("jvm");


            CLRProgram.XML = new XElement("hello", "world");
            CLRProgram.CLRMain(
            );

        }

        private static void Test()
        {
            var __users = "_martin, mike, mac, ken, neo, zen, jay, morpheous, trinity, Agent Smith, _psycho".Split(',');
            var user_filter = "a".Trim().ToLower();
            var user_filter2 = "mac".Trim().ToLower();

            var query = from i in __users
                        where i.ToLower().Contains(user_filter)
                        let name = i.Trim()
                        let isspecial = i.ToLower().Contains(user_filter2)
                        orderby isspecial ascending, name.Length descending, name
                        select new { isspecial, length = name.Length, name };


            query.WithEach(
                x =>
                {
                    Console.WriteLine(x);
                }
            );
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
