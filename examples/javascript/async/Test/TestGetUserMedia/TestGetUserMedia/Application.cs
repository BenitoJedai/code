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

					frame0.canvas.AttachToDocument();

					var frame0sw = Stopwatch.StartNew();
					var frame0c = 0;

					// battery/full speed
					// {{ frame0c = 1752, ElapsedMilliseconds = 66 }}

					// 
					new IHTMLPre {
								() => new { frame0c, frame0sw.ElapsedMilliseconds, fps = 1000 / frame0sw.ElapsedMilliseconds  }
							}.AttachToDocument();


					// jsc, when can we start using semaphores?
					// this could also trgger state sync
					//var xx = new System.Threading.SemaphoreSlim(1);

					var slider = new IHTMLInput
					{
						type = ScriptCoreLib.Shared.HTMLInputTypeEnum.range,
						max = 4 * v.videoWidth * v.videoHeight,
						valueAsNumber = 2 * v.videoWidth * v.videoHeight
					}.AttachToDocument();

					new { }.With(
						async delegate
						{
							// could we hop into worker thread, and await for bytes to render?

							// this is essentially a shader

							// switch to worker here
							// at runtime we should know, which fields in this state are in use

							do
							{
								frame0c++;
								frame0sw = Stopwatch.StartNew();


								frame0.drawImage(
									v,
									0, 0, v.videoWidth, v.videoHeight);


								// could we do thread hopping here to multicore process the data without shaders?
								// RGB,
								// would we have each core work on 8bits. a single color?

								// X:\jsc.svn\examples\javascript\canvas\CanvasFromBytes\CanvasFromBytes\Application.cs 


								var rgba_bytes = frame0.bytes;


								//var rgba_pixels = (rgba[])rgba_bytes;
								//Error CS0030  Cannot convert type 'byte[]' to 'TestGetUserMedia.rgba[]'   TestGetUserMedia X:\jsc.svn\examples\javascript\async\test\TestGetUserMedia\TestGetUserMedia\Application.cs  98

#if FPOINTERS
					unsafe
					{
						//Error CS0030  Cannot convert type 'byte[]' to 'TestGetUserMedia.rgba*'    TestGetUserMedia X:\jsc.svn\examples\javascript\async\test\TestGetUserMedia\TestGetUserMedia\Application.cs  109
						//var rgba_pixels = (rgba*)rgba_bytes;
						//Error CS0030  Cannot convert type 'byte[]' to 'void*' TestGetUserMedia X:\jsc.svn\examples\javascript\async\test\TestGetUserMedia\TestGetUserMedia\Application.cs  110

						fixed (byte* rgba_ptr = rgba_bytes)
						{
							// script: error JSC1000: running a newer compiler? opcode unsupported - [0x0035] sizeof     +1 -0

							// how would a shader do it?
							var rgba_pixels = (rgba*)rgba_ptr;

							// looks legit

							// how does it compile for js?

							for (int x = 0; x < v.videoWidth; x++)
								for (int y = 0; y < v.videoHeight; y++)
								{
									rgba_pixels[x + y * v.videoWidth].b = 0;
									rgba_pixels[x + y * v.videoWidth].g = 0;

								}

						}

					}
#endif

								// make it all blue
								// glsl. u8vec4

								// lets deal only with first half of bytes
								//for (int x = 0; x < rgba_bytes.Length / 2; x += 4)
								//for (int x = 0; x < rgba_bytes.Length; x += 4)
								for (int x = 0; x < slider.valueAsNumber; x += 4)
								{
									//// red
									//rgba_bytes[x + 0] = 0;
									//rgba_bytes[x + 1] = (byte)(1 - rgba_bytes[x + 1]);
									//// blue
									//rgba_bytes[x + 2] = 0;


									// red
									rgba_bytes[x + 0] = 0;
									rgba_bytes[x + 1] = (byte)(
										(3 * 255 - rgba_bytes[x + 0] - rgba_bytes[x + 1] - rgba_bytes[x + 2])
										/ 3
									);

									// blue
									rgba_bytes[x + 2] = 0;
								}

								frame0.bytes = rgba_bytes;


							} while (await Native.window.async.onframe);
						}
					);



					// {{ videoWidth = 640, videoHeight = 480, ElapsedMilliseconds = 109 }}

					await Native.window.async.onblur;

					// stream is not stopped yet?
					//v.Orphanize();

					Native.body.style.backgroundColor = "yellow";
				}
			);
		}

	}

	struct rgba
	{
		public byte r, g, b, a;
	}
}
