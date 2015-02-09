using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

[assembly: Obfuscation(Feature = "script")]


namespace TestRoslynStaticDelegate
{
    [Script(IsNative = true)]
    public delegate void android_app_onAppCmd(object app, object cmd);

    public class Class1
    {
        // jsc wont yet link run static cctors
        // should there be a trap for init on first use?
        static int foo;

        static Class1()
        {

            foo = 2;

        }

        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201502/20150209
        // __that->onAppCmd = TestRoslynStaticDelegate_Class1___c__DisplayClass0___ctor_b__1;
        //     __that->onAppCmd = __static_this_TestRoslynStaticDelegate_Class1___c__DisplayClass0___ctor_b__1;


        android_app_onAppCmd onAppCmd =
            // void __static_this_TestRoslynStaticDelegate_Class1___c__DisplayClass0___ctor_b__1(void*, void*);
            (app, cmd) =>
            {

                // Format(System.String, System.Object[]) used at

                //ScriptCoreLibNative.SystemHeaders.stdio_h.printf("{\{nameof(app)}: %p, \{nameof(cmd)}: %p}", __arglist(app, cmd));
                ScriptCoreLibNative.SystemHeaders.stdio_h.printf("{app: %p, cmd: %p}", __arglist(app, cmd));
                // {app: 0045CFC8, cmd: 00000000}
            };

        public static void Main(string[] args)
        {
            ScriptCoreLibNative.SystemHeaders.stdio_h.printf("%u", __arglist(foo));

            var c = new Class1();

            c.onAppCmd(c, null);


        }
    }
}
