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
using ChromeShaderToyProgramsAsTiles;
using ChromeShaderToyProgramsAsTiles.Design;
using ChromeShaderToyProgramsAsTiles.HTML.Pages;
using ScriptCoreLib.JavaScript.WebGL;
using ChromeShaderToyPrograms;
using System.Diagnostics;
using ChromeShaderToyColumns.Library;

namespace ChromeShaderToyProgramsAsTiles
{
	using ScriptCoreLib.GLSL;
	using gl = WebGLRenderingContext;

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
			// https://www.shadertoy.com/view/MsfXDS


			// show shader based on tab selection?



			//Native.document.documentElement.style.overflow = IStyle.OverflowEnum.auto;


			// chrome by default has no scrollbar, bowser does
			Native.document.documentElement.style.overflow = IStyle.OverflowEnum.hidden;
			Native.body.style.margin = "0px";
			Native.body.Clear();

			// ipad?
			Native.window.onerror +=
				e =>
				{
					new IHTMLPre {
						"error " + new { e.message, e.error }
					}.AttachToDocument();
				};

			// https://www.youtube.com/watch?v=tnS8K0yhmZU
			// http://www.reddit.com/r/oculus/comments/2sv5lk/new_release_of_shadertoy_vr/
			// https://www.shadertoy.com/view/lsSGRz

			// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201503/20150309
			// https://zproxy.wordpress.com/2015/03/09/project-windstorm/
			// https://github.com/jimbo00000/RiftRay


			#region += Launched chrome.app.window
			dynamic self = Native.self;
			dynamic self_chrome = self.chrome;
			object self_chrome_socket = self_chrome.socket;

			if (self_chrome_socket != null)
			{
				if (!(Native.window.opener == null && Native.window.parent == Native.window.self))
				{
					Console.WriteLine("chrome.app.window.create, is that you?");

					// pass thru
				}
				else
				{
					// should jsc send a copresence udp message?
					chrome.runtime.UpdateAvailable += delegate
					{
						new chrome.Notification(title: "UpdateAvailable");

					};

					chrome.app.runtime.Launched += async delegate
					{
						// 0:12094ms chrome.app.window.create {{ href = chrome-extension://aemlnmcokphbneegoefdckonejmknohh/_generated_background_page.html }}
						Console.WriteLine("chrome.app.window.create " + new { Native.document.location.href });

						new chrome.Notification(title: "ChromeUDPSendAsync");

						var xappwindow = await chrome.app.window.create(
							   Native.document.location.pathname, options: null
						);

						//xappwindow.setAlwaysOnTop

						xappwindow.show();

						await xappwindow.contentWindow.async.onload;

						Console.WriteLine("chrome.app.window loaded!");
					};


					return;
				}
			}
			#endregion




			var gl = new WebGLRenderingContext(alpha: true);

			if (gl == null)
			{

				new IHTMLPre {
					// https://code.google.com/p/chromium/issues/detail?id=294207
					"Rats! WebGL hit a snag.",

					new IHTMLAnchor { href = "about:gpu", innerText = "about:gpu" }
				}.AttachToDocument();
				return;
			}

			var combo = new IHTMLSelect().AttachToDocument();

			combo.style.position = IStyle.PositionEnum.absolute;
			combo.style.left = "0px";
			combo.style.top = "0px";
			//combo.style.right = "0px";
			combo.style.width = "100%";

			combo.style.backgroundColor = "rgba(255,255,255,0.5)";
			//combo.style.backgroundColor = "rgba(255,255,0,0.5)";
			//combo.style.background = "linear-gradient(to bottom, rgba(255,255,255,0.5 0%,rgba(255,255,255,0.0 100%))";
			combo.style.border = "0px solid transparent";
			combo.style.fontSize = "large";
			combo.style.paddingLeft = "1em";
			combo.style.fontFamily = IStyle.FontFamilyEnum.Verdana;
			combo.style.cursor = IStyle.CursorEnum.pointer;





			var c = gl.canvas.AttachToDocument();

			#region onresize
			new { }.With(
				async delegate
				{
					do
					{
						c.width = Native.window.Width;
						c.height = Native.window.Height;
						c.style.SetSize(c.width, c.height);
					}
					while (await Native.window.async.onresize);
				}
			);
			#endregion



			#region CaptureMouse
			var mMouseOriX = 0;
			var mMouseOriY = 0;
			var mMousePosX = 0;
			var mMousePosY = 0;

			c.onmousedown += async ev =>
			{
				mMouseOriX = ev.CursorX;
				mMouseOriY = ev.CursorY;
				mMousePosX = mMouseOriX;
				mMousePosY = mMouseOriY;

				// why aint it canvas?
				//ev.Element
				//ev.CaptureMouse();

				// using ?
				ev.Element.requestPointerLock();
				await ev.Element.async.onmouseup;
				Native.document.exitPointerLock();

				mMouseOriX = -Math.Abs(mMouseOriX);
				mMouseOriY = -Math.Abs(mMouseOriY);
			};

