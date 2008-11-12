using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Media
{
	[Script(Implements = typeof(global::System.Windows.Media.Brushes))]
	internal class __Brushes
	{

		public static SolidColorBrush Transparent { get { return new __SolidColorBrush { Color = Colors.Transparent }; } }
		public static SolidColorBrush Red { get { return new __SolidColorBrush { Color = Colors.Red }; } }
		public static SolidColorBrush White { get { return new __SolidColorBrush { Color = Colors.White }; } }
		public static SolidColorBrush Black { get { return new __SolidColorBrush { Color = Colors.Black }; } }
		public static SolidColorBrush GreenYellow { get { return new __SolidColorBrush { Color = Colors.GreenYellow }; } }
		public static SolidColorBrush Blue { get { return new __SolidColorBrush { Color = Colors.Blue }; } }
		public static SolidColorBrush Gray { get { return new __SolidColorBrush { Color = Colors.Gray }; } }
		public static SolidColorBrush Green { get { return new __SolidColorBrush { Color = Colors.Green }; } }
		public static SolidColorBrush Magenta { get { return new __SolidColorBrush { Color = Colors.Magenta }; } }
		public static SolidColorBrush Cyan { get { return new __SolidColorBrush { Color = Colors.Cyan }; } }
		public static SolidColorBrush Yellow { get { return new __SolidColorBrush { Color = Colors.Yellow }; } }
		public static SolidColorBrush Brown { get { return new __SolidColorBrush { Color = Colors.Brown }; } }
		public static SolidColorBrush Pink { get { return new __SolidColorBrush { Color = Colors.Pink }; } }
		public static SolidColorBrush DarkCyan { get { return new __SolidColorBrush { Color = Colors.DarkCyan }; } }
	}
}
