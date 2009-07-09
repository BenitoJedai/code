using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.Extensions;
using System.Collections.Generic;
using System;
using System.IO;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.flash.ui;
//using OrcasFlashApplication.Shared;

namespace OrcasFlashApplication.ActionScript
{
	/// <summary>
	/// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
	/// </summary>
	[Script, ScriptApplicationEntryPoint(WithResources = true)]
	[SWF]
	public class OrcasFlashApplication : Sprite
	{



		/// <summary>
		/// Default constructor
		/// </summary>
		public OrcasFlashApplication()
		{

			for (var j = 0.0; j < 1; j += 0.1)
			{
				this.graphics.beginFill(0xff0000, j);
				this.graphics.drawCircle(40, 40, 40 * (1.0 - j));
				this.graphics.endFill();
			}

			var step = 100;
			for (int i = 0; i < 4; i++)
			{
				addChild(
				   new TextField
				   {
					   text = "hello world",
					   x = step * i,
					   y = 20,
					   textColor = 0x00ff00,
					   sharpness = 400
				   });
			}

			var t = new TextField
				{
					text = "powered by jsc",
					background = true,
					x = 20,
					y = 40,
					alwaysShowSelection = true,
				}.AttachTo(this);


			KnownEmbeddedResources.Default["assets/OrcasFlashApplication/Preview.png"].ToBitmapAsset().AttachTo(this).MoveTo(100, 200);
		}


	}

}