			c.onmousemove += ev =>
			{
				if (ev.MouseButton == IEvent.MouseButtonEnum.Left)
				{
					mMousePosX += ev.movementX;
					mMousePosY += ev.movementY;
				}
			};

			c.onmousewheel += ev =>
			{
				ev.preventDefault();
				ev.stopPropagation();

				mMousePosY += 3 * ev.WheelDirection;
			};

			#endregion



			// http://www.wufoo.com/html5/attributes/05-list.html
			// http://www.w3schools.com/tags/att_input_list.asp
			//uiauto.datalist1.EnsureID();
			//uiauto.search.list = uiauto.datalist1.id;
			//uiauto.datalist1.id = "datalist1";
			//uiauto.search.list = "datalist1";
			//new IHTMLPre { new { uiauto.search.list, uiauto.datalist1.id } }.AttachToDocument();

			var sw = Stopwatch.StartNew();


			var programs = new Dictionary<string, Func<FragmentShader>>
			{
				// should we want to generate it?

				// group by runs on all devices, fps?
				// tags?

				//  /FilterTo:$(SolutionDir)
				// how will those shaders look like on VR?

				["ChromeShaderToyColumns"] = () => new ChromeShaderToyColumns.Shaders.ProgramFragmentShader(),
			};

			new IHTMLOption { value = "", innerText = $"{programs.Count} shaders available" }.AttachTo(combo);

			// we have one goal. show em all.

			//var g = References.programs.Keys group by 4x4





