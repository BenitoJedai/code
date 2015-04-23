using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ChromeExtensionHopToTabThenIFrame;
using ChromeExtensionHopToTabThenIFrame.Design;
using ChromeExtensionHopToTabThenIFrame.HTML.Pages;
using chrome;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using TestSwitchToServiceContextAsync;

namespace ChromeExtensionHopToTabThenIFrame
{
	#region HopToExtension
	public struct HopToExtension : System.Runtime.CompilerServices.INotifyCompletion
	{
		// basically we have to hibernate the current state to resume
		public HopToExtension GetAwaiter() { return this; }
		public bool IsCompleted { get { return false; } }

		public static Action<HopToExtension, Action> VirtualOnCompleted;
		public void OnCompleted(Action continuation) { VirtualOnCompleted(this, continuation); }

		public void GetResult() { }
	}
	#endregion


	#region HopToParent
	public struct HopToParent : System.Runtime.CompilerServices.INotifyCompletion
	{
		// basically we have to hibernate the current state to resume
		public HopToParent GetAwaiter() { return this; }
		public bool IsCompleted { get { return false; } }

		public static Action<HopToParent, Action> VirtualOnCompleted;
		public void OnCompleted(Action continuation) { VirtualOnCompleted(this, continuation); }

		public void GetResult() { }
	}
	#endregion

	#region HopToIFrame
	public struct HopToIFrame : System.Runtime.CompilerServices.INotifyCompletion
	{
		// basically we have to hibernate the current state to resume
		public HopToIFrame GetAwaiter() { return this; }
		public bool IsCompleted { get { return false; } }

		public static Action<HopToIFrame, Action> VirtualOnCompleted;
		public void OnCompleted(Action continuation) { VirtualOnCompleted(this, continuation); }

		public void GetResult() { }


		public IHTMLIFrame frame;
		public static explicit operator HopToIFrame(IHTMLIFrame frame)
		{
			return new HopToIFrame { frame = frame };
		}
	}
	#endregion

	#region HopToChromeTab
	public struct HopToChromeTab : System.Runtime.CompilerServices.INotifyCompletion
	{
		// basically we have to hibernate the current state to resume
		public HopToChromeTab GetAwaiter() { return this; }
		public bool IsCompleted { get { return false; } }

		public static Action<HopToChromeTab, Action> VirtualOnCompleted;
		public void OnCompleted(Action continuation) { VirtualOnCompleted(this, continuation); }

		public void GetResult() { }

		// can we move GetAwaiter to extend TabIdInteger
		public TabIdInteger id;
		public static implicit operator HopToChromeTab(TabIdInteger id)
		{
			return new HopToChromeTab { id = id };
		}

		public static explicit operator HopToChromeTab(Tab tab)
		{
			return tab.id;
		}
	}
	#endregion


	//public static class xHopToChromeTab
	//{
	//	[Obsolete("while possible, reading the source code wont indicate we are about to hop.. keep the cast instead?")]
	//	public static HopToChromeTab GetAwaiter(this TabIdInteger id) { return id; }
	//}

	/// <summary>
	/// Your client side code running inside a web browser as JavaScript.
	/// </summary>
	public sealed class Application : ApplicationWebService
	{
		static Func<string, string> DecoratedString =
	x => x.Replace("-", "_").Replace("+", "_").Replace("<", "_").Replace(">", "_");

		// not available in extension, iframe. only in tab.
		static IHTMLIFrame iframe;


		// source code sent by extension to tab, tab makes it an url to be used creating the iframe 
		static string url;

		// jsc should package displayName  the end of the view-source?
		// should we gzip the string lookup?


		static IHTMLTextArea text3;

