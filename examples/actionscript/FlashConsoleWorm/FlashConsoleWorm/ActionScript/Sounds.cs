using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;

[assembly: ScriptResources(FlashConsoleWorm.ActionScript.Sounds.Path)]

namespace FlashConsoleWorm.ActionScript
{
    /// <summary>
    /// This class should define all links and references to 
    /// external and embeded assets.
    /// </summary>
    [Script]
    internal static class Sounds
    {
        /// <summary>
        /// The files from solution folder 'web/assets/FlashConsoleWorm' 
        /// that are marked as 'Embedded Resource' will be extracted by jsc
        /// to this path. The value only reflects the real folder.
        /// </summary>
        public const string Path = "/assets/FlashConsoleWorm.Sounds";

		[Embed(Path + "/sound20.mp3")]
		public static Class sound20;

		[Embed(Path + "/reveal.mp3")]
		public static Class reveal;
		
		[Embed(Path + "/flag.mp3")]
		public static Class flag;

		[Embed(Path + "/explosion.mp3")]
		public static Class explosion;
    }
}
