using ScriptCoreLib;
using ScriptCoreLib.PHP.Runtime;

namespace ScriptCoreLib.PHP.System
{
    [Script(Implements = typeof(global::System.Int32))]
    public class Int32Impl
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
