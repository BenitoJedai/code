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
	internal class InternalActionScriptToJavaScriptBridge
	{
		//public delegate bool BoolFunc();

		//public static void ExternalInterface_isActive()
		//{
		//    ExternalInterface.addCallback("isActive", new BoolFunc(() => true).ToFunction());
		//}

		public static object ExternalInterface_Invoke(object context, object[] __arguments)
		{
			// need to implement!!
			return null;
		}
	}
}
