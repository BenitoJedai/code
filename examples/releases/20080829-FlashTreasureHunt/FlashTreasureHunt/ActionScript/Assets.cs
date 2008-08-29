using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.mx.core;

[assembly: ScriptResources(FlashTreasureHunt.ActionScript.Assets.PromotionalPath)]
[assembly: ScriptResources(FlashTreasureHunt.ActionScript.Assets.SoundPath)]
[assembly: ScriptResources(FlashTreasureHunt.ActionScript.Assets.MusicPath)]

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


		public const string PromotionalPath = Path + "/Promotional";
		public const string SoundPath = Path + "/Sounds";
		public const string MusicPath = Path + "/Music";

		[Script]
		public class SoundFilesBase
		{
			Assets a;
			string p;

			public SoundFilesBase(Assets a, string p)
			{
				this.a = a;
				this.p = p;
			}

			public SoundAsset this[string e]
			{
				get
				{
					var f = p + "/" + e + ".mp3";
					var r = a[f];

					if (r == null)
						throw new Exception(f);

					return r;
				}
			}
		}

		[Script]
		public partial class SoundFiles : SoundFilesBase
		{

			public SoundFiles(Assets a) : base(a, SoundPath)
			{
			}
			
		}

		[Script]
		public partial class MusicFiles : SoundFilesBase
		{

			public MusicFiles(Assets a) : base(a, MusicPath)
			{
			}

		}

		public Assets()
		{
			Sounds = new SoundFiles(this);
			Music = new MusicFiles(this);
		}

		public readonly SoundFiles Sounds;
		public readonly MusicFiles Music;


	}
}
