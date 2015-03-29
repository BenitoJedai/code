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

					 Action<int> MoveNext = null;

					 // async dont like ref?
					 var s = ShadowIAsyncStateMachine.FromContinuation(continuation, ref MoveNext);

					 Action<int> MoveNext1 = MoveNext;

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

							 MoveNext1(sNext.state);
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
			new IHTMLButton { "click me!" }.AttachToDocument().onclick +=
				async delegate
				{
					// http://blogs.msdn.com/b/csharpfaq/archive/2015/02/23/edit-and-continue-and-make-object-id-improvements-in-ctp-6.aspx?PageIndex=2#comments
					var localString = "can server see and modify this string?";

					new IHTMLHorizontalRule { }.AttachToDocument();

					Console.WriteLine(typeof(object) + " client " + typeof(ApplicationWebService) + new { Thread.CurrentThread.ManagedThreadId, localString });


					await default(HopToService);

					// can we move strings?
					Debugger.Break();
					Console.WriteLine("server console!");

					// can we do debugger, break, edit n contnue yet?
					Console.WriteLine(typeof(object) + " server " + typeof(ApplicationWebService) + new { Thread.CurrentThread.ManagedThreadId, localString });

					await default(HopFromService);

					//Error ENC0021 Adding 'await expression' will prevent the debug session from continuing.TestSwitchToServiceContextAsync X:\jsc.svn\examples\javascript\async\test\TestSwitchToServiceContextAsync\TestSwitchToServiceContextAsync\Application.cs    267

					Console.WriteLine(typeof(object) + " client " + typeof(ApplicationWebService) + new { Thread.CurrentThread.ManagedThreadId, localString });
				};
		}

	}
}
