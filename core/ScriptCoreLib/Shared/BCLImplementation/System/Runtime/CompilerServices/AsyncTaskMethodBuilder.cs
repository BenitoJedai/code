using ScriptCoreLib.ActionScript.BCLImplementation.System.Threading.Tasks;
using ScriptCoreLib.Shared.BCLImplementation.System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Runtime.CompilerServices
{
    // move to Shared?
    // how to deal with InternalSetCompleteAndYield

    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Runtime\CompilerServices\AsyncTaskMethodBuilder.cs
    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Runtime\CompilerServices\AsyncTaskMethodBuilder.cs


    // http://referencesource.microsoft.com/#mscorlib/system/runtime/compilerservices/AsyncMethodBuilder.cs
    // https://github.com/mono/mono/tree/master/mcs/class/corlib/System.Runtime.CompilerServices/AsyncTaskMethodBuilder.cs

    // see: http://msdn.microsoft.com/en-us/library/System.Runtime.CompilerServices.AsyncTaskMethodBuilder.aspx
#if NET45
    [Script(Implements = typeof(global::System.Runtime.CompilerServices.AsyncTaskMethodBuilder))]
#else
    [Script(ImplementsViaAssemblyQualifiedName = "System.Runtime.CompilerServices.AsyncTaskMethodBuilder")]
#endif
    internal class __AsyncTaskMethodBuilder : __IAsyncMethodBuilder
    {
        // Src/Compilers/CSharp/Portable/Lowering/AsyncRewriter/AsyncRewriter.cs	

        // X:\jsc.svn\examples\actionscript\async\Test\TestAsyncTaskRun\TestAsyncTaskRun\ApplicationSprite.cs

        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Runtime\CompilerServices\AsyncTaskMethodBuilder.cs

        public TaskCompletionSource<object> InternalTask = new TaskCompletionSource<object>();

        public Task Task { get { return InternalTask.Task; } }


        public static __AsyncTaskMethodBuilder Create()
        {
            //Console.WriteLine("__AsyncTaskMethodBuilder Create");

            return new __AsyncTaskMethodBuilder();
        }

        public void Start<TStateMachine>(ref  TStateMachine stateMachine)
        //where TStateMachine : __IAsyncStateMachine
        {
            // X:\jsc.svn\examples\javascript\future\TestTaskResume\TestTaskResume\Program.cs

            //Console.WriteLine("__AsyncTaskMethodBuilder.Start, call MoveNext");

            var xstateMachine = (__IAsyncStateMachine)stateMachine;
            xstateMachine.MoveNext();
        }


        public void SetStateMachine(__IAsyncStateMachine stateMachine)
        {

        }

        public void SetResult()
        {
            InternalTask.SetResult(null);
            //Task.InternalSetCompleteAndYield();
        }

        public void SetException(Exception exception)
        {
            Debugger.Break();
        }

        public void AwaitOnCompleted<TAwaiter, TStateMachine>(
             ref  TAwaiter awaiter,
             ref  TStateMachine stateMachine
)
        {
            //Console.WriteLine("__AsyncTaskMethodBuilder AwaitOnCompleted");

            // whats the difference?
            AwaitUnsafeOnCompleted(
                ref awaiter,
                ref stateMachine
            );
        }

        public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(
                   ref  TAwaiter awaiter,
                   ref  TStateMachine stateMachine
              )
        //where TAwaiter : global::System.Runtime.CompilerServices.INotifyCompletion
        //where TStateMachine : __IAsyncStateMachine
        {
			// X:\jsc.svn\examples\java\hybrid\JVMCLRSwitchToCLRContextAsync\JVMCLRSwitchToCLRContextAsync\Program.cs

			//Console.WriteLine("__AsyncTaskMethodBuilder.AwaitUnsafeOnCompleted");
			// AsyncMethodBuilderCore+MoveNextRunner

			var xstateMachine = (__IAsyncStateMachine)stateMachine;
            var zstateMachine = xstateMachine;

            Action yield = () => zstateMachine.MoveNext();

            var xawaiter = (__INotifyCompletion)(object)awaiter;

            xawaiter.OnCompleted(
                delegate
                {
                    //Console.WriteLine("__AsyncTaskMethodBuilder.AwaitUnsafeOnCompleted  xawaiter.OnCompleted");

                    yield();
                }
            );
        }


        public void PreBoxInitialization()
        {
        }
    }

    // see: http://msdn.microsoft.com/en-us/library/hh138506(v=vs.110).aspx
    [Script(ImplementsViaAssemblyQualifiedName = "System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1")]
    internal class __AsyncTaskMethodBuilder<TResult>
    {
        // http://stackoverflow.com/questions/17969603/what-is-the-minimum-set-of-types-required-to-compile-async-code


        public  TaskCompletionSource<TResult> InternalTask = new TaskCompletionSource<TResult>();

        public  Task<TResult> Task { get { return InternalTask.Task; } }


        public void SetResult(TResult result)
        {
            //Task.InternalSetCompleteAndYield(result);
            InternalTask.SetResult(result);
        }

        public void Start<TStateMachine>(ref  TStateMachine stateMachine)
        //where TStateMachine : __IAsyncStateMachine
        {
            var xstateMachine = (__IAsyncStateMachine)stateMachine;
            xstateMachine.MoveNext();
        }


        public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(
             ref  TAwaiter awaiter,
             ref  TStateMachine stateMachine
        )
        //where TAwaiter : global::System.Runtime.CompilerServices.INotifyCompletion
        //where TStateMachine : __IAsyncStateMachine
        {
            //Console.WriteLine("__AsyncTaskMethodBuilder.AwaitUnsafeOnCompleted");

            var xstateMachine = (__IAsyncStateMachine)stateMachine;
            var zstateMachine = xstateMachine;

            Action yield = () => zstateMachine.MoveNext();

            var xawaiter = (__INotifyCompletion)(object)awaiter;

            xawaiter.OnCompleted(
                delegate
                {
                    //Console.WriteLine("__AsyncTaskMethodBuilder.AwaitUnsafeOnCompleted  xawaiter.OnCompleted");

                    yield();
                }
            );
        }

        public static __AsyncTaskMethodBuilder<TResult> Create()
        {
            return new __AsyncTaskMethodBuilder<TResult> { };
        }


        public void SetException(Exception exception)
        {
            Console.WriteLine("__AsyncTaskMethodBuilder.SetException " + new { exception });

            Debugger.Break();
        }

        public void SetStateMachine(
       __IAsyncStateMachine stateMachine
   )
        {
            Console.WriteLine("__AsyncTaskMethodBuilder.SetStateMachine");

        }



    }
}
