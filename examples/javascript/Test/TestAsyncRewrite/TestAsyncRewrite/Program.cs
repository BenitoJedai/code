using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: Obfuscation(Feature = "script")]
namespace xxx
{
    //struct goo
    //{
    //    public int x;

    //    class Closure
    //    {
    //        internal static void forwardref(ref goo x)
    //        {

    //        }
    //    }

    //    void MoveNext()
    //    {
    //        // what will jsc do wtih ref this?


    //        Closure.forwardref(ref this);

    //        var x = new goo { x = 7 };

    //        Closure.forwardref(ref x);

    //    }
    //}

    //static class X
    //{
    //    public static void WriteHashCode<T>(ref T x)
    //    {
    //        var HashCode = x.GetHashCode();

    //        Console.WriteLine(new { HashCode });
    //    }

    //}
    class Program : ScriptCoreLib.Shared.IAssemblyReferenceToken
    {

        // switch rewrite hangs for wait, why?
        //X:\jsc.svn\examples\javascript\Test\TestAsyncRewrite\TestAsyncRewrite\bin\Debug>TestAsyncRewrite.exe
        //enter goo
        //exit goo
        //exit Main

        //X:\jsc.svn\examples\javascript\Test\TestAsyncRewrite\TestAsyncRewrite\bin\Debug>TestAsyncRewrite.ZRewrite.exe
        //enter goo
        //exit goo

        // the problem happens with debug build of jsc





        static void Main(string[] args)
        {
            //enter goo
            //exit goo
            //exit Main

            //foo foo1;
            //foo1.x = 7;
            //X.WriteHashCode(ref foo1);

            //foo foo2 = foo1;
            //foo1.x = 2;
            //X.WriteHashCode(ref foo1);
            //X.WriteHashCode(ref foo2);

            ////{ HashCode = 346948955 }
            ////{ HashCode = 346948958 }
            ////{ HashCode = 346948955 }


            //Console.WriteLine(new { foo2.x });

            //            { TargetMethod = System.Threading.Tasks.Task goo(), DeclaringType = TestAsyncRewrite.Program, Location =
            // assembly: C:\Users\Arvo\AppData\Local\Temp\zzfg4muy.kjh\TestAsyncRewrite.exe
            // type: TestAsyncRewrite.Program, TestAsyncRewrite, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
            // offset: 0x0001
            //  method:Void Main(System.String[]) }
            //3438:02:01 RewriteToAssembly error: System.TypeLoadException: Could not load type '<goo>d__5' from assembly 'TestAsyncRewrite.JSRewrite, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'.
            //   at System.Reflection.Emit.TypeBuilder.TermCreateClass(RuntimeModule module, Int32 tk, ObjectHandleOnStack type)
            //   at System.Reflection.Emit.TypeBuilder.CreateTypeNoLock()
            //   at System.Reflection.Emit.TypeBuilder.CreateType()


            //script: error JSC1000: if block not detected correctly, opcode was { Branch = [0x000c] beq.s      +0 -2{[0x0009] ldloc.2    +1 -0} {[0x000a] ldc.i4.s   +1 -0} , Location =
            // assembly: X:\jsc.svn\examples\javascript\Test\TestAsyncRewrite\TestAsyncRewrite\bin\Debug\TestAsyncRewrite.exe
            // type: TestAsyncRewrite.Program+<<goo>b__0>d__2, TestAsyncRewrite, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
            // offset: 0x000c
            //  method:Void MoveNext() }

            goo().Wait();

            //Console.WriteLine("exit Main");

            //Console.ReadKey(true);
        }

        public async static Task goo()
        {
            Console.WriteLine("enter goo");

            Func<Task> f = async delegate
            {
                Console.WriteLine("enter f");

                Console.WriteLine("before f delay");
                await Task.Delay(1000);
            };
            Console.WriteLine("before f");
            await f();


            Console.WriteLine("before delay");
            await Task.Delay(1000);


            Console.WriteLine("exit goo");
        }
    }
}
