﻿using ScriptCoreLib;
using System.Collections.Generic;
using System.Linq;
using System;
using System.ComponentModel;

namespace ScriptCoreLib.Shared.Lambda
{
	[Script]
	public static partial class LambdaExtensions
	{

		/// <summary>
		/// Returns an action when raised call the filter to decide if to raise the source event
		/// </summary>
		/// <param name="e"></param>
		/// <param name="filter"></param>
		/// <returns></returns>
		public static Action ToFiltered(this Action source, Func<bool> filter)
		{
			return delegate
			{
				if (filter())
					source();
			};
		}

		/// <summary>
		/// Inovocation of the handler is governed by the filter. This extension enables you to skip to a certain index to actually invoke the handler.
		/// </summary>
		/// <param name="handler"></param>
		/// <param name="filter"></param>
		/// <returns></returns>
		public static Action WhereCounter(this Action handler, Func<int, bool> filter)
		{
			var i = 0;

			return delegate
			{
				if (filter(i))
					handler();

				i++;
			};
		}

		public static bool AllWithPrevious<T>(this IEnumerable<T> source, Func<T, T, bool> filter)
		{
			var previous = default(T);
			var ready = false;
			var value = true;

			foreach (var item in source.AsEnumerable())
			{
				if (ready)
				{
					value = filter(previous, item);

					if (!value)
						break;
				}

				ready = true;
				previous = item;
			}

			return value;
		}

		public static void AddRange<T>(this IList<T> a, params T[] e)
		{
			foreach (var v in e)
			{
				a.Add(v);
			}
		}


		public static void Times(this int count, Action h)
		{
			for (int i = 0; i < count; i++)
			{
				h();
			}
		}

		public static void Times(this int count, Action<int> h)
		{
			for (int i = 0; i < count; i++)
			{
				h(i);
			}
		}

		public static void Times(this int count, Action<int, Action> HandlerWithIndexAndSignalNext)
		{
			Enumerable.Range(0, count).ForEach(HandlerWithIndexAndSignalNext);
		}


		public static void Times(this int count, Action<Action> HandlerWithSignalNext)
		{
			Enumerable.Range(0, count).ForEach(
				(i, SignalNext) => HandlerWithSignalNext(SignalNext)
			);
		}


		public static T Take<T>(this IEnumerator<T> e)
		{
			if (e.MoveNext())
				return e.Current;


			throw new Exception("source is empty");
		}

		public static T[] Take<T>(this IEnumerator<T> e, int length)
		{
			var a = new T[length];

			for (int i = 0; i < length; i++)
			{
				a[i] = e.Take();
			}

			return a;
		}

		public static T TakeOrDefault<T>(this IEnumerator<T> e)
		{
			var r = default(T);

			if (e.MoveNext())
				r = e.Current;


			return r;
		}

		public static Action<T> Combine<T>(this IEnumerable<Action<T>> source)
		{
			return
				(a) =>
				{
					foreach (var e in source)
					{
						e(a);
					}
				};
		}

		public static Func<bool> And(this Func<bool> a, Func<bool> b)
		{
			return () => a() && b();
		}

		public static Func<bool> Or(this Func<bool> a, Func<bool> b)
		{
			return () => a() || b();
		}


		public static TReturn[] ToArray<T, TReturn>(this IEnumerable<T> source, Func<T, TReturn> selector)
		{
			return source.Select(selector).ToArray();
		}

		public static IEnumerable<T> WhereNot<T>(this IEnumerable<T> source, Func<T, bool> filter)
		{
			return source.Where(k => !filter(k));
		}

		public static string ConcatToLines(this IEnumerable<string> source)
		{
			return source.Select(k => k + Environment.NewLine).Concat();
		}

		public static string Concat(this IEnumerable<string> source)
		{
			return string.Concat(source.AsEnumerable().ToArray());
		}

		public static int Count(this string e, string subject)
		{
			var i = e.IndexOf(subject);
			var c = 0;

			while (i >= 0)
			{
				c++;
				i = e.IndexOf(subject, i + subject.Length);
			}

			return c;
		}

		public static IEnumerable<T> ConcatSingle<T>(this IEnumerable<T> source, T e)
		{
			return source.Concat(new[] { e });
		}
		public static IEnumerable<T> Replace<T>(this IEnumerable<T> source, T remove, T add)
		{
			var _remove = new[] { remove };
			var _add = new[] { add };

			return source.Replace(_remove, _add);
		}

		public static IEnumerable<T> Replace<T>(this IEnumerable<T> source, IEnumerable<T> remove, IEnumerable<T> add)
		{
			return source.Where(k => !remove.Contains(k)).Concat(add);
		}

		public static string[] Split(this string e, int length)
		{
			var a = new List<string>();
			var u = e.Length;

			for (int i = 0; i < u; i += length)
			{
				var c = length;
				var n = u - i;

				if (n < c)
					n = c;

				a.Add(e.Substring(i, c));
			}

			return a.ToArray();
		}

		public static string[][] Split(this string[] e, int length)
		{
			var a = new List<string[]>();

			for (int i = 0; i < e.Length; i += length)
			{
				var n = new string[length];

				for (int u = 0; u < length; u++)
				{
					var c = i + u;

					if (c < e.Length)
						n[u] = e[c];
				}

				a.Add(n);
			}

			return a.ToArray();
		}

