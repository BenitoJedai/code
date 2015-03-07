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
using ChromeShaderToyColumns;
using ChromeShaderToyColumns.Design;
using ChromeShaderToyColumns.HTML.Pages;

namespace ChromeShaderToyColumns
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
			// https://www.shadertoy.com/view/Xls3WS
			// view-source:https://www.shadertoy.com/view/Xls3WS
			// https://www.shadertoy.com/api
			// https://www.shadertoy.com/js/cmRenderUtils.js
			// https://www.shadertoy.com/js/cmGeneral.js
			// https://www.shadertoy.com/js/cmPreview.js

			// what does it take to import those nice shaders into jsc world?

			// x:\jsc.svn\examples\javascript\webgl\webglchocolux\webglchocolux\application.cs

			// it looks there are no channels.

			// is it a vert or frag?
			//  fragColor = vec4( col, 1.0 );
			// must be a frag

			// https://www.shadertoy.com/js/effect.js
		}

	}
}
