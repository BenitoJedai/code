using ScriptCoreLib;
using ScriptCoreLib.PHP.Runtime;

namespace ScriptCoreLib.PHP.BCLImplementation.System
{
    [Script(Implements = typeof(global::System.Single))]
    internal class __Single
    {
        public class API
        {
            

        }

        public static float Parse(string e)
        {
            return (float)Native.API.floatval(e);
        }
    }
}
