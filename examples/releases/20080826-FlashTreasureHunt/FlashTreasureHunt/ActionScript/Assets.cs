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
	/// <summary>
	/// This class should define all links and references to 
	/// external and embeded assets.
	/// </summary>
	[Script]
	public partial class Assets
	{
		/// <summary>
		/// The files from solution folder 'web/assets/FlashTreasureHunt' 
		/// that are marked as 'Embedded Resource' will be extracted by jsc
		/// to this path. The value only reflects the real folder.
		/// </summary>
		public const string Path = "assets/FlashTreasureHunt";

		public static readonly Assets Default = new Assets();

		public Class this[string e]
		{
			[EmbedByFileName]
			get
			{
				throw new NotImplementedException();
			}
		}


		public const string SoundPath = Path + "/Sounds";

		[Script]
		public partial class SoundFiles
		{
			Assets a;

			public SoundFiles(Assets a)
			{
				this.a = a;
			}

			public SoundAsset this[string e]
			{
				get
				{
					var f = SoundPath + "/" + e + ".mp3";
					var r = a[f];

					if (r == null)
						throw new Exception(f);

					return r;
				}
			}
		}


		public Assets()
		{
			Sounds = new SoundFiles(this);
		}

		public readonly SoundFiles Sounds;


	}
}
