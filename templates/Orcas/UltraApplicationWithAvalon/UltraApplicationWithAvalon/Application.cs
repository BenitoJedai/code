using System;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using System.ComponentModel;

namespace UltraApplicationWithAvalon
{

	[Description("UltraApplicationWithAvalon. Write javascript, flash and java applets within a C# project.")]
	public sealed partial class Application
	{
		public Application(IHTMLElement e)
		{
			Native.Document.title = "UltraApplicationWithAvalon";

			Native.Document.body.Button("GetTime").onclick +=
				delegate
				{

					new UltraWebService().GetTime("time: ",
						result =>
						{
							new IHTMLPre { innerText = result }.AttachToDocument();

						}
					);
				};

			Native.Document.body.style.backgroundColor = "#efefef";

			var a = new ApplicationCanvas
			{
				WebService = new UltraWebService()
			};

			a.AttachToContainer(CreateCenteredContainer(a.Width, a.Height));
		}

		private static IHTMLDiv CreateCenteredContainer(double _Width, double _Height)
		{
			var Width = System.Convert.ToInt32(_Width);
			var Height = System.Convert.ToInt32(_Height);

			var c = new IHTMLDiv();

			c.style.marginLeft = (Width / -2) + "px";
			c.style.marginTop = (Height / -2) + "px";
			c.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;
			c.style.left = "50%";
			c.style.top = "50%";
			c.style.width = Width + "px";
			c.style.height = Height + "px";

			//c.style.overflow = ScriptCoreLib.JavaScript.DOM.IStyle.OverflowEnum.hidden;

			var borders = new IHTMLDiv
			{

			};

			borders.style.backgroundColor = "gray";
			borders.style.SetSize(Width + 2, Height + 2);
			borders.style.SetLocation(-1, -1);
			borders.AttachTo(c);


			c.AttachToDocument();

			return c;
		}


	}


}