			// http://stackoverflow.com/questions/25289390/html-how-to-make-input-type-list-only-accept-a-list-choice
			programs.Keys.WithEachIndex(
				async (key, index) =>
				{
					var text = (1 + index) + " of " + programs.Count + " " + key.SkipUntilIfAny("ChromeShaderToy").Replace("By", " by ");

					// can we get tooltips on canvas?
					var option = new IHTMLOption { value = key, innerText = text }.AttachTo(combo);

					await Native.window.async.onframe;

					// we are about to create 100 objects. does it have any impact to UI?
					var frag = programs[key]();
					var len = frag.ToString().Length;

					option.innerText = text + " " + new
					{
						//frame,
						//load = load.ElapsedMilliseconds + "ms ",

						frag = len + "bytes ",
						// a telemetry to track while running on actual hardware
						//fragGPU = pass0.xCreateShader.fsTranslatedShaderSource.Length + " bytes"
					};

					await option.async.onselect;
					await Native.window.async.onframe;

					var load = Stopwatch.StartNew();

					var pass0 = new ShaderToy.EffectPass(
						gl: gl,
						precission: ShaderToy.DetermineShaderPrecission(gl),
						supportDerivatives: gl.getExtension("OES_standard_derivatives") != null
					);
					pass0.MakeHeader_Image();
					pass0.NewShader_Image(frag);

					load.Stop();




					var frame = 0;
					do
					{
						frame++;

						//option.innerText = key + new { frame };
						option.innerText = text + " " + new
						{
							frame,
							load = load.ElapsedMilliseconds + "ms ",

							frag = len + "bytes ",
							// a telemetry to track while running on actual hardware
							fragGPU = pass0.xCreateShader.fsTranslatedShaderSource.Length + " bytes"
						};

						var vbo = new WebGLBuffer(gl);


						ShaderToy.EffectPass.Paint_ImageDelegate Paint_Image = (time, mouseOriX, mouseOriY, mousePosX, mousePosY, zoom) =>
						{
							var mProgram = pass0.xCreateShader.mProgram;

							//var y = gl.canvas.height * 0.1;

							var xres = gl.canvas.width * zoom;
							var yres = gl.canvas.height * zoom;

							#region Paint_Image
							//new IHTMLPre { "enter Paint_Image" }.AttachToDocument();

							// this is enough to do pip to bottom left, no need to adjust vertex positions even?
							// x and y cannot be used to translate our box, need to move vertex pos then?
							// cannot move or upscale the viewport locked area. so we need to lock it all?
							// how many pixels will we be renderring then?

							// lock the correct region. unzoomed
							// in a way this creates a cool field of view effect anchored to left bottom
							// some programs render on the left bottom, only, while some spill over to the unseen areas.
							// could we use multiple rendering for different detail level?
							// like the edges we could keep low res? 
							//gl.viewport(0, 0, (int)gl.canvas.width, (int)gl.canvas.height);

							// useProgram: program not valid
							gl.useProgram(mProgram);

							// uniform4fv
							var mouse = new[] { mousePosX, mousePosY, mouseOriX, mouseOriY };

							// X:\jsc.svn\examples\glsl\future\GLSLShaderToyPip\GLSLShaderToyPip\Application.cs
							//gl.getUniformLocation(mProgram, "fZoom").With(fZoom => gl.uniform1f(fZoom, zoom));


							var l2 = gl.getUniformLocation(mProgram, "iGlobalTime"); if (l2 != null) gl.uniform1f(l2, time);
							var l3 = gl.getUniformLocation(mProgram, "iResolution"); if (l3 != null) gl.uniform3f(l3, xres, yres, 1.0f);
							var l4 = gl.getUniformLocation(mProgram, "iMouse"); if (l4 != null) gl.uniform4fv(l4, mouse);
							//var l7 = gl.getUniformLocation(this.mProgram, "iDate"); if (l7 != null) gl.uniform4fv(l7, dates);
							//var l9 = gl.getUniformLocation(this.mProgram, "iSampleRate"); if (l9 != null) gl.uniform1f(l9, this.mSampleRate);

							var ich0 = gl.getUniformLocation(mProgram, "iChannel0"); if (ich0 != null) gl.uniform1i(ich0, 0);
							var ich1 = gl.getUniformLocation(mProgram, "iChannel1"); if (ich1 != null) gl.uniform1i(ich1, 1);
							var ich2 = gl.getUniformLocation(mProgram, "iChannel2"); if (ich2 != null) gl.uniform1i(ich2, 2);
							var ich3 = gl.getUniformLocation(mProgram, "iChannel3"); if (ich3 != null) gl.uniform1i(ich3, 3);


							// do cannot yet do shader textures can we


							//for (var i = 0; i < mInputs.Length; i++)
							//{
							//	var inp = mInputs[i];

							//	gl.activeTexture((uint)(gl.TEXTURE0 + i));

							//	if (inp == null)
							//	{
							//		gl.bindTexture(gl.TEXTURE_2D, null);
							//	}
							//}

							var times = new[] { 0.0f, 0.0f, 0.0f, 0.0f };
							var l5 = gl.getUniformLocation(mProgram, "iChannelTime");
							if (l5 != null) gl.uniform1fv(l5, times);

							var resos = new float[12] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };
							var l8 = gl.getUniformLocation(mProgram, "iChannelResolution");
							if (l8 != null) gl.uniform3fv(l8, resos);




							// which are we in the group?
							//var xindex = index % (1 / zoom);

							for (int xindex = 0; xindex < 2; xindex++)
								for (int yindex = 0; yindex < 2; yindex++)
								{
									// will zero origin clip the others?
									// no it will scale everything down?

									// http://stackoverflow.com/questions/23968364/webgl-is-the-gl-useprogram-an-expensive-call-to-make
									// http://stackoverflow.com/questions/23968364/webgl-is-the-gl-useprogram-an-expensive-call-to-make
									gl.viewport(

										xindex * (int)(gl.canvas.width * zoom),
										yindex * (int)(gl.canvas.height * zoom),
										(int)(gl.canvas.width * zoom),
										(int)(gl.canvas.height * zoom)

										);
									//gl.trans

									float left = -1.0f
										+ xindex * zoom * 2.0f;

									// y reversed?

									float top =
										-(-1.0f
										+ yindex * zoom * 2.0f);


									float bottom =
										-(-top + 2.0f * zoom);




									float right = left + 2.0f * zoom;

									// x % 4


									// add spacing

									left += 0.1f;
									right -= 0.1f;

									bottom += 0.1f;
									top -= 0.1f;

									// using ?
									var l1 = (uint)gl.getAttribLocation(mProgram, "pos");

									// using gl, vbo, vertices
									gl.bindBuffer(gl.ARRAY_BUFFER, vbo);

									//// can we translate  to right?
									//left += 0.5f;

									//right += 0.5f;

									#region vertices
									var fvertices =
										new float[]
										{
											// left top
											left, bottom,

											// right top
											//right, -1.0f,
											right, bottom,

											// left bottom
											left, top,

											// right top
											//right, -1.0f,
											right, bottom,

											// right bottom
											//right, 1.0f,
											right, top,

											// left bottom
											left,top
										};

									var vertices = new Float32Array(fvertices);
									#endregion
									gl.bufferData(gl.ARRAY_BUFFER, vertices, gl.STATIC_DRAW);

									gl.vertexAttribPointer(l1, 2, gl.FLOAT, false, 0, 0);
									gl.enableVertexAttribArray(l1);

									gl.drawArrays(gl.TRIANGLES, 0, 6);
									// first frame is now visible
									gl.disableVertexAttribArray(l1);
									gl.bindBuffer(gl.ARRAY_BUFFER, null);
								}
							#endregion


						};

						// tile up the 4K monitor.
						Paint_Image(
							sw.ElapsedMilliseconds / 1000.0f,

							mMouseOriX,
							mMouseOriY,
							mMousePosX,
							mMousePosY,

							//zoom: 0.25f
							zoom: 0.5f
						);

						// what does it do?
						gl.flush();

						// wither we are selected or we are pip?
						await option.async.selected;
					}
					while (await Native.window.async.onframe);

				}
			);

		}

	}
}
