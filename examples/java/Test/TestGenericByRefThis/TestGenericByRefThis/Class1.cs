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
        //     script: error JSC1000: ActionScript : unable to emit initobj at 'TestGenericByRefThis.Class1.Main'#0003: Object reference not set to an instance of an object.
        //at jsc.Script.CompilerCLike.WriteParameterInfoFromStack(MethodBase m, Prestatement p, ILFlowStackItem[] s, Int32 offset) in x:\jsc.internal.git\compiler\jsc\Languages\CompilerCLike.cs:line 214

        // X:\jsc.svn\examples\actionscript\async\Test\TestTaskDelay\TestTaskDelay\ApplicationSprite.cs

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

            // Error	2	Structs cannot contain explicit parameterless constructors	X:\jsc.svn\examples\java\Test\TestGenericByRefThis\TestGenericByRefThis\Class1.cs	39	20	TestGenericByRefThis
            //public Foo()
            //{

            //}

            //public final void Method1()
            //{
            //    this.Method2(this);
            //}

            public void Method1()
            {
                var that = this;

                //        public Class1_Foo_1<G>[] __byref_this1()
                //    {
                //        Class1_Foo_1<G>[] __value = ?
                //        __value[0] = this;
                //        return __value;
                //    }

                //Class1_Foo_1<G>[] ref_foo_10 = ?;
                //    ref_foo_10[0] = new Class1_Foo_1<G>();

                //    ref_foo_10[0] = this;
                //    this.Method2(__byref_this1());


                // Error	1	Cannot pass 'this' as a ref or out argument because it is read-only	X:\jsc.svn\examples\javascript\Test\TestGenericByRefThis\TestGenericByRefThis\ApplicationControl.cs	23	29	TestGenericByRefThis
                Method2(ref this);

                //internal compiler error at method
                // assembly: X:\jsc.svn\examples\java\Test\TestGenericByRefThis\TestGenericByRefThis\bin\Debug\TestGenericByRefThis.dll at
                // type: TestGenericByRefThis.Class1+Foo`1, TestGenericByRefThis, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
                // method: Method1
                // Object reference not set to an instance of an object.
                //    at jsc.Languages.Java.JavaCompiler.GetDecoratedTypeName(Type SourceType, Boolean bExternalAllowed, Boolean bUsePrimitives, Boolean bChopNestedParents, Boolean bDisableArrayToObjectRewrite, Type ContextType, Boolean AllowTypeFullName) in x:\jsc.internal.git\compiler\jsc\Languages\Java\JavaCompiler.cs:line 916

                Method2(ref that);
            }


            void Method2(ref Foo<G> f)
            {
                //Text = "Method2";

                //   ref_arg1[0].Text = new Object();
                //     ref_arg1.Text = {};
                f.Text = new object();


                var x = f.Text;
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
