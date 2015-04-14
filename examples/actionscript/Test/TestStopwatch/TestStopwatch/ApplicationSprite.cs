using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.Extensions;
using System;
using System.Diagnostics;

namespace TestStopwatch
{
	public sealed class ApplicationSprite : Sprite
	{
		public ApplicationSprite()
		{
			var sw = Stopwatch.StartNew();

			// hello {{ now = 14.04.2015 14:19:36, ElapsedMilliseconds = NaN }}
			// hello {{ now = 14.04.2015 14:20:26, IsRunning = true, ElapsedMilliseconds = NaN }}
			var now = DateTime.Now;

			// hello {{ ElapsedMilliseconds = NaN }}
			var t = new TextField
			{


				autoSize = TextFieldAutoSize.LEFT,

				text = "hello " + new { now, sw.IsRunning, sw.ElapsedMilliseconds }
			};

			t.AttachToSprite();
		}

	}
}
