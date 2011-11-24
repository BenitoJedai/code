using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

[assembly: Script, ScriptTypeFilter(ScriptType.Java)]

namespace ScriptCoreLibJava.BCLImplementation.System.Threading
{
    [Script(Implements = typeof(global::System.Threading.Monitor))]
    internal class __Monitor
    {
        public static void Exit(object e)
        {
        }

        public static void Enter(object e, ref bool x)
        {
        }
    }
}

namespace ScriptCoreLibJava.BCLImplementation.System.Threading
{
    [Script(Implements = typeof(global::System.Threading.EventWaitHandle))]
    internal class __EventWaitHandle // : __WaitHandle
    {
        readonly global::java.lang.Object Context = new global::java.lang.Object();

        public bool Set()
        {
            lock (this.Context)
                this.Context.notify();

            return false;
        }
    }
}

namespace java.lang
{
    // http://java.sun.com/j2se/1.4.2/docs/api/java/lang/Object.html
    [Script(IsNative = true)]
    public class Object
    {
        public void notify()
        {
        }
    }
}