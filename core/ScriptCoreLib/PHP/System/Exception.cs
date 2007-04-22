using ScriptCoreLib;
using ScriptCoreLib.PHP.Runtime;

namespace ScriptCoreLib.PHP.System
{


    [Script(InternalConstructor = true, Implements = typeof(global::System.Exception))]
    public class ScriptException : global::System.Exception
    {

        public new string Message
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return Expando<string>.Of(this)["message"];
            }
        }

        #region Constructor

        public ScriptException(string message) : base(message) { }

            [Script(OptimizedCode = @"return new Exception($e);")]
            static ScriptException InternalConstructor(string e)
            {
                return default(ScriptException);
            }
        
        #endregion

    }

}
