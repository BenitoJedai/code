using System;
using System.Collections.Generic;
using System.Text;
using java.lang;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System.Threading
{
    [Script(Implements = typeof(global::System.Threading.Thread))]
    internal class __Thread
    {
        public static void Sleep(int millisecondsTimeout)
        {
            try
            {
                Thread.sleep(millisecondsTimeout);
            }
            catch
            {
                throw new csharp.RuntimeException();
            }
        }
    }
}
