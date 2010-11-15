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
using System.Runtime.InteropServices;
using ScriptCoreLib.Interop;

namespace RewriteToJavaConsoleApplication
{
    public partial class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("hello world");
            Class1.Foo();

#if TEST_XElement
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
#endif

            //var x = new CLRProgram();
            //ExtensionsToSwitchToCLRContext.StaticMethod1("hello");

            var retval1 = new IntPtrInfo(IntPtr.Size);

            "jvm".StaticMethod1(retval1);



            //Console.WriteLine("r: 0x" + r.ToString("x8"));
            Console.WriteLine("r: " + Marshal.PtrToStringAnsi(retval1.IntPtrValue));

            //var r1 = Marshal.PtrToStringAnsi(r);
            //Marshal.FreeHGlobal(r);
            ExtensionsToSwitchToCLRContext.StaticMethod1_free(retval1);
            Marshal.FreeHGlobal(retval1);

            //Console.WriteLine("r1: " + r1);
        }


    }


    [SwitchToCLRContext]
    static class ExtensionsToSwitchToCLRContext
    {
        // XElements are passed by value!
#if TEST_XElement
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
            //var _doc = doc.ToString();
            var _doc = "err";
            Console.WriteLine("exit XMethod2: " + _doc);
         
            // clr aint returning this string?
            return _doc;
        }
#endif
#endif

        public static void StaticMethod1_free(IntPtr retval1)
        {
            Marshal.FreeHGlobal(Marshal.ReadIntPtr(retval1));
        }

        public static string StaticMethod1(this string args, IntPtr retval1, string message2 = "ok")
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

            IntPtrInfo nn = args + " ***";

            Console.WriteLine("nn: 0x" + nn.Pointer.ToString("x8"));
            // we have our LPString...

            var __retval1 = new IntPtrInfo(8, retval1);
            var __retval2 = Marshal.StringToHGlobalAnsi(args + " ***");
            __retval1.Int64Value = __retval2.ToInt64();

            return "hello world!!!";
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
