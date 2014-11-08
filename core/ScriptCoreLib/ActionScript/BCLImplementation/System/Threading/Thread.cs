using System;
using System.Collections.Generic;
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
    internal class __Thread
    {
        // X:\jsc.svn\examples\actionscript\Test\TestThreadStart\TestThreadStart\ApplicationSprite.cs

        #region ParameterizedThreadStart
        public ParameterizedThreadStart InternalParameterizedThreadStart;


        // tested by x:\jsc.svn\examples\javascript\Test\TestThreadStartAsWebWorker\TestThreadStartAsWebWorker\Application.cs
        public __Thread(ParameterizedThreadStart t)
        {
            // it seems we should support scope sharing
            // yet we implemented it in Task.Run instead.
            // do we need to move it around?
            // check with java and actionscript

            InternalParameterizedThreadStart = t;
        }

        public void Start(object e)
        {
            // what about serviceworker?

            // WebWorker?
            // did jsc rewriter detect the threadstart correctly?

            //IsAlive = true;
            InternalParameterizedThreadStart(e);
            //IsAlive = false;
        }
        #endregion
    }
}
