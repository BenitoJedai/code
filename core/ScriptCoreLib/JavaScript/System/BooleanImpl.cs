using ScriptCoreLib.JavaScript;

namespace ScriptCoreLib.JavaScript.System
{
    [Script(Implements = typeof(global::System.Boolean))]
    internal class BooleanImpl
    {
        [Script(OptimizedCode=@"return !!e;")]
        public static BooleanImpl Parse(string e)
        {
            return default(BooleanImpl);
        }
    }
}