using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;

namespace ComplexQueryExperiment
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            //02000041 ScriptCoreLib.Query.Experimental.QueryExpressionBuilder+SQLWriter`1+<>c__DisplayClass61
            //script: error JSC1000: unsupported flow detected, try to simplify.
            // Assembly V:\ComplexQueryExperiment.Application.exe
            // DeclaringType ScriptCoreLib.Query.Experimental.QueryExpressionBuilder+SQLWriter`1+<>c__DisplayClass61, ComplexQueryExperiment.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
            // OwnerMethod <.ctor>b__3b
            // Offset 038c
            // . Try ommiting the return, break or continue instruction.
            //script: error JSC1000: Method: <.ctor>b__3b, Type: ScriptCoreLib.Query.Experimental.QueryExpressionBuilder+SQLWriter`1+<>c__DisplayClass61; emmiting failed : System.InvalidOperationException: unsupported flow detected, try to simplify.

            //>	mscorlib.dll!System.Runtime.CompilerServices.AsyncMethodBuilderCore.ThrowAsync.AnonymousMethod__1(object state) + 0x33 bytes	
            //    mscorlib.dll!System.Threading.QueueUserWorkItemCallback.WaitCallback_Context(object state) + 0x3e bytes	
            //    mscorlib.dll!System.Threading.ExecutionContext.RunInternal(System.Threading.ExecutionContext executionContext, System.Threading.ContextCallback callback, object state, bool preserveSyncCtx) + 0xa7 bytes	
            //    mscorlib.dll!System.Threading.ExecutionContext.Run(System.Threading.ExecutionContext executionContext, System.Threading.ContextCallback callback, object state, bool preserveSyncCtx) + 0x16 bytes	
            //    mscorlib.dll!System.Threading.QueueUserWorkItemCallback.System.Threading.IThreadPoolWorkItem.ExecuteWorkItem() + 0x60 bytes	
            //    mscorlib.dll!System.Threading.ThreadPoolWorkQueue.Dispatch() + 0x149 bytes	
            //    mscorlib.dll!System.Threading._ThreadPoolWaitCallback.PerformWaitCallback() + 0x5 bytes	


            new ApplicationWebService().WebMethod2();

            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}
