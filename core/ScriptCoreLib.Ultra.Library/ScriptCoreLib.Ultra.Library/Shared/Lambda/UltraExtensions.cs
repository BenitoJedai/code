using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.Lambda
{
	public static class UltraExtensions
	{
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

		public static void Invoke(this IEnumerable<Action> e)
		{
			foreach (var item in e)
			{
				item();
			}
		}
	}
}
