using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.mx.core;
using ScriptCoreLib.Shared;

[assembly: ScriptResources(FlashTreasureHunt.ActionScript.Assets.SoundPath)]

namespace FlashTreasureHunt.ActionScript
{
	
	partial class Assets
	{
		partial class SoundFiles
		{
			public SoundAsset revealed
			{
				get
				{
					return this["revealed"];
				}
			}

		
			public SoundAsset teleport
			{
				get
				{
					return this["teleport"];
				}
			}


			public SoundAsset treasure
			{
				get
				{
					return this["treasure"];
				}
			}

			public SoundAsset ammo
			{
				get
				{
					return this["ammo"];
				}
			}

			public SoundAsset gunshot
			{
				get
				{
					return this["gunshot"];
				}
			}

			public SoundAsset yeah
			{
				get
				{
					return this["yeah"];
				}
			}


			public SoundAsset hit
			{
				get
				{
					return this["hit"];
				}
			}


			public SoundAsset death
			{
				get
				{
					return this["death"];
				}
			}

			public SoundAsset gutentag
			{
				get
				{
					return this["gutentag"];
				}
			}
		}

	}
}
