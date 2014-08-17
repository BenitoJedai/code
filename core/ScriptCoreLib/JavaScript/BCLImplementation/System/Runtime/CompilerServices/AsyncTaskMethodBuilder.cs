using ScriptCoreLib.JavaScript.BCLImplementation.System.Threading.Tasks;
using ScriptCoreLib.Shared.BCLImplementation.System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Runtime.CompilerServices
{
    // http://referencesource.microsoft.com/#mscorlib/system/runtime/compilerservices/AsyncMethodBuilder.cs
    // https://github.com/mono/mono/blob/a31c107f59298053e4ff17fd09b2fa617b75c1ba/mcs/class/corlib/System.Runtime.CompilerServices/AsyncTaskMethodBuilder.cs

    // see: http://msdn.microsoft.com/en-us/library/System.Runtime.CompilerServices.AsyncTaskMethodBuilder.aspx
#if NET45
    [Script(Implements = typeof(global::System.Runtime.CompilerServices.AsyncTaskMethodBuilder))]
#else
    [Script(ImplementsViaAssemblyQualifiedName = "System.Runtime.CompilerServices.AsyncTaskMethodBuilder")]
#endif
    internal class __AsyncTaskMethodBuilder : __IAsyncMethodBuilder
    {
        // Src/Compilers/CSharp/Portable/Lowering/AsyncRewriter/AsyncRewriter.cs	


        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Runtime\CompilerServices\AsyncTaskMethodBuilder.cs

        public __TaskCompletionSource<object> InternalTask = new __TaskCompletionSource<object>();

        public __Task Task { get { return InternalTask.InternalTask; } }


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
            Task.InternalSetCompleteAndYield();
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
            Console.WriteLine("__AsyncTaskMethodBuilder AwaitOnCompleted");
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


        public void PreBoxInitialization()
        {
        }
    }

    // see: http://msdn.microsoft.com/en-us/library/hh138506(v=vs.110).aspx
    [Script(ImplementsViaAssemblyQualifiedName = "System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1")]
    internal class __AsyncTaskMethodBuilder<TResult>
    {
        // http://stackoverflow.com/questions/17969603/what-is-the-minimum-set-of-types-required-to-compile-async-code


        public __TaskCompletionSource<TResult> InternalTask = new __TaskCompletionSource<TResult>();

        public __Task<TResult> Task { get { return InternalTask.Task; } }


        public void SetResult(TResult result)
        {
            Task.InternalSetCompleteAndYield(result);
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

        // script: error JSC1000: No implementation found for this native method, please implement [System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1.AwaitUnsafeOnCompleted(System.Runtime.CompilerServices.TaskAwaiter&, AsyncHistoryExperiment.Application+<>c__DisplayClass19+ctor>b__a>d__2b&)]


    }
}
