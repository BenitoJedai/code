using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;

namespace ScriptCoreLib.JavaScript.System
{
    [Script(Implements = typeof(global::System.Diagnostics.Debugger))]
    internal class Debugger
    {

        // Summary:
        //     Signals a breakpoint to an attached debugger.
        public static void Break()
        {
            Native.DebugBreak();
        }
    }

    [Script(Implements = typeof(global::System.Collections.ArrayList))]
    internal class ArrayList
    {
        readonly IArray<object> items = new IArray<object>();

        public void Add(object e)
        {
            items.push(e);
        }
    }

    [Script(Implements = typeof(global::System.WeakReference))]
    internal class WeakReference
    {
        public WeakReference(object e)
        {
            // weak reference not supported
        }
    }

    [Script(Implements = typeof(global::System.Threading.Monitor))]
    internal class Monitor
    {
        public static void Enter(object e)
        {
        }

        public static  void Exit(object obj)
        {
        }
    }
}