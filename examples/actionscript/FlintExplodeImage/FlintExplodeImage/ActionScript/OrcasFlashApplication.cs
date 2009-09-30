using System;
using System.Collections.Generic;
using FlintExplodeImage.ActionScript.Flint.org.flintparticles.threeD.actions;
using FlintExplodeImage.ActionScript.Flint.org.flintparticles.threeD.emitters;
using FlintExplodeImage.ActionScript.Flint.org.flintparticles.threeD.particles;
using FlintExplodeImage.ActionScript.Flint.org.flintparticles.threeD.renderers;
using FlintExplodeImage.ActionScript.Flint.org.flintparticles.threeD.zones;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.events;
using ScriptCoreLib.ActionScript.flash.text;

namespace FlintExplodeImage.ActionScript
{
	using Vector3D = global::FlintExplodeImage.ActionScript.Flint.org.flintparticles.threeD.geom.Vector3D;

	/// <summary>
	/// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
	/// </summary>
	[Script, ScriptApplicationEntryPoint(WithResources = true)]
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
			addChild(txt);

			bitmap = KnownEmbeddedResources.Default["assets/FlintExplodeImage/184098.jpg"].ToBitmapAsset();

			renderer = new DisplayObjectRenderer();
			renderer.camera.dolly(-400);
			renderer.camera.projectionDistance = 400;
			renderer.y = 175;
			renderer.x = 250;
			addChild(renderer);

			emitter = new Emitter3D();
			emitter.addAction(new Move(), 0);
			emitter.addAction( new DeathZone( new FrustrumZone( renderer.camera, new ScriptCoreLib.ActionScript.flash.geom.Rectangle( -290, -215, 580, 430 ) ), true ) , 0);
			emitter.position = new Vector3D(0, 0, 0, 1);

			var particles = Particle3DUtils.createRectangleParticlesFromBitmapData( bitmap.bitmapData, 20, emitter.particleFactory, new Vector3D( -192, 127, 0 ) );
			emitter.addExistingParticles(particles, false);

			renderer.addEmitter(emitter);
			emitter.start();

			stage.click += explode;

			KnownEmbeddedResources.Default["assets/FlintExplodeImage/jsc.png"].ToBitmapAsset().AttachTo(this).MoveTo(100, 100);
		}

		public void explode(MouseEvent ev)
		{
			var p = renderer.globalToLocal(new ScriptCoreLib.ActionScript.flash.geom.Point(ev.stageX, ev.stageY));
			emitter.addAction(new Explosion(8, new Vector3D(p.x, -p.y, 50), 500), 0);
			stage.click -= explode;
		}



	

	}

	

}