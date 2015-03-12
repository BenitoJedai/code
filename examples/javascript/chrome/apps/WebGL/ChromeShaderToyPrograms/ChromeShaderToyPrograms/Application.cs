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
using ChromeShaderToyPrograms;
using ChromeShaderToyPrograms.Design;
using ChromeShaderToyPrograms.HTML.Pages;
using ChromeShaderToyColumns.Library;
using System.Diagnostics;
using ScriptCoreLib.JavaScript.WebAudio;
using ScriptCoreLib.JavaScript.WebGL;
using ChromeShaderToyColumns.HTML.Pages;
using ScriptCoreLib.GLSL;

namespace ChromeShaderToyPrograms
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
		public Application(HTML.Pages.IApp page)
		{
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
						"error " + new { e.error }
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



			var mAudioContext = new AudioContext();


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


			new IHTMLOption { value = "", innerText = $"{References.programs.Count} shaders available" }.AttachTo(combo);

			ShaderToy.EffectPass pip = null;

			// http://stackoverflow.com/questions/25289390/html-how-to-make-input-type-list-only-accept-a-list-choice
			References.programs.Keys.WithEachIndex(
				async (key, index) =>
				{
					var text = (1 + index) + " of " + References.programs.Count + " " + key.SkipUntilIfAny("ChromeShaderToy").Replace("By", " by ");

					var option = new IHTMLOption { value = key, innerText = text }.AttachTo(combo);

					await Native.window.async.onframe;

					// we are about to create 100 objects. does it have any impact to UI?
					var frag = References.programs[key]();
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

					new { }.With(
						async delegate
						{
							while (await option.async.ondeselect)
							{
								pip = pass0;

								await option.async.onselect;
							}
						}
					);


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

						// can we scale?
						pass0.Paint_Image(
							sw.ElapsedMilliseconds / 1000.0f,

							mMouseOriX,
							mMouseOriY,
							mMousePosX,
							mMousePosY,

							zoom: 1.0f
						);

						if (pip != null)
						{
							// can we scale?
							pip.Paint_Image(
								sw.ElapsedMilliseconds / 1000.0f,

								mMouseOriX,
								mMouseOriY,
								mMousePosX,
								mMousePosY,

								zoom: 0.10f
							);

						}

						// what does it do?
						gl.flush();

						// wither we are selected or we are pip?
						await option.async.selected;
					}
					while (await Native.window.async.onframe);

				}
			);





		}


		//		cleaned { id = WebGL.ShaderToy
		//	}
		//	updating { id = WebGL.ShaderToy, ElapsedMilliseconds = 0 }
		//copy { RestorePackagesFromFile = c:/util/jsc/nuget/WebGL.ShaderToy.1.0.0.0.nupkg, ElapsedMilliseconds = 0, path = C:\Users\Administrator\AppData\Local\NuGet\Cache\WebGL.ShaderToy.1.0.0.0.nupkg }
		//file in use...
		//file in use...
		//file in use...
		//file in use...
		//file in use...
		//file in use...
		//System.IO.IOException: The process cannot access the file 'C:\Users\Administrator\AppData\Local\NuGet\Cache\WebGL.ShaderToy.1.0.0.0.nupkg' because it is being used by another process.
		//   at System.IO.__Error.WinIOError(Int32 errorCode, String maybeFullPath)
		//   at System.IO.FileStream.Init(String path, FileMode mode, FileAccess access, Int32 rights, Boolean useRights, FileShare share, Int32 bufferSize, FileOptions options, SECURITY_ATTRIBUTES secAttrs, St
		//ring msgPath, Boolean bFromProxy, Boolean useLongPath, Boolean checkHost)
		//   at System.IO.FileStream..ctor(String path, FileMode mode, FileAccess access, FileShare share)
		//   at NuGet.ZipPackage.<>c__DisplayClass2.<.ctor>b__0()
		//   at NuGet.ZipPackage.EnsureManifest()
		//   at NuGet.ZipPackage..ctor(String filePath, Boolean enableCaching)
		//   at NuGet.ZipPackage..ctor(String filePath)
		//   at jsc.meta.Commands.Reference.ReferenceAssetsLibrary.<>c__DisplayClass20_3.<InternalInvoke>b__39() in X:\jsc.internal.git\compiler\jsc.internal\jsc.internal\meta\Commands\Reference\ReferenceAssets
		//Library.cs:line 451
	}
}
