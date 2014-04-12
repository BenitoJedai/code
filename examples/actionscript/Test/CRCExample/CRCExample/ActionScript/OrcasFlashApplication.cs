using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.Extensions;
using System.Collections.Generic;
using System;
using System.IO;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.flash.ui;

namespace CRCExample.ActionScript
{
	/// <summary>
	/// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
	/// </summary>
	[Script, ScriptApplicationEntryPoint(WithResources = true)]
	[SWF]
	public class CRCExample : Sprite
	{



		/// <summary>
		/// Default constructor
		/// </summary>
		public CRCExample()
		{


			var t = new TextField
				{
					text = "powered by jsc",
					background = true,
					x = 20,
					y = 40,
					alwaysShowSelection = true,
				}.AttachTo(this);


			var crc = new Crc32Helper();
			crc.ComputeCrc32(new byte[] { 1, 2, 0xfe, 0xff });

			// Crc32Value = 1027690409

			t.text = "" + crc.Crc32Value + " vs 1027690409";
		}


	}

}