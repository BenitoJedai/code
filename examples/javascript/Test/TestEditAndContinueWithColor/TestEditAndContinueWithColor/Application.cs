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
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestEditAndContinueWithColor;
using TestEditAndContinueWithColor.Design;
using TestEditAndContinueWithColor.HTML.Pages;
using TestSwitchToServiceContextAsync;

namespace TestEditAndContinueWithColor
{
	/// <summary>
	/// Your client side code running inside a web browser as JavaScript.
	/// </summary>
	public sealed class Application : ApplicationWebService
	{
		static Application()
		{
			// patch the awaiter..
			Console.SetOut(new xConsole());
			HopToService.VirtualOnCompleted =
				 continuation =>
				 {
					 Console.WriteLine("enter VirtualOnCompleted..");
					 Action<ShadowIAsyncStateMachine> MoveNext = null;
					 var s = ShadowIAsyncStateMachine.FromContinuation(continuation, ref MoveNext);
					 var MoveNext1 = MoveNext;
					 new ApplicationWebService { }.Invoke(s).ContinueWith(
						 x =>
						 {
							 var sNext = x.Result;
							 Console.WriteLine("exit VirtualOnCompleted.. " + new { sNext.state });
							 MoveNext1(sNext);
						 }
					 );

				 };
		}

		/// <summary>
		/// This is a javascript application.
		/// </summary>
		/// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
		public Application(IApp page)
		{

			Native.document.onclick += Document_onclick;
		}

		async void Document_onclick(IEvent obj)
		{
			Native.body.Clear();

			var backgroundColor = "blue";
			var color = "white";

			// adding a new variable here, it wont show up as a field for us to send over.
			// so where is it defined? how can we introduce a new variable without a process restart?
			var buttonText = "new button";

			await default(HopToService);
			//Debugger.Break();

			color = "yellow";
			//backgroundColor = "red";
			backgroundColor = "blue";


			buttonText = "buttonText set by the server";

			await default(HopFromService);

			Native.document.body.style.backgroundColor = backgroundColor;
			Native.document.body.style.color = color;

			var button = new IHTMLButton { buttonText }.AttachToDocument();

			// by adding code here. would non server side transver over to the live instance yet?

			var clickCount = 0;
			nextClick:

			var e = await button.async.onclick;
			clickCount++;

			// prevent document onclick
			e.stopPropagation();

			var sw = Stopwatch.StartNew();

			await default(HopToService);
			;
			// remember, jsc will compile this for the client too..

			// can we add a new method and see its IL?

			//Func<string> GetString = ManualPauseAddClass.GetString;
			// where is that new method defined?

			// next statement wont work
			//var value = GetString();

			// hy does the hop take 1400ms?
			// what other information do we have available?
			// GetCurrentMethod
			buttonText =
				//value + 
				@" - set by the server!"
//+ @"
//GetCurrentMethod: " + System.Reflection.MethodInfo.GetCurrentMethod().Name + @"
//GetCurrentMethod DeclaringType:" + System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.FullName
;

			await default(HopFromService);

			button.innerText = buttonText + " (#" + clickCount + " " + sw.ElapsedMilliseconds + "ms)";

			// signify an update
			button.style.color = "blue";
			goto nextClick;
		}
	}

	// uncomment during ENC

	//static class ManualPauseAddClass
	//{
	//	public static string GetString() => "hello!";

	//}
}


//at CallSite.Target(Closure , CallSite , Object )
//   at System.Dynamic.UpdateDelegates.UpdateAndExecute1[T0, TRet](CallSite site, T0 arg0)
//   at ScriptCoreLib.JavaScript.Native.__RTCPeerConnection()
//   at ScriptCoreLib.JavaScript.Native..cctor()


//An exception of type 'System.Runtime.Remoting.RemotingException' occurred in WebDev.WebHost40.dll but was not handled in user code

//Additional information: Object '/90e9e0df_5687_4f43_8f30_03a89ecd1bde/sxe56hufnuap2jnajpy4+55r_30.rem' has been disconnected or does not exist at the server.

//>	TestEditAndContinueWithColor.exe!TestEditAndContinueWithColor.Application.Document_onclick(ScriptCoreLib.JavaScript.DOM.IEvent obj) Line 105	C#
//	[Resuming Async Method]
// TestSwitchToServiceContextAsync.exe!TestSwitchToServiceContextAsync.CLRWebServiceInvoke.Invoke.AnonymousMethod__5() Line 119	C#
//	mscorlib.dll!System.Threading.ThreadHelper.ThreadStart_Context(object state)    Unknown
//	mscorlib.dll!System.Threading.ExecutionContext.RunInternal(System.Threading.ExecutionContext executionContext, System.Threading.ContextCallback callback, object state, bool preserveSyncCtx)   Unknown
//	mscorlib.dll!System.Threading.ExecutionContext.Run(System.Threading.ExecutionContext executionContext, System.Threading.ContextCallback callback, object state, bool preserveSyncCtx)   Unknown
//	mscorlib.dll!System.Threading.ExecutionContext.Run(System.Threading.ExecutionContext executionContext, System.Threading.ContextCallback callback, object state) Unknown
//	mscorlib.dll!System.Threading.ThreadHelper.ThreadStart()    Unknown
//	[Native to Managed Transition]

//	---------------------------
//Microsoft Visual Studio
//---------------------------
//A fatal error has occurred trying to apply code changes and debugging needs to be terminated. %1
//---------------------------
//OK
//---------------------------

//---------------------------
//Microsoft Visual Studio
//---------------------------
//Unable to set the next statement. 
//---------------------------
//OK
//---------------------------

//script: error JSC1000: No implementation found for this native method, please implement [static System.Reflection.MethodBase.GetCurrentMethod()]