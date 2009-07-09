using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.Extensions;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System
{
    [Script(
        Implements = typeof(global::System.Array),
        ImplementationType = typeof(Array)
        )]
    internal class __Array
    {
		public static int IndexOf<T>(T[] array, T value)
		{
			return ((Array)(object)(array)).indexOf(value);
		}

        [Script(OptimizedCode = "d[i] = s[i];")]
        internal static void InternalCopyElement(global::System.Array s, global::System.Array d, int i)
        {
        }

		[Script(OptimizedCode = "d[di] = s[si];")]
		internal static void InternalCopyElement(global::System.Array s, int si, global::System.Array d, int di)
		{
		}


        public static void Copy(global::System.Array sourceArray, global::System.Array destinationArray, int length)
        {
            for (int i = 0; i < length; i++)
            {
                InternalCopyElement(sourceArray, destinationArray, i);
            }
        }

		public static void Copy(global::System.Array sourceArray, int sourceOffset, global::System.Array destinationArray, int destinationOffset, int length)
		{
			for (int i = 0; i < length; i++)
			{
				InternalCopyElement(sourceArray, i + sourceOffset, destinationArray, i + destinationOffset);
			}
		}


        public static void Sort<T>(T[] array, IComparer<T> comparer)
        {
            Sort(array, comparer.Compare);
        }

        public static void Sort<T>(T[] array, Comparison<T> comparison)
        {
            var a = array as global::ScriptCoreLib.ActionScript.Array;

            a.sort(comparison.ToFunction());
        }

        public static void Reverse(global::System.Array array)
        {
            var a = ((object)array) as global::ScriptCoreLib.ActionScript.Array;

            a.reverse();
        }

		public static global::System.Array CreateInstance(Type elementType, int length)
		{
			// in ActionScript we are not yet using vector<>
			// which would have the element type
			// this means that at this time we will return typeless array

			var a = new object[length];

			return (global::System.Array)a;
		}
    }
}
