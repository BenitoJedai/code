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
			var lookup = new Dictionary<string, string>
                {
                    {".zip", OctetStream},
                    {".txt", OctetStream},
                    {".xml", OctetStream},
                    {".ttf", "application/x-font"}
                };

			foreach (var p in lookup)
				if (FileName.EndsWith(p.Key))
					return p.Value;

			return null;
		}
	}
}
