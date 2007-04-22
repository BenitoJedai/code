using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript;

namespace ScriptCoreLib.JavaScript.System
{
    [Script(InternalConstructor = true, Implements = typeof(global::System.Exception))]
    public class ScriptException : global::System.Exception
    {

        public new string Message
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return Expando<string, string>.Of(this)["message"];
            }
        }

        #region Constructor

        public ScriptException(string message) : base(message) { }

        [Script(OptimizedCode = @"return new Error(e);")]
        static ScriptException InternalConstructor(string e)
        {
            return default(ScriptException);
        }

        #endregion

    }
}
