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
using TestBytesFromSemaphore;
using TestBytesFromSemaphore.Design;
using TestBytesFromSemaphore.HTML.Pages;

namespace TestBytesFromSemaphore
{
	/// <summary>
	/// Your client side code running inside a web browser as JavaScript.
	/// </summary>
	public sealed class Application : ApplicationWebService
	{

		#region hex
		Func<byte[], string> hex =
			bytes =>
			{
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
			new IHTMLButton { "click to test " }.AttachToDocument().onclick +=
				async delegate
				{
					var sw = Stopwatch.StartNew();

					var bytes1 = default(byte[]);
					var bytes1sema = new SemaphoreSlim(1);

					var bytes2 = default(byte[]);
					var bytes2sema = new SemaphoreSlim(1);

					new IHTMLPre { () => "working... " + new { sw.Elapsed } }.AttachToDocument();


					//Warning CS4014  Because this call is not awaited, execution of the current method continues before the call is completed.Consider applying the 'await' operator to the result of the call.	TestBytesFromSemaphore X:\jsc.svn\examples\javascript\async\test\TestBytesFromSemaphore\TestBytesFromSemaphore\Application.cs  42

					Task.Run(
						async delegate
						{
							// simlate lag
							await Task.Delay(1000);

							// X:\jsc.svn\examples\javascript\Test\TestNewByteArray\TestNewByteArrayViaScriptCoreLib\Class1.cs
							// why not Uint8ClampedArray?
							bytes1 = new byte[] { 0, 1, 2, 3 };



							Console.WriteLine("worker is signaling ui... " + new { bytes1 });
							// will our bytes make it back to the other side on release?
							bytes1sema.Release();

							//view-source:513506797ms worker is signaling ui...

							// worker resync candidate {{ Name = bytes1, item_value = [object Uint8ClampedArray], item_value_constructor = function Uint8ClampedArray() { [native code] }, item_value_IsByteArray = true }}

							await Task.Delay(1000);

							// now what?
							bytes2 = new byte[] { 9, 8, 7, 6 };

							bytes2sema.Release();

						}
					);


					Console.WriteLine("ui is no awaiting for worker...");
					await bytes1sema.WaitAsync();

					// this is now like a thread hop.

					if (bytes1 == null)
						new IHTMLPre { "working... done! bytes1 is null" }.AttachToDocument();
					else

						new IHTMLPre { "working... done! " + new { sw.Elapsed } + hex(bytes1) }.AttachToDocument();

					// working... done! 00 01 02 03 

					Console.WriteLine("ui is no awaiting for worker...");
					await bytes2sema.WaitAsync();
					new IHTMLPre { "working... done! " + new { sw.Elapsed } + hex(bytes1) + " :: " + hex(bytes2) }.AttachToDocument();
					sw.Stop();

					// working... done! 00 01 02 03  :: 09 08 07 06 

					// what about kicking off another thread now?
				};
		}

	}
}
