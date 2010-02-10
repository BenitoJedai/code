using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using java.lang.reflect;
using java.applet;
using ScriptCoreLib.ActionScript.flash.external;
using ScriptCoreLib.ActionScript.Extensions;

namespace jsc.meta.Library.Templates
{
	internal static class InternalActionScriptToJavaScriptBridge
	{
		//public delegate bool BoolFunc();

		//public static void ExternalInterface_isActive()
		//{
		//    ExternalInterface.addCallback("isActive", new BoolFunc(() => true).ToFunction());
		//}

		public static object ExternalInterface_Invoke(object context, object[] __arguments)
		{
			var a = __arguments;
			var x = a.Length;

			// should we use Function.apply?

			if (x == 1)
				return ExternalInterface.call((string)a[0]);

			if (x == 2)
				return ExternalInterface.call((string)a[0], a[1]);

			if (x == 3)
				return ExternalInterface.call((string)a[0], a[1], a[2]);

			if (x == 4)
				return ExternalInterface.call((string)a[0], a[1], a[2], a[3]);

			if (x == 5)
				return ExternalInterface.call((string)a[0], a[1], a[2], a[3], a[4]);

			if (x == 6)
				return ExternalInterface.call((string)a[0], a[1], a[2], a[3], a[4], a[5]);

			if (x == 7)
				return ExternalInterface.call((string)a[0], a[1], a[2], a[3], a[4], a[5], a[6]);


			throw new NotSupportedException();
		}
	}
}
