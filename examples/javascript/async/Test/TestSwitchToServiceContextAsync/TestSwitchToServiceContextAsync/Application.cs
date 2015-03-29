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
using System.IO;
using System.Linq;
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
				c =>
				{
					// now what?
					Console.WriteLine("enter VirtualOnCompleted");
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
					//e.orp
					//e.Element.orp

					await this.shared.Invoke();

					new IHTMLPre { "done!" }.AttachToDocument();
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

	//	looking at BCLImplementationMergeAssemblies...
	//1adc:01:01 RewriteToAssembly error: System.NullReferenceException: Object reference not set to an instance of an object.
	//   at System.Collections.Generic.Dictionary`2.Insert(TKey key, TValue value, Boolean add)
	//   at System.Collections.Generic.Dictionary`2.set_Item(TKey key, TValue value)
	//   at ScriptCoreLib.ScriptAttribute.<>c__DisplayClass1.<OfProvider>b__0(Type item) in x:\jsc.svn\compiler\ScriptCoreLibA\ScriptAttribute.OfProvider.cs:line 48
	//   at ScriptCoreLib.ScriptAttribute.OfProvider(ICustomAttributeProvider m) in x:\jsc.svn\compiler\ScriptCoreLibA\ScriptAttribute.OfProvider.cs:line 61
	//   at ScriptCoreLib.CSharp.Extensions.ScriptAttributeExtensions.ToScriptAttributeOrDefault(ICustomAttributeProvider p) in x:\jsc.svn\compiler\ScriptCoreLibA\CSharp\Extensions\ScriptAttributeExtensions.cs:line 18

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

		public override void WriteLine(string value)
		{
			//Console.WriteLine(new { value });


			Write(value);

			new IHTMLBreak { }.AttachToDocument();
		}
	}
	#endregion
}
