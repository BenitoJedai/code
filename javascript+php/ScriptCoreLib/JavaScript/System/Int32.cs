using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript;

namespace ScriptCoreLib.JavaScript.System
{
    [Script(Implements = typeof(global::System.Int32))]
    internal class Int32
    {

        [Script(OptimizedCode = "return parseInt(e);")]
        static public Int32 Parse(string e)
        {
            return default(Int32);
        }

        [Script(DefineAsStatic = true)]
        public int CompareTo(Int32 e)
        {
            return Expando.Compare(this, e);
           
        }
    }
}
