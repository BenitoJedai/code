using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using ScriptCoreLibJava.Extensions;

namespace CLRJVMNullString
{
    sealed class FooAttribute : Attribute
    {

    }

    [Foo]
    class Bar
    {

    }

    public class Program
    {
        // X:\jsc.svn\core\ScriptCoreLibJava.Web\ScriptCoreLibJava.Web\BCLImplementation\System\Web\HttpResponse.cs
        // X:\jsc.svn\examples\javascript\Test\TestServiceNullStringField\TestServiceNullStringField\ApplicationWebService.cs
        // X:\jsc.svn\core\ScriptCoreLibJava.Web\ScriptCoreLibJava.Web\BCLImplementation\System\Web\HttpResponse.cs


        public static void Main(string[] args)
        {
            Console.WriteLine("running: " + typeof(object));


            //running: System.Object
            //Set-Cookie:
            //Set-Cookie:
            //Set-Cookie:null
            //running: System.Object


            //running: java.lang.Object
            //Set-Cookie:
            //Set-Cookie:
            //Set-Cookie:
            //Set-Cookie:null
            //running: System.Object




            string FieldNull = null;
            string FieldEmpty = "";
            string FieldNullString = "null";

            //IL_0029:  ldstr      "Set-Cookie:"
            //  IL_002e:  ldloc.0
            //  IL_002f:  call       string [mscorlib]System.String::Concat(string,
            //                                                              string)
            //  IL_0034:  call       void [mscorlib]System.Console::WriteLine(string)


            Console.WriteLine("Set-Cookie:" + FieldNull);
            Console.WriteLine(string.Concat("Set-Cookie:", FieldNull));
            Console.WriteLine("Set-Cookie:" + FieldEmpty);
            Console.WriteLine("Set-Cookie:" + FieldNullString);

            CLRProgram.CLRMain();
        }


    }

    [SwitchToCLRContext]
    static class CLRProgram
    {
        [STAThread]
        public static void CLRMain()
        {
            Console.WriteLine("running: " + typeof(object));

            Console.ReadKey();
        }
    }
}
