using ScriptCoreLib;
using ScriptCoreLib.Shared;

using global::System.Collections;
using global::System.Collections.Generic;


using IDisposable = global::System.IDisposable;
using System;
using System.Linq;

namespace ScriptCoreLib.Shared.Query
{




	internal static partial class __Enumerable
	{
		public static double Average<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
		{
			return source.Select<TSource, int>(selector).Average();
		}



		public static double Average(this IEnumerable<int> source)
		{
			if (source == null)
			{
				throw DefinedError.ArgumentNull("source");
			}
			long num = 0L;
			long num2 = 0L;
			foreach (int num3 in source.AsEnumerable())
			{
				num += num3;
				num2 += 1L;
			}
			if (num2 <= 0L)
			{
				throw DefinedError.NoElements();
			}
			return (((double)num) / ((double)num2));
		}


		public static int Max<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
		{
			var value = 0;
			var dirty = false;

			foreach (var v in source.AsEnumerable())
			{
				var x = selector(v);

				if (dirty)
				{
					if (value < x)
						value = x;
				}
				else
				{
					dirty = true;
					value = x;
				}
			}

			if (!dirty)
				throw DefinedError.NoElements();

			return value;
		}

		public static double Max<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector)
		{
			double value = 0;
			var dirty = false;

			foreach (var v in source.AsEnumerable())
			{
				var x = selector(v);

				if (dirty)
				{
					if (value < x)
						value = x;
				}
				else
				{
					dirty = true;
					value = x;
				}
			}

			if (!dirty)
				throw DefinedError.NoElements();

			return value;
		}


