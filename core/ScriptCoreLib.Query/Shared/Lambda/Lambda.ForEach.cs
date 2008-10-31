using ScriptCoreLib;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ScriptCoreLib.Shared.Lambda
{
	partial class LambdaExtensions
	{
		
		public static IEnumerable<T> ForEach<T>(this IEnumerable<T> array, Action<T, int> action)
		{
			if (array == null)
			{
				throw new System.ArgumentNullException("array");
			}
			if (action == null)
			{
				throw new System.ArgumentNullException("action");
			}

			var i = 0;
			foreach (var v in array.AsEnumerable())
			{
				action(v, i);
				i++;
			}

			return array;
		}

		public static IEnumerable<T> ForEach<T>(this IEnumerable<T> array, IEnumerable<Action<T>> action)
		{
			using (var s = action.GetEnumerator())
				foreach (var item in array)
				{
					if (s.MoveNext())
						s.Current(item);

				}

			return array;
		}

		public static IEnumerable<T> ForEach<T>(this IEnumerable<T> array, Action<T> action)
		{
			if (array == null)
			{
				throw new System.ArgumentNullException("array");
			}
			if (action == null)
			{
				throw new System.ArgumentNullException("action");
			}

			foreach (var v in array.AsEnumerable())
			{
				action(v);
			}

			return array;
		}


		public static IEnumerable<T> ForEachReversed<T>(this IEnumerable<T> array, Action<T> action)
		{
			var a = array.ToArray();

			for (int i = a.Length - 1; i >= 0; i--)
			{
				action(a[i]);
			}


			return array;
		}

		public static void ForEachWithPrevious<T>(this IEnumerable<T> source, Action<T, T> handler)
		{
			var previous = default(T);
			var ready = false;

			foreach (var item in source.AsEnumerable())
			{
				if (ready)
				{
					handler(previous, item);
				}

				ready = true;
				previous = item;
			}
		}
	}
}
