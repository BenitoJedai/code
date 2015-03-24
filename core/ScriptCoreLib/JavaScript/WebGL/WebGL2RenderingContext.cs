using ScriptCoreLib.JavaScript.DOM.HTML;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.WebGL
{
	// https://chromium.googlesource.com/chromium/src/+/master/gpu/blink/webgraphicscontext3d_impl.h
	// http://dxr.mozilla.org/mozilla-central/source/dom/webidl/WebGL2RenderingContext.webidl
	// http://mxr.mozilla.org/mozilla-central/source/dom/webidl/WebGL2RenderingContext.webidl
	// https://www.khronos.org/registry/webgl/specs/latest/2.0/webgl2.idl

	// getting closer. 
	// http://webglreport.com/?v=2
	[Script(HasNoPrototype = true, InternalConstructor = true)]
	[Obsolete("for future reference")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class WebGL2RenderingContext
	{
		// https://bugzilla.mozilla.org/show_bug.cgi?id=709490

		// https://code.google.com/p/chromium/issues/detail?id=295792#c13
		// https://code.google.com/p/chromium/issues/detail?id=295792&q=WebGL2&colspec=ID%20Pri%20M%20Iteration%20ReleaseBlock%20Cr%20Status%20Owner%20Summary%20OS%20Modified

		// http://en.wikipedia.org/wiki/Metaverse
		// https://wemo.io/google-chrome-and-the-future-of-virtual-reality-interview-with-531
		// http://gamedev.stackexchange.com/questions/62164/opengl-what-are-the-adoption-rates-of-the-various-versions-and-whats-a-reason

		// http://www.reddit.com/comments/1iy0vj
		// 2 adds multiple render targets which makes it much more reasonable to bring deferred rendering engines to the web.

		// http://blog.tojicode.com/2014/07/bringing-vr-to-chrome.html

		// 20141228
		// can we have multiscreen HZ on webgl yet?
		// the internet still does not yet have any examples for webgl2?

		// https://wiki.mozilla.org/Platform/GFX/WebGL2


		// http://blog.tojicode.com/2013/09/whats-coming-in-webgl-20.html

		// http://blog.tojicode.com/2014/02/how-blink-has-affected-webgl-part-2.html
		// X:\jsc.svn\examples\java\webgl\Test\TestInstancedANGLE\TestInstancedANGLE\Application.cs


		// tested by ?
		// https://www.youtube.com/watch?v=zPNM3yOsP0I
		// https://wiki.mozilla.org/Platform/GFX/WebGL2

		#region Constructor

		public WebGL2RenderingContext(
			)
		{
			// InternalConstructor
		}

		static WebGL2RenderingContext InternalConstructor(

			)
		{
			// tested by X:\jsc.svn\examples\javascript\ImageCachedIntoLocalStorageExperiment\ImageCachedIntoLocalStorageExperiment\Application.cs
			// X:\jsc.svn\examples\javascript\WebGL\Test\TestWebGL2RenderingContext\TestWebGL2RenderingContext\Application.cs

			var canvas = new IHTMLCanvas();
			var context = (WebGL2RenderingContext)canvas.getContext("experimental-webgl2");

			return context;
		}

		#endregion
	}
}
