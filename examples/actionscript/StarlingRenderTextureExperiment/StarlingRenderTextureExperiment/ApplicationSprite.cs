using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.utils;
using ScriptCoreLib.Extensions;
using starling.core;
using starling.display;
using starling.text;
using starling.textures;
using System;
using System.Diagnostics;

namespace StarlingRenderTextureExperiment
{
	// HD
	[SWF(frameRate = 120, width = 1280, height = 720)]
	public sealed class ApplicationSprite : ScriptCoreLib.ActionScript.flash.display.Sprite
	{

		public ApplicationSprite()
		{
			// i see white.

			bool once = false;

			this.stage.keyUp +=
				   e =>
				   {
					   if (e.keyCode == (uint)System.Windows.Forms.Keys.F11)
					   {
						   this.stage.SetFullscreen(true);
					   }
				   };

			this.stage.click +=
				delegate
				{

					if (once)
						return;

					once = true;

					// http://gamua.com/starling/first-steps/

					__stage = this.stage;

					//stage.align = ScriptCoreLib.ActionScript.flash.display.StageAlign.TOP_LEFT;
					//stage.scaleMode = ScriptCoreLib.ActionScript.flash.display.StageScaleMode.NO_SCALE;

					Starling.handleLostContext = true;

					var s = new Starling(
						typeof(Game).ToClassToken(),
						this.stage
					);

					//s.enableErrorChecking = true;
					s.start();

					// http://forum.starling-framework.org/topic/starling-stage-resizing
					this.stage.resize += delegate
					{
						// http://forum.starling-framework.org/topic/starling-stage-resizing

						s.viewPort = new ScriptCoreLib.ActionScript.flash.geom.Rectangle(
							0, 0, this.stage.stageWidth, this.stage.stageHeight
						);

						s.stage.stageWidth = this.stage.stageWidth;
						s.stage.stageHeight = this.stage.stageHeight;
					};

					s.viewPort = new ScriptCoreLib.ActionScript.flash.geom.Rectangle(
						 0, 0, this.stage.stageWidth, this.stage.stageHeight
					 );

					s.stage.stageWidth = this.stage.stageWidth;
					s.stage.stageHeight = this.stage.stageHeight;
				};
		}

		public static ScriptCoreLib.ActionScript.flash.display.Stage __stage;
	}

	public class Game : Sprite
	{


