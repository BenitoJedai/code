using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript.DOM.HTML;
using Mahjong.Shared;

namespace Mahjong.js
{
	[Script]
	public static class Extensions
	{
		public static IHTMLImage ToImage(this RankAsset e)
		{
			return e.Source + "/" + e.Suit + "/" + e.Rank + ".png";
		}
	}
}
