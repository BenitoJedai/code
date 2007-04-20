using ScriptCoreLib;
using ScriptCoreLib.Shared;

using global::System.Collections;
using global::System.Collections.Generic;

using IDisposable = global::System.IDisposable;
using ScriptException = global::ScriptCoreLib.JavaScript.System.ScriptException;

namespace ScriptCoreLib.Shared.Query
{

    [Script]
    internal static class Error
    {
        public static ScriptException ArgumentNull(string paramName)
        {
            return new ScriptException("ArgumentNull: " + paramName);
        }

        public static ScriptException NoElements()
        {
            return new ScriptException("Sequence contains no elements");
        }

        internal static ScriptException MoreThanOneElement()
        {
            return new ScriptException("Sequence contains more than one element");
        }







    }

}
