using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Drawing;

namespace ScriptCoreLib.JavaScript.Components
{
	/// <summary>
	/// The Designer provides multiple views for a file. One of the views
	/// can be a SolutionFileView and a HTML Designer.
	/// </summary>
	public class SolutionFileDesigner
	{
		public IHTMLDiv Container { get; private set; }

		public IHTMLDiv Content { get; private set; }

		public readonly Action<IHTMLImage, string, Action> AddToolbarButton;

		public SolutionFileDesigner()
		{
			var ToolbarHeight = "1.3em";
			var Container = new IHTMLDiv();

			this.Container = Container;

			Container.style.position = IStyle.PositionEnum.absolute;
			Container.style.left = "0px";
			Container.style.top = "0px";
			Container.style.right = "0px";
			Container.style.bottom = "0px";

			var Content = new IHTMLDiv().AttachTo(Container);
			
			this.Content = Content;

			Content.style.position = IStyle.PositionEnum.absolute;
			Content.style.left = "0px";
			Content.style.top = "0px";
			Content.style.right = "0px";
			Content.style.bottom = ToolbarHeight;


			var Toolbar = new IHTMLDiv().AttachTo(Container);

			Toolbar.style.backgroundColor = Color.FromGray(0xef);
			Toolbar.style.position = IStyle.PositionEnum.absolute;
			Toolbar.style.left = "0px";
			Toolbar.style.height = ToolbarHeight;
			Toolbar.style.right = "0px";
			Toolbar.style.bottom = "0px";

			this.AddToolbarButton =
				(img, text, handler) =>
				{
					var span = new IHTMLSpan { innerText = text };

					span.style.paddingLeft = "1.5em";
					span.style.paddingRight = "0.3em";

					var a = new IHTMLAnchor
					{
						img, span
					};

					img.style.verticalAlign = "middle";
					img.border = 0;
					img.style.position = IStyle.PositionEnum.absolute;

					a.style.backgroundColor = Color.FromGray(0xef);
					a.style.color = Color.Black;
					a.style.textDecoration = "none";
					a.style.fontFamily = IStyle.FontFamilyEnum.Tahoma;

					a.href = "javascript: void(0);";
					a.onclick +=
						delegate
						{
							handler();
						};
					a.style.display = IStyle.DisplayEnum.inline_block;
					a.style.height = "100%";


					a.onmousemove +=
						delegate
						{
							a.style.backgroundColor = Color.FromGray(0xff);
						};

					a.onmouseout +=
						delegate
						{
							a.style.backgroundColor = Color.FromGray(0xef);
						};

					Toolbar.Add(a);
				};

		}
	}
}
