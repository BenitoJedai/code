using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ScriptCoreLib.CSharp.Extensions
{
	[Script]
	public static class AvalonExtensions
	{
		public static ImageSource ToSource(this EmbeddedResourcesExtensions.ManifestResourceEntry fileStream)
		{
			var ext = fileStream.File.ToLower();

			if (ext.EndsWith(".png"))
			{
				var bitmapDecoder = new PngBitmapDecoder(fileStream.Stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.None);
				return bitmapDecoder.Frames[0];
			}

			if (ext.EndsWith(".gif"))
			{
				var bitmapDecoder = new GifBitmapDecoder(fileStream.Stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.None);
				return bitmapDecoder.Frames[0];
			}

			throw new NotSupportedException(ext);

		}

		public static ImageSource ToSource(this string e)
		{
			return e.ToManifestResourceStream().ToSource();
		}
	}
}
