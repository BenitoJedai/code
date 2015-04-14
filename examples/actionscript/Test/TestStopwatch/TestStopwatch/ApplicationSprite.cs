using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.Extensions;
using System.Diagnostics;

namespace TestStopwatch
{
	public sealed class ApplicationSprite : Sprite
	{
		public ApplicationSprite()
		{
			var sw = Stopwatch.StartNew();

			// hello {{ ElapsedMilliseconds = NaN }}
			var t = new TextField { text = "hello " + new { sw.ElapsedMilliseconds } };

			t.AttachToSprite();
		}

	}
}
