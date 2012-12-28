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
using System.Collections.Specialized;

namespace TestNameValueCollectionEnumerator
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


            Console.WriteLine("hi! vm:" + typeof(object).FullName);

            try
            {


                var Headers = new NameValueCollection();

                Headers["Connection"] = "close";


                foreach (var item in Headers.AllKeys)
                {
                    //android.util.Log.wtf("InternalWriteHeaders", item);

                    Console.WriteLine(item + ": " + Headers[item]);
                }

                var x = new StringDictionary();

                x["x"] = "x";

                foreach (var item in x)
                {
                    Console.WriteLine(item);
                }
            }
            catch (Exception ex)
            {
                //                             error! { Message = , StackTrace = java.lang.RuntimeException
                //       at ScriptCoreLibJava.BCLImplementation.System.Collections.Generic.__Dictionary_2.get_Count(__Dictionary_2.java:60)
                //       at ScriptCoreLib.Shared.BCLImplementation.System.Collections.Specialized.__StringDictionary.get_Count(__StringDictionary.java:46)
                //       at ScriptCoreLib.Shared.BCLImplementation.System.Collections.Specialized.__NameValueCollection.get_AllKeys(__NameValueCollection.java:31)
                //       at TestNameValueCollectionEnumerator.Program.main(Program.java:36)
                //}

                //               error! { Message = , StackTrace = java.lang.RuntimeException
                //       at ScriptCoreLibJava.BCLImplementation.System.Collections.Generic.__Dictionary_2___KeyCollection.global__System_Collections_IEnumerable_GetEnumerator(__Dictionary_2___KeyCollection.java:66)
                //       at ScriptCoreLibJava.BCLImplementation.System.Collections.Generic.__Dictionary_2___KeyCollection.System_Collections_IEnumerable_GetEnumerator(__Dictionary_2___KeyCollection.java:132)
                //       at ScriptCoreLib.Shared.BCLImplementation.System.Collections.Specialized.__NameValueCollection.get_AllKeys(__NameValueCollection.java:33)
                //       at TestNameValueCollectionEnumerator.Program.main(Program.java:36)
                //}

                //               error! { Message = , StackTrace = java.lang.RuntimeException
                //       at ScriptCoreLibJava.BCLImplementation.System.Collections.Generic.__Dictionary_2___KeyCollection___iterator.System_Collections_IEnumerator_get_Current(__Dictionary_2___KeyCollection___iterator.java:40)
                //       at ScriptCoreLib.Shared.BCLImplementation.System.Collections.Specialized.__NameValueCollection.get_AllKeys(__NameValueCollection.java:38)
                //       at TestNameValueCollectionEnumerator.Program.main(Program.java:36)
                //}


                System.Console.WriteLine("error! " + new { ex.Message, ex.StackTrace });
            }

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
