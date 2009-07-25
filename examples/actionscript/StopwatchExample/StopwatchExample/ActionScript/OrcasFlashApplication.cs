using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.Extensions;
using System.Collections.Generic;
using System;
using System.IO;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.flash.ui;
using System.Diagnostics;
using ScriptCoreLib.ActionScript.flash.utils;

namespace StopwatchExample.ActionScript
{
	/// <summary>
	/// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
	/// </summary>
	[Script, ScriptApplicationEntryPoint(WithResources = true)]
	[SWF]
	public class StopwatchExample : Sprite
	{
		public StopwatchExample()
		{
			var t = new TextField
			{
				text = "powered by jsc",
				background = true,
				width = 400,
				x = 20,
				y = 40,
				alwaysShowSelection = true,
			}.AttachTo(this);

			var w = new Stopwatch();
			w.Start();


			var timer = new Timer(3000, 1);

			timer.timer +=
				delegate
				{


					w.Stop();

					t.appendText(" work done in " + w.Elapsed.TotalMilliseconds);
				};

			timer.start();

			KnownEmbeddedResources.Default["assets/StopwatchExample/Preview.png"].ToBitmapAsset().AttachTo(this).MoveTo(100, 200);
		}


	}

}