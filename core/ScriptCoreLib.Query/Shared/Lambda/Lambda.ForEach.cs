using ScriptCoreLib;
using System.Collections.Generic;
using System.Linq;
using System;
using System.ComponentModel;

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

		/// <summary>
		/// Skips the first element and then passes previous-current pairs to the handler.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source"></param>
		/// <param name="handler"></param>
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

		public static BindingList<T> ForEachNewOrExistingItem<T>(this BindingList<T> source, Action<T, int, Action<Action<Action>>> HandlerWithDelayedWork)
		{
			var LazyLoad = new FutureStream();

			var LazyLoadInitialize = LazyLoad.Continue(
				SignalNext =>
				{
					SignalNext();
				}
			);

			LazyLoadInitialize();


			return source.ForEachNewOrExistingItem(
				(value, index) =>
				{
					HandlerWithDelayedWork(value, index, LazyTask => LazyLoad.Continue(LazyTask));
				}
			);
		}

		public static BindingList<T> ForEachNewOrExistingItem<T>(this BindingList<T> source, Action<T, int> handler)
		{
			source.ForEach(handler);

			source.ListChanged +=
				(sender0, args0) =>
				{
					if (args0.ListChangedType == ListChangedType.ItemAdded)
					{
						handler(source[args0.NewIndex], args0.NewIndex);
					}
				};

			return source;

		}

		public static BindingList<T> ForEachNewOrExistingItem<T>(this BindingList<T> source, Action<T> handler)
		{
			source.ForEachNewItem(handler).ForEach(handler);

			return source;
		}

		public static BindingList<T> ForEachNewItem<T>(this BindingList<T> source, Action<T> handler)
		{
			source.ListChanged +=
				(sender0, args0) =>
				{
					if (args0.ListChangedType == ListChangedType.ItemAdded)
					{
						handler(source[args0.NewIndex]);
					}
				};

			return source;
		}

		public static BindingList<T> ForEachItemDeleted<T>(this BindingList<T> source, Action<T> handler)
		{
			var cache = new List<T>();

			cache.AddRange(source);

			source.ListChanged +=
				(sender0, args0) =>
				{
					if (args0.ListChangedType == ListChangedType.ItemAdded)
					{
						cache.Add(source[args0.NewIndex]);
						return;
					}

					if (args0.ListChangedType == ListChangedType.ItemDeleted)
					{
						var k = cache[args0.NewIndex];

						cache.RemoveAt(args0.NewIndex);

						handler(k);
					}
				};

			return source;
		}

	
		public static BindingListWithEvents<T> WithEvents<T>(this BindingList<T> source)
		{
			return new BindingListWithEvents<T>(source);
		}

		public static BindingList<T> ForEachItemDeleted<T>(this BindingList<T> source, Action<T, Action> handler)
		{
			var cache = new List<T>();

			cache.AddRange(source);

			ListChangedEventHandler h = null;

			Action Dispose =
				delegate
				{
					source.ListChanged -= h;
					h = null;
					cache.Clear();
					cache = null;
				};

			h = (sender0, args0) =>
				{
					if (args0.ListChangedType == ListChangedType.ItemAdded)
					{
						cache.Add(source[args0.NewIndex]);
						return;
					}

					if (args0.ListChangedType == ListChangedType.ItemDeleted)
					{
						var k = cache[args0.NewIndex];

						cache.RemoveAt(args0.NewIndex);

						handler(k, Dispose);
					}
				};

			source.ListChanged += h;

			return source;
		}
	}
}
