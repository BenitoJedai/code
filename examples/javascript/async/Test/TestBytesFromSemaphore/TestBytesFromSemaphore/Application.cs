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
		/// <summary>
		/// This is a javascript application.
		/// </summary>
		/// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
		public Application(IApp page)
		{
			new IHTMLButton { "click to test " }.AttachToDocument().onclick +=
				async delegate
				{
					var bytes1 = default(byte[]);
					var bytes1sema = new SemaphoreSlim(1);

					new IHTMLPre { "working... " }.AttachToDocument();


					//Warning CS4014  Because this call is not awaited, execution of the current method continues before the call is completed.Consider applying the 'await' operator to the result of the call.	TestBytesFromSemaphore X:\jsc.svn\examples\javascript\async\test\TestBytesFromSemaphore\TestBytesFromSemaphore\Application.cs  42

					Task.Run(
						async delegate
						{
							// simlate lag
							await Task.Delay(1000);

							// why not Uint8ClampedArray?
							bytes1 = new byte[] { 0, 1, 2, 3 };



							Console.WriteLine("worker is signaling ui... " + new { bytes1 });
							// will our bytes make it back to the other side on release?
							bytes1sema.Release();

							//view-source:513506797ms worker is signaling ui...

							//30007ms worker resync candidate { { Name = bytes1, item_value = 0,1,2,3, item_value_constructor = function Array() { [native code] }, item_value_IsArray = true, self_Uint8ClampedArray = function Uint8ClampedArray() { [native code]
							//30007ms worker resync candidate {{ Name = bytes1sema, item_value = [object Object], item_value_constructor = function vCpL8AJAwTmO6BklLAu35w() { }, item_value_IsArray = false, self_Uint8ClampedArray = function Uint8ClampedArray() { [native code] } }}

							await Task.Delay(1000);

							// now what?
						}
					);


					Console.WriteLine("ui is no awaiting for worker...");
					await bytes1sema.WaitAsync();

					// this is now like a thread hop.

					if (bytes1 == null)
						new IHTMLPre { "working... done! bytes1 is null" }.AttachToDocument();
					else

						new IHTMLPre { "working... done! " + new { bytes1 } }.AttachToDocument();

				};
		}

	}
}