		static Application()
		{

			// what about console? consolidate all core apps into one?

			// 0ms  cctor Application did we make the jump yet? {{ href = http://example.com/ }}
			Console.WriteLine(" cctor Application did we make the jump yet? " + new
			{

				// wont be available
				//Native.document.currentScript.src,

				Native.document.location.href
			});



			dynamic self = Native.self;
			dynamic self_chrome = self.chrome;
			object self_chrome_tabs = self_chrome.tabs;

			Console.WriteLine("nop");

			#region self_chrome_tabs
			if (self_chrome_tabs != null)
			{

				// X:\jsc.svn\examples\javascript\ScriptDynamicSourceBuilder\ScriptDynamicSourceBuilder\Application.cs
				// X:\jsc.svn\examples\javascript\Test\TestRedirectWebWorker\TestRedirectWebWorker\Application.cs
				// or. were we injected? then our source is different?
				// makeURL ? did chrome extension prep the special url yet?
				var codetask = new WebClient().DownloadStringTaskAsync(
						 new Uri(Worker.ScriptApplicationSource, UriKind.Relative)
					);

				#region HopToChromeTab.VirtualOnCompleted
				HopToChromeTab.VirtualOnCompleted = async (that, continuation) =>
				{
					//Console.WriteLine("HopToChromeTab.VirtualOnCompleted ");
					Console.WriteLine("HopToChromeTab.VirtualOnCompleted " + new { that.id });

					// um. whats the tab we are to jump into?
					// signal we are about to inject
					//await that.id.insertCSS(
					//		new
					//		{
					//			code = @"

					//html { 
					//border-left: 1em solid yellow;
					//}


					//"
					//		}
					//	);


					// where is it defined?
					// X:\jsc.svn\examples\javascript\async\Test\TestSwitchToServiceContextAsync\TestSwitchToServiceContextAsync\ShadowIAsyncStateMachine.cs
					// TestSwitchToServiceContextAsync


					// async dont like ref?
					var r = TestSwitchToServiceContextAsync.ShadowIAsyncStateMachine.ResumeableFromContinuation(continuation);

					// um. now what?
					// send shadowstate over?
					// first we have to open a channel

					// do we have our view-source yet?
					var code = await codetask;

					// 5240ms HopToChromeTab.VirtualOnCompleted {{ id = 449, state = 1, Length = 3232941 }}

					// 226632ms HopToChromeTab.VirtualOnCompleted {{ id = 95, state = 0, Length = 3254419 }}
					Console.WriteLine("HopToChromeTab.VirtualOnCompleted " + new { that.id, r.shadowstate.state, code.Length });

					if (r.shadowstate.state == 0)
						Console.WriteLine("HopToChromeTab.VirtualOnCompleted bugcheck. state 0?");

					//// how can we inject ourselves and send a signal back to set this thing up?

					//// https://developer.chrome.com/extensions/tabs#method-executeScript
					//// https://developer.chrome.com/extensions/tabs#type-InjectDetails
					//// https://developer.chrome.com/extensions/content_scripts#pi

					//// Content scripts execute in a special environment called an isolated world. 
					//// They have access to the DOM of the page they are injected into, but not to any JavaScript variables or 
					//// functions created by the page. It looks to each content script as if there is no other JavaScript executing
					//// on the page it is running on. The same is true in reverse: JavaScript running on the page cannot call any 
					//// functions or access any variables defined by content scripts.

					r.shadowstate.code = code;

					var result = await that.id.executeScript(
						//new { file = url }
						new { code }
					);

					// now what?

					Console.WriteLine("HopToChromeTab.VirtualOnCompleted after executeScript, sendMessage " + new { that.id });

					// send a SETI message?

					// https://developer.chrome.com/extensions/messaging
					//chrome.runtime.Message += (object message, chrome.MessageSender sender, IFunction sendResponse) =>
					//{
					//	Console.WriteLine("will invoke sendResponse... done??");

					//};

					that.id.connect(connectInfo: new { name = "jumpBack!" }).With(
						p =>
						{
							p.onMessage.addListener(
								IFunction.OfDelegate(
									new Action<object, chrome.MessageSender, IFunction>(
										(object message, chrome.MessageSender sender, IFunction sendResponse) =>
										{
											// 9846ms will invoke sendResponse... done?? {{ message = [object Object] }}

											Console.WriteLine("will invoke sendResponse... done! " + new { message });

											var rr = (ShadowIAsyncStateMachine)message;

											InternalInvoke(rr);
										}
									)
								)
							);


						}
					);

					//chrome.runtime.connect(
					//chrome.tabs.sendMessage(


					/// never returns a response? if we are not sending back no..
					var response = await that.id.sendMessage(
							//"hello"

							r.shadowstate
						);

					Console.WriteLine("will invoke sendResponse... done");

					// jumping back already? likely now into a new state machine tho, due to a onclick handler?
					Console.WriteLine("HopToChromeTab.VirtualOnCompleted after sendMessage " + new { response });

					// HopToChromeTab.VirtualOnCompleted after sendMessage {{ response = response }}

					// https://developer.chrome.com/extensions/messaging#connect



				};
				#endregion


				return;
			}
			#endregion

			// ok. now we are running inside the tab. lets set up the hop to iframe.
			Console.WriteLine("nop");


			var pendingPortalBack = new Dictionary<IHTMLIFrame, MessageEvent>();


			#region HopToIFrame
			HopToIFrame.VirtualOnCompleted = async (that, continuation) =>
			{
				//new IHTMLPre {
				//	"enter HopToIFrame.VirtualOnCompleted.."
				//}.AttachToDocument();

				var r = TestSwitchToServiceContextAsync.ShadowIAsyncStateMachine.ResumeableFromContinuation(continuation);

				if (pendingPortalBack.ContainsKey(that.frame))
				{
					var mm = pendingPortalBack[that.frame];

					Console.WriteLine("we are hoping back to the iframe via a pending portal...");
					// then forget the entry...
					pendingPortalBack.Remove(that.frame);

					mm.postMessage(r.shadowstate);

					return;
				}


				// X:\jsc.svn\examples\javascript\Test\TestSwitchToIFrame\TestSwitchToIFrame\Application.cs
				//var m = await that.frame.contentWindow.async.onmessage;

				// um for each iframe we should only await the first handshake once?
				var m = await that.frame.async.onmessage;

				//new IHTMLPre {
				//	" VirtualOnCompleted postMessageAsync " + new { r.shadowstate.state }
				//}.AttachToDocument();


				// um. we need to tell that iframe, where to jump to..
				//var firstmessageback = that.frame.contentWindow.postMessageAsync(r.shadowstate);


				m.postMessage(r.shadowstate);

				// will it work?
				that.frame.ownerDocument.defaultView.onmessage +=
					mm =>
					{
						Console.WriteLine("about to hop to parent... complete?");

						if (mm.source != that.frame.contentWindow)
							return;

						Console.WriteLine("enter that.frame.ownerDocument.defaultView.onmessage ");

						// remember that we can hop back.
						pendingPortalBack[that.frame] = mm;

						var shadowstate = (TestSwitchToServiceContextAsync.ShadowIAsyncStateMachine)mm.data;

						InternalInvoke(shadowstate);



					};

			};
			#endregion

			#region HopToParent
			HopToParent.VirtualOnCompleted = async (that, continuation) =>
			{
				// the state is in a member variable?

				var r = TestSwitchToServiceContextAsync.ShadowIAsyncStateMachine.ResumeableFromContinuation(continuation);

				// should not be a zero state
				// or do we have statemachine name clash?

				//new IHTMLPre {
				//	"enter HopToParent.VirtualOnCompleted.. " + new { r.shadowstate.state }
				//}.AttachToDocument();


				Console.WriteLine("about to hop to parent... "
					+ new { StringFields = r.shadowstate.StringFields.items.Length }

					);

				var m = await Native.window.parent.postMessageAsync(r.shadowstate);

				// 1205ms we are hoping back to the iframe via a pending portal... done, got data? {{ StringFields = 1 }}
				Console.WriteLine("we are hoping back to the iframe via a pending portal... done, got data? " +

					new { StringFields = m.data.StringFields.items.Length }
					);

				InternalInvoke(m.data);

				//Console.WriteLine("parent is hopping back here?");

				// we actually wont use the response yet..
				// will we get a jump back?
			};
			#endregion



			Console.WriteLine("nop");
		}



