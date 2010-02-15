using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;

namespace UltraTutorial04
{
	public sealed class UltraSprite : Sprite
	{
		public const int DefaultWidth = 600;
		public const int DefaultHeight = 200;

		TextField t;

		public UltraSprite()
		{
			this.graphics.beginFill(0xffffff);
			this.graphics.drawRect(0, 0, DefaultWidth, DefaultHeight);
			this.graphics.lineStyle(2, 0xa0, 1);
			this.graphics.beginFill(0xffffff, 0);
			this.graphics.drawRect(0, 0, DefaultWidth, DefaultHeight);

			t = new TextField
			{
				width = DefaultWidth,
				height = DefaultHeight
			};

			t.AttachTo(this);

			this.AppendLine("This is flash.display.Sprite (version 101)");
		}

		public void AppendLine(string e)
		{
			t.appendText(e + Environment.NewLine);
		}

		public void WhenReady(Action e)
		{
			// a call from javascript over here is delayed until
			// this element is loaded...
			try
			{
				e();
			}
			catch (Exception ex)
			{
				AppendLine("error: WhenReady  " + ex.Message);
			}
		}
	}
}
