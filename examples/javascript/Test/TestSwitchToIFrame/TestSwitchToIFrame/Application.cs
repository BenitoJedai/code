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
				 new IHTMLIFrame { src = Native.document.location.href }.AttachToDocument();
			 };

		}

		public Application(IApp page)
		{
			Native.body.Clear();


			// X:\jsc.svn\examples\javascript\WebMessagingExample\WebMessagingExample\Application.cs

			Console.WriteLine(
				new { Native.window.parent }
				);

			if (Native.window.parent != null)
			{
				// we are an iframe?


				return;
			}

			// we are a window/tab?

			new IHTMLButton { "click to switch" }.AttachToDocument().onclick += async delegate
			{
				Console.WriteLine("on UI thread " + new { Native.document.location.href });

				// a chrome extenstion could inject itself like it to any tab?
				await default(HopToIFrame);

				Console.WriteLine("on other thread");

				// hop back?
			};
		}

	}
}
