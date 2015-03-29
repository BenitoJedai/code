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
				continuation =>
				{
					// now what?
					Console.WriteLine("enter VirtualOnCompleted..");

					Console.WriteLine(new { continuation });
					Console.WriteLine(new { continuation.Method });
					Console.WriteLine(new { continuation.Target });

					var f = continuation.Target.GetType().GetFields(
						  System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance
					  );


					//var AsyncStateMachineSource = continuation.Target as IAsyncStateMachine;
					var AsyncStateMachineSource = default(IAsyncStateMachine);
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
						return;

					if (AsyncStateMachineStateField == null)
						return;

					var ShadowIAsyncStateMachine = new ShadowIAsyncStateMachine
					{
						TypeName = AsyncStateMachineType.FullName,
						state = (int)AsyncStateMachineStateField.GetValue(AsyncStateMachineSource)
					};

					// types need to be signed by the server, so we could trust a jump?
					Console.WriteLine(new { ShadowIAsyncStateMachine.state, ShadowIAsyncStateMachine.TypeName });
					// {{ state = 0, TypeName = <Namespace>._Invoke_d__3 }}
					// um we dont have the full type name available?
					// what did we use to jump into worker?

					// X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Threading\Tasks\Task\Task.ctor.cs
					// X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Type.cs

					// we seem to use GetTypeIndex
					// the server wont know the index tho...

					// time to implement .displayName for types?

					new { }.With(
						async delegate
						{
							ShadowIAsyncStateMachine = await new ApplicationWebService { }.Invoke(ShadowIAsyncStateMachine);
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

			new { }.With(
				async delegate
				{
					await new IHTMLButton { "click to start inline context hop " + new { this.shared } }.AttachToDocument().async.onclick;

					new IHTMLHorizontalRule { }.AttachToDocument();

					Console.WriteLine(typeof(object) + " client " + typeof(SharedProgram) + new { Thread.CurrentThread.ManagedThreadId });

					// can server choose the correct jump target?
					await default(HopToService);

					// can we fake the stacktrace yet?
					// can we share data yet?
					Debugger.Break();

					// can we do debugger, break, edit n contnue yet?
					Console.WriteLine(typeof(object) + " server " + typeof(SharedProgram) + new { Thread.CurrentThread.ManagedThreadId });
				}
			);
		}

	}

	public class ShadowIAsyncStateMachine
	{
		public string TypeName;
		public int state;
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
