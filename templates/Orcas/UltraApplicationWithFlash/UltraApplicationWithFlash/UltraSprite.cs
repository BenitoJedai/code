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
					doc.createElement("img",
						img =>
						{
							img.AttachToDocument(doc);
							img.setAttribute("src", LogoImageBase64());

							EnhanceLogo(img);
						}
					);

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

		public static string LogoImageBase64()
        {
            return "data:image/png;base64,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";
        }

	}

}
