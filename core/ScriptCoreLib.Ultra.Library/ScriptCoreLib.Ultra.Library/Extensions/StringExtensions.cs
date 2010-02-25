using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra.Library.Extensions
{
	public static class StringExtensions
	{
		public static string SkipUntilIfAny(this string e, string u)
		{
			var i = e.IndexOf(u);

			if (i < 0)
				return e;

			return e.Substring(i + u.Length);
		}

		public static string TakeUntilIfAny(this string e, string u)
		{
			var i = e.IndexOf(u);

			if (i < 0)
				return e;

			return e.Substring(0, i);
		}

		public static string ToHexString(this byte[] e)
		{
			var w = new StringBuilder();

			foreach (var v in e)
			{
				w.Append(v.ToHexString());
			}

			return w.ToString();
		}

		public static string ToHexString(this byte e)
		{
			const string u = "0123456789abcdef";

			return u.Substring((e >> 4) & 0xF, 1) + u.Substring((e >> 0) & 0xF, 1);
		}
	}
}
