using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: Obfuscation(Feature = "script")]

namespace TestStructByRefInit
{
    [Script(Implements = typeof(global::System.ValueType))]
    internal abstract class __ValueType
    {

    }

    public struct Class1
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201402/20140217
        public bool IsCompleted { get; set; }

        // byref not supported yet for fields.
        //static Class1 x;


        // Error	1	Structs cannot contain explicit parameterless constructors	X:\jsc.svn\examples\java\async\Test\TestStructByRefInit\TestStructByRefInit\Class1.cs	26	16	TestStructByRefInit
        //public Class1()
        //{
        //    // ?
        //}

        static void foo()
        {
            //var ref$b = [new a6i_be5HP_aDmwkqFk7mQ_akA()];
            //AwAABpHP_aDmwkqFk7mQ_akA(ref$b);

            //Class1[] ref_class10 = new Class1[1];
            //ref_class10[0] = new Class1();

            //            var ref_class10:Array = [];
            //            ref_class10[0] = new Class1()
            //;

            //            Class1.foo_e5c7fab1_06000003(ref_class10);

            // method: foo
            //1>   ActionScript : unable to emit initobj at 'TestStructByRefInit.Class1.foo'#0003: Object reference not set to an instance of an object.
            //1> 

            //1>  0006 02000002 TestStructByRefInit::TestStructByRefInit.__ValueType
            //1>script : error JSC1000: ActionScript : unable to emit initobj at 'TestStructByRefInit.Class1.foo'#0003: Object reference not set to an instance of an object.
            //1>     at jsc.Script.CompilerCLike.WriteParameterInfoFromStack(MethodBase m, Prestatement p, ILFlowStackItem[] s, Int32 offset) in x:\jsc.internal.svn\compiler\jsc\Languages\CompilerCLike.cs:line 214


            //var x = new Class1();

            //IL_0001:  ldloca.s   x
            //IL_0003:  initobj    TestStructByRefInit.Class1
            var x = default(Class1);

            //Class1[] ref_class10 = new Class1[1];
            //ref_class10[0] = new Class1();

            //class10 = new Class1();


            // Error	1	Use of unassigned local variable 'x'	X:\jsc.svn\examples\java\async\Test\TestStructByRefInit\TestStructByRefInit\Class1.cs	39	21	TestStructByRefInit
            foo(ref x);
        }

        static int foo(ref Class1 x)
        {
            //IL_0001:  ldarg.0
            //IL_0002:  call       instance bool TestStructByRefInit.Class1::get_IsCompleted()

            //if (ref_arg1.get_IsCompleted())
            if (x.IsCompleted)
                return 1;

            return 3;
        }
    }
}
