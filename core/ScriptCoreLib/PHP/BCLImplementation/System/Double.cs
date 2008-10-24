using ScriptCoreLib;
using ScriptCoreLib.PHP.Runtime;

namespace ScriptCoreLib.PHP.BCLImplementation.System
{
    [Script(Implements = typeof(global::System.Double))]
    internal class __Double
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
