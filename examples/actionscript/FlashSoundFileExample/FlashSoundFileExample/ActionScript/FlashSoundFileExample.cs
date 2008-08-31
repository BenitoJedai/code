using ScriptCoreLib;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;

namespace FlashSoundFileExample.ActionScript
{
	/// <summary>
	/// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
	/// </summary>
	[Script, ScriptApplicationEntryPoint]
	public class FlashSoundFileExample : Sprite
	{

		public FlashSoundFileExample()
		{
			var m = new MemoryStream();
			var ba = new BinaryWriter(m);
			var d = 1.0/44100.0;
			var g = 1.0;

			foreach (int _i in Enumerable.Range(0, 44100))
			{
				var i = 44100 - _i;

				var f = ((Math.Sin(i * Math.PI / 10) + 1.0) / 2.0) * 0.5 +
							  ((Math.Sin((i + 1000) * Math.PI / 20) + 1.0) / 2.0) * 0.5 +
							  ((Math.Sin((i + 2000) * Math.PI / 40) + 1.0) / 2.0) * 0.5;

				f = f * (1.0 - (d * i));
				if (f > 1) f = 1; else if (f < 0) f = 0;
				ba.Write(Convert.ToByte(255.0 * f));
			}


	        
			foreach (int i in Enumerable.Range(0, 44100))
			{
				var f = ((Math.Sin(i*Math.PI/10) + 1.0)/2.0)*0.5 +
							  ((Math.Sin((i+1000)*Math.PI/20) + 1.0)/2.0)*0.5 +
							  ((Math.Sin((i+2000)*Math.PI/40) + 1.0)/2.0)*0.5;
	            
				f = f*(1.0-(d*i));
				if (f > 1) f=1; else if (f < 0) f=0;
				ba.Write(Convert.ToByte(255.0*f));
			}

			foreach (int _i in Enumerable.Range(0, 44100))
			{
				var i = 44100 - _i;

				var f = ((Math.Sin(i * Math.PI / 10) + 1.0) / 2.0) * 0.5 +
							  ((Math.Sin((i + 1000) * Math.PI / 20) + 1.0) / 2.0) * 0.5 +
							  ((Math.Sin((i + 2000) * Math.PI / 40) + 1.0) / 2.0) * 0.5;

				f = f * (1.0 - (d * i));
				if (f > 1) f = 1; else if (f < 0) f = 0;
				ba.Write(Convert.ToByte(255.0 * f));
			}

		

			//trace("Playing the wave...");
			DynSound.playSound(m);
			//trace("Done");
	        
			var spr  = new  Sprite();
			spr.graphics.beginFill(0x00CCFF);
			spr.graphics.drawRect(0, 100, 512, 184);
			spr.graphics.endFill();
	        
			spr.AttachTo(this);

			spr.click +=
				delegate
				{
					DynSound.playSound(m);
				};

		}
	}
}