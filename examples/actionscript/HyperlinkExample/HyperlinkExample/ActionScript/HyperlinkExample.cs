using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.Extensions;
using System.Collections.Generic;
using System;
using System.IO;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.flash.net;

namespace HyperlinkExample.ActionScript
{
	/// <summary>
	/// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
	/// </summary>
	[Script, ScriptApplicationEntryPoint]
	public class HyperlinkExample : Sprite
	{

		[Script]
		class Assets
		{
			public static readonly Assets Default = new Assets();

			public Class this[string e]
			{
				[EmbedByFileName]
				get
				{
					throw new NotImplementedException();
				}
			}
		}


		/// <summary>
		/// Default constructor
		/// </summary>
		public HyperlinkExample()
		{

	
			var s = new Sprite().AttachTo(this);
			var img = Assets.Default["assets/HyperlinkExample/plus_google.gif"].ToBitmapAsset().AttachTo(s);
			s.useHandCursor = true;

			s.click +=
				delegate
				{
					new URLRequest("http://zproxy.wordpress.com").NavigateTo("_blank");
				};

		}

	}
}