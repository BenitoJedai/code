using ScriptCoreLib;
using System.Collections.Generic;
using System.Linq;
using System;
using System.ComponentModel;

namespace ScriptCoreLib.Shared.Lambda
{
	public static partial class LambdaExtensions
	{

		
		public static ParamsAction<B> FixParam<A, B>(this ParamsAction<A, B> f, A a)
		{
			return b => f(a, b);
		}

		public static ParamsAction<B> FixParam<A, B>(this  A a, ParamsAction<A, B> f)
		{
			return FixParam(f, a);
		}

		public static Action FixParam<A>(this A a, Action<A> f)
		{
			return FixParam(f, a);
		}

		public static Action FixParam<A>(this global::System.Action<A> f, A a)
		{
			return () => f(a);
		}

		public static global::System.Func<T> FixParam<A, T>(this global::System.Func<A, T> f, A a)
		{
			return () => f(a);
		}

		public static global::System.Func<A, T> FixParam<A, B, T>(this global::System.Func<A, B, T> f, B b)
		{
			return (a) => f(a, b);
		}

		#region FixFirstParam

		public static global::System.Func<B, T> FixFirstParam<A, B, T>(this global::System.Func<A, B, T> f, A a)
		{
			return (b) => f(a, b);
		}

		public static Action<B> FixFirstParam<A, B>(this Action<A, B> f, A a)
		{
			return (b) => f(a, b);
		}

		#endregion

		#region FixFirstParam

		public static global::System.Func<A, T> FixLastParam<A, B, T>(this global::System.Func<A, B, T> f, B b)
		{
			return (a) => f(a, b);
		}

		public static ParamsAction<A> FixLastParamToIndex<A>(this Action<A, int> f)
		{
			return
				(a) =>
				{
					a.ForEach(f);
				};
		}

		public static global::System.Func<A, B, T> FixLastParam<A, B, C, T>(this global::System.Func<A, B, C, T> f, C c)
		{
			return (a, b) => f(a, b, c);
		}

		public static global::System.Func<A, B, C, T> FixLastParam<A, B, C, D, T>(this global::System.Func<A, B, C, D, T> f, D d)
		{
			return (a, b, c) => f(a, b, c, d);
		}

		public static Action<A, B> FixLastParam<A, B, C>(this Action<A, B, C> f, C c)
		{
			return (a, b) => f(a, b, c);
		}

		public static Action<A, B, C> FixLastParam<A, B, C, D>(this Action<A, B, C, D> f, D d)
		{
			return (a, b, c) => f(a, b, c, d);
		}


		#endregion

		

	}
}
