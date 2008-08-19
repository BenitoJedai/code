using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.utils;

namespace FlashTreasureHunt.ActionScript
{
	/// <summary>
	/// This class defines the extension methods for this project
	/// </summary>
	[Script]
	internal static class Extensions
	{
		public static Timer AtInterval(this int e, Action<Timer> a)
		{
			var t = new Timer(e);

			t.timer += delegate { a(t); };

			t.start();

			return t;
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
