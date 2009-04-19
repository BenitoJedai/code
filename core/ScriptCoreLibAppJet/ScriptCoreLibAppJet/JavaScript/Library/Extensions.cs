using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLibAppJet.AppJet;

namespace ScriptCoreLibAppJet.Library
{
	[Script]
	public static class Extensions
	{
		[Script(OptimizedCode = "return e[k];")]
		public static T GetValue<T>(this Storage e, string k)
		{
			return default(T);
		}

		[Script(OptimizedCode = "return !!e[k];")]
		public static bool Contains(this Storage e, string k)
		{
			return default(bool);
		}

		[Script(OptimizedCode = "return e[k] = value;")]
		public static void SetValue<T>(this Storage e, string k, T value)
		{
		}

		public static void ToConsole(this string e)
		{
			Native.printHTML(e);
		}
	}
}
