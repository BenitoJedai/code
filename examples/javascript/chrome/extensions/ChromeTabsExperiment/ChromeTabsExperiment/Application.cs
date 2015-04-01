using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ChromeTabsExperiment;
using ChromeTabsExperiment.Design;
using ChromeTabsExperiment.HTML.Pages;
using chrome;
using System.Diagnostics;

namespace ChromeTabsExperiment
{
	/// <summary>
	/// Your client side code running inside a web browser as JavaScript.
	/// </summary>
	public sealed class Application
	{
		public readonly ApplicationWebService service = new ApplicationWebService();

		static Application()
		{
			Console.WriteLine("typeof Application available...");

		}
		/// <summary>
		/// This is a javascript application.
		/// </summary>
		/// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
		public Application(IApp page)
		{
			// http://developer.chrome.com/extensions/override.html

			dynamic self = Native.self;
			dynamic self_chrome = self.chrome;
			object self_chrome_tabs = self_chrome.tabs;

			if (self_chrome_tabs != null)
			{
				//                ---------------------------
				//Extension error
				//---------------------------
				//Could not load extension from 'A:\'. Could not load options page '_generated_background_page.html'.
				//---------------------------
				//OK   
				//---------------------------


				Action<Tab> pageActionClick = delegate
				{
				};

				Console.WriteLine("loading ChromeTabsExperiment...");

				chrome.Notification.DefaultTitle = "ChromeTabsExperiment";

				#region chrome.runtime

				// unavailable for extension
				//  chrome.app.runtime.Restarted +=
				//delegate
				//{
				//    new Notification
				//    {
				//        Message = "Restarted!"
				//    };
				//};


				chrome.runtime.Installed += delegate
				{
					new chrome.Notification
					{
						Message = "Extension Installed"
					};
				};

				chrome.runtime.Startup +=
					delegate
					{
						new chrome.Notification
						{
							Message = "Startup!"
						};
					};


				var t = new Stopwatch();
				t.Start();

				chrome.runtime.Suspend +=
					delegate
					{
						var n = new chrome.Notification
						{
							Message = "Suspend! " + new { t.ElapsedMilliseconds }
						};

						n.Clicked += delegate
						{
							runtime.reload();
						};

					};
				#endregion


				//chrome.tabs.Created +=
				//    tab =>
				//    {
				//        if (tab.url.StartsWith("chrome-devtools://"))
				//            return;

				//        if (tab.url.StartsWith("chrome://"))
				//            return;

				//        new chrome.Notification
				//        {
				//            Message = "chrome.tabs.Created " + new
				//            {
				//                tab.id,
				//                tab.url,
				//                tab.status,
				//                tab.title
				//            }
				//        };



				//    };


				chrome.tabs.Updated +=
					(i, x, tab) =>
					{
						// chrome://newtab/

						if (tab.url.StartsWith("chrome-devtools://"))
							return;

						if (tab.url.StartsWith("chrome://"))
							return;

						if (tab.status == "complete")
						{


							// lso keep in mind that content scripts are not injected into any chrome:// or extension gallery pages.
							//chrome.tabs.insertCSS(

							//tab.id.insertCSS(
							//    new
							//    {
							//        code = "body { background-color: cyan; }"
							//    },
							//    null
							//);


							// this does not work
							//tab.id.insertCSS(
							//    new
							//    {
							//        code = "body { background-color: yellow; }"
							//    },
							//    IFunction.OfDelegate(
							//        new Action(
							//            delegate
							//            {
							//            }
							//        )
							//    )
							//);

							// can we have a new Worker here?
							// upon click?


							var xhr = new IXMLHttpRequest();
							//Console.WriteLine(new { asset });

							xhr.open(ScriptCoreLib.Shared.HTTPMethodEnum.GET, "view-source");


							xhr.bytes.ContinueWith(
								task =>
								{
									var code = Encoding.UTF8.GetString(task.Result);

									//var n = new chrome.Notification
									//{
									//    Title = "click to inject... ",
									//    Message = tab.url,
									//};

									#region inject
									Action inject = async delegate
									{
										Console.WriteLine("executeScript");

										var st = new Stopwatch();
										st.Start();

										//var port = tab.id.connect();

										////port.
										//port.onMessage.addListener(
										//    new Action<object>(
										//        data =>
										//        {
										//            Console.WriteLine(
										//                "onMessage " + new { tab.id, data }
										//            );
										//        }
										//    )
										//);

										await tab.id.executeScript(
											new { code }
										);

										//await System.Threading.Tasks.Task.Delay(200);

										Console.WriteLine("sendMessage " + new { st.ElapsedMilliseconds });

										// http://stackoverflow.com/questions/14790389/return-value-from-chrome-tabs-executescript

										var forever = true;

										while (forever)
										{
											await System.Threading.Tasks.Task.Delay(333);


											var responses = (object[])await tab.id.executeScript(
											  new { code = "(onxmessage('hello!'));" }
										  );

											//// http://ferdinandfly.blogspot.com/2013/04/chrome-extension-error-attempting-to.html
											//// message: "Attempting to use a disconnected port object"
											//port.postMessage(
											//      new { hello = "world" }
											//    );

											//var response = await tab.id.sendMessage(
											//    new { hello = "world" }
											//);

											if (responses != null)
											{
												responses.WithEach(
													response =>
													{
														if (response == null)
															return;

														forever = false;

														var nn = new chrome.Notification
														{
															Title = "signal!",
															Message = (string)response,
														};


														// does this work?
														nn.Closed +=
															delegate
															{

															};
														tab.id.show();

													}
												);
											}
										}

									};
									#endregion


									// http://stackoverflow.com/questions/4022179/chrome-page-action-click-not-working
									// Uncaught TypeError: Cannot read property 'onClicked' of undefined 
									chrome.pageAction.Clicked +=
										xtab =>
										{
											if (xtab.id != tab.id)
												return;

											// let extension call app
											// which in turn can call
											// udp android
											pageActionClick(tab);

											if (inject != null)
											{
												tab.id.hide();

												inject();
												inject = null;
											}
										};

									//tab.id.setTitle(
									tab.id.show();

									//n.Clicked += async delegate
									//{

									//};


								}
							);

							return;
						}

						//new chrome.Notification
						//{
						//    Message = "chrome.tabs.Updated " + new
						//    {
						//        tab.id,
						//        tab.url,
						//        tab.status,
						//        tab.title
						//    }
						//};
					};


				chrome.tabs.Detached +=
				  (id, x) =>
				  {
					  new chrome.Notification
					  {
						  Message = "chrome.tabs.Detached " + new { id }
					  };
				  };


				//new Notification
				//{
				//    Message = "extension!"
				//};

				#region slave
				// Port: Could not establish connection. Receiving end does not exist. 
				var slave = "fkgibadjpabiongmgoeomdbcefhabmah";
				// http://stackoverflow.com/questions/13921970/google-chrome-socket-api-in-extensions
				// No, extensions do not have access to the socket API, and they aren't likely to ever get it.

				Console.WriteLine("connect " + new { slave });
				chrome.runtime.connect(slave).With(
					port =>
					{
						Console.WriteLine("connect done " + new { slave });

						port.onDisconnect.addListener(
							 new Action(
								  delegate
								  {
									  Console.WriteLine("connect onDisconnect");

								  }
							  )
						);

						port.onMessage.addListener(
							  new Action<object>(
								  message =>
								  {
									  Console.WriteLine("connect onMessage " + new { message });

									  var nn = new chrome.Notification
									  {
										  Title = "hybrid app signal!",
										  Message = new { message }.ToString(),
									  };
								  }
							  )
						  );

						port.postMessage(
										 new { hello = "slave" }
									 );


						pageActionClick +=
							tab =>
							{
								Console.WriteLine("pageActionClick " + new { tab.id });

								try
								{
									port.postMessage(
										new { tab.id }
									);
								}
								catch
								{
									Console.WriteLine("error pageActionClick " + new { tab.id });
								}

							};

						Console.WriteLine("connect posted " + new { slave });
					}
				);
				#endregion



				return;
			}



			Console.WriteLine("hello!");

			IStyleSheet.Default["h1"].style.color = "blue";
			IStyleSheet.Default["body"].style.backgroundColor = "yellow";


			//http://developer.chrome.com/extensions/messaging.html
			// This will expose the messaging API to any page which matches the URL patterns you specify. The URL pattern must contain at least a second-level domain - that is, hostname patterns like "*", "*.com", "*.co.uk", and "*.appspot.com" are prohibited. From the web page, use the runtime.sendMessage or runtime.connect APIs to send a message to a specific app or extension. For example:

			// this wont be called by the chrome overlord?
			Native.window.onmessage +=
				e =>
				{
					Console.WriteLine("onmessage " + new { e.data });

					e.ports.WithEach(
						port =>
						{
							port.postMessage(new { marko = new { e.data } });

						}
					);

				};

			#region onxmessage
			var xst = new Stopwatch();

			xst.Start();

			// this will work
			Func<string, object> onxmessage =
				data =>
				{
					Console.WriteLine("onxmessage " + new { data });

					if (xst.IsRunning)
						if (xst.ElapsedMilliseconds > 2000)
						{
							xst.Stop();

							return "special message from " + new { Native.document.location.href, data };

						}
					return null;
				};


			(Native.self as dynamic).onxmessage = onxmessage;
			#endregion

			// http://slashdot.org/
			//<iframe id="google_ads_iframe_243358" name="google_ads_iframe_243358"

			Native.document.getElementsByTagName("iframe").WithEach(
				iframe =>
				{
					if (iframe.name.StartsWith("google_ads"))
					{
						iframe.style.Opacity = 0.3;
						return;
					}

					// aswift
					if (iframe.name.StartsWith("aswift"))
					{
						iframe.style.Opacity = 0.3;
						return;
					}

					// "<iframe width="300" height="250" frameborder="0" marginwidth="0" marginheight="0" vspace="0" hspace="0" allowtransparency="true" scrolling="no" onload="var i=this.id,s=window.google_iframe_oncopy,H=s&amp;&amp;s.handlers,h=H&amp;&amp;H[i],w=this.contentWindow,d;try{d=w.document}catch(e){}if(h&amp;&amp;d&amp;&amp;(!d.body||!d.body.firstChild)){if(h.call){setTimeout(h,0)}else if(h.match){w.location.replace(h)}}" id="aswift_0" name="aswift_0" style="left:0;position:absolute;top:0;"></iframe>"


					//<div id="rightCol" role="complementary" aria-label="Reminders, people you may know, and ads"><

				}
			);

			Native.document.getElementsByTagName("div").WithEach(
				 x =>
				 {

					 if (x.attributes.Any(a => a.value == "Reminders, people you may know, and ads"))
					 {
						 x.style.Opacity = 0.3;
						 return;
					 }

					 //<div id="rightCol" role="complementary" aria-label="Reminders, people you may know, and ads"><

				 }
			 );

			//chrome.runtime.Message +=
		}

	}
}
