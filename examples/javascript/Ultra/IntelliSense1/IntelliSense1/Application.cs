using System;
using System.ComponentModel;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.Ultra.Library.Extensions;

namespace IntelliSense1
{
	[Description("IntelliSense1. Write javascript, flash and java applets within a C# project.")]
	public sealed partial class Application
	{
		public Application(IHTMLElement e)
		{
			Native.Document.title = "IntelliSense1";

			var c = new IHTMLDiv
			{

			}.AttachToDocument();

			c.onmouseover +=
				delegate
				{
					c.style.backgroundColor = "#efefff";
				};

			c.onmouseout +=
				delegate
				{
					c.style.backgroundColor = "";
				};


			c.style.margin = "2em";
			c.style.padding = "2em";
			c.style.border = "1px solid #777777";
			c.style.borderLeft = "2em solid #777777";


			new IHTMLDiv
			{
				new IHTMLAnchor
				{
					innerText = "Write javascript, flash and java applets within a C# project.",
					href = "http://www.jsc-solutions.net"
				}
			}.AttachTo(c);


			{
				var btn = new IHTMLButton { innerText = "UltraWebService" }.AttachTo(c);

				btn.onclick +=
					delegate
					{

						new UltraWebService().GetTime("time: ",
							result =>
							{
								new IHTMLDiv { innerText = result }.AttachTo(c);

							}
						);

					};
			}

			var Editor = new IHTMLDiv().AttachToDocument();

			Editor.style.position = IStyle.PositionEnum.relative;
			Editor.style.backgroundColor = "#efefef";
			Editor.style.SetSize(400, 200);
			//Editor.style.overflow = IStyle.OverflowEnum.auto;

			var CodeShadowContainer = new IHTMLDiv().AttachTo(Editor);
			var CodeShadowSize = new IHTMLSpan().AttachTo(CodeShadowContainer);
			var CodeShadow = new IHTMLSpan().AttachTo(CodeShadowContainer);
			var CodeShadowLine = new IHTMLSpan().AttachTo(CodeShadowContainer);
			var CodeShadowMenu = new IHTMLDiv().AttachTo(CodeShadowContainer);

			CodeShadowMenu.style.backgroundColor = "red";

			var Code = new IHTMLTextArea().AttachTo(Editor);
			Code.style.SetLocation(0, 0, 400, 200);

			CodeShadowSize.style.position = IStyle.PositionEnum.absolute;
			CodeShadowSize.style.SetLocation(0, 0);

			CodeShadow.style.position = IStyle.PositionEnum.absolute;
			CodeShadow.style.SetLocation(0, 0);

			CodeShadowLine.style.position = IStyle.PositionEnum.absolute;
			CodeShadowLine.style.SetLocation(0, 0);

			CodeShadowContainer.style.SetLocation(0, 0, 400, 200);

			Action Update =
				delegate
				{
					CodeShadowSize.innerText = Code.value;

					var n = Code.value.Substring(0, Code.SelectionStart).Replace("\r", "");

					CodeShadow.innerText = n;
					CodeShadowLine.innerText = n.SkipUntilLastIfAny("\n");

					//var w = CodeShadowSize.offsetWidth;
					//if (w < 400)
					//    w = 400;

					//var h = CodeShadowSize.offsetHeight;
					//if (h < 200)
					//    h = 200;

					//Code.style.SetSize(w, h);

					if (n.EndsWith("."))
					{
						CodeShadowMenu.style.SetLocation(
							CodeShadowLine.offsetWidth,
							CodeShadow.offsetHeight,
							64, 32
						);
					}
					else
					{
						CodeShadowMenu.style.SetLocation(
							CodeShadowLine.offsetWidth,
							CodeShadow.offsetHeight,
							12, 12
						);
					}
				};

			Code.onkeyup +=
				delegate
				{
					Update();
				};

			Code.onchange +=
				delegate
				{
					Update();
				};

			Code.onmouseup +=
				delegate
				{
					Update();
				};

			Action<IStyle> SetStyle =
				style =>
				{
					style.padding = "0";
					style.margin = "0";
					style.display = IStyle.DisplayEnum.inline;

					style.fontFamily = ScriptCoreLib.JavaScript.DOM.IStyle.FontFamilyEnum.Verdana;
					style.fontSize = "1em";

					// http://www.tagindex.net/css/form/line_height.html
					// http://www.eskimo.com/~bloo/indexdot/css/properties/dimension/lineheight.htm
					style.lineHeight = "200%";

					// http://www.w3schools.com/CSS/pr_text_white-space.asp
					style.whiteSpace = IStyle.WhiteSpaceEnum.pre;
					//style.textWrap = 
				};

			Code.style.border = "0";

			// http://www.idocs.com/tags/forms/_TEXTAREA_WRAP.html
			Code.wrap = "off";
			Code.style.overflow = IStyle.OverflowEnum.hidden;
			Code.style.backgroundColor = JSColor.Transparent;

			//if (IsMicrosoftInternetExplorer)
			//{
			//    CodeShadow.style.lineHeight = "125%";
			//}

			CodeShadow.style.color = "gray";
			CodeShadow.style.backgroundColor = "yellow";

			CodeShadowLine.style.color = "gray";
			CodeShadowLine.style.backgroundColor = "cyan";

			SetStyle(Code);
			SetStyle(CodeShadowSize);
			SetStyle(CodeShadow);
			SetStyle(CodeShadowLine);

			Code.value = "hello1\nhello2\nhello3 WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW\n...";
			Update();
		}


		/// <summary>
		/// Microsoft Internet Explorer does not support using opacity on an image with an alpha layer.
		/// </summary>
		public static bool IsMicrosoftInternetExplorer
		{
			get
			{
				return (bool)new IFunction("/*@cc_on return true; @*/ return false;").apply(null);
			}
		}
	}


}
