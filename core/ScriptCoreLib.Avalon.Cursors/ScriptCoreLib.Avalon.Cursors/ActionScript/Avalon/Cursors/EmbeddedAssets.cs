using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.Avalon.Cursors
{
	[Script]
	public class EmbeddedAssets
	{
		// this class embedds the resources and should be 
		// passed to KnownEmbeddedResources

		public static readonly EmbeddedAssets Default = new EmbeddedAssets();

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
