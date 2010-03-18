using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;
using System.Diagnostics;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.JavaScript.Remoting.DOM.HTML.Remoting;

namespace UltraApplicationWithFlash1
{
	public sealed partial class UltraSprite : Sprite
	{
		public const int DefaultWidth = 600;
		public const int DefaultHeight = 200;

		TextField t;

		// does get work in IE?
		public PHTMLElementAction DoSomethingCoolWithThisElementInJavaScript { get; set; }

		public UltraSprite()
		{
			UltraSprite2();

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

			var Button1 = new Sprite();

			Button1.graphics.beginFill(0xff00);
			Button1.graphics.drawRect(0, 0, 64, 24);
			Button1.useHandCursor = true;
			Button1.buttonMode = true;

			Button1.AttachTo(this).MoveTo(DefaultWidth - 64 - 8, 8);

			Button1.click +=
				delegate
				{
					// jsc.meta should use Convert type for us
					if (Convert.ToBoolean(this.WebServiceEnabled.IsEnabled))
					{
						this.WebService.GetTime("[flash client time]: " + DateTime.Now + " [server time]",
							x =>
							{
								this.AppendLine(x);
							}
						);
					}
					else
					{
						this.AppendLine("WebService is disabled!");
					}
				};
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

		public IAlphaWebService WebService { get; set; }
		public IWebServiceEnabled WebServiceEnabled { get; set; }


	}
}
