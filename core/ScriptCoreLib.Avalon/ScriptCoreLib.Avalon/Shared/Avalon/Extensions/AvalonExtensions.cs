using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using ScriptCoreLib.CSharp.Extensions;
using System.IO;
using System.Windows;

namespace ScriptCoreLib.Shared.Avalon.Extensions
{


	// reimplement for target languages
	public static class AvalonExtensions
	{

		public static void ToStringAsset(this string e, Action<string> h)
		{
			var s = e.ToManifestResourceStream();

			s.Stream.Position = 0;

			var b = new byte[s.Stream.Length];
			s.Stream.Read(b, 0, b.Length);

			var v = Encoding.UTF8.GetString(b);

			h(v);
		}

		internal static ImageSource ToSource(this EmbeddedResourcesExtensions.ManifestResourceEntry fileStream)
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

			if (ext.EndsWith(".jpg"))
			{
				var bitmapDecoder = new JpegBitmapDecoder(fileStream.Stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.None);
				return bitmapDecoder.Frames[0];
			}


			throw new NotSupportedException(ext);

		}



		public static ImageSource ToSource(this string e)
		{
			// EmbeddedResource?

			return e.ToManifestResourceStream().ToSource();
		}

		public static BitmapSource ToSource(this Stream e)
		{

			return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
				new System.Drawing.Bitmap(e).GetHbitmap(), IntPtr.Zero, Int32Rect.Empty,
				System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions()
			);
		}
	}
}
