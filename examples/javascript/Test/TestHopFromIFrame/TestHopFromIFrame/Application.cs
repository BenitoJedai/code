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
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestHopFromIFrame;
using TestHopFromIFrame.Design;
using TestHopFromIFrame.HTML.Pages;

namespace TestHopFromIFrame
{
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


	/// <summary>
	/// Your client side code running inside a web browser as JavaScript.
	/// </summary>
	public sealed class Application : ApplicationWebService
	{
		public Application(IApp page) { vctor(page); }

		static Action<IApp> vctor;

		static Application()
		{
			new IHTMLPre {
				new { Native.document.currentScript.src }
			}.AttachToDocument();



			#region VirtualOnCompleted
			HopToIFrame.VirtualOnCompleted = async (that, continuation) =>
			{
				new IHTMLPre {
					"enter VirtualOnCompleted.."
				}.AttachToDocument();

				var r = TestSwitchToServiceContextAsync.ShadowIAsyncStateMachine.ResumeableFromContinuation(continuation);

				// X:\jsc.svn\examples\javascript\Test\TestSwitchToIFrame\TestSwitchToIFrame\Application.cs
				//var m = await that.frame.contentWindow.async.onmessage;
				var m = await that.frame.async.onmessage;

				new IHTMLPre {
					" VirtualOnCompleted postMessageAsync " + new { r.shadowstate.state }
				}.AttachToDocument();


				// um. we need to tell that iframe, where to jump to..
				//var firstmessageback = that.frame.contentWindow.postMessageAsync(r.shadowstate);

				m.postMessage(r.shadowstate);
			};
			#endregion


			// fsharpy look
			vctor = (IApp page) =>
			{
				// {{ href = blob:https%3A//192.168.1.196%3A27831/bafa8242-82bd-44ef-8581-9f76f909cd86 }}

				new IHTMLPre {
					new { Native.document.location.href }
				}.AttachToDocument();

				if (Native.window.parent != Native.window)
				{
					// inside iframe



					new { }.With(
						async delegate
						{
							// start the handshake
							// we gain intellisense, but the type is partal, likely not respawned, acivated, initialized 
							//var m = await Native.window.parent.postMessageAsync<TestSwitchToServiceContextAsync.ShadowIAsyncStateMachine>();

							new IHTMLPre {
								"inside iframe awaiting onmessage"
							}.AttachToDocument();

							var m = await Native.window.parent.postMessageAsync<TestSwitchToServiceContextAsync.ShadowIAsyncStateMachine>();
							var shadowstate = m.data;

							//var m = await Native.window.parent.async.onmessage;
							//var shadowstate = (TestSwitchToServiceContextAsync.ShadowIAsyncStateMachine)m.data;

							new IHTMLPre {
								new { shadowstate.state }
							}.AttachToDocument();

							// about to invoke
						}
					);


					return;
				}

				var codetask = new WebClient().DownloadStringTaskAsync(
						 new Uri(Worker.ScriptApplicationSource, UriKind.Relative)
				);

				#region click to switch
				new IHTMLButton { "click to switch" }.AttachToDocument().onclick += async delegate
				{
					Native.body.style.backgroundColor = "yellow";

					// can we compile shaders in frames in parallel?

					var code = await codetask;

					var aFileParts = new[] { code };
					var oMyBlob = new Blob(aFileParts, new { type = "text/javascript" });
					//var url = oMyBlob.ToObjectURL();
					var url = URL.createObjectURL(oMyBlob);

					var frame = new IHTMLIFrame {
						new XElement("script", new XAttribute("src", url), " ")
					}.AttachToDocument();

					// can we our code in that blank document?

					// not fired for blank?
					await frame.async.onload;

					////var x = frame.ownerDocument.createElement(IHTMLElement.HTMLElementEnum.button);
					////x.AttachTo(frame.ownerDocument.documentElement);

					Native.body.style.backgroundColor = "cyan";


					await (HopToIFrame)frame;

					var f = new IHTMLButton { "in the frame! click to notify parent" }.AttachToDocument();

					f.onclick += async delegate
					{
						// can we jump back?
						// can we ask how many frames are there?
						// can we jump in any other frame?

						// if we jump back to another statemachine, can we reference the outer statemachine?
						// can we call the server?
					};


				};
				#endregion


			};

		}

	}
}
