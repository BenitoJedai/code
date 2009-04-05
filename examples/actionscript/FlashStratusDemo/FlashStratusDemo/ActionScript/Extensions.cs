using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.utils;

namespace FlashStratusDemo.ActionScript
{
	/// <summary>
	/// This class defines the extension methods for this project
	/// </summary>
	[Script]
	internal static class Extensions
	{

		public static Timer AtInterval(this int e, Action h)
		{
			var t = new Timer(e);
			t.timer += delegate { h(); };
			t.start();
			return t;
		}

		public static Timer AtDelay(this int e, Action h)
		{
			var t = new Timer(e, 1);
			t.timer += delegate { h(); };
			t.start();
			return t;

		}

	}
}
