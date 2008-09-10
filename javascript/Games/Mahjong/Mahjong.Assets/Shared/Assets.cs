using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.mx.core;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.Shared;
using Mahjong.Shared;

[assembly: ScriptResources("assets/Mahjong")]
[assembly: ScriptResources("assets/Mahjong.Assets")]
[assembly: ScriptResources("assets/Mahjong.Layouts")]
[assembly: ScriptResources("assets/Mahjong.Sounds")]
[assembly: ScriptResources(CursorAssets.Path)]

namespace Mahjong.Shared
{
	public class Assets
	{

		public static readonly Assets Default = new Assets();

		public string[] FileNames
		{
			[EmbedGetFileNames]
			get
			{
				return ScriptCoreLib.CSharp.Extensions.EmbeddedResourcesExtensions.GetEmbeddedResources(null, this.GetType().Assembly);
			}
		}

	

	}

	[Script]
	public class CursorAssets
	{
		public const string Path = "assets/Mahjong.Cursors";

		public string this[string prefix, string color]
		{
			get
			{
				var p = Path + "/" + prefix;

				if (!string.IsNullOrEmpty(color))
					p += "_" + color;

				return p + ".png";
			}
		}

		public string this[string color]
		{
			get
			{
				return this["aero_arrow", color];
			}
		}

		public static readonly CursorAssets Cursors = new CursorAssets();
	}
}
