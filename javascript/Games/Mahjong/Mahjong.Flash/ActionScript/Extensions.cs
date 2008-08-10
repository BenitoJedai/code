using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.mx.core;
using Mahjong.Shared;
using Mahjong.ActionScript;

namespace Mahjong.ActionScript
{
	/// <summary>
	/// This class defines the extension methods for this project
	/// </summary>
	[Script]
	public static class Extensions
	{
		public static BitmapAsset ToImage(this RankAsset e)
		{
			var source = e.Source + "/" + e.Suit + "/" + e.Rank + ".png";
			var n = Images.ByFileName(source);

			if (n == null)
				throw new Exception(source);

			return n;
		}
	}
}
