#define XENC1

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
			//Console.SetOut(new xConsole());
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
			Native.body.innerText = "click me to test ENC, during pause, change code. server side changes apply.";

			Native.document.onclick += Document_onclick;
		}

		// can we change the implementation of this during ENC?
		// how would we know?
		static string ENCGetString() => "hello world. the other string";
		// or what if we were to change static field?
		// what does it mean for ENC to change a static string
		public static string ENCStaticStringField = "hey";


		static string hex(byte[] bytes)
		{
			var w = new StringBuilder();

			for (int i = 0; i < bytes.Length; i++)
			{
				w.Append(bytes[i].ToString("x2") + " ");
			}

			return w.ToString();
		}

		//Error ENC0278 Modifying 'method' which contains a lambda expression will prevent the debug session from continuing.TestEditAndContinueWithColor    X:\jsc.svn\examples\javascript\test\TestEditAndContinueWithColor\TestEditAndContinueWithColor\Application.cs    89

		void StartILChangeDetector()
		{
			new IHTMLPre { () => ENCGetString() }.AttachToDocument();
			new IHTMLPre { () => ENCStaticStringField }.AttachToDocument();
		}

		// setting the stage... refresh?
		async void Document_onclick(IEvent obj)
		{
			Native.body.Clear();

			StartILChangeDetector();


			var backgroundColor = "blue";
			var color = "white";

			// adding a new variable here, it wont show up as a field for us to send over.
			// so where is it defined? how can we introduce a new variable without a process restart?
			var buttonText = "new button";

			await default(HopToService);
			//Debugger.Break();

			// is t better if we do ENC on first await?
			var x = System.Reflection.MethodInfo.GetCurrentMethod();
			// can we add a comment without current statment jumping to the end?
			// yes?
			// loc = "C:\\Users\\Arvo\\AppData\\Local\\Temp\\Temporary ASP.NET Files\\root\\3ea1022a\\80f01a9\\assembly\\dl3\\c9dda0a3\\6fdc1561_446ad001\\TestEditAndContinueWithColor.exe"
			var loc = x.DeclaringType.Assembly.Location;
			// can we add a new type now?
			// watch
			// +		typeof(ManualPauseAddClass)	null	System.Type

			var xx = typeof(ApplicationWebService);
			// xx_TypesBeforeENC = {System.Type[7]}
			var xx_TypesBeforeENC = xx.Assembly.GetTypes();
			var xx_ENCGetString = hex(new Func<string>(ENCGetString).Method.GetMethodBody().GetILAsByteArray());

			// 
			//Additional information: Token 3a010070 is not a valid string token in the scope of module TestEditAndContinueWithColor.exe.
			//var xx_ldstr = xx.Assembly.ManifestModule.ResolveString(0x7000013a);
			// changng a constant will add a new constant!
			// how would we know which method changed?
			// Additional information: Token 7000043a is not a valid string token in the scope of module TestEditAndContinueWithColor.exe.
			var xx_ldstr = xx.Assembly.ManifestModule.ResolveString(0x7000043d);
			// um we see new IL but cant see new text? but its there!
			// xx_ldstr = "hello world"
			// xx_ENCGetString = "72 3d 04 00 70 0a 2b 00 06 2a "
			// xx_ENCGetString = "72 3a 01 00 70 0a 2b 00 06 2a "
			// xx_ENCGetString = "72 3a 01 00 70 0a 2b 00 06 2a "
			// 
			// "72 3a 01 00 70 ldstr
			// 0a stloc 
			// 2b 00  br +0
			// 06 ldloc 
			// 2a ret "

			// now go change xx_ENCGetString
			Debugger.Break();
			// ENC changes should be done only if  a break is reached. otherwise out of sync it seems?
			// where is the example we did to control webgl on the server?

			// do we have to add a local ahead  of time to debug it?
#if !XENC1
			xx = typeof(ManualPauseAddClass);
#endif
			// jsc could spawn a blank app while it is loading the actual app..

			// xx_TypesAfterENC = {System.Type[8]}
			var xx_TypesAfterENC = xx.Assembly.GetTypes();
			var xx_AfterENCGetString = hex(new Func<string>(ENCGetString).Method.GetMethodBody().GetILAsByteArray());

			color = "yellow";
			//backgroundColor = "red";
			backgroundColor = "blue";


			buttonText = $"buttonText set by the server xx_ldstr: {xx_ldstr} xx_TypesBeforeENC: {xx_TypesBeforeENC.Length}  xx_TypesAfterENC: {xx_TypesAfterENC.Length} before {xx_ENCGetString} after {xx_AfterENCGetString}";

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

			// 		Message	"Cannot evaluate a security function."	string
			// "C:\Users\Arvo\AppData\Local\Temp\Temporary ASP.NET Files\root\3ea1022a\80f01a9\assembly\dl3\c9dda0a3\f83b653a_406ad001\TestEditAndContinueWithColor.EXE"
			// +		typeof(ManualPauseAddClass)	null	System.Type
			// 		ManualPauseAddClass	error CS0119: 'ManualPauseAddClass' is a type, which is not valid in the given context	

			//Debugger.Break();
			//var x = System.Reflection.MethodInfo.GetCurrentMethod();
			//var loc = x.DeclaringType.Assembly.Location;
			// where are we loaded?
			// loc = "C:\\Users\\Arvo\\AppData\\Local\\Temp\\Temporary ASP.NET Files\\root\\3ea1022a\\80f01a9\\assembly\\dl3\\c9dda0a3\\38a710a3_436ad001\\TestEditAndContinueWithColor.exe"

			//+x   { Void MoveNext()}
			//System.Reflection.MethodBase { System.Reflection.RuntimeMethodInfo}
			//x.DeclaringType.AssemblyQualifiedName   "TestEditAndContinueWithColor.Application+<Document_onclick>d__2, TestEditAndContinueWithColor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"  string
			// C:\Users\Arvo\AppData\Local\Temp\Temporary ASP.NET Files\root\3ea1022a\80f01a9\assembly\dl3\c9dda0a3\9c8a23ef_416ad001\TestEditAndContinueWithColor.exe
			// is the module being replaced at the first edit?

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

	// uncomment during ENC, this will move the next statement to exit current method?
	//Error ENC0033 Deleting 'class' will prevent the debug session from continuing.TestEditAndContinueWithColor X:\jsc.svn\examples\javascript\test\TestEditAndContinueWithColor\TestEditAndContinueWithColor\Application.cs	22

	// adding a type does show up at GetTypes
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