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
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestSwitchToServiceContextAsync;
using TestSwitchToServiceContextAsync.Design;
using TestSwitchToServiceContextAsync.HTML.Pages;

namespace TestSwitchToServiceContextAsync
{
	#region HopToThreadPoolAwaitable
	// http://referencesource.microsoft.com/#mscorlib/system/security/cryptography/CryptoStream.cs
	// simple awaitable that allows for hopping to the thread pool
	public struct HopFromService : System.Runtime.CompilerServices.INotifyCompletion
	{
		// could we fork ? run in parallerl?

		public HopFromService GetAwaiter() { return this; }
		public bool IsCompleted { get { return false; } }

		public static Action<Action> VirtualOnCompleted;
		public void OnCompleted(Action continuation) { VirtualOnCompleted(continuation); }

		public void GetResult() { }
	}
	#endregion


	public struct HopToService : System.Runtime.CompilerServices.INotifyCompletion
	{
		// basically we have to hibernate the current state to resume
		public HopToService GetAwaiter() { return this; }
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

			// X:\jsc.svn\examples\java\hybrid\JVMCLRSwitchToCLRContextAsync\JVMCLRSwitchToCLRContextAsync\Program.cs
			new { }.With(
				async delegate
				{
					var e = await new IHTMLButton { "click to start context hop " + new { this.shared } }.AttachToDocument().async.onclick;

					new IHTMLHorizontalRule { }.AttachToDocument();

					//e.orp
					//e.Element.orp

					await this.shared.Invoke();

					// server wont yet return a continuation instruction!
					new IHTMLPre { "done!" }.AttachToDocument();

				}
			);

