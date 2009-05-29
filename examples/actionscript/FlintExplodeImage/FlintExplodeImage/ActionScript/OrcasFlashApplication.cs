using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.Extensions;
using System.Collections.Generic;
using System;
using System.IO;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.flash.ui;
using FlintExplodeImage.Shared;
using ScriptCoreLib.ActionScript.flash.events;
using ScriptCoreLib.ActionScript.flash.geom;

namespace FlintExplodeImage.ActionScript
{
	/// <summary>
	/// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
	/// </summary>
	[Script, ScriptApplicationEntryPoint]
	[SWF]
	public class FlintExplodeImage : Sprite
	{
		private Emitter3D emitter;
		private Bitmap bitmap;
		private DisplayObjectRenderer renderer;

		public FlintExplodeImage()
		{
			var txt = new TextField();
			txt.text = "Click on the image";
			txt.textColor = 0xFFFFFF;
			addChild( txt );

			bitmap =  KnownEmbeddedResources.Default[KnownAssets.Path.Assets + "/184098.jpg"].ToBitmapAsset();
			
			renderer = new DisplayObjectRenderer();
			renderer.camera.dolly( -400 );
			renderer.camera.projectionDistance = 400;
			renderer.y = 175;
			renderer.x = 250;
			addChild( renderer );
			
			emitter = new Emitter3D();
			emitter.addAction( new Move() );
			emitter.addAction( new DeathZone( new FrustrumZone( renderer.camera, new Rectangle( -290, -215, 580, 430 ) ), true ) );
			emitter.position = new Vector3D( 0, 0, 0, 1 );

			var particles = Particle3DUtils.createRectangleParticlesFromBitmapData( bitmap.bitmapData, 20, emitter.particleFactory, new Vector3D( -192, 127, 0 ) );
			emitter.addExistingParticles( particles, false );
									
			renderer.addEmitter( emitter );
			emitter.start();

			stage.click += explode;
		}

		public void explode(MouseEvent ev)
		{
			var p = renderer.globalToLocal(new Point(ev.stageX, ev.stageY));
			emitter.addAction(new Explosion(8, new Vector3D(p.x, -p.y, 50), 500));
			stage.click -= explode;
		}



		static FlintExplodeImage()
		{
			// add resources to be found by ImageSource
			KnownEmbeddedAssets.RegisterTo(
				KnownEmbeddedResources.Default.Handlers
			);
		}

	}

	[Script]
	public class KnownEmbeddedAssets
	{
		[EmbedByFileName]
		public static Class ByFileName(string e)
		{
			throw new NotImplementedException();
		}

		public static void RegisterTo(List<Converter<string, Class>> Handlers)
		{
			// assets from current assembly
			Handlers.Add(e => ByFileName(e));

			//AvalonUgh.Assets.ActionScript.KnownEmbeddedAssets.RegisterTo(Handlers);

			//// assets from referenced assemblies
			//Handlers.Add(e => global::ScriptCoreLib.ActionScript.Avalon.Cursors.EmbeddedAssets.Default[e]);
			//Handlers.Add(e => global::ScriptCoreLib.ActionScript.Avalon.TiledImageButton.Assets.Default[e]);

		}
	}

}