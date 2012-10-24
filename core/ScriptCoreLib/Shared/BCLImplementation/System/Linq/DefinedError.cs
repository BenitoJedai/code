using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Linq
{
    [Script]
    internal static class __DefinedError
    {
        public static InvalidOperationException ArgumentOutOfRange(string paramName)
        {
            return new InvalidOperationException("ArgumentOutOfRange: " + paramName);
        }



        public static InvalidOperationException NoElements()
        {
            return new InvalidOperationException("Sequence contains no elements");
        }

        public static InvalidOperationException MoreThanOneElement()
        {
            return new InvalidOperationException("Sequence contains more than one element");
        }

     
    }
}
