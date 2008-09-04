using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.mx.core;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;

namespace Mahjong.ActionScript
{
	[Script]
	public class Assets
	{

		public static readonly Assets Default = new Assets();

		public void PlaySound(string SoundName)
		{
			var AssetName = this.FileNames.FirstOrDefault(k => k.EndsWith(SoundName + ".mp3"));

			if (AssetName != null)
				this[AssetName].ToSoundAsset().play();
		}

		public string[] FileNames
		{
			[EmbedGetFileNames]
			get
			{
				throw new NotImplementedException();
			}
		}

		public Class this[string e]
		{
			[EmbedByFileName]
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}
