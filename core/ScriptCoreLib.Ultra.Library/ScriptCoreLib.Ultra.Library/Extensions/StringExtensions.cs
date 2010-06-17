using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Extensions
{
	public static class StringExtensions
	{
   

		public static string[] ToLines(this string e)
		{
			return e.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
		}

		public static string SkipUntilLastIfAny(this string e, string u)
		{
			var i = e.LastIndexOf(u);

			if (i < 0)
				return e;

			return e.Substring(i + u.Length);
		}


		public static string SkipUntilLastOrEmpty(this string e, string u)
		{
			var i = e.LastIndexOf(u);

			if (i < 0)
				return "";

			return e.Substring(i + u.Length);
		}

		public static string SkipUntilIfAny(this string e, string u)
		{
			var i = e.IndexOf(u);

			if (i < 0)
				return e;

			return e.Substring(i + u.Length);
		}


		public static string SkipUntilOrEmpty(this string e, string u)
		{
			var i = e.IndexOf(u);

			if (i < 0)
				return "";

			return e.Substring(i + u.Length);
		}

		public static string TakeUntilIfAny(this string e, string u)
		{
			var i = e.IndexOf(u);

			if (i < 0)
				return e;

			return e.Substring(0, i);
		}

		public static string TakeUntilOrEmpty(this string e, string u)
		{
			var i = e.IndexOf(u);

			if (i < 0)
				return "";

			return e.Substring(0, i);
		}

		public static string TakeUntilLastIfAny(this string e, string u)
		{
			var i = e.LastIndexOf(u);

			if (i < 0)
				return e;

			return e.Substring(0, i);
		}


		public static string TakeUntilLastOrEmpty(this string e, string u)
		{
			var i = e.LastIndexOf(u);

			if (i < 0)
				return "";

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

		public static void AtIndecies(this string e, string target, AtIndeciesDelegate h)
		{
			var i = e.IndexOf(target);
			var YieldIndex = -1;
			while (i >= 0)
			{
				YieldIndex++;

				h(
					new AtIndeciesArguments
					{
						e = e,
						i = i,
						target = target,
						YieldIndex = YieldIndex,
						YieldBreak = () => i = -1
					}
				);


				if (i >= 0)
					i = e.IndexOf(target, i + target.Length);
			}
		}


	}

	public class AtIndeciesArguments
	{
		public string e;
		public string target;
		public int i;

		public int YieldIndex;

		public Action YieldBreak;
	}

	public delegate void AtIndeciesDelegate(AtIndeciesArguments a);
}
