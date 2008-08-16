using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Media
{
	[Script(Implements = typeof(global::System.Windows.Media.SolidColorBrush))]
	internal class __SolidColorBrush : __Brush
	{
		public Color Color { get; set; }


		public static implicit operator __SolidColorBrush(SolidColorBrush e)
		{
			return (__SolidColorBrush)(object)e;
		}

		public static implicit operator SolidColorBrush(__SolidColorBrush e)
		{
			return (SolidColorBrush)(object)e;
		}
	}
}
