using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.WebGL;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestWebGL2RenderingContext;
using TestWebGL2RenderingContext.Design;
using TestWebGL2RenderingContext.HTML.Pages;

namespace TestWebGL2RenderingContext
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
			//https://wiki.mozilla.org/Platform/GFX/WebGL2
			// https://twitter.com/etribz/status/359954523789328387
			// 2years since?
			// https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext
			// cannot find any example. retry later

			var c = new WebGL2RenderingContext();

			new IHTMLPre { new { c } }.AttachToDocument();
		}

	}
}
