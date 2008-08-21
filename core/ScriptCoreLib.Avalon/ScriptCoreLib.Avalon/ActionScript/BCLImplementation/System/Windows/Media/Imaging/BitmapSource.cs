using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Media.Imaging
{
	[Script(Implements = typeof(global::System.Windows.Media.Imaging.BitmapSource))]
	internal class __BitmapSource : __ImageSource
	{
		public static implicit operator __BitmapSource(BitmapSource e)
		{
			return (__BitmapSource)(object)e;
		}

		public static implicit operator BitmapSource(__BitmapSource e)
		{
			return (BitmapSource)(object)e;
		}
	}

}
