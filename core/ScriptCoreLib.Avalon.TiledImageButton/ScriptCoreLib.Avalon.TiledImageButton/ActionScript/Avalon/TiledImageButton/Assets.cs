using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.Extensions;

namespace ScriptCoreLib.ActionScript.Avalon.TiledImageButton
{
	[Script]
	public class Assets
	{
		public const string Path = "assets/ScriptCoreLib.Avalon.TiledImageButton";

		public static readonly Assets Default = new Assets();

		public Class this[string e]
		{
			[EmbedByFileName]
			get
			{
				throw new NotImplementedException();
			}
		}

		//static Assets()
		//{
		//    // add resources to be found by ImageSource
		//    KnownEmbeddedResources.Default.Handlers.Add(
		//        e => Assets.Default[e]
		//    );
		//}
	}
}
