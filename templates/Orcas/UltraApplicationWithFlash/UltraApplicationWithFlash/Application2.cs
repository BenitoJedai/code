using System;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript;
using System.Net;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.JavaScript.Remoting.Extensions;

namespace UltraApplicationWithFlash
{

	//[Description("UltraApplicationWithFlash. Write javascript, flash and java applets within a C# project.")]


	public sealed partial class Application : IWebServiceEnabled
	{
		public Application(IHTMLElement e)
		{
			var note1 = new IHTMLPre
			{
				innerHTML = @"Notice: If flash does not respond to events, you need to clear your cache. 
+ Chrome flash in 'localhost' on Cassini always fails?
  - If so Try http://127.0.0.1 or http://COMPUTERNAME
+ Opera does not pass delegates?
+ IE cannot return from javascript to flash
"

			}.AttachToDocument();

			note1.style.whiteSpace = ScriptCoreLib.JavaScript.DOM.IStyle.WhiteSpaceEnum.pre;
			note1.style.fontSize = "small";
			note1.style.color = Color.Red;


			var s = new UltraSprite();

			global::ScriptCoreLib.JavaScript.Extensions.SpriteExtensions.AttachSpriteToDocument(s);

			var SayHello = new IHTMLButton { innerText = "Say Hello to flash!" }.AttachToDocument();

			SayHello.onclick +=
				delegate
				{
					s.AppendLine("Hello from javascript");

					s.WhenReady(
						delegate
						{
							s.AppendLine("What about using the web service?");
						}
					);
				};

			s.AppendLine("This call is delayed until flash is loaded (1)");
			s.AppendLine("This call is delayed until flash is loaded (2)");
			s.AppendLine("This call is delayed until flash is loaded (3)");

			s.WhenReady(
				delegate
				{
					s.AppendLine("What about using the web service?");
				}
			);

			// passing the interface to flash is delayed until it is loaded, using the getter will fault
			s.WebService = new UltraWebService();
			s.WebServiceEnabled = this;

			s.AppendLine("This call is delayed until flash is loaded (4)");

			new IHTMLBreak().AttachToDocument();


			this.WebServiceEnabled = new IHTMLInput(ScriptCoreLib.Shared.HTMLInputTypeEnum.checkbox);

			var WebServiceEnabledLabel = new IHTMLLabel("WebService is enabled for flash", this.WebServiceEnabled);

			new IHTMLDiv(
				WebServiceEnabledLabel,
				WebServiceEnabled
			).AttachToDocument();

			ButtonsForWebService();


			var o = s;


			o.AttachSpriteToDocument();

			var cc = new IHTMLButton
			{
				innerText = "Continue"
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
							if (y == p)
							{
								Native.Document.body.appendChild(new IHTMLDiv("ok"));
							}
							else
							{
								Native.Document.body.appendChild(new IHTMLDiv("fault"));
							}
							y.Method1();
						}
					);

					o.PingPongServiceBase(p,
						y =>
						{
							if (y == p)
							{
								Native.Document.body.appendChild(new IHTMLDiv("PingPongServiceBase: ok"));
							}
							else
							{
								Native.Document.body.appendChild(new IHTMLDiv("PingPongServiceBase: fault"));
							}
							y.Method2();
						}
					);

					//Native.Document.body.appendChild(new IHTMLDiv("BuildPage"));
					//o.BuildPage((IHTMLBuilderImplementation)Native.Document.body);
					Native.Document.body.appendChild(new IHTMLDiv("BuildPage2"));
					o.BuildPage2(Native.Document.ToProxy());

				};
		}

		private static void ButtonsForWebService()
		{
			AddButtonForGetTime();

			var url = new IHTMLInput(ScriptCoreLib.Shared.HTMLInputTypeEnum.text, "http://example.com");

			url.AttachToDocument();


			var DownloadData = new IHTMLButton { innerText = "DownloadData" }.AttachToDocument();

			DownloadData.onclick +=
				delegate
				{
					new UltraWebService().DownloadData(url.value,
						x =>
						{
							new IHTMLPre { innerText = url.value + Environment.NewLine + Environment.NewLine + x }.AttachToDocument();
						}
					);
				};
		}


		public IHTMLInput WebServiceEnabled;

		#region IWebServiceEnabled Members

		public string IsEnabled
		{
			get { return Convert.ToString(WebServiceEnabled.@checked); }
		}

		#endregion
	}

	public delegate void DownloadDataResult(string e);

	public sealed partial class UltraWebService : IAlphaWebService
	{

		public void DownloadData(string url, DownloadDataResult result)
		{
			var c = new WebClient();

			result(c.DownloadString(url));
		}
	}
}
