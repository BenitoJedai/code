using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Media
{
	[Script(Implements = typeof(global::System.Windows.Media.Brushes))]
	internal class __Brushes
	{

		public static SolidColorBrush Transparent { get { return new __SolidColorBrush { Color = Colors.Transparent }; } }
		public static SolidColorBrush Red { get { return new __SolidColorBrush { Color = Colors.Red }; } }
		public static SolidColorBrush GreenYellow { get { return new __SolidColorBrush { Color = Colors.GreenYellow }; } }
	}
}
