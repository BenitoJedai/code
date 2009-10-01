using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using ScriptCoreLib.CSharp.Extensions;
using System.IO;
using System.Windows;
using System.Windows.Navigation;
using System.Net;
using System.Diagnostics;

namespace ScriptCoreLib.Shared.Avalon.Extensions
{


	// reimplement for target languages
	public static class AvalonExtensions
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
			if (context != null)
			{
				var s = NavigationService.GetNavigationService(context);

				if (s != null)
				{
					// http://social.msdn.microsoft.com/Forums/en-US/wpf/thread/6810d6f6-ea51-4efd-a9d4-f75319691a3b/

					s.Navigate(e);

					return;
				}
			}

			NavigateToViaDefaultBrowser(e);
		}

		private static void NavigateToViaDefaultBrowser(Uri e)
		{
			// http://forums.microsoft.com/MSDN/ShowPost.aspx?PostID=910999&SiteID=1
			// http://msdn.microsoft.com/en-us/library/system.windows.documents.hyperlink.navigateuri.aspx
			// http://msdn.microsoft.com/en-us/library/ms750478.aspx
			global::System.Diagnostics.Process.Start(e.ToString());
		}

		public static void ToStringAsset(this string e, Action<string> h)
		{
			var context = new StackFrame(1).GetMethod().DeclaringType.Assembly;

			var s = e.ToManifestResourceStream(context);

			s.Stream.Position = 0;

			var b = new byte[s.Stream.Length];
			s.Stream.Read(b, 0, b.Length);

			var v = Encoding.UTF8.GetString(b);

			h(v);
		}

		public static void ToMemoryStreamAsset(this string e, Action<MemoryStream> h)
		{
			var context = new StackFrame(1).GetMethod().DeclaringType.Assembly;
			var s = e.ToManifestResourceStream(context);

			s.Stream.Position = 0;

			var b = new byte[s.Stream.Length];
			s.Stream.Read(b, 0, b.Length);

			h(new MemoryStream(b));
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
			var context = new StackFrame(1).GetMethod().DeclaringType.Assembly;
			// EmbeddedResource?

			return e.ToManifestResourceStream(context).ToSource();
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
