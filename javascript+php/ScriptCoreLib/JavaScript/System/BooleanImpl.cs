using ScriptCoreLib.JavaScript;

namespace ScriptCoreLib.JavaScript.System
{
    [Script(Implements = typeof(bool))]
    internal class BooleanImpl
    {
        [Script(OptimizedCode=@"return !!e;")]
        public static BooleanImpl Parse(string e)
        {
            return default(BooleanImpl);
        }
    }
}