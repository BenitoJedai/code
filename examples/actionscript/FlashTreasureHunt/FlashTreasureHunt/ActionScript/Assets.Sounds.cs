using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.mx.core;

////[assembly: ScriptResources(FlashTreasureHunt.ActionScript.Assets.Path)]

namespace FlashTreasureHunt.ActionScript
{
	
	partial class Assets
	{
		public SoundAsset music
		{
			get
			{
				return this[Path + "/WONDERIN.mp3"].ToSoundAsset();
			}
		}

		public SoundAsset treasure
		{
			get
			{
				return this[Path + "/treasure.mp3"].ToSoundAsset();
			}
		}
	}
}
