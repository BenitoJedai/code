using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ScriptCoreLib.ActionScript.BCLImplementation.System;
using ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Media;
using ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Media.Imaging;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.media;
using ScriptCoreLib.ActionScript.flash.net;
using ScriptCoreLib.Shared.Avalon;

namespace ScriptCoreLib.ActionScript.UCLImplementation
{
	[Script(Implements = typeof(global::ScriptCoreLib.Shared.Avalon.Extensions.AvalonExtensions))]
	internal static class __AvalonExtensions
	{
		public static AvalonSoundChannel ToSound(this string asset)
		{
			var x = KnownEmbeddedResources.Default[asset].ToSoundAsset();
			double v = 1;
			var y = default(SoundChannel);
		
			var c = new AvalonSoundChannel();

			c.SetVolume =
				value =>
				{
					v = value;

					if (y != null)
						y.soundTransform = new SoundTransform(value);
				};

			c.Start =
				delegate
				{
					if (y != null)
					{
						y.stop();
						y = null;
					}

					y = x.play(0, 0, new SoundTransform(v));

					y.soundComplete +=
						delegate
						{
							c.RaisePlaybackComplete();
						};
				};

			c.Stop =
				delegate
				{
					if (y != null)
					{
						y.stop();
						y = null;
					}
				};


		

			return c;
		}

		public static AvalonSoundChannel PlaySound(this string asset)
		{
			var x = KnownEmbeddedResources.Default[asset].ToSoundAsset().play();

			var c = new AvalonSoundChannel
			{
				Stop = x.stop,
			};

			c.SetVolume = value => x.soundTransform = new SoundTransform(value);


			x.soundComplete +=
				delegate
				{
					c.RaisePlaybackComplete();
				};

			return c;
		}



		public static void NavigateTo(this Uri e, DependencyObject context)
		{
			//var _e = (__Uri)(object)e;

			global::ScriptCoreLib.ActionScript.Extensions.CommonExtensions.NavigateTo(
				new URLRequest(e.OriginalString),
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
