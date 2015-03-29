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
	struct HopFromService : System.Runtime.CompilerServices.INotifyCompletion
	{
		// could we fork ? run in parallerl?

		public HopFromService GetAwaiter() { return this; }
		public bool IsCompleted { get { return false; } }

		public static Action<Action> VirtualOnCompleted;
		public void OnCompleted(Action continuation) { VirtualOnCompleted(continuation); }

		public void GetResult() { }
	}
	#endregion


	struct HopToService : System.Runtime.CompilerServices.INotifyCompletion
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

	public class ShadowIAsyncStateMachine
	{
		public string TypeName;
		public int state;

		public static ShadowIAsyncStateMachine FromContinuation(
			Action continuation,

			//Error	CS0177	The out parameter 'AsyncStateMachineSource' must be assigned to before control leaves the current method	TestSwitchToServiceContextAsync	X:\jsc.svn\examples\javascript\async\test\TestSwitchToServiceContextAsync\TestSwitchToServiceContextAsync\Application.cs	284
			//out IAsyncStateMachine AsyncStateMachineSource // = default(IAsyncStateMachine)

			ref Action<int> MoveNext
			)
		{
			var AsyncStateMachineSource = default(IAsyncStateMachine);

			Console.WriteLine(new { continuation });
			Console.WriteLine(new { continuation.Method });
			Console.WriteLine(new { continuation.Target });

			var f = continuation.Target.GetType().GetFields(
				  System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance
			  );


			//var AsyncStateMachineSource = continuation.Target as IAsyncStateMachine;
			var AsyncStateMachineType = default(Type);
			var AsyncStateMachineFields = default(FieldInfo[]);

			var AsyncStateMachineStateField = default(FieldInfo);

			if (continuation.Target is IAsyncStateMachine)
			{
				AsyncStateMachineSource = (IAsyncStateMachine)continuation.Target;
			}

			if (AsyncStateMachineSource == null)
			{
				f.WithEach(
					SourceField =>
					{
						var SourceField_value = SourceField.GetValue(continuation.Target);
						Console.WriteLine(new { SourceField, value = SourceField_value });

						// could it be, we are already enumerating asyncstatemachine ?

						var m_stateMachine = SourceField_value as IAsyncStateMachine;
						if (m_stateMachine != null)
						{
							//Error CS1628  Cannot use ref or out parameter 'AsyncStateMachineSource' inside an anonymous method, lambda expression, or query expression TestSwitchToServiceContextAsync X:\jsc.svn\examples\javascript\async\test\TestSwitchToServiceContextAsync\TestSwitchToServiceContextAsync\Application.cs    219

							AsyncStateMachineSource = m_stateMachine;
							AsyncStateMachineType = m_stateMachine.GetType();

							AsyncStateMachineFields = AsyncStateMachineType.GetFields(
								System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance
							);

							AsyncStateMachineFields.WithEach(
								AsyncStateMachineSourceField =>
								{
									var value = AsyncStateMachineSourceField.GetValue(AsyncStateMachineSource);

									if (AsyncStateMachineSourceField.Name.EndsWith("1__state"))
									{
										AsyncStateMachineStateField = AsyncStateMachineSourceField;
									}

									Console.WriteLine(new { AsyncStateMachineSourceField, value });
								}
							);
						}

					}
				);
			}
			else
			{
				AsyncStateMachineType = AsyncStateMachineSource.GetType();

				// inline mode?

				f.WithEach(
					AsyncStateMachineSourceField =>
					{

						var value = AsyncStateMachineSourceField.GetValue(AsyncStateMachineSource);

						if (AsyncStateMachineSourceField.Name.EndsWith("1__state"))
						{
							AsyncStateMachineStateField = AsyncStateMachineSourceField;
						}

						Console.WriteLine(new { AsyncStateMachineSourceField, value });

					}
				);
			}

			// does it look like in JVM?

			//enter VirtualOnCompleted..
			//{{ continuation = [object Object] }}
			//{{ Method = {{ InternalMethodToken = MycABsh3zjevXeqty6qy4w }} }}
			//{{ Target = [object Object] }}
			//{{ SourceField = zstateMachine, value = [object Object] }}
			//{{ AsyncStateMachineSourceField = __t__builder, value = [object Object] }}
			//{{ AsyncStateMachineSourceField = __1__state, value = 0 }}
			//{{ AsyncStateMachineSourceField = e, value = hello from server }}
			//{{ AsyncStateMachineSourceField = __u__1, value = [object Object] }}
			//{{ AsyncStateMachineSourceField = __u__2, value = null }}
			//{{ SourceField = yield, value = [object Object] }}

			// js seems to have the same issue where, struct fields are not inited?
			// for js, the ctor needs to do it? or can we do it on prototype level? 

			Console.WriteLine(new { AsyncStateMachineType, AsyncStateMachineStateField });

			if (AsyncStateMachineType == null)
				return null;

			if (AsyncStateMachineStateField == null)
				return null;

			var s = new ShadowIAsyncStateMachine
			{
				TypeName = AsyncStateMachineType.FullName,
				state = (int)AsyncStateMachineStateField.GetValue(AsyncStateMachineSource)
			};

			Console.WriteLine(new { s.state, s.TypeName });

			MoveNext =
				NextState =>
				{
					Console.WriteLine("enter MoveNext " + new { NextState });

					AsyncStateMachineStateField.SetValue(AsyncStateMachineSource, NextState);
					AsyncStateMachineSource.MoveNext();
				};

			return s;
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
	class xConsole : TextWriter
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
