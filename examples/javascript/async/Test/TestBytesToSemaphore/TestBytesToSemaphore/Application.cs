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
		#region hex
		static Func<byte[], string> hex =
			bytes =>
			{
				if (bytes == null)
					return "null";

				var v = "";


				for (int i = 0; i < bytes.Length; i++)
				{
					v += bytes[i].ToString("x2");

					if (i % 16 == 15)
						v += "\n";
					else
						if (i % 16 == 7)
						v += "  ";

					// tab wont show in debug monitor
					//v += "\t";
					else
						v += " ";
				}

				return v;
			};
		#endregion

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
					var sw = Stopwatch.StartNew();

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

					var resultsw = new Stopwatch();

					// X:\jsc.svn\examples\javascript\async\test\TestSemaphoreSlimAwaitThenReleaseInWorker\TestSemaphoreSlimAwaitThenReleaseInWorker\Application.cs
					// we need a teste where we can await ahead of time!
					bytes2sema.WaitAsync().ContinueWith(delegate
					{
						// worker1 has signaled worker2... 00 01 02 03  :: null
						// worker1 has signaled worker2... 00 01 02 03  :: ff fe fd fc 

						Console.WriteLine("did ui resync happen already?");

						// worker1 has signaled worker2... 00 01 02 03  :: ff fe fd fc  {{ ElapsedMilliseconds = 647 }}
						// worker1 has signaled worker2... 00 01 02 03  :: ff fe fd fc  {{ ElapsedMilliseconds = 905 }}
						new IHTMLPre { "worker1 has signaled worker2... "
							+ hex(bytes1) + " :: " + hex(bytes2) + " " + new { sw.ElapsedMilliseconds }
						}.AttachToDocument();

						resultsw.Start();
						bytes1sema.Release();
					});



					//3537ms (10) (bytes1sema) xSemaphoreSlim.InternalVirtualRelease, ui is sending signal to worker
					//2015-04-10 10:20:22.422 :19566/view-source:51020 3538ms (11) (bytes1sema) xSemaphoreSlim.InternalVirtualRelease, ui is sending signal to worker
					//2015-04-10 10:20:22.426 :19566/view-source:51020 3541ms [11] (bytes1sema) ui has sent a release signal, yet nobody awaiting
					//2015-04-10 10:20:22.427 :19566/view-source:51020 3542ms [10] (bytes1sema) ui has sent a release signal
					//2015-04-10 10:20:22.428 :19566/view-source:51020 3543ms [10] worker2 is working... {{ bytes2 = null }}

					// um, if we transfer scope to thread, will it dissapear from ui?
					var result = Task.Run(
						async delegate
						{
							// pass2
							// 10 worker2 is awaiting{{ bytes1 = null }}
							Console.WriteLine("worker2 is awaiting" + new { bytes1 });
							await bytes1sema.WaitAsync();
							// timed by resultsw


							//4227ms [10] worker2 is working... {{ bytes2 = null }}
							//2015-04-10 10:14:04.929 view-source:69308 Uncaught TypeError: Cannot read property '_2xQABp_b1ITCbIktNs3el5Q' of null

							// did we get the bytes?
							// do we have hex?
							Console.WriteLine("worker2 is working... " + new { bytes2 = hex(bytes2) });

							// worker2 is working... {{ bytes2 = [object Uint8ClampedArray] }}
							// lets update the bytes1 again?

							// can we entable stopwatches already?
							//sw.Stop();

							//var bytes3 = new byte[bytes2.Length];

							// https://msdn.microsoft.com/en-us/library/vstudio/dd267698(v=vs.100).aspx
							var z = bytes1.Zip(
								bytes2,
								(x, y) => hex(new[] { x, y })
							);


							return string.Join(":: ", z.ToArray());
						}
					);


					Task.Run(
						async delegate
						{
							// pass1
							Console.WriteLine("enter worker1! " + new { bytes1 });

							var ws = Stopwatch.StartNew();

							// worker1 has now computed pass1! {{ bytes2 = 255,254,253,252 }}
							// well, in java we do the special unboxing. should do it for js also.
							//bytes2 = Enumerable.ToArray(
							//	from x in bytes1
							//	let y = (byte)(x ^ 0xff)
							//	select y
							//);

							bytes2 = new byte[bytes1.Length];

							for (int i = 0; i < bytes1.Length; i++)
							{
								bytes2[i] = (byte)(bytes1[i] ^ 0xff);

							}

							//Array.ConstrainedCopy(


							// LINQ would not know we want a bytearray?

							// worker1 has now computed pass1! {{ bytes2 = [object Uint8ClampedArray], ElapsedMilliseconds = 0 }}
							Console.WriteLine("worker1 has now computed pass1! " + new { bytes2, ws.ElapsedMilliseconds });

							// will it copy bytes2 to ui?
							bytes2sema.Release();

						}
					);


					//var text = await result;
					new IHTMLPre { await result, new { total = sw.ElapsedMilliseconds, resultsw = resultsw.ElapsedMilliseconds, resultfps = 1000 / resultsw.ElapsedMilliseconds } }.AttachToDocument();

					// battery saver:
					// 00 ff :: 01 fe :: 02 fd :: 03 fc {{ total = 2298, resultsw = 55, resultfps = 18 }}
					// 00 ff :: 01 fe :: 02 fd :: 03 fc {{ total = 2363, resultsw = 58, resultfps = 17 }}
					// 00 ff :: 01 fe :: 02 fd :: 03 fc {{ total = 2188, resultsw = 50, resultfps = 20 }}

					// 00 ff :: 01 fe :: 02 fd :: 03 fc {{ total = 743, resultsw = 16, resultfps = 62 }}
					// 00 ff :: 01 fe :: 02 fd :: 03 fc {{ total = 945, resultsw = 22, resultfps = 45 }}
					// 00 ff :: 01 fe :: 02 fd :: 03 fc {{ total = 908, resultsw = 42, resultfps = 23 }}
					// 00 ff :: 01 fe :: 02 fd :: 03 fc {{ total = 715, resultsw = 15, resultfps = 66 }}
					// 00 ff :: 01 fe :: 02 fd :: 03 fc {{ total = 757, resultsw = 16, resultfps = 62 }}

					// 00 ff :: 01 fe :: 02 fd :: 03 fc {{ total = 678, resultsw = 15 }}
					// 00 ff :: 01 fe :: 02 fd :: 03 fc {{ total = 687, resultsw = 16 }}
					// 00 ff :: 01 fe :: 02 fd :: 03 fc {{ total = 841, resultsw = 32 }}

					// 00 ff :: 01 fe :: 02 fd :: 03 fc {{ ElapsedMilliseconds = 3702 }}
					// 00 ff :: 01 fe :: 02 fd :: 03 fc {{ ElapsedMilliseconds = 724 }}
					// 00 ff :: 01 fe :: 02 fd :: 03 fc {{ ElapsedMilliseconds = 602 }}

					// next we should enable stings like for thread hopping,
					// and bytes for thread hoping

					// 00 ff :: 01 fe :: 02 fd :: 03 fc {{ ElapsedMilliseconds = 2809 }}
					// 00 ff :: 01 fe :: 02 fd :: 03 fc {{ ElapsedMilliseconds = 613 }}
					// 00 ff :: 01 fe :: 02 fd :: 03 fc {{ ElapsedMilliseconds = 573 }}

					// 00 ff :: 01 fe :: 02 fd :: 03 fc {{ ElapsedMilliseconds = 920 }}
					// 00 ff :: 01 fe :: 02 fd :: 03 fc {{ ElapsedMilliseconds = 662 }}
				};


		}

	}
}

//3999ms(11) will download worker source into cache...
//2015-04-10 11:32:06.380 :26375/view-source:51134 4051ms (11) will download worker source into cache...done {{ Length = 3129566 } }
//2015-04-10 11:32:06.413 :26375/view-source:51134 4084ms(10) will download worker source into cache...done {{ Length = 3129566 } }