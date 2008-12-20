using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib.JavaScript.DOM;
using System.Collections;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{

    [Script(Implements = typeof(global::System.Array))]
    internal class __Array
    {
		public static int IndexOf<T>(T[] array, T value)
		{
			return ((IArray<T>)(object)(array)).indexOf(value);
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


        /*
        public static IEnumerable<TSource> Sort<TSource>(IEnumerable<TSource> source, Func<TSource, TSource, int> c)
        {
 
        }*/
        /*
        public static void Sort(Array array, IComparer comparer)
        {
        }*/

        public static void Sort<T>(T[] array, Comparison<T> c)
        {
            ((IArray<T>)(object)(array)).sort((a, b) => c(a, b));
        }

        public static void Sort<T>(T[] array, IComparer<T> comparer)
        {
            Sort(array, comparer.Compare);
        }
    }
}
