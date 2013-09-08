using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using ScriptCoreLibJava.Extensions;
using System.Threading;

namespace YieldKeywordExperiment
{

    public class Program
    {
        //0001 02000002 YieldKeywordExperiment.Program

        //public Object PublicField;
        //private Object InternalField;
        //private Object PrivateField;
        //private Object ProtectedField;

        //- javac
        //"C:\Program Files (x86)\Java\jdk1.6.0_35\bin\javac.exe" -classpath "Y:\staging\web\java";release -d release java\YieldKeywordExperiment\Program.java
        //Y:\staging\web\java\YieldKeywordExperiment\Program__GetBytes_d__0__MoveNext_.java:14: cannot find symbol
        //symbol  : class _ArrayType_20
        //location: class YieldKeywordExperiment.Program__GetBytes_d__0__MoveNext_
        //    private static _ArrayType_20 _MoveNext__0000__lookup;
        //                   ^

        public object PublicField;
        internal object InternalField;
        private object PrivateField;
        protected object ProtectedField;
        // Implementation not found for type import :
        // type: System.Threading.Thread
        // method: System.Threading.Thread get_CurrentThread()
        // Did you forget to add the [Script] attribute?
        // Please double check the signature!


        //- javac
        //"C:\Program Files (x86)\Java\jdk1.6.0_35\bin\javac.exe" -classpath "Y:\staging\web\java";release -d release java\YieldKeywordExperiment\Program.java
        //Y:\staging\web\java\YieldKeywordExperiment\Program__GetBytes_d__0__MoveNext_.java:14: cannot find symbol
        //symbol  : class _ArrayType_20
        //location: class YieldKeywordExperiment.Program__GetBytes_d__0__MoveNext_
        //    private static _ArrayType_20 _MoveNext__0000__lookup;
        //                   ^
        //Y:\staging\web\java\YieldKeywordExperiment\Program__GetBytes_d__0.java:67: __this has private access in YieldKeywordExperiment.Program__GetBytes_d__0__MoveNext_
        //        next_0.__this = this;
        //              ^
        //Y:\staging\web\java\YieldKeywordExperiment\Program__GetBytes_d__0.java:69: _ret has private access in YieldKeywordExperiment.Program__GetBytes_d__0__MoveNext_
        //        return next_0._ret;
        //                     ^
        //Y:\staging\web\java\YieldKeywordExperiment\Program__GetBytes_d__0.java:186: __this has private access in YieldKeywordExperiment.Program__GetBytes_d__0__MoveNext_
        //        num0 = _arg0.__this.__1__state;
        //                    ^
        //Y:\staging\web\java\YieldKeywordExperiment\Program__GetBytes_d__0.java:187: __loc1 has private access in YieldKeywordExperiment.Program__GetBytes_d__0__MoveNext_

        public static void Main(string[] args)
        {
            if (null == args)
            {
                Console.WriteLine("args is null");
            }
            else
            {
                Console.WriteLine("args: " + args.Length);

                for (int i = 0; i < args.Length; i++)
                {
                    Console.WriteLine("#" + i + " " + args[i]);
                }
            }

            Console.WriteLine("string is " + typeof(string).FullName);


            foreach (var item in GetBytes())
            {
                Console.WriteLine(new { item });
            }


            CLRProgram.CLRMain();
        }

//{ item = 0 }
//{ item = 2 }
//{ item = 1 }

        public static IEnumerable<byte> GetBytes()
        {
            yield return 0;
            Thread.Sleep(100);
            yield return 2;
            Thread.Sleep(100);
            yield return 1;
        }
    }

    [SwitchToCLRContext]
    static class CLRProgram
    {
        [STAThread]
        public static void CLRMain()
        {
            Console.WriteLine("running inside CLR");

            Console.WriteLine(Path.GetFileName(Assembly.GetExecutingAssembly().Location));

            foreach (var item in AppDomain.CurrentDomain.GetAssemblies())
            {
                Console.WriteLine(Path.GetFullPath(item.Location) + " types: " + item.GetTypes().Length);
            }

            Console.WriteLine("any key");
            Console.ReadKey(true);

        }
    }
}
