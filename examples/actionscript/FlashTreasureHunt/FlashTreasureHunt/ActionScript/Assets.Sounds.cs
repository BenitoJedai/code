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
		public const string SoundPath = Path + "/Sounds";

		public SoundAsset music_endlevel
		{
			get
			{
				return this[SoundPath + "/ENDLEVEL.mp3"].ToSoundAsset();
			}
		}

		public SoundAsset music
		{
			get
			{
				return this[SoundPath + "/WONDERIN.mp3"].ToSoundAsset();
			}
		}

		public SoundAsset treasure
		{
			get
			{
				return this[SoundPath + "/treasure.mp3"].ToSoundAsset();
			}
		}

		public SoundAsset ammo
		{
			get
			{
				return this[SoundPath + "/ammo.mp3"].ToSoundAsset();
			}
		}

		public SoundAsset gunshot
		{
			get
			{
				return this[SoundPath + "/gunshot.mp3"];
			}
		}
		
		public SoundAsset yeah
		{
			get
			{
				return this[SoundPath + "/yeah.mp3"];
			}
		}

		public SoundAsset gutentag
		{
			get
			{
				return this[SoundPath + "/gutentag.mp3"].ToSoundAsset();
			}
		}
	}
}
