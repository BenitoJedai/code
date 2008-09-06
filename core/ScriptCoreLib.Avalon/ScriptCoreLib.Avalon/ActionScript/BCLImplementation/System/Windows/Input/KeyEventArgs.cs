using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.events;
using System.Windows.Input;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Input
{
	[Script(Implements = typeof(global::System.Windows.Input.KeyEventArgs))]
	internal class __KeyEventArgs : __KeyboardEventArgs
	{
		public Key Key { get; set; }

		public static void InternalInvoke(KeyEventHandler f, object sender, KeyboardEvent e)
		{
			var a = new __KeyEventArgs
			{
				Key = global::System.Windows.Input.KeyInterop.KeyFromVirtualKey((int)e.keyCode),
				Handled = false
			};

			f(sender, a);

			if (a.Handled)
				e.preventDefault();
		}

		public static implicit operator KeyEventArgs(__KeyEventArgs e)
		{
			return (KeyEventArgs)(object)e;
		}
	}
}
