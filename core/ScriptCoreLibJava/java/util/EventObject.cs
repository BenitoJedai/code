using ScriptCoreLib;

namespace java.util
{
    // http://java.sun.com/j2se/1.4.2/docs/api/java/util/EventObject.html
    [Script(IsNative = true)]
    public class EventObject
    {
        public object getSource()
        {
            return default(object);
        }
    }
}