		public static double Min<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector)
		{
			double value = 0;
			var dirty = false;

			foreach (var v in source.AsEnumerable())
			{
				var x = selector(v);

				if (dirty)
				{
					if (value > x)
						value = x;
				}
				else
				{
					dirty = true;
					value = x;
				}
			}

			if (!dirty)
				throw DefinedError.NoElements();

			return value;
		}
		public static IEnumerable<TSource> Reverse<TSource>(this IEnumerable<TSource> source)
		{
			var a = source.ToList();

			a.Reverse();

			return a;
		}


		public static bool Any<TSource>(this IEnumerable<TSource> source)
		{
			if (source == null)
			{
				throw DefinedError.ArgumentNull("source");
			}


			var r = false;

			foreach (var v in source.AsEnumerable())
			{
				r = true;

				break;
			}

			return r;
		}

		public static bool Any<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw DefinedError.ArgumentNull("source");
			}

			if (predicate == null)
			{
				throw DefinedError.ArgumentNull("predicate");
			}

			var r = false;

			foreach (var v in source.AsEnumerable())
			{
				if (predicate(v))
				{
					r = true;

					break;
				}
			}

			return r;
		}

		public static bool All<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw DefinedError.ArgumentNull("source");
			}

			if (predicate == null)
			{
				throw DefinedError.ArgumentNull("predicate");
			}

			var r = true;

			foreach (var v in source.AsEnumerable())
			{
				if (!predicate(v))
				{
					r = false;

					break;
				}
			}

			return r;
		}

		public static bool Contains<TSource>(this IEnumerable<TSource> source, TSource value)
		{
			if (source == null)
			{
				throw DefinedError.ArgumentNull("source");
			}


			var r = false;

			foreach (var v in source.AsEnumerable())
			{
				if (object.ReferenceEquals(v, value))
				{
					r = true;

					break;
				}
			}

			return r;
		}

		#region Min
		public static int Min(this IEnumerable<int> source)
		{
			if (source == null)
			{
				throw DefinedError.ArgumentNull("source");
			}
			int num2 = 0;
			bool flag2 = false;
			foreach (int num3 in source.AsEnumerable())
			{
				if (flag2)
				{
					if (num3 < num2)
					{
						num2 = num3;
					}
					continue;
				}
				num2 = num3;
				flag2 = true;
			}
			if (!flag2)
			{
				throw DefinedError.NoElements();
			}
			return num2;
		}





		#endregion

		#region Max



		public static int Max(this IEnumerable<int> source)
		{
			if (source == null)
			{
				throw DefinedError.ArgumentNull("source");
			}
			int num2 = 0;
			bool flag2 = false;
			foreach (int num3 in source.AsEnumerable())
			{
				if (flag2)
				{
					if (num3 > num2)
					{
						num2 = num3;
					}
					continue;
				}
				num2 = num3;
				flag2 = true;
			}
			if (!flag2)
			{
				throw DefinedError.NoElements();
			}
			return num2;
		}
		#endregion

		public static int Count<T>(this IEnumerable<T> e, global::System.Func<T, bool> predicate)
		{
			int c = 0;

			foreach (var v in e.AsEnumerable()) if (predicate(v)) c++;

			return c;
		}

		public static int Count<T>(this IEnumerable<T> e)
		{
			int c = 0;

			foreach (var v in e.AsEnumerable()) c++;

			return c;
		}


		public static T ElementAt<T>(this IEnumerable<T> e, int index)
		{
			int i = -1;

			T r = default(T);

			foreach (var v in e.AsEnumerable())
			{
				i++;

				if (i == index)
				{
					r = v;
					break;
				}
			}

			return r;
		}




		public static TSource LastOrDefault<TSource>(this IEnumerable<TSource> source)
		{
			if (source == null)
			{
				throw DefinedError.ArgumentNull("source");
			}


			var value = default(TSource);

			foreach (TSource local in source.AsEnumerable())
			{
				value = local;

			}

			return value;
		}

		public static TSource Last<TSource>(this IEnumerable<TSource> source)
		{
			if (source == null)
			{
				throw DefinedError.ArgumentNull("source");
			}

			TSource current;

			using (IEnumerator<TSource> enumerator = source.AsEnumerable().GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					current = enumerator.Current;

					while (enumerator.MoveNext())
					{
						current = enumerator.Current;
					}

				}
				else
					throw DefinedError.NoElements();
			}

			return current;
		}


		public static TSource First<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> filter)
		{
			return source.Where(filter).First();
		}

		public static TSource First<TSource>(this IEnumerable<TSource> source)
		{
			if (source == null)
			{
				throw DefinedError.ArgumentNull("source");
			}

			TSource current;


			using (IEnumerator<TSource> enumerator = source.AsEnumerable().GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					current = enumerator.Current;
				}
				else
					throw DefinedError.NoElements();

			}

			return current;

		}

		public static TSource FirstOrDefault<TSource>(this IEnumerable<TSource> source)
		{
			if (source == null)
			{
				throw DefinedError.ArgumentNull("source");
			}

			var current = default(TSource);

			using (IEnumerator<TSource> enumerator = source.AsEnumerable().GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					current = enumerator.Current;
				}
			}

			return current;
		}

		public static TSource FirstOrDefault<TSource>(this IEnumerable<TSource> source, global::System.Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw DefinedError.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw DefinedError.ArgumentNull("predicate");
			}

			var value = default(TSource);

			foreach (TSource local in source.AsEnumerable())
			{
				if (predicate(local))
				{
					value = local;

					break;
				}
			}

			return value;
		}



		public static TSource Single<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			return source.Where(predicate).Single();
		}

		public static TSource Single<TSource>(this IEnumerable<TSource> source)
		{
			if (source == null)
			{
				throw DefinedError.ArgumentNull("source");
			}

			TSource current;

			using (IEnumerator<TSource> enumerator = source.AsEnumerable().GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					throw DefinedError.NoElements();
				}
				current = enumerator.Current;

				if (enumerator.MoveNext())
				{
					throw DefinedError.MoreThanOneElement();


				}
			}

			return current;
		}


		public static TSource SingleOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			return source.Where(predicate).SingleOrDefault();
		}

		public static TSource SingleOrDefault<TSource>(this IEnumerable<TSource> source)
		{
			if (source == null)
			{
				throw DefinedError.ArgumentNull("source");
			}

			var current = default(TSource);

			using (IEnumerator<TSource> enumerator = source.AsEnumerable().GetEnumerator())
			{
				if (enumerator.MoveNext())
					current = enumerator.Current;


			}

			return current;
		}

		//public static void ForEach<T, R>(this IEnumerable<T> array, Func<T, R> func)
		//{
		//    array.ForEach(func.AsAction());
		//}







		public static TSource[] ToArray<TSource>(this IEnumerable<TSource> source)
		{
			return source.ToList().ToArray();
		}

		public static List<TSource> ToList<TSource>(this IEnumerable<TSource> source)
		{
			if (source == null)
			{
				throw DefinedError.ArgumentNull("source");
			}
			return new List<TSource>(source);
		}


		public static U Aggregate<T, U>(this IEnumerable<T> source,
								U seed, global::System.Func<U, T, U> func)
		{
			U result = seed;

			foreach (T element in source.AsEnumerable())
				result = func(result, element);

			return result;
		}

		//public static U Aggregate<T, U>(this IEnumerable<T> source,
		//                        U seed, Action<U, T> func)
		//{
		//    return source.Aggregate(seed, (u, t) => { func(u, t); return u; });
		//}








	}





}