		static Action<IHTMLIFrame> fixHeight =
			async stackfix_iframe =>
			{
				do
				{
					stackfix_iframe.style.height = (Native.window.Height - 64) + "px";
				}
				while (await Native.window.async.onresize);
			};


		static Action<IHTMLIFrame> animateIFrame =
			 iframe =>
			{
				// wont work?
				new IStyle(Native.document.documentElement)
				{
					borderLeft = "rgba(0, 0, 255, 1) 1px solid",
				};


				iframe.onmouseover +=
					delegate
					{
						new IStyle(Native.document.documentElement)
						{
							borderLeft = "rgba(255, 0, 0, 1) 1px solid",
						};

						new IStyle(iframe)
						{
							left = "1px",
						};
					};

				iframe.onmouseout +=
					delegate
					{
						new IStyle(Native.document.documentElement)
						{
							borderLeft = "rgba(0, 0, 255, 1) 1px solid",
						};

						// hide toolbar!
						new IStyle(iframe)
						{
							left = "-5em",
						};
					};
			};

		// called also by hoping back to extension
		static void InternalInvoke(TestSwitchToServiceContextAsync.ShadowIAsyncStateMachine that)
		{

			//var m = await Native.window.parent.async.onmessage;
			//var shadowstate = (TestSwitchToServiceContextAsync.ShadowIAsyncStateMachine)m.data;

			//new IHTMLPre {
			//			new { shadowstate.state }
			//}.AttachToDocument();

			// about to invoke

			#region xAsyncStateMachineType
			var xAsyncStateMachineType = typeof(Application).Assembly.GetTypes().FirstOrDefault(
			item =>
			{
				// safety check 1

				//Console.WriteLine(new { sw.ElapsedMilliseconds, item.FullName });

				var xisIAsyncStateMachine = typeof(IAsyncStateMachine).IsAssignableFrom(item);
				if (xisIAsyncStateMachine)
				{
					//Console.WriteLine(new { item.FullName, isIAsyncStateMachine });

					return item.FullName == that.TypeName;
				}

				return false;
			}
		);
			#endregion


			var NewStateMachine = FormatterServices.GetUninitializedObject(xAsyncStateMachineType);
			var isIAsyncStateMachine = NewStateMachine is IAsyncStateMachine;

			var NewStateMachineI = (IAsyncStateMachine)NewStateMachine;

			#region 1__state
			xAsyncStateMachineType.GetFields(
				  System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance
			  ).WithEach(
			   AsyncStateMachineSourceField =>
			   {

				   Console.WriteLine(new { AsyncStateMachineSourceField });

				   if (AsyncStateMachineSourceField.Name.EndsWith("1__state"))
				   {
					   AsyncStateMachineSourceField.SetValue(
						   NewStateMachineI,
						   that.state
						);
				   }

				   // X:\jsc.svn\examples\javascript\async\Test\TestSwitchToServiceContextAsync\TestSwitchToServiceContextAsync\CLRWebServiceInvoke.cs
				   // field names/ tokens need to be encrypted like typeinfo.

				   // some do manual restore
				   // X:\jsc.svn\examples\javascript\chrome\extensions\ChromeExtensionHopToTabThenIFrame\ChromeExtensionHopToTabThenIFrame\Application.cs

				   // or, are we supposed to initialize a string value here?
				   var xStringField = that.StringFields.AsEnumerable().FirstOrDefault(
					   f => DecoratedString(f.FieldName) == DecoratedString(AsyncStateMachineSourceField.Name)
				   );

				   if (xStringField != null)
				   {
					   // once we are to go back to client. we need to reverse it?

					   AsyncStateMachineSourceField.SetValue(
						   NewStateMachineI,
						   xStringField.value
						);
					   // next xml?
					   // before lets send our strings back with the new state!
					   // what about exceptions?
				   }
			   }
		  );
			#endregion

			NewStateMachineI.MoveNext();
		}

