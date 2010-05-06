using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Extensions
{
	public static class UltraLambdaExtensions
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
	}
}
