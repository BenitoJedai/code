using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;

////[assembly: ScriptResources(RayCaster4.ActionScript.Assets.Path)]

namespace RayCaster6.ActionScript
{
	/// <summary>
	/// This class should define all links and references to 
	/// external and embeded assets.
	/// </summary>
	[Script]
	internal static class Assets
	{
		/// <summary>
		/// The files from solution folder 'web/assets/RayCaster4' 
		/// that are marked as 'Embedded Resource' will be extracted by jsc
		/// to this path. The value only reflects the real folder.
		/// </summary>
		public const string Path = "/assets/RayCaster6";


		[Script]
		public static class ZipFiles
		{
			[Embed(Path + "/dude5.zip")]
			public static Class MyZipFile;

			[Embed(Path + "/stuff.zip")]
			public static Class MyStuff;

			[Embed(Path + "/sprites.zip")]
			public static Class MySprites;
		}


	}
}
