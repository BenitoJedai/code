using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Shared.Archive;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using System.IO;

namespace ScriptCoreLib.ActionScript.Archive.Extensions
{
	[Script]
	public static class ArchiveActionScriptExtensions
	{


		public static void ToImages(this IEnumerable<MemoryStream> m, Action<Bitmap[]> h)
		{
			m.ToImages(null, h);
		}

		public static void ToImages(this IEnumerable<MemoryStream> m, Action<int, int> progress, Action<Bitmap[]> h)
		{
			var a = m.ToArray();

			var n = new Bitmap[a.Length];

			var Next = default(Action<int>);

			Next =
				k => a[k].ToByteArray().LoadBytes<Bitmap>(
					u =>
					{
						n[k] = u;

						if (progress != null)
							progress(k, a.Length);

						if (k == 0)
							h(n);
						else
							Next(k - 1);
					}
				);

			Next(a.Length - 1);
		}


		public static void ToBitmapArray(this ZIPFile zip, Action<int, int> progress, Bitmap[] cache, Action<Bitmap[]> h)
		{
			if (cache == null)
				zip.ToBitmapArray(progress, h);
			else
				h(cache);
		}

		public static void ToBitmapArray(this ZIPFile zip, Bitmap[] cache, Action<Bitmap[]> h)
		{
			if (cache == null)
				zip.ToBitmapArray(h);
			else
				h(cache);
		}

		public static void ToBitmapArray(this ZIPFile zip, Action<int, int> progress, Action<Bitmap[]> handler)
		{
			zip.Items.Select(k => k.Data).ToImages(progress, handler);
		}

		public static void ToBitmapArray(this ZIPFile zip, Action<Bitmap[]> handler)
		{
			zip.Items.Select(k => k.Data).ToImages(handler);
		}


		public static void ToBitmapDictionary(this ZIPFile zip, Action<Dictionary<string, Bitmap>> handler)
		{
			var Keys = zip.Items.Select(k => k.FileName).ToArray();

			zip.Items.Select(k => k.Data).ToImages(
				e =>
				{
					var n = new Dictionary<string, Bitmap>();

					e.ForEach((v, i) => n.Add(Keys[i], v));


					handler(n);
				}
			);
		}

		public static ZIPFile ToZIPFile(this Class c)
		{
			return new BinaryReader(c.ToByteArrayAsset().ToMemoryStream());
		}
	}
}
