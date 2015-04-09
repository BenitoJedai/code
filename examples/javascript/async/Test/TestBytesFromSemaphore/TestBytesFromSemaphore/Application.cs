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

					Task.Run(
						async delegate
						{
							// simlate lag
							await Task.Delay(1000);

							bytes1 = new byte[] { 0, 1, 2, 3 };



							Console.WriteLine("worker is signaling ui...");
							// will our bytes make it back to the other side on release?
							bytes1sema.Release();

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
