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
using System.Threading.Tasks;
using System.Xml.Linq;
using TestGetUserMedia;
using TestGetUserMedia.Design;
using TestGetUserMedia.HTML.Pages;

namespace TestGetUserMedia
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
			new { }.With(
				async delegate
				{
					Native.body.Clear();

					// would it be easy to do head tracking via webcam for VR?
					// the app would run on android, yet
					// two sattelites could spawn on two laptops to track the head.

					// would we be able to thread hop between camera devices and android?

					var v = await Native.window.navigator.async.onvideo;


					v.AttachToDocument();

					v.play();

					// what do we see at this point?

					// first, could we detect greenscreen without having one?

					// assuming the camera is static, we could remove the pixels that never seem to move

					// a shader program, consuming the video would be able to apply the effects a lot faster.
					// doing it in ui thread will slow it down.

					//					videoHeight: 480
					//videoWidth: 640
					new IHTMLPre {

						new { v.videoWidth, v.videoHeight }
					}.AttachToDocument();
					// do we know the size of the cam?
					// {{ videoWidth = 0, videoHeight = 0 }}

					var sw = Stopwatch.StartNew();

					while (v.videoWidth == 0)
						await Native.window.async.onframe;

					new IHTMLPre {
						new { v.videoWidth, v.videoHeight, sw.ElapsedMilliseconds }
					}.AttachToDocument();

					// {{ videoWidth = 640, videoHeight = 480, ElapsedMilliseconds = 94 }}


				}
			);
		}

	}
}
