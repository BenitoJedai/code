using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using java.applet;
using java.awt;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;

namespace UltraWebApplication
{
	public sealed class UltraApplication
	{
		public UltraApplication(IHTMLElement e)
		{
			new IHTMLDiv { innerHTML = "Hello world!" }.AttachToDocument();

			{
				var btn = new IHTMLButton { innerText = "UltraWebService" }.AttachToDocument();

				btn.onclick +=
					delegate
					{

						new UltraWebService().GetTime("time: ",
							result =>
							{
								new IHTMLDiv { innerText = result }.AttachToDocument();

							}
						);

					};
			}

			{
				var x = new IHTMLButton("create UltraSprite proxied");

				x.AttachToDocument();

				x.onclick +=
					delegate
					{
						var o = new UltraSprite();

						o.AttachSpriteToDocument();
					};
			}

			{
				var x = new IHTMLButton("create UltraApplet proxied");

				x.AttachToDocument();

				x.onclick +=
					delegate
					{
						var o = new UltraApplet();

						o.AttachAppletToDocument();
					};
			}
		}


		public sealed class UltraApplet : Applet
		{
			public const int DefaultWidth = 500;
			public const int DefaultHeight = 400;


			public override void init()
			{
				base.resize(DefaultWidth, DefaultHeight);
				// creating the java applet

			}

			static Color GetBlue(double b)
			{
				int u = (int)(0xff * b);

				return new Color(u);
			}

			public override void paint(global::java.awt.Graphics g)
			{
				// old school gradient :)

				var h = this.getHeight();
				var w = this.getWidth();

				for (int i = 0; i < h; i++)
				{

					g.setColor(GetBlue(1 - (double)i / (double)h));
					g.drawLine(0, i, w, i);
				}
			}
		}



		public sealed class UltraSprite : Sprite
		{
			public const int DefaultWidth = 500;
			public const int DefaultHeight = 400;

			public UltraSprite()
			{
				// creating the flash object 
				// + stratus
				// + alchemy

				// funny :) i have forgotten how to write anything
				// on flash API ... too much WPF API?
				var r = new Sprite();

				r.graphics.beginFill(0x7070);
				r.graphics.drawRect(8, 8, 64, 64);


				r.AttachTo(this);
			}


		}

	}

	public delegate void StringAction(string e);

	public sealed class UltraWebService
	{
		public void GetTime(string x, StringAction result)
		{
			Debugger.Break();

			result(x + DateTime.Now);
		}
	}

}