using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.external;

namespace ScriptCoreLib.ActionScript.Extensions
{
	[Script]
	public static class ExternalExtensions
	{
		public static void ExternalAtDelayAndAlertErrors(this int e, string f)
		{
			e.ExternalAtDelay("try { " + f + " } catch (_ex) { alert(_ex.message + ' : ' + _ex); }");
		}

		public static void ExternalAtDelay(this int e, string f)
		{
			"setTimeout".External(f, e);
		}

		public static object External(this string f)
		{
			return ExternalInterface.call(f);
		}

		public static object External(this string f, object a0)
		{
			return ExternalInterface.call(f, a0);
		}

		public static object External(this string f, object a0, object a1)
		{
			return ExternalInterface.call(f, a0, a1);
		}

		public static object External(this string f, object a0, object a1, object a2)
		{
			return ExternalInterface.call(f, a0, a1, a2);
		}

		public static object External(this string f, object a0, object a1, object a2, object a3)
		{
			return ExternalInterface.call(f, a0, a1, a2, a3);
		}


		public static void External<T, R>(this string f, Converter<T, R> h)
		{
			ExternalInterface.addCallback(f, h.ToFunction());
		}

		public static void External(this string f, Action h)
		{
			ExternalInterface.addCallback(f, h.ToFunction());
		}
	}
}
