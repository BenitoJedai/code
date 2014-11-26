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
    // X:\jsc.svn\core\ScriptCoreLibNative\ScriptCoreLibNative\BCLImplementation\System\Threading\Thread.cs


    [Script(Implements = typeof(global::System.Threading.Thread))]
    public class __Thread
    {
        // "X:\jsc.svn\market\synergy\oculus\Oculus_Mobile_v0.4.0_SDK_Documentation\Technical Notes\Runtime_Threads.pdf"

        public static Thread CurrentThread
        {
            get
            {

                return (Thread)(object)new __Thread { };
            }
        }


        // X:\jsc.svn\examples\actionscript\Test\TestWorkerConsole\TestWorkerConsole\ApplicationSprite.cs
        public int ManagedThreadId { get; set; }


        // X:\jsc.svn\examples\actionscript\air\AIRThreadedSound\AIRThreadedSound\ApplicationSprite.cs
        // X:\jsc.svn\examples\actionscript\air\AIRAudioWorker\AIRAudioWorker\ApplicationSprite.cs
        // X:\jsc.svn\examples\c\Test\TestThreadStart\TestThreadStart\BCLImplementation.cs

        // http://bitsofinfo.wordpress.com/2008/09/22/threads-in-as3-flex-actionscript/
        // http://stackoverflow.com/questions/11902863/can-actionscript-workers-be-used-to-play-generate-sounds-in-a-separate-thread

        public static implicit operator __Thread(Thread e)
        {
            return (__Thread)(object)e;
        }

        // X:\jsc.svn\examples\actionscript\Test\TestThreadStartInternalWorkerInvoke\TestThreadStartInternalWorkerInvoke\ApplicationSprite.cs
        // X:\jsc.svn\examples\actionscript\Test\TestThreadStart\TestThreadStart\ApplicationSprite.cs

        public __Thread()
            : this(null)
        {

        }

        #region ParameterizedThreadStart
        public ParameterizedThreadStart InternalParameterizedThreadStart;


        // tested by x:\jsc.svn\examples\javascript\Test\TestThreadStartAsWebWorker\TestThreadStartAsWebWorker\Application.cs
        public __Thread(ParameterizedThreadStart t)
        {
            // what about scope objects?

            if (t != null)
            {
                // unless jsc adds scope sharing intel!

                if (t.Target != null)
                    throw new NotImplementedException("for now AIR supports only static thread starts..");

                // it seems we should support scope sharing
                // yet we implemented it in Task.Run instead.
                // do we need to move it around?
                // check with java and actionscript

                InternalParameterizedThreadStart = t;
            }
        }

        public Action<Worker> InternalBeforeStart;

        public void Start(object parameter)
        {
            // what about serviceworker?

            // WebWorker?
            // did jsc rewriter detect the threadstart correctly?

            var w = WorkerDomain.current.createWorker(
             InternalPrimordialSprite.loaderInfo.bytes
         );

            __MethodInfo m = InternalParameterizedThreadStart.Method;



            #region fromWorkerConsole
            var fromWorkerConsole = w.createMessageChannel(Worker.current);
            w.setSharedProperty("fromWorkerConsole", fromWorkerConsole);
            fromWorkerConsole.channelMessage += e =>
            {
                var data = (string)fromWorkerConsole.receive();

                // X:\jsc.svn\examples\actionscript\Test\TestWorkerConsole\TestWorkerConsole\ApplicationSprite.cs
                Console.WriteLine(data);
            };
            #endregion



            w.setSharedProperty("FunctionToken_TypeFullName", m._Method.FunctionToken_TypeFullName);
            w.setSharedProperty("FunctionToken_MethodName", m._Method.FunctionToken_MethodName);

            w.setSharedProperty("arg0", parameter);

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

        // X:\jsc.svn\examples\actionscript\Test\TestWorkerThrow\TestWorkerThrow\ApplicationSprite.cs
        // for workers
        public static Sprite InternalWorkerSprite;

        [Obsolete("called by the compiler from Sprite ctor")]
        public static bool InternalWorkerInvoke(Sprite that)
        {
            // pop ?

            if (Worker.current.isPrimordial)
            {
                InternalPrimordialSprite = that;
                return false;
            }

            // X:\jsc.svn\examples\actionscript\Test\TestWorkerThrow\TestWorkerThrow\ApplicationSprite.cs
            InternalWorkerSprite = that;

            if (__Task.InternalWorkerInvoke(that))
            {
                // X:\jsc.svn\examples\actionscript\async\Test\TestAsyncTaskRun\TestAsyncTaskRun\ApplicationSprite.cs
                // Task.Run called itself

                return true;
            }


            #region xfromWorkerConsole
            // X:\jsc.svn\examples\actionscript\Test\TestWorkerConsole\TestWorkerConsole\ApplicationSprite.cs
            var xfromWorkerConsole = (MessageChannel)Worker.current.getSharedProperty("fromWorkerConsole");

            var w = new __Console.__OutWriter
            {
                AtWriteLine = z =>
                {
                    xfromWorkerConsole.send(z);
                }
            };

            Console.SetOut(w);
            #endregion



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


            try
            {

                mm.Invoke(null, new object[] { arg0 });

            }
            catch (Exception err)
            {
                // lets report anything back to the app
                // for now the best we can do is to print it to console in worker
                // and hope the app is showing the console in textfield

                //__Thread.InternalWorkerSprite.loaderInfo.uncaughtErrorEvents.uncaughtError +=
                // does not seem to work yet?

                Console.WriteLine("catch " + new { err });
                // X:\jsc.svn\examples\actionscript\Test\TestWorkerThrow\TestWorkerThrow\ApplicationSprite.cs
            }


            return true;
        }
    }
}
