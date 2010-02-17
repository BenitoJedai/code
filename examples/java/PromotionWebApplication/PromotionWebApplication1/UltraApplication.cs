using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.JavaScript.DOM;
using System.ComponentModel;
using PromotionWebApplication1.Library;
using ScriptCoreLib.JavaScript.Runtime;

namespace PromotionWebApplication1
{

	
	public sealed class UltraApplication
	{
		public UltraApplication(IHTMLElement e)
		{
			Native.Document.title = "jsc solutions";

			var c = new IHTMLDiv().AttachToDocument();
			
			c.style.position = IStyle.PositionEnum.absolute;
			c.style.left = "50%";
			c.style.top = "50%";

			var a = new IHTMLAnchor { href = "#about" }.AttachTo(c);

			var jsc = new IHTMLImage("assets/ScriptCoreLib/jsc.png").AttachTo(a);

			jsc.style.borderStyle = "none";
			jsc.style.marginLeft = "-48px";
			jsc.style.marginTop = "-48px";

			jsc.style.SetSize(96, 96);
			jsc.style.Opacity = 0;

			jsc.InvokeOnComplete(
				delegate
				{
					jsc.FadeIn(500, 1000,
						delegate
						{
							new Timer(
								t =>
								{
									jsc.style.Opacity = (Math.Cos(t.Counter * 0.1) + 1.0) * 0.5;
								}
							, 1, 1000 / 15);
						}
					);
				}
			);
		}
	}

	public delegate void StringAction(string e);

	public sealed class UltraWebService
	{

		public void Hello(string data, StringAction result)
		{
			result(data + " hello");
			result(data + " world");
		}
	}
}