			// http://blogs.msdn.com/b/csharpfaq/archive/2015/02/23/edit-and-continue-and-make-object-id-improvements-in-ctp-6.aspx
			// Modifying await expressions wrapped inside other expressions (e.g., G(await F());)
			new { }.With(
				async delegate
				{
					//var xbase = base;

					await new IHTMLButton { "click to start inline context hop " + new { this.shared } }.AttachToDocument().async.onclick;

					new IHTMLHorizontalRule { }.AttachToDocument();

					Console.WriteLine(typeof(object) + " client " + typeof(SharedProgram) + new { Thread.CurrentThread.ManagedThreadId });

					// can server choose the correct jump target?
					await default(HopToService);

					// can we fake the stacktrace yet?
					// can we share data yet? xml?

					// https://visualstudio.uservoice.com/forums/121579-visual-studio/suggestions/4079440-edit-continue-allow-the-modification-of-lambdas
					// http://blogs.msdn.com/b/csharpfaq/archive/2015/02/23/edit-and-continue-and-make-object-id-improvements-in-ctp-6.aspx
					// https://msdn.microsoft.com/en-us/library/ms164927.aspx
					//Error ENC0280 Modifying 'constructor' which contains an anonymous type will prevent the debug session from continuing.TestSwitchToServiceContextAsync X:\jsc.svn\examples\javascript\async\test\TestSwitchToServiceContextAsync\TestSwitchToServiceContextAsync\Application.cs    220
					var loc1 = "can we change il while on debugger, and send a patch back to the client?";

					// return jump can only be to the same state machine
					// we should pass an encrypted type to server
					// live gpu programming yet?
					// can we reference base or this?
					// debugger visualizers?
					// can we send back a result yet?
					Debugger.Break();

					// can we do debugger, break, edit n contnue yet?
					Console.WriteLine(typeof(object) + " server " + typeof(SharedProgram) + new { Thread.CurrentThread.ManagedThreadId });

					await default(HopFromService);

					//Error ENC0021 Adding 'await expression' will prevent the debug session from continuing.TestSwitchToServiceContextAsync X:\jsc.svn\examples\javascript\async\test\TestSwitchToServiceContextAsync\TestSwitchToServiceContextAsync\Application.cs    267

					Console.WriteLine(typeof(object) + " client " + typeof(SharedProgram) + new { Thread.CurrentThread.ManagedThreadId });
				}
			);
		}

	}

	public class SharedProgram
	{
		public string text;

		public override string ToString() => new { text }.ToString();


		// X:\jsc.svn\examples\java\hybrid\JVMCLRSwitchToCLRContextAsync\JVMCLRSwitchToCLRContextAsync\Program.cs
		// X:\jsc.svn\examples\javascript\async\test\TestSwitchToServiceContextAsync\TestSwitchToServiceContextAsync\Application.cs

		public Task Invoke() => Invoke(this.text);
		public static async Task Invoke(string e)
		{
			Console.WriteLine(typeof(object) + " enter " + typeof(SharedProgram) + new { Thread.CurrentThread.ManagedThreadId });

			await default(HopToService);

			// can we fake the stacktrace yet?
			// can we share data yet?
			Debugger.Break();

			// can we do debugger, break, edit n contnue yet?
			Console.WriteLine(typeof(object) + " CLR state 1 " + typeof(SharedProgram) + new { Thread.CurrentThread.ManagedThreadId });

			await default(HopFromService);

			Console.WriteLine(typeof(object) + " JVM state 2 " + typeof(SharedProgram) + new { Thread.CurrentThread.ManagedThreadId });

			await default(HopToService);

			Console.WriteLine(typeof(object) + " CLR state 3 " + typeof(SharedProgram) + new { Thread.CurrentThread.ManagedThreadId });

			await default(HopFromService);

			Console.WriteLine(typeof(object) + " exit " + typeof(SharedProgram) + new { Thread.CurrentThread.ManagedThreadId });
		}
	}

	//>	TestSwitchToServiceContextAsync.exe!TestSwitchToServiceContextAsync.SharedProgram.Invoke(string e) Line 222	C#
	// 	[Resuming Async Method]
	//	 TestSwitchToServiceContextAsync.exe!TestSwitchToServiceContextAsync.ApplicationWebService.Invoke.AnonymousMethod__3() Line 134	C#
	// 	mscorlib.dll!System.Threading.ThreadHelper.ThreadStart_Context(object state)    Unknown
	// 	mscorlib.dll!System.Threading.ExecutionContext.RunInternal(System.Threading.ExecutionContext executionContext, System.Threading.ContextCallback callback, object state, bool preserveSyncCtx)   Unknown
	// 	mscorlib.dll!System.Threading.ExecutionContext.Run(System.Threading.ExecutionContext executionContext, System.Threading.ContextCallback callback, object state, bool preserveSyncCtx)   Unknown
	// 	mscorlib.dll!System.Threading.ExecutionContext.Run(System.Threading.ExecutionContext executionContext, System.Threading.ContextCallback callback, object state) Unknown
	// 	mscorlib.dll!System.Threading.ThreadHelper.ThreadStart()    Unknown
	// 	[Native to Managed Transition]

	#region xConsole
	//class xConsole : StringWriter
	[Obsolete("jsc:js does not allow to overrider an override?")]
	public class xConsole : TextWriter
	{
		// http://www.danielmiessler.com/study/encoding_encryption_hashing/
		[Obsolete("can we have encrypted encoding?")]
		public override Encoding Encoding
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public override void Write(string value)
		{
			var p = new IHTMLCode { innerText = value }.AttachToDocument();
			var s = p.style;

			// jsc, enum tostring?
			if (Console.ForegroundColor == ConsoleColor.Red)
				s.color = "red";

			if (Console.ForegroundColor == ConsoleColor.Blue)
				s.color = "blue";

			if (Console.ForegroundColor == ConsoleColor.Gray)
				s.color = "gray";
		}

		public override void WriteLine(object value)
		{
			Write("" + value);

			new IHTMLBreak { }.AttachToDocument();
		}
		public override void WriteLine(string value)
		{
			//Console.WriteLine(new { value });


			Write(value);

			new IHTMLBreak { }.AttachToDocument();
		}
	}
	#endregion
}
