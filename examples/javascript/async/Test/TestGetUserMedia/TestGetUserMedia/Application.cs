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

					//Error CS4004  Cannot await in an unsafe context TestGetUserMedia    X:\jsc.svn\examples\javascript\async\test\TestGetUserMedia\TestGetUserMedia\Application.cs  45
					// https://social.msdn.microsoft.com/Forums/en-US/29a3ca5b-c783-4197-af08-7b3c83585e99/minor-compiler-message-unsafe-async?forum=async


					while (v.videoWidth == 0)
						await Native.window.async.onframe;

					new IHTMLPre {
						new { v.videoWidth, v.videoHeight, sw.ElapsedMilliseconds, Environment.ProcessorCount }
					}.AttachToDocument();

					// {{ videoWidth = 640, videoHeight = 480, ElapsedMilliseconds = 94 }}

					var frame0 = new CanvasRenderingContext2D(
						v.videoWidth, v.videoHeight
					);

					frame0.drawImage(
						v,
						0, 0, v.videoWidth, v.videoHeight);

					frame0.canvas.AttachToDocument();

					// could we do thread hopping here to multicore process the data without shaders?
					// RGB,
					// would we have each core work on 8bits. a single color?

					// X:\jsc.svn\examples\javascript\canvas\CanvasFromBytes\CanvasFromBytes\Application.cs 

					var rgba_bytes = frame0.bytes;


					//var rgba_pixels = (rgba[])rgba_bytes;
					//Error CS0030  Cannot convert type 'byte[]' to 'TestGetUserMedia.rgba[]'   TestGetUserMedia X:\jsc.svn\examples\javascript\async\test\TestGetUserMedia\TestGetUserMedia\Application.cs  98

					unsafe
					{
						//Error CS0030  Cannot convert type 'byte[]' to 'TestGetUserMedia.rgba*'    TestGetUserMedia X:\jsc.svn\examples\javascript\async\test\TestGetUserMedia\TestGetUserMedia\Application.cs  109
						//var rgba_pixels = (rgba*)rgba_bytes;
						//Error CS0030  Cannot convert type 'byte[]' to 'void*' TestGetUserMedia X:\jsc.svn\examples\javascript\async\test\TestGetUserMedia\TestGetUserMedia\Application.cs  110

						fixed (byte* rgba_ptr = rgba_bytes)	
						{
							var rgba_pixels = (rgba*)rgba_ptr;

							// looks legit

							// how does it compile for js?
						}

					}

					await Native.window.async.onblur;

					// stream is not stopped yet?
					v.Orphanize();
				}
			);
		}

	}

	struct rgba
	{
		public byte r, g, b, a;
	}
}
