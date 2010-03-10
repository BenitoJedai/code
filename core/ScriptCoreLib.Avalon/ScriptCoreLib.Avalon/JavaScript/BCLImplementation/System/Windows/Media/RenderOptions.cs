using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Controls;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Media
{
	[Script(Implements = typeof(global::System.Windows.Media.RenderOptions))]
	internal class __RenderOptions
	{
		public static void SetBitmapScalingMode(DependencyObject target, BitmapScalingMode bitmapScalingMode)
		{
			// http://blogs.msdn.com/wpf3d/archive/2009/06/24/what-s-new-in-graphics-for-4-0-beta-1.aspx
			// The default RenderOptions.BitmapScalingMode (Unspecified) is now Linear instead of Fant. If you still want Fant, you can re-enable it.

			var i = target as Image;

			if (i != null)
			{
				var i_ = (__Image)(object)i;
		

			}
		}
	}
}
