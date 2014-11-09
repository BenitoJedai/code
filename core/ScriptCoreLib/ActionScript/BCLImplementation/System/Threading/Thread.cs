using ScriptCoreLib.ActionScript.BCLImplementation.System.Reflection;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.system;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System
{
    // http://referencesource.microsoft.com/#mscorlib/system/threading/thread.cs
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System.Threading/Thread.cs

    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Threading\Thread.cs
    // X:\jsc.svn\core\ScriptCoreLib\ActionScript\BCLImplementation\System\Threading\Thread.cs
    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Threading\Thread.cs


    [Script(Implements = typeof(global::System.Threading.Thread))]
    public class __Thread
    {
        // http://stackoverflow.com/questions/11902863/can-actionscript-workers-be-used-to-play-generate-sounds-in-a-separate-thread

        public static implicit operator __Thread(Thread e)
        {
            return (__Thread)(object)e;
        }

        // X:\jsc.svn\examples\actionscript\Test\TestThreadStartInternalWorkerInvoke\TestThreadStartInternalWorkerInvoke\ApplicationSprite.cs
        // X:\jsc.svn\examples\actionscript\Test\TestThreadStart\TestThreadStart\ApplicationSprite.cs

        #region ParameterizedThreadStart
        public ParameterizedThreadStart InternalParameterizedThreadStart;


        // tested by x:\jsc.svn\examples\javascript\Test\TestThreadStartAsWebWorker\TestThreadStartAsWebWorker\Application.cs
        public __Thread(ParameterizedThreadStart t)
        {
            if (t.Target != null)
                throw new NotImplementedException("for now AIR supports only static thread starts..");

            // it seems we should support scope sharing
            // yet we implemented it in Task.Run instead.
            // do we need to move it around?
            // check with java and actionscript

            InternalParameterizedThreadStart = t;
        }

        public Action<Worker> InternalBeforeStart;

        public void Start(object e)
        {
            // what about serviceworker?

            // WebWorker?
            // did jsc rewriter detect the threadstart correctly?

            var w = WorkerDomain.current.createWorker(
             InternalPrimordialSprite.loaderInfo.bytes
         );

            __MethodInfo m = InternalParameterizedThreadStart.Method;

            w.setSharedProperty("FunctionToken_TypeFullName", m._Method.FunctionToken_TypeFullName);
            w.setSharedProperty("FunctionToken_MethodName", m._Method.FunctionToken_MethodName);

            w.setSharedProperty("arg0", e);

            if (InternalBeforeStart != null)
                InternalBeforeStart(w);

            w.start();


            //IsAlive = true;
            //InternalParameterizedThreadStart(e);
            //IsAlive = false;
        }
        #endregion

        // like the android Context, a ref we desparatley need
        public static Sprite InternalPrimordialSprite;

        [Obsolete("called by the compiler from Sprite ctor")]
        public static bool InternalWorkerInvoke(Sprite that)
        {
            // pop ?

            if (Worker.current.isPrimordial)
            {
                InternalPrimordialSprite = that;
                return false;
            }


            if (__Task.InternalWorkerInvoke(that))
            {
                // X:\jsc.svn\examples\actionscript\async\Test\TestAsyncTaskRun\TestAsyncTaskRun\ApplicationSprite.cs
                // Task.Run called itself

                return true;
            }


            // X:\jsc.svn\examples\actionscript\Test\TestThreadStart\TestThreadStart\ApplicationSprite.cs

            // are we inside worker?

            var FunctionToken_TypeFullName = (string)Worker.current.getSharedProperty("FunctionToken_TypeFullName");
            var FunctionToken_MethodName = (string)Worker.current.getSharedProperty("FunctionToken_MethodName");
            var arg0 = (string)Worker.current.getSharedProperty("arg0");

            IntPtr pp = __IntPtr.OfFunctionToken(null,
                 FunctionToken_TypeFullName,
                 FunctionToken_MethodName
             );


            MethodInfo mm = new __MethodInfo { _Method = pp };

            //    t.text = "after invoke " + new { TheOtherClass.SharedField, sw.ElapsedMilliseconds };

            //xfromWorker.send("message from worker " + new { FunctionToken_TypeFullName, FunctionToken_MethodName });

            //throw null;



            mm.Invoke(null, new object[] { arg0 });


            return true;
        }
    }
}
