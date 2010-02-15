using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using java.lang.reflect;
using java.applet;
using ScriptCoreLib.ActionScript.flash.external;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;

namespace jsc.meta.Library.Templates
{
	internal static class InternalActionScriptToJavaScriptBridge
	{
		//public delegate bool BoolFunc();

		//public static void ExternalInterface_isActive()
		//{
		//    ExternalInterface.addCallback("isActive", new BoolFunc(() => true).ToFunction());
		//}

		public static object ExternalInterface_Invoke(object context, object[] arguments)
		{
			var a = new string[arguments.Length - 1];

			System.Array.Copy(arguments, 1, a, 0, a.Length);

			return Invoke((Sprite)context, (string)arguments[0], a);
		}

		public static object Invoke(Sprite a, string method, object[] args)
		{
			if (method == null)
				throw new NotSupportedException();

			var w = new StringBuilder();

			// hey look, we are amitting javascript just as jsc does :)

			w.Append("function () { ");

			//w.Append("if (confirm('debug " + method + "?')) debugger;");

			w.Append("return ");

			w.Append(method);
			w.Append("(");
			for (int i = 0; i < args.Length; i++)
			{
				// we are only supporting strings at the moment!

				if (i > 0)
					w.Append(", ");

				w.Append("'");

				var t = ((string)args[i]).Replace("'", @"\'");

				w.Append(t);

				w.Append("'");
			}
			w.Append(")");

			w.Append("; }");

			return ExternalInterface.call(w.ToString());

			//var a = __arguments;
			//var x = a.Length;

			//// should we use Function.apply?

			//if (x == 1)
			//    return ExternalInterface.call((string)a[0]);

			//if (x == 2)
			//    return ExternalInterface.call((string)a[0], a[1]);

			//if (x == 3)
			//    return ExternalInterface.call((string)a[0], a[1], a[2]);

			//if (x == 4)
			//    return ExternalInterface.call((string)a[0], a[1], a[2], a[3]);

			//if (x == 5)
			//    return ExternalInterface.call((string)a[0], a[1], a[2], a[3], a[4]);

			//if (x == 6)
			//    return ExternalInterface.call((string)a[0], a[1], a[2], a[3], a[4], a[5]);

			//if (x == 7)
			//    return ExternalInterface.call((string)a[0], a[1], a[2], a[3], a[4], a[5], a[6]);


			//throw new NotSupportedException();
		}
	}
}
