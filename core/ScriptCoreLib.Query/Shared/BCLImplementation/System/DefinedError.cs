using ScriptCoreLib;
using ScriptCoreLib.Shared;

using global::System.Collections;
using global::System.Collections.Generic;

using IDisposable = global::System.IDisposable;

using System;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Linq
{

    [Script]
    internal static class __DefinedError
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

        public static Exception MoreThanOneElement()
        {
            return new Exception("Sequence contains more than one element");
        }

        public static Exception NotImplemented()
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }

}
