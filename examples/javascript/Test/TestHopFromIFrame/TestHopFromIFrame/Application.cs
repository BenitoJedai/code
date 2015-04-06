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
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestHopFromIFrame;
using TestHopFromIFrame.Design;
using TestHopFromIFrame.HTML.Pages;

namespace TestHopFromIFrame
{
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



			#region HopToIFrame
			HopToIFrame.VirtualOnCompleted = async (that, continuation) =>
			{
				new IHTMLPre {
					"enter HopToIFrame.VirtualOnCompleted.."
				}.AttachToDocument();

				var r = TestSwitchToServiceContextAsync.ShadowIAsyncStateMachine.ResumeableFromContinuation(continuation);

				// X:\jsc.svn\examples\javascript\Test\TestSwitchToIFrame\TestSwitchToIFrame\Application.cs
				//var m = await that.frame.contentWindow.async.onmessage;
				var m = await that.frame.async.onmessage;

				//new IHTMLPre {
				//	" VirtualOnCompleted postMessageAsync " + new { r.shadowstate.state }
				//}.AttachToDocument();


				// um. we need to tell that iframe, where to jump to..
				//var firstmessageback = that.frame.contentWindow.postMessageAsync(r.shadowstate);

				m.postMessage(r.shadowstate);

				that.frame.ownerDocument.defaultView.onmessage +=
					e =>
					{
						if (e.source != that.frame.contentWindow)
							return;

						var shadowstate = (TestSwitchToServiceContextAsync.ShadowIAsyncStateMachine)e.data;

						// are we jumping into a new statemachine?

						new IHTMLPre {
							"iframe is about to jump to parent " + new { shadowstate.state }
						}.AttachToDocument();

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

								return item.FullName == shadowstate.TypeName;
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
									   shadowstate.state
									);
							   }


						   }
					  );
						#endregion

						NewStateMachineI.MoveNext();

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

				new IHTMLPre {
					"enter HopToParent.VirtualOnCompleted.. " + new { r.shadowstate.state }
				}.AttachToDocument();

				//Native.window.parent.postMessage(r.shadowstate);

				// we actually wont use the response yet..
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

					//	new IHTMLPre {
					//			"inside iframe awaiting onmessage"
					//}.AttachToDocument();

					var m = await Native.window.parent.postMessageAsync<TestSwitchToServiceContextAsync.ShadowIAsyncStateMachine>();
					var shadowstate = m.data;

					//var m = await Native.window.parent.async.onmessage;
					//var shadowstate = (TestSwitchToServiceContextAsync.ShadowIAsyncStateMachine)m.data;

					new IHTMLPre {
								new { shadowstate.state }
					}.AttachToDocument();

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

							return item.FullName == shadowstate.TypeName;
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
								   shadowstate.state
								);
						   }


					   }
				  );
					#endregion

					NewStateMachineI.MoveNext();

					// we can now send one hop back?
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

			// 134ms TypeError: Cannot set property 'outerscope' of null
			//var outerscope = "outerscope";

			f.onclick += async delegate
			{
				var innerscope = "innerscope";

				// can we jump back?
				// can we ask how many frames are there?
				// can we jump in any other frame?

				// if we jump back to another statemachine, can we reference the outer statemachine?
				// can we call the server?

				new IHTMLPre {
							"inside iframe about to jump to parent " + new   {
								//outerscope,
								innerscope }
				}.AttachToDocument();

				await default(HopToParent);

				new IHTMLPre {
							"iframe onclick, inside parent now"
				}.AttachToDocument();

				// can we jump back? would we know where to jump back to?

				//await default(HopToIFrame);


			};


		};
			#endregion


		};

		}

	}
}
