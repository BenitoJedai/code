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
			var n = Assets.Default[e.ResourceAlias];

			if (n == null)
				throw new NotSupportedException();

			return n;
		}
	}
}
