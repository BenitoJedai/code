using ScriptCoreLib;
using ScriptCoreLib.PHP.Runtime;

namespace ScriptCoreLib.PHP.System
{
    [Script(Implements = typeof(global::System.Double))]
    public class DoubleImpl
    {
        public class API
        {
            

        }

        public static double Parse(string e)
        {
            return (double)Native.API.floatval(e);
        }
    }
}
