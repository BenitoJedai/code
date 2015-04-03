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
using System.Threading.Tasks;
using System.Xml.Linq;
using FindByClassAndObserve;
using FindByClassAndObserve.Design;
using FindByClassAndObserve.HTML.Pages;

namespace FindByClassAndObserve
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


			Native.css[" [class='player-video-title']"].style.backgroundColor = "yellow";

			// https://developer.mozilla.org/en-US/docs/Web/API/Document/evaluate
			// https://msdn.microsoft.com/en-us/library/system.xml.linq.xelement.xpathselectelements%28v=vs.90%29.aspx?f=255&MSPPError=-2147217396

			// http://stackoverflow.com/questions/6466831/selecting-element-from-dom-with-javascript-and-xpath
			//Native.document.eva
			Native.document.querySelectorAll(" [class='player-video-title']").WithEach(
				async e =>
				{
					do
					{
						Native.document.title = e.innerText;
					}
					while (await e.async.onmutation);
				}
			);

		}

	}
}
