using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM;
using System.Windows.Input;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Input
{
	[Script(Implements = typeof(global::System.Windows.Input.KeyEventArgs))]
	internal class __KeyEventArgs : __KeyboardEventArgs
	{
		public Key Key { get; set; }

		public static void InternalInvoke(KeyEventHandler f, object sender, IEvent e)
		{
			var a = new __KeyEventArgs
			{
				Key = global::System.Windows.Input.KeyInterop.KeyFromVirtualKey(e.KeyCode),
				Handled = false
			};

			f(sender, a);

			if (a.Handled)
				e.PreventDefault();
		}

		public static implicit operator KeyEventArgs(__KeyEventArgs e)
		{
			return (KeyEventArgs)(object)e;
		}
	}
}
