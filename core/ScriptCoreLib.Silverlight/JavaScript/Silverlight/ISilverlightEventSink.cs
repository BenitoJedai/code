using ScriptCoreLib;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.Silverlight.Input;

using ScriptCoreLib.Shared;


namespace ScriptCoreLib.JavaScript.Silverlight
{
    [Script(HasNoPrototype=true)]
    public interface ISilverlightEventSink
    {
        void AddEventListener(string eventType, string functionName);
        void RemoveEventListener(string eventType, string functionName);
    }


    [Script]
    public static class SilverlightEventSinkExtensions
    {
        // c# 3 enables the extension methods
        // there is not support for extension properties or events
        // you can still use the [Script(DefineAsStatic = true)] instead

        static public void AddEventListenerAsProxy(this ISilverlightEventSink e, string eventType, global::System.Delegate value)
        {
            var p = EventHandlerProxy.Add(value);

            if (p != null)
                e.AddEventListener(eventType, "javascript:" + p.FunctionName);
        }

        static public void RemoveEventListenerAsProxy(this ISilverlightEventSink e, string eventType, global::System.Delegate value)
        {
            var p = EventHandlerProxy.Remove(value);

            if (p != null)
                e.RemoveEventListener(eventType, "javascript:" + p.FunctionName);
        }
    }


}