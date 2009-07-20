using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.Extensions;
using System.Collections.Generic;
using System;
using System.IO;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.flash.ui;
//using MP3PitchExample.Shared;

namespace MP3PitchExample.ActionScript
{
	/// <summary>
	/// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
	/// </summary>
	[Script, ScriptApplicationEntryPoint(WithResources = true)]
	[SWF]
	public class MP3PitchExample : Sprite
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public MP3PitchExample()
		{
			// http://blog.andre-michelle.com/upload/mp3pitch/MP3Pitch.as
			// http://blog.andre-michelle.com/2009/pitch-mp3/

			


			KnownEmbeddedResources.Default["assets/MP3PitchExample/Preview.png"].ToBitmapAsset().AttachTo(this).MoveTo(100, 200);
		}


	}

}