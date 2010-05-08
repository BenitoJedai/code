using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Extensions
{
	public static class LinqExtensions
	{
		public static T With<T>(this T e, Action<T> h) where T : class
		{
			h(e);

			return e;
		}

		public static IEnumerable<T> WithEach<T>(this IEnumerable<T> collection, Action<T> h) where T : class
		{
			InternalWithEach<T>(collection, h);

			return collection;
		}

		private static void InternalWithEach<T>(IEnumerable<T> collection, Action<T> h) where T : class
		{
			foreach (var item in collection)
			{
				h(item);
			}
		}

		public static IEnumerable<T> SelectWithSeparator<T>(this IEnumerable<T> source, T f)
		{
			return source.SelectWithSeparator((p, c) => f);
		}


		public static IEnumerable<T> SelectWithSeparator<T>(this IEnumerable<T> source, Func<T, T, T> f)
		{
			var i = -1;
			var x = default(T);

			return source.SelectMany(
				c =>
				{
					var y = x;
					x = c;
					i++;

					if (i > 0)
						return new[] { f(y, c), c };

					return new[] { c };
				}
			);
		}

		public static IEnumerable<Action> Invoke(this IEnumerable<Action> source)
		{
			foreach (var item in source.ToArray())
			{
				if (item != null)
					item();
			}

			return source;
		}

		public class AnonymousContainer<T>
		{
			public T Default;
		}

		public static AnonymousContainer<T> ToAnonymousContainer<T>(this T t)
		{
			return new AnonymousContainer<T> { Default = t };
		}

		public static Func<T, A, A> ToFunc<T, A>(this AnonymousContainer<T> c, Func<T, A> h)
		{
			return
				(t, Default) =>
				{
					if (t == null)
						return Default;

					return h(t);
				};
		}

		public static Func<F, A, A> FirstParameter<T, A, F>(this Func<T, A, A> s, Func<F, T> c)
		{
			return (f, a) => s(c(f), a);
		}

		public static Func<T> ToCachedFunc<T>(this Func<T> u)
		{
			var f = default(Func<T>);

			f = delegate
			{
				var r = u();

				f = () => r;

				return r;
			};

			return () => f();
		}

	
	}
}
