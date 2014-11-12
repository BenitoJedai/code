using ScriptCoreLib.ActionScript.BCLImplementation.System.Reflection;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.system;
using ScriptCoreLib.ActionScript.flash.utils;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System
{

    internal partial class __Task
    {
        // X:\jsc.svn\examples\actionscript\async\Test\TestAsyncTaskRun\TestAsyncTaskRun\ApplicationSprite.cs

        public static Task<TResult> Run<TResult>(Func<Task<TResult>> function)
        {
            // what about nested workers?

            // when will we get scope sharing, two way sharing?
            // will we be able to also Transfer TaskCompletionSource and Progress types?
            if (function.Target != null)
                throw new NotImplementedException("for now AIR supports only static thread starts..");


            var s = new TaskCompletionSource<TResult>();

            // X:\jsc.svn\core\ScriptCoreLib\ActionScript\BCLImplementation\System\Threading\Thread.cs

            // each worker is a vm. we could pool them and reuse them later?
            var w = WorkerDomain.current.createWorker(
             __Thread.InternalPrimordialSprite.loaderInfo.bytes
            );

            __MethodInfo m = function.Method;

            // or should we use typeof().Name ? nameof()?
            w.setSharedProperty("InternalWorkerInvoke", "Run(Func<Task<TResult>>)");

            w.setSharedProperty("FunctionToken_TypeFullName", m._Method.FunctionToken_TypeFullName);
            w.setSharedProperty("FunctionToken_MethodName", m._Method.FunctionToken_MethodName);

            //w.setSharedProperty("arg0", e);

            //if (InternalBeforeStart != null)
            //    InternalBeforeStart(w);

            // X:\jsc.svn\examples\actionscript\Test\TestThreadStartInternalWorkerInvoke\TestThreadStartInternalWorkerInvoke\ApplicationSprite.cs

            var fromWorker = w.createMessageChannel(Worker.current);

            w.setSharedProperty("fromWorker", fromWorker);

            fromWorker.channelMessage += e =>
            {
                var data = (TResult)fromWorker.receive();

                s.SetResult(data);
            };


            w.start();


            return s.Task;
        }


        [Obsolete("called by the compiler from Sprite ctor")]
        public static bool InternalWorkerInvoke(Sprite that)
        {
            var magic = (string)Worker.current.getSharedProperty("InternalWorkerInvoke");

            if (magic != "Run(Func<Task<TResult>>)")
                return false;


            var FunctionToken_TypeFullName = (string)Worker.current.getSharedProperty("FunctionToken_TypeFullName");
            var FunctionToken_MethodName = (string)Worker.current.getSharedProperty("FunctionToken_MethodName");

            IntPtr pp = __IntPtr.OfFunctionToken(null,
                 FunctionToken_TypeFullName,
                 FunctionToken_MethodName
             );


            MethodInfo mm = new __MethodInfo { _Method = pp };

            //    t.text = "after invoke " + new { TheOtherClass.SharedField, sw.ElapsedMilliseconds };

            //xfromWorker.send("message from worker " + new { FunctionToken_TypeFullName, FunctionToken_MethodName });

            //throw null;



            // we are on a worker thread.
            // are we able to await and then respond and terminate?
            var value = (Task<object>)mm.Invoke(null, new object[] { });

            value.ContinueWith(
                task =>
                {
                    // is the return type transerable?
                    // or would we have to serialize it?

                    var xfromWorker = (MessageChannel)Worker.current.getSharedProperty("fromWorker");
                    // or are we to capture all fields modified within worker and only update those?
                    xfromWorker.send(task.Result);

                }
            );



            return true;
        }
    }
}
