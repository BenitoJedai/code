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
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestEditAndContinue;
using TestEditAndContinue.Design;
using TestEditAndContinue.HTML.Pages;
using TestSwitchToServiceContextAsync;

namespace TestEditAndContinue
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
				 //async continuation =>
				 continuation =>
				 {
					 // now what?
					 Console.WriteLine("enter VirtualOnCompleted..");

					 Action<ShadowIAsyncStateMachine> MoveNext = null;

					 // async dont like ref?
					 var s = ShadowIAsyncStateMachine.FromContinuation(continuation, ref MoveNext);

					 var MoveNext1 = MoveNext;

					 // types need to be signed by the server, so we could trust a jump?
					 // {{ state = 0, TypeName = <Namespace>._Invoke_d__3 }}
					 // um we dont have the full type name available?
					 // what did we use to jump into worker?

					 // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Threading\Tasks\Task\Task.ctor.cs
					 // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Type.cs

					 // we seem to use GetTypeIndex
					 // the server wont know the index tho...

					 // time to implement .displayName for types?


					 //var sNext = await new ApplicationWebService { }.Invoke(s);
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
			// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201503/20150329

			new IHTMLButton { "click me!" }.AttachToDocument().onclick += Application_onclick;
		}

		private async void Application_onclick(IEvent<IHTMLButton> obj)
		{
			// http://stackoverflow.com/questions/937049/edit-and-continue-quit-working-for-me-at-some-point
			// http://stackoverflow.com/questions/14183810/how-to-continue-debugging-after-editing-a-method-containing-a-lambda-expression

			// http://blogs.msdn.com/b/csharpfaq/archive/2015/02/23/edit-and-continue-and-make-object-id-improvements-in-ctp-6.aspx?PageIndex=2#comments
			// CLR: { AsyncStateMachineSourceField = System.String <localString>5__1 }
			// chrome: {{ AsyncStateMachineSourceField = _localString_5__1, value = can server see and modify this string? }}
			// would the jsc rewriter be kind and add analysis, where this field is to be used, so we could know when a field can be optimized out of LAN sync?
			var localString = "can server see and modify this string?";
			// what about other types? primitives? glsl? structs? memory?
			// stacktrace next?
			new IHTMLHorizontalRule { }.AttachToDocument();

			Console.WriteLine(typeof(object) + " client " + typeof(ApplicationWebService) + new ENC0280 { ManagedThreadId = Thread.CurrentThread.ManagedThreadId, localString = localString });


			await default(HopToService);

			// at times, will it revert to original, ?
			// could we get jsc to be or code visualizer?
			// https://msdn.microsoft.com/en-us/library/zayyhzts.aspx
			// can we get to be out of sync?
			// can we move strings?
			Debugger.Break();

			Console.WriteLine("server console!");

			// can we do debugger, break, edit n contnue yet?
			//Console.WriteLine(typeof(object) + " server " + typeof(ApplicationWebService) + new { Thread.CurrentThread.ManagedThreadId, localString });
			Console.WriteLine(typeof(object) + " server " + typeof(ApplicationWebService) + new ENC0280 { ManagedThreadId = Thread.CurrentThread.ManagedThreadId, localString = localString });

			var canWeAddANewLocalWeCouldUseOnTheServer = "hello. once paused. dont forget to unpause! would jsc analyzer be able to pick up the change? is it IL visible?";

			// conditional invoke?
			localString = "ENC changed by the server? we can edit here. and let go to see it in effect. what if we manually pause/ edit and resume? at the next cycle the change can be sent back. " + canWeAddANewLocalWeCouldUseOnTheServer;

			await default(HopFromService);

			//Error ENC0021 Adding 'await expression' will prevent the debug session from continuing.TestSwitchToServiceContextAsync X:\jsc.svn\examples\javascript\async\test\TestSwitchToServiceContextAsync\TestSwitchToServiceContextAsync\Application.cs    267
			//Error ENC0272 Modifying 'constructor' which contains an anonymous method will prevent the debug session from continuing.TestEditAndContinue X:\jsc.svn\examples\javascript\async\Test\TestEditAndContinue\TestEditAndContinue\Application.cs    88
			//Error ENC0272 Modifying 'method' which contains an anonymous method will prevent the debug session from continuing.TestEditAndContinue X:\jsc.svn\examples\javascript\async\Test\TestEditAndContinue\TestEditAndContinue\Application.cs    93
			//Error ENC0280 Modifying 'method' which contains an anonymous type will prevent the debug session from continuing.TestEditAndContinue X:\jsc.svn\examples\javascript\async\Test\TestEditAndContinue\TestEditAndContinue\Application.cs    102


			// need to move it out of constructor then?

			//Console.WriteLine(typeof(object) + " client " + typeof(ApplicationWebService) + new { Thread.CurrentThread.ManagedThreadId, localString });
			Console.WriteLine(typeof(object) + " client " + typeof(ApplicationWebService) + new ENC0280 { ManagedThreadId = Thread.CurrentThread.ManagedThreadId, localString = localString });
		}
	}
}




