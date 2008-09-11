using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Lambda;
using System.IO;

namespace Mahjong.Code
{
	[Script]
	public static class MyExtensions
	{


		public static IEnumerable<T> Multiply<T>(this IEnumerable<T> e, int count)
		{
			if (count < 1)
				throw new NotImplementedException();

			var c = e;

			for (int i = 2; i <= count; i++)
			{
				c = c.Concat(e);
			}

			return c;
		}

		public static List<T> MultiplyElements<T>(this IEnumerable<T> e, int count)
		{
			var a = new List<T>();

			foreach (var v in e.AsEnumerable())
			{
				for (int i = 0; i < count; i++)
				{
					a.Add(v);
				}
			}

			return a;
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
	}
}
