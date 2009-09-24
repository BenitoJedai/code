using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Media;
using System.Windows;
using ScriptCoreLib.JavaScript.BCLImplementation.System;
using ScriptCoreLib.Shared.Avalon;
using ScriptCoreLib.JavaScript.DOM;

namespace ScriptCoreLib.JavaScript.UCLImplementation
{
	[Script(Implements = typeof(global::ScriptCoreLib.Shared.Avalon.Extensions.AvalonExtensions))]
	internal static class __AvalonExtensions
	{
		public static AvalonSoundChannel ToSound(this string asset)
		{
			return new AvalonSoundChannel();
		}

		public static AvalonSoundChannel PlaySound(this string asset)
		{
			return new AvalonSoundChannel();
		}

		public static void NavigateTo(this Uri e, DependencyObject context)
		{
			//var _e = (__Uri)(object)e;

			var w = Native.Window.open(e.OriginalString, "_blank");

		}

		public static void ToStringAsset(this string e, Action<string> h)
		{
			new IXMLHttpRequest(
				ScriptCoreLib.Shared.HTTPMethodEnum.GET,
				e,
				r =>
				{
					h(r.responseText);
				}
			);
		}

		public static ImageSource ToSource(this string e)
		{
			// the c# version must do some internal work to figure
			// out the right stream name
			// in actionscript we are using [Embed]

			return new __ImageSource { InternalManifestResourceAlias = e };
		}
	}
}
