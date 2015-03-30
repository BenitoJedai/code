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
using System.Runtime.Serialization;
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
			//Console.SetOut(new xConsole());

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

		// each iframe has its own static?
		static Stopwatch sw = Stopwatch.StartNew();

		public Application(IApp page)
		{
			Native.body.Clear();


			// X:\jsc.svn\examples\javascript\WebMessagingExample\WebMessagingExample\Application.cs

			Console.WriteLine(
				new { Native.window.parent }
				);

			#region NewStateMachineI.MoveNext
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

						var that = m.data;


						Console.WriteLine("postMessageAsync in " + new { sw.ElapsedMilliseconds, m.data.TypeName, m.data.state });

						// ElapsedMilliseconds = 12, data = [object Object] }}
						// ElapsedMilliseconds = 13, TypeName = <Namespace>.___ctor_b__1_1_d, state = 0 }}

						// will we find the type based on typename?
						// or do we need typeindex?

						var types = typeof(Application).Assembly.GetTypes();

						Console.WriteLine(new { sw.ElapsedMilliseconds, types = types.Length });


						// X:\jsc.svn\examples\javascript\async\Test\TestSwitchToServiceContextAsync\TestSwitchToServiceContextAsync\CLRWebServiceInvoke.cs
						var xAsyncStateMachineType = types.FirstOrDefault(
							item =>
							{
								// safety check 1

								//Console.WriteLine(new { sw.ElapsedMilliseconds, item.FullName });

								var xisIAsyncStateMachine = typeof(IAsyncStateMachine).IsAssignableFrom(item);
								if (xisIAsyncStateMachine)
								{
									//Console.WriteLine(new { item.FullName, isIAsyncStateMachine });

									return item.FullName == m.data.TypeName;
								}

								return false;
							}
						);


						//						postMessageAsync in {{ ElapsedMilliseconds = 18, TypeName = <Namespace>.___ctor_b__1_1_d, state = 0 }}
						//{{ types = 77 }}
						//{{ FullName = <Namespace>.___ctor_b__1_0_d, isIAsyncStateMachine = true }}
						//{{ FullName = <Namespace>.___ctor_b__1_1_d, isIAsyncStateMachine = true }}
						//{{ FullName = <Namespace>.___cctor_b__1_d, isIAsyncStateMachine = true }}

						//Console.WriteLine(new { item.FullName, isIAsyncStateMachine });

						Console.WriteLine("GetUninitializedObject " + new { sw.ElapsedMilliseconds, xAsyncStateMachineType });

						var NewStateMachine = FormatterServices.GetUninitializedObject(xAsyncStateMachineType);
						var isIAsyncStateMachine = NewStateMachine is IAsyncStateMachine;

						Console.WriteLine(" " + new { sw.ElapsedMilliseconds, NewStateMachine, isIAsyncStateMachine });

						// {{ ElapsedMilliseconds = 30, NewStateMachine = [object Object] }}

						//var NewStateMachine = Activator.CreateInstance(xAsyncStateMachineType);
						//var NewStateMachineI = NewStateMachine as IAsyncStateMachine;

						// bugcheck, as operator broken?
						var NewStateMachineI = (IAsyncStateMachine)NewStateMachine;

						Console.WriteLine(new { sw.ElapsedMilliseconds, NewStateMachineI });


						Func<string, string> DecoratedString =
							x => x.Replace("-", "_").Replace("+", "_").Replace("<", "_").Replace(">", "_");


						#region 1__state
						xAsyncStateMachineType.GetFields(
								  System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance
							  ).WithEach(
							   AsyncStateMachineSourceField =>
							   {
								   // we need to populate the data for the debugger?

								   //var SourceField_value = AsyncStateMachineSourceField.GetValue(NewStateMachine);

								   // it is a new type.
								   Console.WriteLine(new { AsyncStateMachineSourceField });

								   if (AsyncStateMachineSourceField.Name.EndsWith("1__state"))
								   {
									   AsyncStateMachineSourceField.SetValue(
										   NewStateMachineI,
										   that.state
										);
								   }

								   // field names/ tokens need to be encrypted like typeinfo.

								   // or, are we supposed to initialize a string value here?

								   // cannot move List via onmessage that easly...

								   //var xStringField = that.StringFields.FirstOrDefault(
								   //	   f => DecoratedString(f.FieldName) == DecoratedString(AsyncStateMachineSourceField.Name)
								   //   );

								   //if (xStringField != null)
								   //{
								   // // once we are to go back to client. we need to reverse it?

								   // AsyncStateMachineSourceField.SetValue(
								   //		   NewStateMachineI,
								   //		   xStringField.value
								   //		);
								   // // next xml?
								   // // before lets send our strings back with the new state!
								   // // what about exceptions?
								   //}
							   }
						  );
						#endregion

						// run it?
						Console.WriteLine(new { sw.ElapsedMilliseconds } + " call MoveNext..");

						NewStateMachineI.MoveNext();

						Console.WriteLine(new { sw.ElapsedMilliseconds } + " call MoveNext.. done");
					}
				);

				return;
			}
			#endregion


			// we are a window/tab?

			new IHTMLButton { "click to switch" }.AttachToDocument().onclick += async delegate
			{
				new IHTMLPre {
					"on UI thread " + new { Native.document.location.href, Native.window.Width }
				}.AttachToDocument();

				// could we sync
				// iprogress?

				// a chrome extenstion could inject itself like it to any tab?
				await default(HopToIFrame);

				// no scope data was synced at this point...
				// perhaps we should send struct data via postmessage and reattach interface methods later?

				// TypeError: Cannot set property 'sw' of null
				//var sw = Stopwatch.StartNew();

				Native.window.history.replaceState(null, "iframe2", "/iframe2");

				new IHTMLPre {
					"on iframe thread " + new { Native.document.location.href, Native.window.Width }
					, () => new { sw.ElapsedMilliseconds }
				}.AttachToDocument();

				var b2 = new IHTMLButton { "click to switch to nested frame 2" }.AttachToDocument();

				await b2.async.onclick;
				b2.disabled = true;

				// nested fram wont show up if url is the same as parent?
				await default(HopToIFrame);

				new IHTMLPre {
					"on neste iframe thread " + new { Native.document.location.href, Native.window.Width }
				}.AttachToDocument();

				// hop to parent?
			};
		}

	}
}
