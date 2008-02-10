using ScriptCoreLib;
using ScriptCoreLib.Shared;

using global::System.Collections;
using global::System.Collections.Generic;

using IDisposable = global::System.IDisposable;

using System;

namespace ScriptCoreLib.ActionScript.Query
{

    [Script]
    internal static class DefinedErrors
    {
        internal static Exception ArgumentOutOfRange(string paramName)
        {
            return new Exception("ArgumentOutOfRange: " + paramName);
        }

        public static Exception ArgumentNull(string paramName)
        {
            return new Exception("ArgumentNull: " + paramName);
        }

        public static Exception NoElements()
        {
            return new Exception("Sequence contains no elements");
        }

        internal static Exception MoreThanOneElement()
        {
            return new Exception("Sequence contains more than one element");
        }








        internal static Exception NotImplemented()
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }

}
