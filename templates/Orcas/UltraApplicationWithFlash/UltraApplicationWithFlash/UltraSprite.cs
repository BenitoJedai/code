using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;
using System.Diagnostics;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Remoting.DOM.HTML.Remoting;
using ScriptCoreLib.JavaScript.Remoting.Extensions;
using ScriptCoreLib.JavaScript.Remoting.DOM;
using ScriptCoreLib.ActionScript.flash.geom;

namespace UltraApplicationWithFlash
{
	public sealed partial class UltraSprite : Sprite
	{
		public const int DefaultWidth = 400;
		public const int DefaultHeight = 200;

		

		public UltraSprite()
		{
			{
				var r = new Sprite();


				r.graphics.beginFill(0x7000);
				r.graphics.drawRect(8, 8, 64, 32);

				r.AttachTo(this);

				ClearTarget = r;
			}

			{
				var r = new Sprite();

				var fillType = GradientType.LINEAR;
				var colors = new uint[] { 0xFF0000, 0xFF0000 };
				var alphas = new double[] { 1, 0 };
				var ratios = new int[] { 0x00, 0xFF };
				var matr = new Matrix();
				matr.createGradientBox(DefaultWidth / 2, DefaultHeight, 0, 0, 0);
				var spreadMethod = SpreadMethod.PAD;
				this.graphics.beginGradientFill(fillType, colors, alphas, ratios, matr, spreadMethod);
				this.graphics.drawRect(0, 0, DefaultWidth / 2, DefaultHeight);

				r.AttachTo(this);
			}
		}


		public void BuildPage(PHTMLElement that)
		{
			that.get_style(
				style =>
				{
					style.border = "1px solid blue";
				}
			);

			that.get_ownerDocument(
				doc =>
				{
					Action<string> CreateColorButton =
						Color =>
						{
							doc.createElement("button",
								button =>
								{
									button.AttachToDocument(doc);

									button.innerText = "BackgroundColor: " + Color;
									button.setAttribute("onclick", "void(document.body.style.backgroundColor = '" + Color + "');");
								}
							);
						};

					// js known colors
					CreateColorButton("yellow");
					CreateColorButton("white");
					CreateColorButton("cyan");
					CreateColorButton("gray");

				
					// could we do a redirect here knowing we are in flash world?
					// new IHTMLButton ?
					doc.createElement("button",
						button1 =>
						{
							button1.innerText = "Hello from Flash!";

							button1.get_style(
								style =>
								{
									style.color = "red";
								}
							);

							button1.AttachToDocument(doc);

							int i = 0;

							button1.onclick +=
								delegate
								{
									i++;
									button1.innerText = "Click #" + i;
								};
						}
					);
				}
			);
		}

		Sprite ClearTarget;
		public void Clear()
		{
			ClearTarget.Orphanize();
		}
	}

}
