using ScriptCoreLib;
using ScriptCoreLib.Shared.BCLImplementation.System.Runtime.CompilerServices;
using ScriptCoreLibJava.BCLImplementation.System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCoreLibJava.BCLImplementation.System.Runtime.CompilerServices
{
    // move to Shared?

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
        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Runtime\CompilerServices\AsyncTaskMethodBuilder.cs

        public __TaskCompletionSource<object> InternalTask = new __TaskCompletionSource<object>();

        public Task Task { get { return this.InternalTask.Task; } }


        public static __AsyncTaskMethodBuilder Create()
        {
            //Console.WriteLine("__AsyncTaskMethodBuilder Create");

            return new __AsyncTaskMethodBuilder();
        }

        public void Start<TStateMachine>(ref  TStateMachine stateMachine)
        //where TStateMachine : __IAsyncStateMachine
        {
            //Console.WriteLine("__AsyncTaskMethodBuilder.Start, call MoveNext");

            var xstateMachine = (__IAsyncStateMachine)stateMachine;
            xstateMachine.MoveNext();
        }

        public void PreBoxInitialization()
        {
            //throw new NotImplementedException();
        }



        public void SetStateMachine(__IAsyncStateMachine stateMachine)
        {

        }


        public void SetException(Exception exception)
        {
            Console.WriteLine("__AsyncTaskMethodBuilder " + new { exception.Message, exception.StackTrace });
            Debugger.Break();
        }



        public void SetResult()
        {
            this.InternalTask.SetResult(null);
        }



        //Implementation not found for type import :
        //type: System.Runtime.CompilerServices.AsyncTaskMethodBuilder
        //method: Void AwaitUnsafeOnCompleted[TAwaiter,TStateMachine](TAwaiter ByRef, TStateMachine ByRef)
        //Did you forget to add the [Script] attribute?
        //Please double check the signature!


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
    }



    // see: http://msdn.microsoft.com/en-us/library/hh138506(v=vs.110).aspx
    [Script(ImplementsViaAssemblyQualifiedName = "System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1")]
    internal class __AsyncTaskMethodBuilder<TResult>
    {
        public static __AsyncTaskMethodBuilder<TResult> Create()
        {
            return new __AsyncTaskMethodBuilder<TResult> { };
        }

        public void Start<TStateMachine>(ref  TStateMachine stateMachine)
        //where TStateMachine : __IAsyncStateMachine
        {
            var xstateMachine = (__IAsyncStateMachine)stateMachine;
            xstateMachine.MoveNext();
        }

        public __TaskCompletionSource<TResult> InternalTask = new __TaskCompletionSource<TResult>();

        public Task<TResult> Task { get { return this.InternalTask.Task; } }


        public void SetStateMachine(__IAsyncStateMachine stateMachine)
        {

        }


        public void SetException(Exception exception)
        {
            Console.WriteLine("__AsyncTaskMethodBuilder " + new { exception.Message, exception.StackTrace });

            Debugger.Break();
        }

        public void SetResult(TResult result)
        {
            this.InternalTask.SetResult(result);
        }

        //Implementation not found for type import :
        //type: System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1[[System.Data.DataTable, System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]
        //method: Void AwaitUnsafeOnCompleted[TAwaiter,TStateMachine](TAwaiter ByRef, TStateMachine ByRef)
        //Did you forget to add the [Script] attribute?
        //Please double check the signature!


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
    }
}
