using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;

#pragma warning disable 649

namespace FlashSpaceInvaders.ActionScript
{
	/// <summary>
	/// This class should define all links and references to 
	/// external and embeded assets.
	/// </summary>
	[Script, EmbedFields(Path, "mp3")]
	public static partial class Sounds
	{
		/// <summary>
		/// The files from solution folder 'web/assets/FlashSpaceInvaders.Assets.Sounds' 
		/// that are marked as 'Embedded Resource' will be extracted by jsc
		/// to this path. The value only reflects the real folder.
		/// </summary>
		public const string Path = "/assets/FlashSpaceInvaders.Assets.Sounds";

		public static readonly Class miu;
		public static readonly Class baseexplode;
	}
}

#pragma warning restore 649
