using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript;

namespace ScriptCoreLib.JavaScript.System
{
    [Script(Implements = typeof(global::System.Double))]
    internal class Double
    {

        [Script(OptimizedCode = "return parseFloat(e);")]
        static public Double Parse(string e)
        {
            return default(Double);
        }


        [Script(DefineAsStatic = true)]
        public int CompareTo(Double e)
        {
            return Expando.Compare(this, e);
        }
    }
}
