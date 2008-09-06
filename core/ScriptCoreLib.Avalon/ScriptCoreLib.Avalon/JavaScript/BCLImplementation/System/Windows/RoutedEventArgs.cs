using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows
{
	[Script(Implements = typeof(global::System.Windows.RoutedEventArgs))]
	internal class __RoutedEventArgs : __EventArgs
	{
		public bool Handled { get; set; }
	}
}
