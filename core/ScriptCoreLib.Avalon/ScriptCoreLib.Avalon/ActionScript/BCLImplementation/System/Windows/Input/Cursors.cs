using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Input
{
	[Script(Implements = typeof(global::System.Windows.Input.Cursors))]
	internal static class __Cursors 
	{
		public static Cursor Arrow { get; set; }
		public static Cursor None { get; set; }

		static __Cursors()
		{
			Arrow = new __Cursor();
			None = new __Cursor();
		}
	}
}
