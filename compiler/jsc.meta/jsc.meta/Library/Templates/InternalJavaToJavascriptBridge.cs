using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using java.lang.reflect;
using java.applet;

namespace jsc.meta.Library.Templates
{
	internal class InternalJavaToJavaScriptBridge
	{
		public static object ExternalInterface_Invoke(object context, object[] arguments)
		{
			return null;
		}

		public static object Invoke(Applet a, string method, object[] args)
		{
			var w = new StringBuilder();

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

			return EvaluateJavaScript(a, w.ToString());
		}

		public static object EvaluateJavaScript(Applet that, string js)
		{
			object r = null;
			Method getWindow = null;
			Method eval = null;

			try
			{
				// 2047
				// http://www.rgagnon.com/javadetails/java-0172.html

				var c = global::java.lang.Class.forName("netscape.javascript.JSObject");

				foreach (Method m in c.getMethods())
				{
					if (m.getName() == "getWindow") getWindow = m;
					if (m.getName() == "eval") eval = m;
				}

				var getWindow_Arguments = new object[] { that };

				var jsWindow = getWindow.invoke(c, getWindow_Arguments);

				var eval_Arguments = new object[] { js };

				r = eval.invoke(jsWindow, eval_Arguments);

				//base.getAppletContext().showDocument(new URL("javascript:" + js));
			}
			catch (global::csharp.ThrowableException e)
			{
				throw new global::csharp.RuntimeException("Script is not supported. " + e.Message);
			}


			return r;
		}
	}
}
