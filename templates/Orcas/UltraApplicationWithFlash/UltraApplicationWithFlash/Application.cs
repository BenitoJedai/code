using System;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Remoting.DOM.HTML.Remoting;
using ScriptCoreLib.JavaScript.Remoting.Extensions;

namespace UltraApplicationWithFlash
{

	//[Description("UltraApplicationWithFlash. Write javascript, flash and java applets within a C# project.")]

	public sealed partial class Application
	{
		public Application(IHTMLElement e)
		{
			Native.Document.title = "UltraApplicationWithFlash";

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


			#region GetTime
			{
				var btn = new IHTMLButton { innerText = "UltraWebService.GetTime" }.AttachTo(c);

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
			#endregion

			var x = new IHTMLButton("create UltraSprite ");

			x.AttachToDocument();

			x.onclick +=
				delegate
				{
					var o = new UltraSprite();

					o.ToTransparentSprite();

					o.AttachSpriteToDocument();

					
					// we are sending a proxy of HTML element to flash!
					o.BuildPage(o.ToHTMLElement().ToProxy());
				};
		}


	}





	public sealed partial class UltraWebService
	{
		public void GetTime(string prefix, GetTimeResult result)
		{
			result(prefix + ": " + DateTime.Now);
		}
	}



	public delegate void GetTimeResult(string e);


	partial class Application
	{
		private static void AddButtonForGetTime()
		{
			var GetTime = new IHTMLButton { innerText = "GetTime" }.AttachToDocument();

			GetTime.onclick +=
				delegate
				{
					new UltraWebService().GetTime("[client time]: " + DateTime.Now + " [server time]",
						x =>
						{
							new IHTMLDiv { innerText = x }.AttachToDocument();
						}
					);
				};
		}
	}
}
