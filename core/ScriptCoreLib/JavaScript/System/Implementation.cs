using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;

namespace ScriptCoreLib.JavaScript.System
{
    [Script(Implements = typeof(global::System.Collections.ArrayList))]
    public class ArrayList
    {
        readonly IArray<object> items = new IArray<object>();

        public void Add(object e)
        {
            items.push(e);
        }
    }

    [Script(Implements = typeof(global::System.WeakReference))]
    public class WeakReference
    {
        public WeakReference(object e)
        {
            // weak reference not supported
        }
    }
}