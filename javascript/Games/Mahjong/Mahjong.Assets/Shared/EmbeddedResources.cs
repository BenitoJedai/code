using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;


[assembly: ScriptResources(Mahjong.Shared.EmbeddedResources.Path)]
[assembly: ScriptResources(Mahjong.Shared.EmbeddedResources.AssetsPath)]
[assembly: ScriptResources(Mahjong.Shared.EmbeddedResources.SoundsPath)]

namespace Mahjong.Shared
{
	[Script]
	public static class EmbeddedResources
	{
		public const string Path = "assets/Mahjong";
		public const string AssetsPath = Path + ".Assets";
		public const string SoundsPath = Path + ".Sounds";

	}
}
