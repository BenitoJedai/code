using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Shared.BCLImplementation.System;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Windows
{
	[Script(Implements = typeof(global::System.Windows.RoutedEventArgs))]
	public class __RoutedEventArgs : __EventArgs
	{
		public bool Handled { get; set; }
	}
}
