using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.mx.core;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.Shared;

namespace Mahjong.ActionScript
{
	partial class __Assets
	{
		public static IEnumerable<Converter<string, Class>> ReferencedKnownEmbeddedResources()
		{
			return new List<Converter<string, Class>>
			{
				{ e => global::Mahjong.ActionScript.__Assets.Default[e] },
				{ e => global::ScriptCoreLib.ActionScript.Avalon.TiledImageButton.Assets.Default[e] },
				{ e => global::ScriptCoreLib.ActionScript.Avalon.Cursors.EmbeddedAssets.Default[e] },
			};
		}
	}
}
