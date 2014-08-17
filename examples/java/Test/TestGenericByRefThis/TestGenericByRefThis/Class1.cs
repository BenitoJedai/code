using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ScriptCoreLib;

[assembly: Obfuscation(Feature = "script")]

namespace TestGenericByRefThis
{
    [Script(Implements = typeof(ValueType))]
    class __ValueType
    { }

    public class Class1
    {
        //     Unhandled Exception: System.Reflection.TargetInvocationException: Exception has been thrown by the target of an invocation. ---> System.Reflection.TargetInvocationException: Exception has been thrown by the target of an invocation. ---> System.InvalidOperationException: Java : class import : no implementation for System.String at TestGenericByRefThis.Class1
        //        at jsc.Script.CompilerBase.BreakToDebugger(String e) in x:\jsc.internal.git\compiler\jsc\Languages\CompilerBase.cs:line 266
        //at jsc.Script.CompilerBase.Break(String e) in x:\jsc.internal.git\compiler\jsc\Languages\CompilerBase.cs:line 226
        //at jsc.Languages.Java.JavaCompiler.GetImportTypes(Type t, Boolean bExcludeJavaLang) in x:\jsc.internal.git\compiler\jsc\Languages\Java\JavaCompiler.WriteImportTypes.cs:line 503
        //at jsc.Languages.Java.JavaCompiler.WriteImportTypes(Type ContextType) in x:\jsc.internal.git\compiler\jsc\Languages\Java\JavaCompiler.WriteImportTypes.cs:line 23

        // class import: no implementation for System.ValueType at TestGenericByRefThis.Class1+Foo`1

        struct Foo<G> // : ValueType
        {
            // X:\jsc.svn\examples\java\Test\JVMCLRByRefThis\JVMCLRByRefThis\Program.cs
            // X:\jsc.svn\examples\java\hybrid\Test\TestJVMCLRGenericByRefThis\TestJVMCLRGenericByRefThis\Program.cs

            public object Text;


            //public final void Method1()
            //{
            //    this.Method2(this);
            //}

            public void Method1()
            {
                var that = this;

                // Error	1	Cannot pass 'this' as a ref or out argument because it is read-only	X:\jsc.svn\examples\javascript\Test\TestGenericByRefThis\TestGenericByRefThis\ApplicationControl.cs	23	29	TestGenericByRefThis
                Method2(ref this);
                Method2(ref that);
            }


            void Method2(ref Foo<G> f)
            {
                //Text = "Method2";

                f.Text = new object();
            }
        }

        static void Main()
        {
            var s = default(Foo<object>);

            s.Method1();

            var z = s.Text;
        }
    }
}
