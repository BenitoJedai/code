using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Controls;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Media
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
				
				i_.InternalSetSmoothing(bitmapScalingMode == BitmapScalingMode.Linear);


				

			}
		}
	}
}
