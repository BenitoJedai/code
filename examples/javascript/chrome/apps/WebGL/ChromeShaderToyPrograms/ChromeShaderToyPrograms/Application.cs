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
			// https://www.youtube.com/watch?v=tnS8K0yhmZU
			// http://www.reddit.com/r/oculus/comments/2sv5lk/new_release_of_shadertoy_vr/
			// https://www.shadertoy.com/view/lsSGRz

			// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201503/20150309
			// https://zproxy.wordpress.com/2015/03/09/project-windstorm/


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


			//Native.document.documentElement.style.overflow = IStyle.OverflowEnum.auto;
			Native.body.style.margin = "0px";

			var gl = new WebGLRenderingContext(alpha: true);

			if (gl == null)
			{

				new IHTMLPre { "gpu failure. restart browser." }.AttachToDocument();
				return;
			}

			var combo = new IHTMLSelect().AttachToDocument();

			combo.style.position = IStyle.PositionEnum.absolute;
			combo.style.left = "0px";
			combo.style.top = "0px";
			//combo.style.right = "0px";
			combo.style.width = "100%";

			combo.style.backgroundColor = "rgba(255,255,255,0.5)";
			combo.style.border = "0px solid transparent";


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

			// http://stackoverflow.com/questions/17047602/proper-way-to-initialize-a-c-sharp-dictionary-with-values-already-in-it
			var programs = new Dictionary<string, Func<FragmentShader>>
			{
				//  /FilterTo:$(SolutionDir)
				// how will those shaders look like on VR?

				["ChromeShaderToyColumns"] = () => new ChromeShaderToyColumns.Shaders.ProgramFragmentShader(),

				// can we have alt-tab pip ?

				["ChromeShaderToyAlbertArchesByDr2"] = () => new ChromeShaderToyAlbertArchesByDr2.Shaders.ProgramFragmentShader(),
				["ChromeShaderToyAnimationByFlyguy"] = () => new ChromeShaderToyAnimationByFlyguy.Shaders.ProgramFragmentShader(),

				["ChromeShaderToyBenderByIq"] = () => new ChromeShaderToyBenderByIq.Shaders.ProgramFragmentShader(),

				["ChromeShaderToyCatchByAhihi"] = () => new ChromeShaderToyCatchByAhihi.Shaders.ProgramFragmentShader(),
				["ChromeShaderToyCentaursByErucipe"] = () => new ChromeShaderToyCentaursByErucipe.Shaders.ProgramFragmentShader(),
				["ChromeShaderToyCubeOfCubesByFlyguy"] = () => new ChromeShaderToyCubeOfCubesByFlyguy.Shaders.ProgramFragmentShader(),
				["ChromeShaderToyCubicEntanglementByEiffie"] = () => new ChromeShaderToyCubicEntanglementByEiffie.Shaders.ProgramFragmentShader(),

				["ChromeShaderToyDesertMorningByEPitz"] = () => new ChromeShaderToyDesertMorningByEPitz.Shaders.ProgramFragmentShader(),
				["ChromeShaderToyDiningRoomBySquid"] = () => new ChromeShaderToyDiningRoomBySquid.Shaders.ProgramFragmentShader(),
				["ChromeShaderToyDoomE1M1ByPMalin"] = () => new ChromeShaderToyDoomE1M1ByPMalin.Shaders.ProgramFragmentShader(),
				["ChromeShaderToyDustStormByNimitz"] = () => new ChromeShaderToyDustStormByNimitz.Shaders.ProgramFragmentShader(),
				["ChromeShaderToyDustyByHat"] = () => new ChromeShaderToyDustyByHat.Shaders.ProgramFragmentShader(),

				["ChromeShaderToyEiffieBallsByEiffie"] = () => new ChromeShaderToyEiffieBallsByEiffie.Shaders.ProgramFragmentShader(),
				["ChromeShaderToyEiffieBox"] = () => new ChromeShaderToyEiffieBox.Shaders.ProgramFragmentShader(),
				["ChromeShaderToyExplosionByGreen"] = () => new ChromeShaderToyExplosionByGreen.Shaders.ProgramFragmentShader(),

				["ChromeShaderToyFireballByGreen"] = () => new ChromeShaderToyFireballByGreen.Shaders.ProgramFragmentShader(),
				["ChromeShaderToyFlowingLavaByFizzer"] = () => new ChromeShaderToyFlowingLavaByFizzer.Shaders.ProgramFragmentShader(),
				["ChromeShaderToyFlyingBoatByGaz"] = () => new ChromeShaderToyFlyingBoatByGaz.Shaders.ProgramFragmentShader(),
				["ChromeShaderToyFrozenWastelandByDave"] = () => new ChromeShaderToyFrozenWastelandByDave.Shaders.ProgramFragmentShader(),

				["ChromeShaderToyHetchyScketchyByXbe"] = () => new ChromeShaderToyHetchyScketchyByXbe.Shaders.ProgramFragmentShader(),
				["ChromeShaderToyKajastusByMarken"] = () => new ChromeShaderToyKajastusByMarken.Shaders.ProgramFragmentShader(),
				["ChromeShaderToyLimboByDaeken"] = () => new ChromeShaderToyLimboByDaeken.Shaders.ProgramFragmentShader(),
				["ChromeShaderToyllamelsByEiffie"] = () => new ChromeShaderToyllamelsByEiffie.Shaders.ProgramFragmentShader(),

				["ChromeShaderToyMinecraftBlocksByReinder"] = () => new ChromeShaderToyMinecraftBlocksByReinder.Shaders.ProgramFragmentShader(),
				["ChromeShaderToyMetatunnelByDuprat"] = () => new ChromeShaderToyMetatunnelByDuprat.Shaders.ProgramFragmentShader(),
				["ChromeShaderToyMorningCityByDevin"] = () => new ChromeShaderToyMorningCityByDevin.Shaders.ProgramFragmentShader(),
				["ChromeShaderToyMotionBlurByKig"] = () => new ChromeShaderToyMotionBlurByKig.Shaders.ProgramFragmentShader(),
				["ChromeShaderToyMountainsByHoskins"] = () => new ChromeShaderToyMountainsByHoskins.Shaders.ProgramFragmentShader(),

				["ChromeShaderToyOblivionByNimitz"] = () => new ChromeShaderToyOblivionByNimitz.Shaders.ProgramFragmentShader(),
				["ChromeShaderToyPigsRunningByGaz"] = () => new ChromeShaderToyPigsRunningByGaz.Shaders.ProgramFragmentShader(),
				["ChromeShaderToyPortalByHLorenzi"] = () => new ChromeShaderToyPortalByHLorenzi.Shaders.ProgramFragmentShader(),
				["ChromeShaderToyPortalTurretByMattz"] = () => new ChromeShaderToyPortalTurretByMattz.Shaders.ProgramFragmentShader(),
				["ChromeShaderToyPrimitivesByQuilez"] = () => new ChromeShaderToyPrimitivesByQuilez.Shaders.ProgramFragmentShader(),
				["ChromeShaderToyRaymarchByElias"] = () => new ChromeShaderToyRaymarchByElias.Shaders.ProgramFragmentShader(),
				["ChromeShaderToyRecogniserByFizzer"] = () => new ChromeShaderToyRecogniserByFizzer.Shaders.ProgramFragmentShader(),
				//["ChromeShaderToyRefractionByHLorenzi"] = () => new ChromeShaderToyRefractionByHLorenzi.Shaders.ProgramFragmentShader(),
				["ChromeShaderToyRelentlessBySrtuss"] = () => new ChromeShaderToyRelentlessBySrtuss.Shaders.ProgramFragmentShader(),
				["ChromeShaderToyRotatingByGa2arch"] = () => new ChromeShaderToyRotatingByGa2arch.Shaders.ProgramFragmentShader(),

				["ChromeShaderToySandDunesByBoinx"] = () => new ChromeShaderToySandDunesByBoinx.Shaders.ProgramFragmentShader(),
				["ChromeShaderToySeascapeByTDM"] = () => new ChromeShaderToySeascapeByTDM.Shaders.ProgramFragmentShader(),
				["ChromeShaderToyShadyBuildingByZtri"] = () => new ChromeShaderToyShadyBuildingByZtri.Shaders.ProgramFragmentShader(),
				["ChromeShaderToySpaceRaceByKali"] = () => new ChromeShaderToySpaceRaceByKali.Shaders.ProgramFragmentShader(),
				["ChromeShaderToySphereAndWalls"] = () => new ChromeShaderToySphereAndWalls.Shaders.ProgramFragmentShader(),
				["ChromeShaderToySpitfirePursuitByDr2"] = () => new ChromeShaderToySpitfirePursuitByDr2.Shaders.ProgramFragmentShader(),

				["ChromeShaderToyTextCandyByCPU"] = () => new ChromeShaderToyTextCandyByCPU.Shaders.ProgramFragmentShader(),
				["ChromeShaderToyTexturedEllipsoidsByFabrice"] = () => new ChromeShaderToyTexturedEllipsoidsByFabrice.Shaders.ProgramFragmentShader(),
				["ChromeShaderToyTokyoByReinder"] = () => new ChromeShaderToyTokyoByReinder.Shaders.ProgramFragmentShader(),
				["ChromeShaderToyTrainByDr2"] = () => new ChromeShaderToyTrainByDr2.Shaders.ProgramFragmentShader(),
				["ChromeShaderToyTrainRideByDr2"] = () => new ChromeShaderToyTrainRideByDr2.Shaders.ProgramFragmentShader(),

				["ChromeShaderToyWetStoneByTDM"] = () => new ChromeShaderToyWetStoneByTDM.Shaders.ProgramFragmentShader(),
				["ChromeShaderToyWolfensteinByReinder"] = () => new ChromeShaderToyWolfensteinByReinder.Shaders.ProgramFragmentShader(),

				// cube?
				//["BokehBlurByKabuto"] = () => new ChromeShaderToyBokehBlurByKabuto.Shaders.ProgramFragmentShader(),
				//["VRCardboardGrid"] = () => new ChromeShaderToyVRCardboardGrid.Shaders.ProgramFragmentShader(),
			};

			// http://www.wufoo.com/html5/attributes/05-list.html
			// http://www.w3schools.com/tags/att_input_list.asp
			//uiauto.datalist1.EnsureID();
			//uiauto.search.list = uiauto.datalist1.id;
			//uiauto.datalist1.id = "datalist1";
			//uiauto.search.list = "datalist1";
			//new IHTMLPre { new { uiauto.search.list, uiauto.datalist1.id } }.AttachToDocument();

			var sw = Stopwatch.StartNew();


			new IHTMLOption { value = "", innerText = $"{programs.Count} shaders available" }.AttachTo(combo);

			// http://stackoverflow.com/questions/25289390/html-how-to-make-input-type-list-only-accept-a-list-choice
			programs.Keys.WithEachIndex(
				async (key, index) =>
				{
					var text = index + " " + key.SkipUntilIfAny("ChromeShaderToy").Replace("By", " by ");

					var option = new IHTMLOption { value = key, innerText = text }.AttachTo(combo);

					await option.async.onselect;
					await Native.window.async.onframe;

					var load = Stopwatch.StartNew();

					var frag = programs[key]();
					var len = frag.ToString().Length;

					//do
					//{
					//	// allow any deselect to happen
					//	await Native.window.async.onframe;
					//	new IHTMLPre { "option select " + new { key, combo.selectedIndex, fragSource.Length } }.AttachToDocument();

					//	await option.async.ondeselect;
					//	new IHTMLPre { "option ondeselect " + new { key } }.AttachToDocument();
					//}
					//while (await option.async.onselect);


					var pass0 = new ShaderToy.EffectPass(
				mAudioContext,
				gl,
				precission: ShaderToy.DetermineShaderPrecission(gl),
				supportDerivatives: gl.getExtension("OES_standard_derivatives") != null,
				callback: null,
				obj: null,
				forceMuted: false,
				forcePaused: false,
				quadVBO: ShaderToy.createQuadVBO(gl),
				outputGainNode: null
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
							source = len + "bytes "
						};

						// can we scale?
						pass0.Paint_Image(
			sw.ElapsedMilliseconds / 1000.0f,

			mMouseOriX,
			mMouseOriY,
			mMousePosX,
			mMousePosY
		);

						// what does it do?
						gl.flush();


						await option.async.selected;
					}
					while (await Native.window.async.onframe);

				}
			);





		}

	}
}
