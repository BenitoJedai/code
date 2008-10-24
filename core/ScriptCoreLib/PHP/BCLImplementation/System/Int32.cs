using ScriptCoreLib;
using ScriptCoreLib.PHP.Runtime;

namespace ScriptCoreLib.PHP.BCLImplementation.System
{
    [Script(Implements = typeof(global::System.Int32))]
    internal class __Int32
    {
        public class API
        {
            

        }

        public static int Parse(string e)
        {
            return Native.API.intval(e);
        }
    }
}
