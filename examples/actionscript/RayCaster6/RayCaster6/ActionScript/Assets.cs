using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;

[assembly: ScriptResources(RayCaster6.ActionScript.Assets.SoundFiles.Path)]

namespace RayCaster6.ActionScript
{
	/// <summary>
	/// This class should define all links and references to 
	/// external and embeded assets.
	/// </summary>
	[Script]
	internal static partial class Assets
	{
		/// <summary>
		/// The files from solution folder 'web/assets/RayCaster4' 
		/// that are marked as 'Embedded Resource' will be extracted by jsc
		/// to this path. The value only reflects the real folder.
		/// </summary>


		[Script]
		public static class ZipFiles
		{
			public const string Path = "/assets/RayCaster6";


			[Embed(Path + "/dude5.zip")]
			public static Class MyZipFile;

			[Embed(Path + "/stuff.zip")]
			public static Class MyStuff;

			[Embed(Path + "/sprites.zip")]
			public static Class MySprites;

			[Embed(Path + "/gold.zip")]
			public static Class MyGold;

			[Embed(Path + "/ammo.zip")]
			public static Class ammo;
		}


		[Script]
		public static class SoundFiles
		{
			public const string Path = "/assets/RayCaster6.Sounds";

			[Embed(Path + "/treasure.mp3")]
			public static Class treasure;

			[Embed(Path + "/ammo.mp3")]
			public static Class ammo;

			[Embed(Path + "/teleport.mp3")]
			public static Class teleport;
		}
	}
}
