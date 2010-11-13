using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Reflection;
using ClassLibrary1;
using ScriptCoreLib;
using System.IO;
using System.Xml.Linq;

namespace RewriteToJavaConsoleApplication
{
    public partial class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("hello world");
            Class1.Foo();

            var doc1 = new XElement("request");

#if FEATURE_XElement
            Console.WriteLine(doc1.XMethod2().ToString());
#else
            var _doc1 = doc1.ToString();
            Console.WriteLine("calling XMethod2: " + _doc1);
            var _result1 = _doc1.XMethod2();
            Console.WriteLine("returned XMethod2: " + _result1);

            Console.WriteLine(XElement.Parse(_result1).ToString());
#endif

            //var x = new CLRProgram();
            ExtensionsToSwitchToCLRContext.StaticMethod1("hello");

            Console.WriteLine("jvm".StaticMethod1());
        }


    }


    [SwitchToCLRContext]
    static class ExtensionsToSwitchToCLRContext
    {
        // XElements are passed by value!
#if FEATURE_XElement
        public static XElement XMethod2(this XElement x)
        {
#else
        public static string XMethod2(this string _x)
        {
            Console.WriteLine("enter XMethod2: " + _x);
            var x = XElement.Parse(_x);
#endif
            var doc = new XElement("result");

            doc.Add(x);

#if FEATURE_XElement
            return doc;
        }
#else
            var _doc = doc.ToString();
            Console.WriteLine("exit XMethod2: " + _doc);

            // clr aint returning this string?
            return _doc;
        }
#endif

        public static string StaticMethod1(this string args, string message2 = "ok")
        {
            Console.WriteLine("CLR!!: " + DateTime.Now + args);

            try
            {

                //Console.WriteLine(new { Environment.CurrentDirectory });
                //Console.WriteLine(new { GetExecutingAssembly = Assembly.GetExecutingAssembly().Location });
                //Console.WriteLine(new { GetCallingAssembly = Assembly.GetCallingAssembly().Location });

                if (Assembly.GetEntryAssembly() == null)
                    Console.WriteLine("CLR adhoc? GetEntryAssembly is null");
                else

                    Console.WriteLine("CLR: " + new { GetEntryAssembly = Assembly.GetEntryAssembly().Location });

                Console.WriteLine(message2);




                Foo1.Foo1Method();

            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.ToString());
            }

            return args + " ***";
        }

    }

    [Obfuscation(Exclude = true, ApplyToMembers = true)]
    class Foo1
    {
        public string Bar;

        public static void Foo1Method()
        {
            ClassLibraryForCLR.Class1.Foo();
        }
    }
}