		public Game()
		{

			// http://forum.starling-framework.org/topic/framerate-drops-a-lot
			// http://forum.starling-framework.org/topic/30-fps-in-chrome-but-60-fps-in-internet-explorer-huh
			// http://forum.starling-framework.org/topic/frame-rate-oddities
			// http://forums.tigsource.com/index.php?topic=23953.0
			// IE 59
			// FF 56
			// Chrome, 62? after a restart of chrome!

			var info = new TextField(100, 400, "Welcome to StarlingRenderTextureExperiment!");
			addChild(info);

			var maxframe = new Stopwatch();
			var maxframe_elapsed = 0.0;


			var xinfo = new TextField(400, 300, "Welcome to StarlingRenderTextureExperiment!");
			var xsw = new Stopwatch();
			xsw.Start();

			var content_rot = new Sprite();

			var texture0 = Texture.fromBitmap(new ActionScript.Images.jsc());

			////var cc = 128; // 10 FPS
			//var cc = 64; // 33 FPS, 44 FPS
			var cc = 128; //59

			var bytes = 0;

			var texsize = 512;

			#region new_tile

			Func<Image> new_tile = delegate
			{

				//var rtex = new RenderTexture(2048, 2048, true, 1);
				var rtex = new RenderTexture(texsize, texsize, true, 1);
				var rimg = new Image(rtex);


				var img0 = new Image(texture0);

				img0.scaleX = 0.3;
				img0.scaleY = 0.3;

				Action updatetexture = delegate
				{
					rtex.drawBundled(
						new Action(
							delegate
							{
								for (int iy = 0; iy <= cc; iy++)
									for (int ix = 0; ix <= cc; ix++)
									{


										img0.x = ix * 64 * img0.scaleX;
										img0.y = iy * 64 * img0.scaleY;

										if (img0.x < rtex.width)
											if (img0.y < rtex.height)
												rtex.draw(img0);
									}
							}
						).ToFunction()
					);

				};

				updatetexture();

				ApplicationSprite.__stage.stage3Ds[0].context3DCreate +=
					delegate
					{
						rtex = new RenderTexture(texsize, texsize, true, 1);

						updatetexture();

						rimg.texture = rtex;
					};

				bytes += texsize * texsize * 6;

				return rimg;
			};
			#endregion


			var memory_for_text = new RenderTexture(1024, 1024, true, 1);

			//        Error: Error #3691: Resource limit for this resource type exceeded.
			//at flash.display3D::Context3D/createTexture()

			var count = 0;

			for (int iy = -1; iy <= 4; iy++)
			{
				for (int ix = -1; ix <= 4; ix++)
				{
					try
					{
						var rimg = new_tile();
						rimg.scaleX = 0.1;
						rimg.scaleY = 0.1;
						rimg.y = iy * texsize * rimg.scaleX;
						rimg.x = ix * texsize * rimg.scaleY;
						content_rot.addChild(rimg);

						count++;
					}
					catch
					{
						// skip it
					}
				}
			}

			memory_for_text.dispose();

			//            StarlingRenderTextureExperiment.ApplicationSprite+__in_Delegate__in_Method
			//System.NullReferenceException: Object reference not set to an instance of an object.
			//   at jsc.Languages.ActionScript.ActionScriptCompiler.WriteMethodCallVerified(Prestatement p, ILInstruction i, MethodBase m) in x:\jsc.internal.svn\compiler\jsc\Languages\ActionScript\ActionScriptCompiler.WriteMethodCallVerified.cs:line 99
			//   at jsc.Script.CompilerBase.WriteMethodCall(Prestatement p, ILInstruction i, MethodBase m) in x:\jsc.internal.svn\compiler\jsc\Languages\CompilerBase.cs:line 1307
			//script: error JSC1000: ActionScript : failure at starling.display.DisplayObject.add_enterFrame : Object reference not set to an instance of an object.



			// do events from flash native actually work?
			ApplicationSprite.__stage.enterFrame +=
				delegate
				{
					content_rot.rotation += 0.0001 * xsw.ElapsedMilliseconds;

					xsw.Restart();
				};


			var loc = new Sprite();

			content_rot.addChild(xinfo);
			loc.addChild(content_rot);

			loc.x = 200;
			loc.y = 200;

			addChild(loc);



			#region fps
			var sw = new Stopwatch();

			sw.Start();

			var ii = 0;

			maxframe.Start();
			#region enterFrame
			ApplicationSprite.__stage.enterFrame +=
				delegate
				{
					maxframe.Stop();

					//                    System.TimeSpan for Boolean op_GreaterThan(System.TimeSpan, System.TimeSpan) used at
					//FlashHeatZeeker.ApplicationSprite+<>c__DisplayClass11.<.ctor>b__d at offset 001e.

					//                TypeError: Error #1009: Cannot access a property or method of a null object reference.
					//at FlashHeatZeeker::ApplicationSprite___c__DisplayClass11/__ctor_b__d_100663322()[U:\web\FlashHeatZeeker\ApplicationSprite___c__DisplayClass11.as:141]

					if (maxframe.Elapsed.TotalMilliseconds > maxframe_elapsed)
						maxframe_elapsed = maxframe.Elapsed.TotalMilliseconds;

					if (sw.ElapsedMilliseconds < 1000)
					{
						ii++;

						maxframe.Restart();

						return;
					}

					var KB = bytes / 1024;
					var MB = KB / 1024;

					// 335 - 28
					// 67MB for other?

					// 321 - 25 = 296
					info.text = new { count, bytes, KB, system_GPU_memory = MB + "MB", fps = ii, maxframe_elapsed }.ToString();

					//if (fps != null)
					//    fps("" + ii);

					ii = 0;
					maxframe_elapsed = 0;
					sw.Restart();
				};
			#endregion

			#endregion
		}
	}
}
