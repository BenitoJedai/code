using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Input
{
	[Script(Implements = typeof(global::System.Windows.Input.Keyboard))]
	internal class __Keyboard
	{
		public static ModifierKeys Modifiers { get; set; }
	}
}
