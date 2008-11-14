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
using ScriptCoreLib.ActionScript.flash.net;
using System.Windows;
using ScriptCoreLib.ActionScript.BCLImplementation.System;

namespace ScriptCoreLib.ActionScript.UCLImplementation
{
	[Script(Implements = typeof(global::ScriptCoreLib.Shared.Avalon.Extensions.AvalonExtensions))]
	internal static class __AvalonExtensions
	{

		public static Action PlaySound(this string asset)
		{
			var x = KnownEmbeddedResources.Default[asset].ToSoundAsset().play();

			return x.stop;
		}

		public static void NavigateTo(this Uri e, DependencyObject context)
		{
			var _e = (__Uri)(object)e;

			global::ScriptCoreLib.ActionScript.Extensions.CommonExtensions.NavigateTo(
				new URLRequest(_e.OriginalString),
				"_blank"
			);
		}

		public static void ToStringAsset(this string e, Action<string> h)
		{
			h(KnownEmbeddedResources.Default[e].ToStringAsset());
		}

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
