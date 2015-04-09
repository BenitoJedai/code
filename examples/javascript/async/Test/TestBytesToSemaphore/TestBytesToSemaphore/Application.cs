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
using TestBytesToSemaphore;
using TestBytesToSemaphore.Design;
using TestBytesToSemaphore.HTML.Pages;

#pragma warning disable 1998, 4014

namespace TestBytesToSemaphore
{
	/// <summary>
	/// Your client side code running inside a web browser as JavaScript.
	/// </summary>
	public sealed class Application : ApplicationWebService
	{
		/// <summary>
		/// This is a javascript application.
		/// </summary>
		/// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
		public Application(IApp page)
		{
			// X:\jsc.svn\examples\javascript\async\Test\TestBytesFromSemaphore\TestBytesFromSemaphore\Application.cs

			// what about hopping to multple threads?


			//Warning CS1998  This async method lacks 'await' operators and will run synchronously.Consider using the 'await' operator to await non - blocking API calls, or 'await Task.Run(...)' to do CPU - bound work on a background thread.	TestBytesToSemaphore X:\jsc.svn\examples\javascript\async\test\TestBytesToSemaphore\TestBytesToSemaphore\Application.cs  40
			//Warning CS4014  Because this call is not awaited, execution of the current method continues before the call is completed.Consider applying the 'await' operator to the result of the call.	TestBytesToSemaphore X:\jsc.svn\examples\javascript\async\test\TestBytesToSemaphore\TestBytesToSemaphore\Application.cs  58


			new IHTMLButton { "click to test " + new { Environment.CurrentManagedThreadId } }.AttachToDocument().onclick +=
				async delegate
				{
					//var sw = Stopwatch.StartNew();

					// initial state we will be senging to thread one.
					var bytes1 = new byte[] { 0, 1, 2, 3 };
					var bytes1sema = new SemaphoreSlim(1);

					var bytes2 = default(byte[]);
					var bytes2sema = new SemaphoreSlim(1);

					//new IHTMLPre { () => "working... " + new { bytes1 = bytes1, sw.Elapsed } }.AttachToDocument();
					new IHTMLPre { () => "working... " + new { bytes1 = bytes1 } }.AttachToDocument();
					// working... {{ bytes1 = [object Uint8ClampedArray] }}



					// each thread may build its internal state, yet
					// when crossing the thread bondary, hopping a thread, not much data should be exchanged
					// all binary structures should be supported for resync
					// jsc can we recast bytes to structures yet?



					// in case worker1 is ready, and worker2 need to be signaled or more of them?
					// would thread be able to signal the other thread without expicit ui thread code?
					
					// we need a teste where we can await ahead of time!
					bytes2sema.WaitAsync().ContinueWith(delegate
					{
						new IHTMLPre { "worker1 has signaled worker2..." }.AttachToDocument();

						bytes1sema.Release();
					});


					// um, if we transfer scope to thread, will it dissapear from ui?
					Task.Run(
						async delegate
						{
							// pass2
							// 10 worker2 is awaiting{{ bytes1 = null }}
							Console.WriteLine("worker2 is awaiting" + new { bytes1 });
							await bytes1sema.WaitAsync();
							Console.WriteLine("worker2 is working...");

							// lets update the bytes1 again?

							// can we entable stopwatches already?
							//sw.Stop();
						}
					);


					Task.Run(
						async delegate
						{
							// pass1
							Console.WriteLine("enter worker1! " + new { bytes1 });
							// 11 enter worker1! {{ bytes1 = null }}

							//bytes2 = Enumerable.ToArray(
							//	from x in bytes1
							//	let y = (byte)(x ^ 0xff)
							//	select y
							//);

							Console.WriteLine("worker1 has now computed pass1!");

							bytes2sema.Release();

						}
					);


				};


		}

	}
}
