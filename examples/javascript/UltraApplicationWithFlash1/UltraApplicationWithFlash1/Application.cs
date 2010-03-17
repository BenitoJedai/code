using System;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Remoting.DOM.HTML.Remoting;
using ScriptCoreLib.JavaScript.Remoting.Extensions;

namespace UltraApplicationWithFlash1
{

	//[Description("UltraApplicationWithFlash1. Write javascript, flash and java applets within a C# project.")]

	public sealed partial class Application
	{

		public void OLDApplication(IHTMLElement e)
		{
			Native.Document.title = "UltraApplicationWithFlash1";

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

			// do we jave mxmlc configured? 
			this.CreateSprite();
		}


	}




	public static class UltraSpriteIntegration
	{
		public static void CreateSprite(this Application a)
		{
			var x = new IHTMLButton("create UltraSprite ");

			x.AttachToDocument();

			x.onclick +=
				delegate
				{
					var o = new UltraSprite();

					o.AttachSpriteToDocument();

					var cc = new IHTMLButton
					{
						innerText = "Continue 1"
					};

					cc.AttachToDocument();

					cc.onclick +=
						delegate
						{
							var p = new JavaScriptPingPong
								{
									AtMethod1 =
										delegate
										{
											Native.Document.body.appendChild(new IHTMLDiv("AtMethod1"));
										}
								};

							o.PingPongService(p,
								y =>
								{
									//if (y == p)
									//{
									//    Native.Document.body.appendChild(new IHTMLDiv("ok"));
									//}
									//else
									//{
									//    Native.Document.body.appendChild(new IHTMLDiv("fault"));
									//}
									y.Method1();
								}
							);

							//Native.Document.body.appendChild(new IHTMLDiv("BuildPage"));
							//o.BuildPage((IHTMLBuilderImplementation)Native.Document.body);
							//Native.Document.body.appendChild(new IHTMLDiv("BuildPage2"));
							//o.BuildPage2(Native.Document.ToProxy());

						};
				};

		}
	}


	public delegate void StringAction(string e);

	public sealed partial class UltraWebService
	{
		public void GetTime(string prefix, GetTimeResult result)
		{
			result(prefix + ": " + DateTime.Now);
		}
	}

	public interface IAlphaWebService
	{
		void DownloadData(string url, DownloadDataResult result);
		void GetTime(string prefix, GetTimeResult result);
	}

	public interface IWebServiceEnabled
	{
		// primitives not yet supporter
		string IsEnabled { get; }
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
