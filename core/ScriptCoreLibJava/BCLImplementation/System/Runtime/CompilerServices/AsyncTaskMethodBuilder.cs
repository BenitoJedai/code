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
            Debugger.Break();
        }



        public void SetResult()
        {
            this.InternalTask.SetResult(null);
        }
    }
}
