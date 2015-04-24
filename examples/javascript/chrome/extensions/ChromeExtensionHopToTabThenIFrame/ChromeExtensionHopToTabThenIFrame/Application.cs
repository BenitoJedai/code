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
using System.Diagnostics;

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
		static string ztabIdString;
		static Dictionary<string, IHTMLPre> lookup_info = new Dictionary<string, IHTMLPre>();
		static Dictionary<string, bool> lookup_info_YTmissing = new Dictionary<string, bool>();


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

				var FromExtensionToTab = new Dictionary<int, object>();

				#region HopToChromeTab.VirtualOnCompleted
				HopToChromeTab.VirtualOnCompleted = async (that, continuation) =>
				{
					//Console.WriteLine("HopToChromeTab.VirtualOnCompleted ");
					Console.WriteLine("HopToChromeTab.VirtualOnCompleted " + new { that.id });

					// async dont like ref?
					var r = TestSwitchToServiceContextAsync.ShadowIAsyncStateMachine.ResumeableFromContinuation(continuation);


					if (FromExtensionToTab.ContainsKey((int)(object)that.id))
					{
						Console.WriteLine("jumping back to tab for more?");

						that.id.sendMessage(
							//"hello"

							r.shadowstate
						);

						return;
					}

					FromExtensionToTab[(int)(object)that.id] = new object();

					// um. now what?
					// send shadowstate over?
					// first we have to open a channel

					// do we have our view-source yet?
					var code = await codetask;

					// 5240ms HopToChromeTab.VirtualOnCompleted {{ id = 449, state = 1, Length = 3232941 }}
					// 226632ms HopToChromeTab.VirtualOnCompleted {{ id = 95, state = 0, Length = 3254419 }}
					//Console.WriteLine("HopToChromeTab.VirtualOnCompleted " + new { that.id, r.shadowstate.state, code.Length });

					//if (r.shadowstate.state == 0)
					//	Console.WriteLine("HopToChromeTab.VirtualOnCompleted bugcheck. state 0?");

					//// how can we inject ourselves and send a signal back to set this thing up?

					//// https://developer.chrome.com/extensions/tabs#method-executeScript
					//// https://developer.chrome.com/extensions/tabs#type-InjectDetails
					//// https://developer.chrome.com/extensions/content_scripts#pi

					//// Content scripts execute in a special environment called an isolated world. 
					//// They have access to the DOM of the page they are injected into, but not to any JavaScript variables or 
					//// functions created by the page. It looks to each content script as if there is no other JavaScript executing
					//// on the page it is running on. The same is true in reverse: JavaScript running on the page cannot call any 
					//// functions or access any variables defined by content scripts.

					// only for the very first jump~
					r.shadowstate.code = code;

					var result = await that.id.executeScript(
						//new { file = url }
						new { code }
					);

					// now what?
					// will the tab get our strings?
					Console.WriteLine("HopToChromeTab.VirtualOnCompleted after executeScript, sendMessage " + new { that.id, StringFields = r.shadowstate.StringFields.items.Length });

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

				new IStyle(iframe)
				{
					border = "1px solid blue"
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
							left = "-1px",
							border = "1px solid red"

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
							border = "1px solid blue"
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
				Console.WriteLine("extension is now running...");

				#region isYTMissing
				new { }.With(
					async delegate
					{
						var vid = "TXExg6Xj3aA";

						var thumbnail = $"https://img.youtube.com/vi/{vid}/0.jpg";

						var thumbnailImage = new IHTMLImage
						{
							src = thumbnail
						};

						// wont get those events for 404?

						//Native.window.onerror += err =>
						//{
						//	Console.WriteLine(
						//		"window onerror " +

						//		new
						//		{
						//			thumbnail,
						//			err,

						//			thumbnailImage.complete,
						//			thumbnailImage.width,
						//			thumbnailImage.naturalWidth,
						//			thumbnailImage.naturalHeight
						//		}
						//	);

						//};

						//thumbnailImage.onerror += err =>
						//{
						//	Console.WriteLine(
						//		"thumbnailImage onerror " +

						//		new
						//		{
						//			thumbnail,
						//			err,

						//			thumbnailImage.complete,
						//			thumbnailImage.width,
						//			thumbnailImage.naturalWidth,
						//			thumbnailImage.naturalHeight
						//		}
						//	);

						//};


						await thumbnailImage.async.oncomplete;


						Console.WriteLine(
							new { thumbnail, thumbnailImage.complete, thumbnailImage.width, thumbnailImage.naturalWidth, thumbnailImage.naturalHeight }
						);


						var thumbnailBytes = await thumbnailImage.async.bytes;

						Console.WriteLine(
							new { thumbnailBytes.Length }
						);


						// crc?
						var sw = Stopwatch.StartNew();

						var crc = CRCExample.ActionScript.Crc32Helper.GetCrc32(thumbnailBytes);

						// 247ms {{ crc = 9e47636d, ElapsedMilliseconds = 7 }}

						var isYTMissing = 0x9e47636d == crc;

						Console.WriteLine(
								new { crc = crc.ToString("x8"), sw.ElapsedMilliseconds, isYTMissing }
							  );

					}
				);
				#endregion

				var oncePerTab = new Dictionary<TabIdInteger, object>();

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

					if (oncePerTab.ContainsKey(tab.id))
						return;

					oncePerTab[tab.id] = new object();

					// where is the hop to iframe?
					// X:\jsc.svn\examples\javascript\Test\TestSwitchToIFrame\TestSwitchToIFrame\Application.cs


					// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201504/20150403

					//await (HopToChromeTab)tab.id;
					Console.WriteLine("chrome.tabs.Updated will delay, to increment state");

					// why do we need this fake await?
					// cannot resume state otherwise?
					await Task.Delay(1);

					// 
					// X:\jsc.svn\market\synergy\javascript\chrome\chrome\chrome.idl
					var tabIdString = Convert.ToString((object)tab.id);

					// 13032ms chrome.tabs.Updated will HopToChromeTab {{ tabIdString = 608 }}
					Console.WriteLine("chrome.tabs.Updated will HopToChromeTab " + new { tabIdString });
					// state1:
					await (HopToChromeTab)tab;

					// TypeError: Cannot set property 'ztabIdString' of null
					ztabIdString = tabIdString;

					//await tab.id;

					// are we now on the tab?
					// can we jump back?

					// 531ms yt found {{ Length = 108 }}
					// X:\jsc.svn\examples\javascript\test\TestShadowForIFrame\TestShadowForIFrame\Application.cs
					//var yt1 = Native.document.querySelectorAll(" [class='youtube-player']");

					// <iframe width="420" height="315" src="https://www.youtube.com/embed/sJIh70IZua8" frameborder="0" allowfullscreen=""></iframe>

					// 503ms create iframe... {{ ztabIdString = null }}
					// what about jumping with files/uploads?
					Console.WriteLine("create iframe... " + new { ztabIdString });



					iframe = new IHTMLIFrame {
						//src = "about:blank"


						//new XElement("button", "did extension send us our code? " )

						new XElement("script", new XAttribute("src", url), " ")

					}.AttachTo(
					   Native.document.documentElement
					   );


					var yt1 = Native.document.querySelectorAll("iframe");

					Console.WriteLine("yt found " + new { yt1.Length });
					yt1.WithEach(
						  async e =>
						  {
							  //if (e == null)
							  // return;

							  var xiframe = (IHTMLIFrame)e;

							  //13ms yt found { { Length = 9 } }
							  //VM1190: 51533 515ms enter catch
							  //{
							  // mname = < 01ed > ldloca.s.try } ClauseCatchLocal:
							  // VM1190: 51533 515ms TypeError: Cannot read property 'src' of null


							  if (!xiframe.src.StartsWith("https://www.youtube.com/"))
							  {
								  return;
							  }

							  // can we interact, swap the videos?

							  var size = new { xiframe.clientWidth, xiframe.clientHeight };

							  var swap = new IHTMLDiv
							  {
								  //new IHTMLPre { xiframe.src },

								  //new IHTMLContent {  }
							  };

							  new IStyle(swap)
							  {
								  backgroundColor = "yellow",

								  width = size.clientWidth + "px",
								  height = size.clientHeight + "px",
								  overflow = IStyle.OverflowEnum.hidden

							  };

							  // https://code.google.com/p/dart/issues/detail?id=19561
							  // 27ms HierarchyRequestError: Failed to execute 'createShadowRoot' on 'Element': Author-created shadow roots are disabled for this element.
							  //swap.AttachTo(xiframe.shadow);

							  xiframe.ReplaceWith(swap);



							  var sh = new IHTMLDiv { }.AttachTo(swap.shadow);

							  var vid = xiframe.src.TakeUntilIfAny("?").SkipUntilOrEmpty("/embed/");




							  //						  Remote Address:[2a00: 1450:400f:802::100e]:443
							  //Request URL: https://img.youtube.com/vi/TXExg6Xj3aA/0.jpg
							  //Request Method: GET
							  //Status Code: 404 OK


							  var thumbnail404 = $"https://img.youtube.com/vi/{vid}/0.jpg";
							  // lets look at the image. is the video removed?
							  // https://img.youtube.com/vi/TXExg6Xj3aA/0.jpg

							  // http://stackoverflow.com/questions/22097747/getimagedata-error-the-canvas-has-been-tainted-by-cross-origin-data

							  //.AttachToHead();


							  new IStyle(sh)
							  {
								  width = size.clientWidth + "px",
								  height = size.clientHeight + "px",
								  overflow = IStyle.OverflowEnum.hidden,
								  position = IStyle.PositionEnum.relative,

								  backgroundImage = $"url('{thumbnail404}')",

							  };

							  //var info = new IHTMLPre { xiframe.src };
							  var info = new IHTMLPre { new { vid } };
							  // so we can later find it again...
							  lookup_info[thumbnail404] = info;

							  // assume its not missing until we know more
							  lookup_info_YTmissing[thumbnail404] = false;

							  info.AttachTo(sh);


							  new IStyle(info)
							  {
								  backgroundColor = "rgba(0,0,255, 0.5)",
								  color = "rgba(255,255,0, 0.9)",

								  left = "0px",
								  bottom = "0px",
								  right = "0px",
								  //height = size.clientHeight + "px",

								  position = IStyle.PositionEnum.absolute
							  };

							  // at this point we need to consult the extension as it can download the bytes

							  new { }.With(
								  async delegate
								  {
									  var xtabIdString = ztabIdString;
									  var xthumbnail404 = thumbnail404;

									  // 517ms about to jump to extension to inspect the damn image... {{ xthumbnail404 = https://img.youtube.com/vi/TXExg6Xj3aA/0.jpg, xtabIdString = 608 }}
									  Console.WriteLine("about to jump to extension to inspect the damn image... " + new { xthumbnail404, xtabIdString });
									  // fixup?
									  await Task.Delay(1);

									  await default(HopToExtension);


									  Console.WriteLine("about to jump to extension to inspect the damn image... done " + new { xthumbnail404, xtabIdString });
									  // 43228ms about to jump to extension to inspect the damn image... done {{ xthumbnail404 = https://img.youtube.com/vi/TXExg6Xj3aA/0.jpg, xtabIdString = 608 }}



									  // can we jump back once we know what it was?
									  var thumbnailImage = new IHTMLImage
									  {
										  src = xthumbnail404
									  };

									  await thumbnailImage.async.oncomplete;
									  var thumbnailBytes = await thumbnailImage.async.bytes;
									  var sw = Stopwatch.StartNew();
									  var crc = CRCExample.ActionScript.Crc32Helper.GetCrc32(thumbnailBytes);

									  // we only do strings at this point, not integers, we could do booleans tho...
									  var isYTMissing = Convert.ToString(0x9e47636d == crc);

									  var xtab = await chrome.tabs.get(tabId: Convert.ToInt32(xtabIdString));

									  //xthumbnail404 = https://img.youtube.com/vi/BZIeqnQ1rY0/0.jpg, isYTMissing = false, xtabIdString = 608, url = https://zproxy.wordpress.com/2015/04/05/thought-form-magicians/ }}
									  //  xthumbnail404 = https://img.youtube.com/vi/TXExg6Xj3aA/0.jpg, isYTMissing = true, xtabIdString = 608, url = https://zproxy.wordpress.com/2015/04/05/thought-form-magicians/ }}

									  Console.WriteLine(">> " + new { xthumbnail404, isYTMissing, xtabIdString, xtab.url });

									  // can we jump back to the tab to color it red if missing?
									  // 43340ms >> {{ xthumbnail404 = https://img.youtube.com/vi/TXExg6Xj3aA/0.jpg, isYTMissing = true, xtabIdString = 608 }}

									  //(HopToChromeTab)

									  // ok now we now what tab we are in but even if we jump back. we wont have a ref to the infobar?

									  await (HopToChromeTab)xtab;

									  Console.WriteLine(">> " + new { xthumbnail404, isYTMissing, xtabIdString } + " back in the tab?");

									  // would the old variables be reconnected for us?
									  // jumps should be able to resume an old startemachine.

									  lookup_info_YTmissing[xthumbnail404] = Convert.ToBoolean(isYTMissing);

									  var xinfo = lookup_info[xthumbnail404];

									  //xthumbnail404 = https://img.youtube.com/vi/BZIeqnQ1rY0/0.jpg, isYTMissing = false, xtabIdString = 744 }} back in the tab?

									  if (Convert.ToBoolean(isYTMissing))
									  {
										  // yikes. video pulled!
										  new IStyle(xinfo)
										  {
											  backgroundColor = "rgba(255,0,0, 0.7)",
										  };

										  new IHTMLSpan { " do we have a local copy?" }.AttachTo(xinfo);

										  // we should send udp to the archive server?
										  // if it has how can we load it up?

										  return;
									  }

									  new IStyle(xinfo)
									  {
										  backgroundColor = "rgba(255,255,0, 0.5)",
									  };



								  }
							  );

							  // https://www.youtube.com/embed/jQLNMljadyo?vers
							  // https://www.youtube.com/embed/M4RBW85J4Cg
							  // http://img.youtube.com/vi/<insert-youtube-video-id-here>/0.jpg



							  await sh.async.onmouseover;

							  // no reason to load it. its missing!
							  if (lookup_info_YTmissing[thumbnail404])
								  return;

							  xiframe.AttachTo(sh);
							  info.AttachTo(sh);

							  var copyToToolbar = new IHTMLButton { "+" }.AttachTo(info);


							  await copyToToolbar.async.onclick;
							  copyToToolbar.Orphanize();

							  //width = "128px",
							  //xiframe.style.transform = "scale(0.2)";

							  new IStyle(xiframe)
							  {
								  width = (128) + "px",
								  height = (96) + "px",
							  };

							  // This video contains content from WMG. It is restricted from playback on certain sites.
							  xiframe.AttachTo(
								//iframe.contentWindow.document.documentElement
								iframe.contentWindow.document.body
							  );



						  }
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
						width = "128px",

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
						margin = "0px",
						//marginRight = "1em",

						padding = "0px",
						paddingRight = "1em",

						transition = "background-color 300ms linear",
						backgroundColor = "rgba(0, 0, 255, 0.1)",

						//border = "1px solid red"
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
					text3.style.width = "100%";

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

						// https://developer.chrome.com/extensions/tabs
						// https://developer.chrome.com/extensions/windows
						// https://developer.chrome.com/extensions/processes

						//chrome.runtime.
						new chrome.Notification(
							title: new { data3, tt }.ToString()

						);


						//chrome.tabs.rel

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

			#region Connect
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
			#endregion

			#region chrome.runtime.Message
			chrome.runtime.Message += (object message, chrome.MessageSender sender, IFunction sendResponse) =>
			{
				var s = (TestSwitchToServiceContextAsync.ShadowIAsyncStateMachine)message;

				if (url != null)
				{
					Console.WriteLine("jumping back to tab for more? done");

					InternalInvoke(s);

					return;
				}

				Console.WriteLine("chrome.runtime.Message " + new { s.state, sender.id, code = s.code.Length });
				// at this point we can initialize hop to iframe since we have the code we can actually use?

				// 276ms xonmessage {{ state = 1, id = fkgibadjpabiongmgoeomdbcefhabmah, code = 3296612 }}

				#region url
				var aFileParts = new[] { s.code };
				var oMyBlob = new Blob(aFileParts, new { type = "text/javascript" });
				//var url = oMyBlob.ToObjectURL();
				url = URL.createObjectURL(oMyBlob);
				#endregion



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


				InternalInvoke(s);



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
