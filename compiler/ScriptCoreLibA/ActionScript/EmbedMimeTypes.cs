using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript
{
	public static class EmbedMimeTypes
	{
		public const string OctetStream = "application/octet-stream";

		public static string Resolve(string FileName)
		{
			// http://blog.wezside.co.za/2008/04/everything-embe.html
			// http://livedocs.adobe.com/flex/3/html/help.html?content=embed_3.html

			// You can use the [Embed] metadata tag to import JPEG, GIF, PNG, SVG, SWF, TTF, and MP3 files.

			//Flex supports the following MIME types.

			//    * application/octet-stream
			//    * application/x-font
			//    * application/x-font-truetype
			//    * application/x-shockwave-flash
			//    * audio/mpeg
			//    * image/gif
			//    * image/jpeg
			//    * image/png
			//    * image/svg
			//    * image/svg-xml


			var lookup = new Dictionary<string, string>
                {
                    {".zip", OctetStream},
                    {".txt", OctetStream},
                    {".xml", OctetStream},
                    {".lay", OctetStream},
                    {".ttf", "application/x-font"}
                };

			foreach (var p in lookup)
				if (FileName.EndsWith(p.Key))
					return p.Value;

			return null;
		}
	}
}
