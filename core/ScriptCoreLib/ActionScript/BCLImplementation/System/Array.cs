using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System
{
    [Script(
        Implements = typeof(global::System.Array),
        ImplementationType = typeof(Array)
        )]
    internal class __Array
    {
        [Script(OptimizedCode = "d[i] = s[i];")]
        internal static void InternalCopyElement(global::System.Array s, global::System.Array d, int i)
        {
        }

        public static void Copy(global::System.Array sourceArray, global::System.Array destinationArray, int length)
        {
            for (int i = 0; i < length; i++)
            {
                InternalCopyElement(sourceArray, destinationArray, i);
            }
        }
    }
}
