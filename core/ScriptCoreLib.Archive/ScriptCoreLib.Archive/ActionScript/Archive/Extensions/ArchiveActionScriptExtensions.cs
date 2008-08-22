using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Shared.Archive;
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
			var a = m.ToArray();
			var c = a.Length;
			var n = new Bitmap[c];

			for (int i = 0; i < a.Length; i++)
			{
				var k = i;

				a[k].ToByteArray().LoadBytes<Bitmap>(
					u =>
					{
						n[k] = u;

						c--;

						if (c == 0)
							h(n);
					}
				);
			}
		}

		public static void ToBitmapArray(this ZIPFile zip, Action<Bitmap[]> handler)
		{
			zip.Items.Select(k => k.Data).ToImages(handler);
		}

		public static ZIPFile ToZIPFile(this Class c)
		{
			return new BinaryReader(c.ToByteArrayAsset().ToMemoryStream());
		}
	}
}
