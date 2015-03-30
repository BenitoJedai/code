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
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestSwitchToIFrame;
using TestSwitchToIFrame.Design;
using TestSwitchToIFrame.HTML.Pages;
using TestSwitchToServiceContextAsync;

namespace TestSwitchToIFrame
{
	public struct HopToIFrame : System.Runtime.CompilerServices.INotifyCompletion
	{
		// basically we have to hibernate the current state to resume
		public HopToIFrame GetAwaiter() { return this; }
		public bool IsCompleted { get { return false; } }

		public static Action<Action> VirtualOnCompleted;
		public void OnCompleted(Action continuation) { VirtualOnCompleted(continuation); }

		public void GetResult() { }
	}

	/// <summary>
	/// Your client side code running inside a web browser as JavaScript.
	/// </summary>
	public sealed class Application : ApplicationWebService
	{
		static Application()
		{
			// patch the awaiter..
			Console.SetOut(new xConsole());

			HopToIFrame.VirtualOnCompleted =
			 continuation =>
			 {
				 Console.WriteLine("enter VirtualOnCompleted..");

				 Action<ShadowIAsyncStateMachine> MoveNext = null;

				 var s = ShadowIAsyncStateMachine.FromContinuation(continuation, ref MoveNext);

				 // we want to run in it!
				 new IHTMLIFrame { src = Native.document.location.href }.With(
					 async f =>
					 {
						 f.style.width = "100%";
						 f.style.height = "30em";

						 var sw = Stopwatch.StartNew();

						 //var mm = f.contentWindow.async.onmessage;
						 await f.async.onload;

						 Console.WriteLine("loaded in " + new { sw.ElapsedMilliseconds });

						 // loaded in {{ ElapsedMilliseconds = 2417 }}

						 //var m = await f.contentWindow.async.onmessage;
						 //var m = await Native.window.async.onmessage;
						 //var m = await f.ownerDocument.defaultView.async.onmessage;
						 //var m = await f.ownerDocument.defaultView.async.onmessage;
						 var m = await f.async.onmessage;

						 //var m = await mm;

						 Console.WriteLine("onmessage in " + new { sw.ElapsedMilliseconds });

						 // wait for response?
						 m.postMessage(s);
					 }
				 ).AttachToDocument();

			 };

		}

		public Application(IApp page)
		{
			Native.body.Clear();


			// X:\jsc.svn\examples\javascript\WebMessagingExample\WebMessagingExample\Application.cs

			Console.WriteLine(
				new { Native.window.parent }
				);

			if (Native.window.parent != Native.window)
			{
				// we are an iframe?

				new { }.With(
					async delegate
					{
						var sw = Stopwatch.StartNew();

						Console.WriteLine("postMessageAsync ... ");

						// start the handshake
						// we gain intellisense, but the type is partal, likely not respawned, acivated, initialized 
						var m = await Native.window.parent.postMessageAsync<ShadowIAsyncStateMachine>();



						Console.WriteLine("postMessageAsync in " + new { sw.ElapsedMilliseconds, m.data.TypeName, m.data.state });

						// ElapsedMilliseconds = 12, data = [object Object] }}
						// ElapsedMilliseconds = 13, TypeName = <Namespace>.___ctor_b__1_1_d, state = 0 }}

						// will we find the type based on typename?
						// or do we need typeindex?

						var types = typeof(Application).Assembly.GetTypes();

						Console.WriteLine(new { types = types.Length });

						foreach (var item in types)
						{
							// safety check 1


							var isIAsyncStateMachine = typeof(IAsyncStateMachine).IsAssignableFrom(item);

							Console.WriteLine(new { item.FullName, isIAsyncStateMachine });
						}
					}
				);

				return;
			}

			// we are a window/tab?

			new IHTMLButton { "click to switch" }.AttachToDocument().onclick += async delegate
			{
				Console.WriteLine("on UI thread " + new { Native.document.location.href });

				// a chrome extenstion could inject itself like it to any tab?
				await default(HopToIFrame);

				Console.WriteLine("on other thread");

				// hop to parent?
			};
		}

	}
}
