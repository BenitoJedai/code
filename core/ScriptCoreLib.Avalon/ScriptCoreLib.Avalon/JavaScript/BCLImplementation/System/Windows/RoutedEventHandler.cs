using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows
{
	[Script(Implements = typeof(global::System.Windows.RoutedEventHandler))]
	public delegate void __RoutedEventHandler(object sender, RoutedEventArgs e);
}
