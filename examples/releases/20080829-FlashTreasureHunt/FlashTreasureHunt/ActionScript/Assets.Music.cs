using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.mx.core;

[assembly: ScriptResources(FlashTreasureHunt.ActionScript.Assets.SoundPath)]

namespace FlashTreasureHunt.ActionScript
{
	
	partial class Assets
	{
		partial class MusicFiles
		{
			

			public SoundAsset music_endlevel
			{
				get
				{
					return this["ENDLEVEL"];
				}
			}

			public SoundAsset music
			{
				get
				{
					return this["WONDERIN"];
				}
			}

			public SoundAsset funkyou
			{
				get
				{
					return this["FUNKYOU"];
				}
			}
		}

	}
}
