using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript;

namespace jsc.meta.Library.Mashups
{
	class ImplicitClientSideType
	{
		public IHTMLImage Container;

		public string Text;

		public event Action<ImplicitClientSideType> AtClick;
	}

	class UltraWebServiceWithDelayedDOM
	{
		public void GetImplicitClientSideType(Action<ImplicitClientSideType> YieldReturn)
		{
			var ImplicitClientSideType1 = new ImplicitClientSideType
			{
				Text = "server says hi",
				Container = new IHTMLImage
				{
					src = "http://xxx"
				}

				// Container = Images.Image1
			};


			YieldReturn(ImplicitClientSideType1);
		}

		public void GetImage1(Action<IHTMLImage> YieldReturn)
		{
			// JSC will emit instrumentation instructions for 
			// IHTMLElement types or any type which references it
			// because it would be a client side type then...

			// this requires XElement support for php and java

			// <img>
			var Image1 = new IHTMLImage();

			//	<style>
			//		<border>
			//			0
			//		</border>
			//  </style>

			Image1.style.border = "0";

			//	<onclick>
			//		<add>
			//			UltraWebServiceWithDelayedDOM.Click1
			//		</add>
			//	<onclick>
			Image1.onclick += new ScriptCoreLib.Shared.EventHandler<IEvent>(Image1_onclick);

			YieldReturn(Image1);
		}

		void Image1_onclick(IEvent e)
		{
			// add something to database?
			// we do not even know which client called us! :)
			
			// could we use e.Element to call back to client now?
			// calling back would mean
			// we store callback in the DB
			// and the client polls us for new events.
		}

		public void GetData1(Action<string> YieldReturn)
		{
			YieldReturn("hi");
		}

		public void Page1(Action<IHTMLDocument> YieldReturn)
		{
			// this time we are rendering html document

			var doc = new IHTMLDocument();

			doc.title = "hello world";


			var body = new IHTMLBody();

			body.AttachTo(doc);

			var btn = new IHTMLButton { innerText = "click me" };

			btn.onclick +=
				delegate
				{
					// as long as we do not touch web server internal API
					// we shall be able to run in the client


					// as long as we do not touch client internal API
					// we shall be able to run in the server

					// js code:

					Native.Document.title = "hello world";

					// doc needs to inject script application
					// and bind to the rendered html

					// here we can talk to flash and java elements

					var wbtn = new IHTMLButton("GetData1");

					wbtn.AttachToDocument();

					wbtn.onclick +=
						delegate
						{
							// we are creating the proxy version in the client
							var ws = new UltraWebServiceWithDelayedDOM();

							ws.GetData1(
								ss =>
								{
									Native.Window.alert("data: " + ss);
								}
							);
						};
				};

			btn.AttachTo(body);

			YieldReturn(doc);
		}
	}
}
