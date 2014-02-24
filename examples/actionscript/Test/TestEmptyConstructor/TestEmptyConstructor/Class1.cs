using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: Obfuscation(Feature = "script")]
namespace TestEmptyConstructor
{
    public class Class1
    {
        public Class1()
            : this(null)
        {

        }

        public Class1(Class1 c)
        {
            //Unhandled Exception: System.Reflection.TargetInvocationException: Exception has been thrown by the target of an invocation. ---> System.Reflection.TargetInvocationException: Exception has been thrown by the target of an invocation. ---> System.AggregateException: One or more errors occurred. ---> System.NotSupportedException: Unable to transform overloaded constructors to a single constructor via optional parameters for TestEmptyConstructor.Class1
            //   at jsc.Languages.ActionScript.ActionScriptCompiler.ConstructorInlineInfo..ctor(Type z, Boolean DisableThrow) in x:\jsc.internal.svn\compiler\jsc\Languages\ActionScript\ActionScriptCompiler.WriteTypeInstanceConstructors.cs:line 123
            //   at jsc.Languages.ActionScript.ActionScriptCompiler.WriteTypeInstanceConstructorsAndGetPrimary(Type z) in x:\jsc.internal.svn\compiler\jsc\Languages\ActionScript\ActionScriptCompiler.WriteTypeInstanceConstructors.cs:line 167
            //   at jsc.Languages.ActionScript.ActionScriptCompiler.CompileType(Type z) in x:\jsc.internal.svn\compiler\jsc\Languages\ActionScript\ActionScriptCompiler.CompileType.cs:line 114
            //   at jsc.Languages.CompilerJob.<>c__DisplayClass7.<CompileActionScript>b__3(Type xx) in x:\jsc.internal.svn\compiler\jsc\Languages\ActionScript\CompilerJob.cs:line 117
            //   at System.Linq.Parallel.ForAllOperator`1.ForAllEnumerator`1.MoveNext(TInput& currentElement, Int32& currentKey)
            //   at System.Linq.Parallel.ForAllSpoolingTask`2.SpoolingWork()
            //   at System.Linq.Parallel.SpoolingTaskBase.Work()

        }
    }
}