		public static T Apply<T>(this T e, Action<T> h)
			where T : class
		{
			if (e != null)
				h(e);

			return e;
		}

		public static T Apply<T>(this T e, Action<T, Action> HandlerWithRetry)
		where T : class
		{
			var Retry = default(Action);

			Retry =
				delegate
				{
					if (e != null)
						HandlerWithRetry(e, Retry);
				};

			Retry();

			return e;
		}

		public static void Do(this IEnumerable<Action> e)
		{
			foreach (var a in e)
			{
				// some handlers can be null

				if (a != null)
					a();
			}
		}

		public static T AtModulus<T>(this IEnumerable<T> a, int i)
		{
			var x = a.ToArray();

			return x[i % x.Length];
		}

		public static T AtOrDefault<T>(this IEnumerable<T> a, int i, T value)
		{
			var j = 0;
			var r = value;

			foreach (var v in a)
			{
				if (j == i)
				{
					r = v;
					break;
				}

				j++;
			}

			return r;
		}


		public static T Previous<T>(this IEnumerable<T> source, Func<T, bool> predicate)
		{
			var prev = source.Last();
			var r = default(T);

			foreach (var v in source.AsEnumerable())
			{
				if (predicate(v))
				{
					r = prev;
					break;
				}

				prev = v;
			}

			return r;
		}

		public static T Next<T>(this IEnumerable<T> source, Func<T, bool> predicate)
		{
			var last = source.Last();
			var r = default(T);

			foreach (var v in source.AsEnumerable())
			{
				if (predicate(last))
				{
					r = v;
					break;
				}

				last = v;
			}

			return r;
		}


		#region min max

		public static double Max(this double e, double x)
		{

			if (e > x)
				return e;

			return x;
		}

		public static double Min(this double e, double x)
		{
			if (e < x)
				return e;

			return x;
		}

		public static int Min(this int e, int x)
		{
			if (e < x)
				return e;

			return x;
		}

		public static int Max(this int e, int x)
		{
			if (e > x)
				return e;

			return x;
		}
		#endregion

		public static Action AsCyclic(this global::System.Action<Action> f)
		{
			var done = default(Action);

			done = () => f(done);

			return done;
		}

	
		public static ParamsAction<A> AsParamsAction<A>(this global::System.Action<A> f)
		{
			return
				a =>
				{
					foreach (var i in a)
					{
						f(i);
					}
				};
		}


		public static global::System.Func<A, bool> AsNegative<A>(this global::System.Func<A, bool> f)
		{
			return i => !f(i);
		}

		public static Action<A> AsAction<A, T>(this global::System.Func<A, T> f)
		{
			return (a) => f(a);
		}

		public static Action<int, int> WithOffset(this Action<int, int> f, int x, int y)
		{
			return (ix, iy) => f(ix + x, iy + y);
		}

		public static Func<int, int, T> WithOffset<T>(this Func<int, int, T> f, int x, int y)
		{
			return (ix, iy) => f(ix + x, iy + y);
		}
	}

	[Script]
	public delegate global::System.Func<A, T> YFunc<A, T>(global::System.Func<A, T> e);

	[Script]
	public delegate global::System.Func<A, B, T> YFunc<A, B, T>(global::System.Func<A, B, T> e);



	partial class LambdaExtensions
	{
		public static global::System.Func<A, T> Y<A, T>(this YFunc<A, T> le)
		{
			var me = default(global::System.Func<A, T>); return me = (a) => le(me)(a);
		}

		public static global::System.Func<A, B, T> Y<A, B, T>(this YFunc<A, B, T> le)
		{
			var me = default(global::System.Func<A, B, T>); return me = (a, b) => le(me)(a, b);
		}


	}

	[Script]
	public delegate void ParamsAction<A, B>(A a, params B[] b);

	[Script]
	public delegate void ParamsAction<A>(params A[] a);

	[Script]
	public delegate TReturn ParamsFunc<A, B, TReturn>(A a, params B[] b);

	[Script]
	public delegate TReturn ParamsFunc<A, TReturn>(params A[] a);


	[Script]
	public delegate System.Action<A> YAction<A>(System.Action<A> e);

	[Script]
	public delegate System.Action<A, B> YAction<A, B>(System.Action<A, B> e);



	partial class LambdaExtensions
	{
		public static Action<A> Y<A>(this YAction<A> le)
		{
			var me = default(Action<A>); return me = (a) => le(me)(a);
		}

		public static Action<A, B> Y<A, B>(this YAction<A, B> le)
		{
			var me = default(Action<A, B>); return me = (a, b) => le(me)(a, b);
		}
	}

	partial class LambdaExtensions
	{
		public static TSeed Aggregate<TSeed>(this TSeed e, Action<TSeed> a)
		{
			a(e);

			return e;
		}

		public static IEnumerable<TSource> Randomize<TSource>(this IEnumerable<TSource> u)
		{
			var x = u.ToList();
			var y = new List<TSource>();
			var r = new System.Random();

			while (x.Count > 0)
			{
				var i = r.Next() % x.Count;

				y.Add(x[i]);
				x.RemoveAt(i);
			}

			return y;
		}

		public static T Random<T>(this IEnumerable<T> e)
		{
			return e.Randomize().First();
		}

		public static T Random<T>(this IEnumerable<T> e, Func<T, bool> filter)
		{
			return e.Where(filter).Randomize().First();
		}


	}
}
