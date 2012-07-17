using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace ScriptCoreLib.PHP.BCLImplementation.System
{

    [Script(Implements = typeof(global::System.Array))]
    internal class __Array
    {
	

        public static void Sort<T>(T[] array, Comparison<T> c)
        {
        }

        public static void Sort<T>(T[] array, IComparer<T> comparer)
        {
        }

    }
}
