using System;
using System.ComponentModel;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;

namespace OrcasUltraApplication
{
	[Description("OrcasUltraApplication. Write javascript, flash and java applets within a C# project.")]
	public sealed partial class Application
	{
		public Application(IHTMLElement e)
		{
			Native.Document.title = "OrcasUltraApplication";

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


		}


	}


}
