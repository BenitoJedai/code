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
using ChromeExtensionHopToTab;
using ChromeExtensionHopToTab.Design;
using ChromeExtensionHopToTab.HTML.Pages;
using chrome;
using System.Net;

namespace ChromeExtensionHopToTab
{
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
		// jsc should package displayName  the end of the view-source?
		// should we gzip the string lookup?

		static Application()
		{
			// what about console? consolidate all core apps into one?

			// 0ms  cctor Application did we make the jump yet? {{ href = http://example.com/ }}
			Console.WriteLine(" cctor Application did we make the jump yet? " + new { Native.document.location.href });



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
				await that.id.insertCSS(
						new
						{
							code = @"

				html { 
				border-left: 1em solid yellow;
				}


				"
						}
					);


				// where is it defined?
				// X:\jsc.svn\examples\javascript\async\Test\TestSwitchToServiceContextAsync\TestSwitchToServiceContextAsync\ShadowIAsyncStateMachine.cs

				// async dont like ref?
				var r = TestSwitchToServiceContextAsync.ShadowIAsyncStateMachine.ResumeableFromContinuation(continuation);

				// um. now what?
				// send shadowstate over?
				// first we have to open a channel

				// do we have our view-source yet?
				var code = await codetask;

				// 5240ms HopToChromeTab.VirtualOnCompleted {{ id = 449, state = 1, Length = 3232941 }}
				Console.WriteLine("HopToChromeTab.VirtualOnCompleted " + new { that.id, r.shadowstate.state, code.Length });

				//// how can we inject ourselves and send a signal back to set this thing up?

				//// https://developer.chrome.com/extensions/tabs#method-executeScript
				//// https://developer.chrome.com/extensions/tabs#type-InjectDetails
				//// https://developer.chrome.com/extensions/content_scripts#pi

				//// Content scripts execute in a special environment called an isolated world. 
				//// They have access to the DOM of the page they are injected into, but not to any JavaScript variables or 
				//// functions created by the page. It looks to each content script as if there is no other JavaScript executing
				//// on the page it is running on. The same is true in reverse: JavaScript running on the page cannot call any 
				//// functions or access any variables defined by content scripts.

				var result = await that.id.executeScript(
					//new { file = url }
					new { code }
				);

				// now what?
			};
			#endregion

		}

		public Application(IApp page)
		{
			// X:\jsc.svn\examples\javascript\chrome\extensions\ChromeTabsExperiment\ChromeTabsExperiment\Application.cs

			dynamic self = Native.self;
			dynamic self_chrome = self.chrome;
			object self_chrome_tabs = self_chrome.tabs;

			//if (self_chrome_tabs == null)
			//	return;


			//	....488: { SourceMethod = Void.ctor(ChromeExtensionHopToTab.HTML.Pages.IApp), i = [0x00ba] brtrue.s + 0 - 1 }
			//1984:02:01 RewriteToAssembly error: System.ArgumentException: Value does not fall within the expected range.
			//at jsc.ILInstruction.ByOffset(Int32 i) in X:\jsc.internal.git\compiler\jsc\CodeModel\ILInstruction.cs:line 1184
			//at jsc.ILInstruction.get_BranchTargets() in X:\jsc.internal.git\compiler\jsc\CodeModel\ILInstruction.cs:line 1225

			#region self_chrome_tabs
			if (self_chrome_tabs != null)
			{

				chrome.tabs.Updated += async (i, x, tab) =>
				{
					// chrome://newtab/

					if (tab.url.StartsWith("chrome-devtools://"))
						return;

					if (tab.url.StartsWith("chrome://"))
						return;


					// while running tabs.insertCSS: The extensions gallery cannot be scripted.
					if (tab.url.StartsWith("https://chrome.google.com/webstore/"))
						return;


					if (tab.status != "complete")
						return;

					new chrome.Notification
					{
						Message = "chrome.tabs.Updated " + new
						{
							tab.id,
							tab.url,
							tab.status,
							tab.title
						}
					};


					// while running tabs.insertCSS: The tab was closed.

					// 		public static Task<object> insertCSS(this TabIdInteger tabId, object details);
					// public static void insertCSS(this TabIdInteger tabId, object details, IFunction callback);


					// for some sites the bar wont show as they html element height is 0?
					await tab.id.insertCSS(
								new
								{
									code = @"

	html { 
	border-left: 1em solid cyan;
	padding-left: 1em; 
	}


	"
								}
							);

					Console.WriteLine(
						"insertCSS done " + new { tab.id, tab.url }
						);


					// where is the hop to iframe?
					// X:\jsc.svn\examples\javascript\Test\TestSwitchToIFrame\TestSwitchToIFrame\Application.cs


					// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201504/20150403

					//await (HopToChromeTab)tab.id;
					await (HopToChromeTab)tab;
					//await tab.id;

					// are we now on the tab?
					// can we jump back?

					Console.WriteLine("// are we now on the tab yet?");
				};



				return;
			}
			#endregion


			// ?
		}

	}
}