		public Application(IApp page)
		{
			// X:\jsc.svn\examples\javascript\chrome\extensions\ChromeTabsExperiment\ChromeTabsExperiment\Application.cs

			dynamic self = Native.self;
			dynamic self_chrome = self.chrome;
			object self_chrome_tabs = self_chrome.tabs;

			#region self_chrome_tabs
			if (self_chrome_tabs != null)
			{
				Console.WriteLine("self_chrome_tabs");

				chrome.tabs.Updated += async (i, x, tab) =>
				{

					// chrome://newtab/

					// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201504/20150423

					#region tab
					if (tab == null)
					{
						Console.WriteLine("bugcheck :198 iframe? called with the wrong state?");
						return;
					}

					Console.WriteLine("enter async chrome.tabs.Updated " + new { tab.url, tab.status });


					if (tab.url.StartsWith("chrome-devtools://"))
					{
						return;
					}

					if (tab.url.StartsWith("chrome://"))
						return;


					// while running tabs.insertCSS: The extensions gallery cannot be scripted.
					if (tab.url.StartsWith("https://chrome.google.com/webstore/"))
						return;


					if (tab.status != "complete")
					{
						Console.WriteLine("exit async chrome.tabs.Updated, not complete?");
						return;
					}

					#endregion


					// where is the hop to iframe?
					// X:\jsc.svn\examples\javascript\Test\TestSwitchToIFrame\TestSwitchToIFrame\Application.cs


					// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201504/20150403

					//await (HopToChromeTab)tab.id;
					Console.WriteLine("chrome.tabs.Updated will delay, to increment state");

					// why do we need this fake await?
					// cannot resume state otherwise?
					await Task.Delay(1);

					Console.WriteLine("chrome.tabs.Updated will HopToChromeTab");
					// state1:
					await (HopToChromeTab)tab;
					//await tab.id;

					// are we now on the tab?
					// can we jump back?

					// what about jumping with files/uploads?
					Console.WriteLine("create iframe...");



					iframe = new IHTMLIFrame {
						//src = "about:blank"


						//new XElement("button", "did extension send us our code? " )

						new XElement("script", new XAttribute("src", url), " ")

					}.AttachTo(
					   Native.document.documentElement
					   );


					Console.WriteLine("create iframe... done");


					//iframe.allowTransparency = true;
					// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201504/20150423
					// __8__1 is null. why? a struct?
					//     b.__this.__8__1.scope = new ctor$vQEABrCB_bTmuL0jGQp_bnVw(b.__this._iframe_5__2);

					// oh crap. the container is initialized before await (HopToChromeTab)tab; thus lost due to context switch.
					var scope = new { iframe };

					//Console.WriteLine("iframe visible? " + new { scope });

					new IStyle(iframe)
					{
						borderWidth = "0",

						// wont animate?

						transition = "left 300ms linear",

						// dont want the white box while it loads..
						backgroundColor = "rgba(0, 0, 255, 0)",

						position = IStyle.PositionEnum.@fixed,


						//left = "1px",
						top = "32px",
						width = "6em",

						// animateIFrame
						// pre hide it
						left = "-5em",

						//height = "100px",

						// wont work on slashdot?
						//bottom = "3em"

						// can we be topmost?
						//zIndex = 30000
						zIndex = 1999999999

					};

					fixHeight(iframe);
					animateIFrame(iframe);

					//var __iframe = iframe;
					await iframe.async.onload;




					//new IStyle(iframe)
					//{
					//	backgroundColor = "rgba(0, 0, 255, 0.7)",
					//};

					Console.WriteLine("lets hop from tab context to iframe context... ");
					await (HopToIFrame)iframe;

					Console.WriteLine("lets hop from tab context to iframe context... done!");


					new IStyle(Native.document.body)
					{
						transition = "background-color 300ms linear",
						backgroundColor = "rgba(0, 0, 255, 0.1)",
					};




					var button4 = new IHTMLButton { "hop to parent, extension, app" }.AttachToDocument();

					new IStyle(button4)
					{
						display = IStyle.DisplayEnum.block
					};

					// not yet. need a new example
					button4.disabled = true;



					//Native.body.backgroundColor = "rgba(0, 0, 255, 0.7)";

					//var text3 = new IHTMLTextArea { value = "hey" }.AttachToDocument();

					// need to use static, as scope variables are being encapsulated, and due to context jumps we dont support it yet
					text3 = new IHTMLTextArea { value = "hey" }.AttachToDocument();

					var button3 = new IHTMLButton { "hop to parent, extension" }.AttachToDocument();

					new IStyle(button3)
					{
						display = IStyle.DisplayEnum.block
					};

					button3.onclick += async delegate
					{
						// can we hop back?

						var data3 = text3.value;
						Console.WriteLine("enter button3 " + new { data3 });
						// 2602ms enter button3 {{ data3 = hey }}

						// why do we need this fake await?
						// cannot resume state otherwise?
						await Task.Delay(1);

						await default(HopToParent);


						var tab_title = Native.document.title;

						// 3105ms are strings being synchronized to iframe? {{ tab_title = IANA — IANA-managed Reserved Domains, data3 = null }}
						Console.WriteLine("are strings being synchronized from iframe? " + new { tab_title, data3 });

						/// 2926ms will hop back to extension!? can we resume later?
						await default(HopToExtension);

						//Native.window.alert("hi! i am back home! do we even know which tab we were in? did i get some data yet? " + new { tab_title });
						Console.WriteLine("hi! i am back home! do we even know which tab we were in? did i get some data yet? " + new { tab_title, data3 });
						// 13844ms hi! i am back home! do we even know which tab we were in? did i get some data yet? {{ tab_title = IANA — IANA-managed Reserved Domains }}

						// lets guess which tab we clicked on?
						var tt = await chrome.tabs.getCurrent();

						new chrome.Notification(
							title: new { data3, tt.url }.ToString()

						);


						// can we do appwindow here or we need to jump out of extension?
					};


					var button2 = new IHTMLButton { "hop to parent" }.AttachToDocument();

					new IStyle(button2)
					{
						display = IStyle.DisplayEnum.block
					};

					button2.onclick += async delegate
					{
						// can we hop back?

						Console.WriteLine("enter button2");

						// why do we need this fake await?
						// cannot resume state otherwise?
						await Task.Delay(1);

						await default(HopToParent);


						var tab_title = Native.document.title;

						Console.WriteLine("are strings being synchronized to iframe? " + new { tab_title });


						// we are jumping back, yet into a new state machine.
						//Native.window.alert("hi! in a tab, in an extension! " + new
						//{
						//	Native.document.title
						//	,
						//	iframe
						//});

						// can we jump back? is the iframe awaiting for multiple jumps?
						await (HopToIFrame)iframe;

						Native.window.alert("hi! iframe, in a tab, in an extension! " + new { Native.document.title, tab_title });

						// ok. we now know how to
						// jump from extension to tab to iframe to tab to iframe

						// what about workers, and jumping back to extension?


						// but likely we did not jump to the same data?


					};

					var button1 = new IHTMLButton { "click me" }.AttachToDocument();

					new IStyle(button1)
					{
						display = IStyle.DisplayEnum.block
					};

					//new { }.With(
					//	async delegate
					//	{
					//		// stack variables not visible, why?
					//		while (await button1.async.onmouseover)
					//		{
					//			new IStyle(Native.document.body)
					//			{
					//				backgroundColor = "rgba(0, 0, 255, 0.8)",
					//			};

					//			await button1.async.onmouseout;

					//			new IStyle(Native.document.body)
					//			{
					//				backgroundColor = "rgba(0, 0, 255, 0.2)",
					//			};
					//		}
					//	}
					//);

					//oldschool as a workaround


					button1.onclick += delegate
					{
						Native.window.alert("hi! iframe, in a tab, in an extension! " + new { Native.document.title });

					};


					button1.onmouseover +=
						e =>
						{
							new IStyle(Native.document.body)
							{
								backgroundColor = "rgba(255, 0, 0, 0.9)",
							};

							e.stopPropagation();
						};

					button1.onmouseout +=
						delegate
						{
							new IStyle(Native.document.body)
							{
								backgroundColor = "rgba(0, 0, 255, 0.1)",
							};
						};


					// we are in iframe!
					Native.document.onmouseover +=
						delegate
						{
							new IStyle(Native.document.body)
							{
								backgroundColor = "rgba(0, 0, 255, 0.3)",
							};
						};

					Native.document.onmouseout +=
						delegate
						{
							new IStyle(Native.document.body)
							{
								backgroundColor = "rgba(0, 0, 255, 0.1)",
							};
						};




					//var f = new IHTMLButton { "in the frame! click to notify parent" }.AttachToDocument();

					//await default(HopToParent);


					// why need this stackfix?
					//var stackfix_iframe = iframe;

					//               new { }.With(
					//	async delegate
					//	{
					//		while (await Native.window.async.onresize)
					//		{
					//			stackfix_iframe.style.height = (Native.window.Height - 64) + "px";
					//		}
					//	}
					//);


					// X:\jsc.svn\examples\javascript\Test\TestHopFromIFrame\TestHopFromIFrame\Application.cs
					// can we jump?

					// <div class="player-video-title">Ariana Grande - One Last Time (Official)</div>

					//Native.document.title = "(" + Native.document.title + ")";

					// X:\jsc.svn\examples\javascript\xml\FindByClassAndObserve\FindByClassAndObserve\Application.cs

					// luckyly its only hidden... no need to await the element and find it later

					// <span id="eow-title" class="watch-title " dir="ltr" title="THORnews Weird Weather Watch! Wind Dragon inbound to USA Pacific Coast!">


					//var yt0 = Native.document.querySelectorAll(" [class='player-video-title']");
					//var yt1 = Native.document.querySelectorAll(" [class='watch-title ']");

					//yt0.Concat(yt1).WithEach(
					//	 async e =>
					//	 {
					//		 do
					//		 {
					//			 Native.document.title = e.innerText;

					//			 // X:\jsc.svn\examples\javascript\chrome\extensions\ChromeExtensionHopToTabThenIFrame\ChromeExtensionHopToTabThenIFrame\Application.cs
					//			 // we would need to jump back here to do extension notification
					//			 // the jump back would be to another state machine tho
					//			 // we would need other ports opened?

					//			 Native.document.documentElement.style.borderLeft = "1em solid yellow";
					//			 for (int xi = 0; xi < 5; xi++)
					//			 {
					//				 Native.body.style.borderLeft = "1em solid yellow";
					//				 await Task.Delay(100);
					//				 Native.body.style.borderLeft = "1em solid black";
					//				 await Task.Delay(100);
					//			 }
					//			 Native.document.documentElement.style.borderLeft = "1em solid red";

					//			 // or actually instead of jumping back we need to send back progress?
					//		 }
					//		 while (await e.async.onmutation);
					//	 }
					// );

					// lets start monitoring
				};



				return;
			}
			#endregion

			// 420ms TypeError: Cannot read property 'url' of null

			Console.WriteLine("nop");

#if true


			#region inside iframe
			if (Native.window.parent != Native.window)
			{
				// X:\jsc.svn\examples\javascript\chrome\extensions\ChromeExtensionHopToTabThenIFrame\ChromeExtensionHopToTabThenIFrame\Application.cs
				// inside iframe

				new { }.With(
					async delegate
					{
						// start the handshake
						// we gain intellisense, but the type is partal, likely not respawned, acivated, initialized 
						//var m = await Native.window.parent.postMessageAsync<TestSwitchToServiceContextAsync.ShadowIAsyncStateMachine>();

						//	new IHTMLPre {
						//			"inside iframe awaiting onmessage"
						//}.AttachToDocument();

						// first null jump

						//Native.window.parent.postMessage(r.shadowstate);

						var m = await Native.window.parent.postMessageAsync(default(TestSwitchToServiceContextAsync.ShadowIAsyncStateMachine));

						Console.WriteLine("iframe got the first state...");
						// we can now send one hop back?
						InternalInvoke(m.data);
					}
				);

				return;
			}
			#endregion

#endif

			Console.WriteLine("nop");

			// we made the jump?
			//Native.body.style.borderLeft = "0em solid red";

			// yes we did. can we talk to the chrome extension?

			// lets do some SETI

			// The runtime.onMessage event is fired in each content script running in the specified tab for the current extension.

			// Severity	Code	Description	Project	File	Line
			//Error       'runtime.onMessage' is inaccessible due to its protection level ChromeExtensionHopToTabThenIFrame X:\jsc.svn\examples\javascript\chrome\extensions\ChromeExtensionHopToTabThenIFrame\ChromeExtensionHopToTabThenIFrame\Application.cs 272

			// public static event System.Action<object, object, object> Message

			Action<object> PostToExtension = null;

			chrome.runtime.Connect +=
				 port =>
				 {
					 Console.WriteLine("chrome.runtime.Connect " + new { port.name, port.sender });

					 // 8295ms will invoke sendResponse... done?? {{ message = jump! }}
					 //port.postMessage("jump! " + new { port.name, port.sender });

					 PostToExtension = xdata =>
					 {
						 // PostToExtension {{ xdata = [object Object] }}
						 Console.WriteLine("PostToExtension " + new { xdata });
						 //Console.WriteLine("PostToExtension " + new { xdata = JSON.stringify(xdata) });

						 port.postMessage(xdata);
					 };
				 };

			#region chrome.runtime.Message
			chrome.runtime.Message += (object message, chrome.MessageSender sender, IFunction sendResponse) =>
			{
				var s = (TestSwitchToServiceContextAsync.ShadowIAsyncStateMachine)message;

				Console.WriteLine("chrome.runtime.Message " + new { s.state, sender.id, code = s.code.Length });
				// at this point we can initialize hop to iframe since we have the code we can actually use?

				// 276ms xonmessage {{ state = 1, id = fkgibadjpabiongmgoeomdbcefhabmah, code = 3296612 }}

				#region url
				var aFileParts = new[] { s.code };
				var oMyBlob = new Blob(aFileParts, new { type = "text/javascript" });
				//var url = oMyBlob.ToObjectURL();
				url = URL.createObjectURL(oMyBlob);
				#endregion



				#region xAsyncStateMachineType
				var xAsyncStateMachineType = typeof(Application).Assembly.GetTypes().FirstOrDefault(
				item =>
				{
					// safety check 1

					//Console.WriteLine(new { sw.ElapsedMilliseconds, item.FullName });

					var xisIAsyncStateMachine = typeof(IAsyncStateMachine).IsAssignableFrom(item);
					if (xisIAsyncStateMachine)
					{
						//Console.WriteLine(new { item.FullName, isIAsyncStateMachine });

						return item.FullName == s.TypeName;
					}

					return false;
				}
			);
				#endregion


				var NewStateMachine = FormatterServices.GetUninitializedObject(xAsyncStateMachineType);
				var isIAsyncStateMachine = NewStateMachine is IAsyncStateMachine;

				var NewStateMachineI = (IAsyncStateMachine)NewStateMachine;

				#region 1__state
				xAsyncStateMachineType.GetFields(
					  System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance
				  ).WithEach(
				   AsyncStateMachineSourceField =>
				   {

					   Console.WriteLine(new { AsyncStateMachineSourceField });

					   if (AsyncStateMachineSourceField.Name.EndsWith("1__state"))
					   {
						   Console.WriteLine(new { AsyncStateMachineSourceField, s.state });

						   AsyncStateMachineSourceField.SetValue(
						   NewStateMachineI,
						   s.state
						);
					   }


				   }
			  );
				#endregion

				Console.WriteLine("will enter the state machine...");
				NewStateMachineI.MoveNext();


				Console.WriteLine("will we want to jump back to extension?");

				HopToExtension.VirtualOnCompleted +=
					(that, continuation) =>
					{
						Console.WriteLine("will hop back to extension!? can we resume later?");

						var r = TestSwitchToServiceContextAsync.ShadowIAsyncStateMachine.ResumeableFromContinuation(continuation);

						// can we only send once?
						//Console.WriteLine("will invoke sendResponse... " + new { sendResponse });
						Console.WriteLine("will invoke sendResponse... ");
						//sendResponse.apply(null, r);

						//will invoke sendResponse...
						//(program):51533 1664ms enter catch
						//{
						//	mname = < 0154 > ldarg.0.try } ClauseCatchLocal:
						//	(program):51533 1664ms TypeError: Converting circular structure to JSON

						// planB

						PostToExtension(r.shadowstate);
					};

				//Task.Delay(1000).ContinueWith(
				//	delegate
				//	{
				//		sendResponse.apply(null, "response");
				//	}
				//);

			};
			#endregion


			//         Native.window.onmessage += e =>
			//{

			//};
		}

	}
}
