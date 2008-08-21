using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Media;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Media.Imaging;

namespace ScriptCoreLib.ActionScript.UCLImplementation
{
	[Script(Implements = typeof(global::ScriptCoreLib.Shared.Avalon.Extensions.AvalonExtensions))]
	internal static class __AvalonExtensions
	{
		public static ImageSource ToSource(this string e)
		{
			// the c# version must do some internal work to figure
			// out the right stream name
			// in actionscript we are using [Embed]

			return new __ImageSource { InternalManifestResourceAlias = e };
		}

		public static BitmapSource ToSource(this Stream e)
		{
			return new __BitmapSource { InternalStreamAlias = e };
		}
	}
}
