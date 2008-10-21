﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;

namespace NavigationButtons.Assets.ActionScript
{
	[Script]
	public class KnownAndReferencedEmbeddedAssets
	{
		public static void RegisterTo(List<Converter<string, Class>> Handlers)
		{
			KnownEmbeddedAssets.RegisterTo(Handlers);

			// assets from current assembly
			//Handlers.Add(e => ByFileName(e));

			//// assets from referenced assemblies
			//Handlers.Add(e => global::ScriptCoreLib.ActionScript.Avalon.Cursors.EmbeddedAssets.Default[e]);
			Handlers.Add(e => global::ScriptCoreLib.ActionScript.Avalon.TiledImageButton.Assets.Default[e]);

		}
	}
}
