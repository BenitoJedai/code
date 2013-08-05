package  
{
	import flare.basic.*;
	import flare.core.*;
	import flare.flsl.*;
	import flare.loaders.*;
	import flare.primitives.*;
	import flash.display.*;
	import flash.events.*;
	import flash.geom.*;
	
	/**
	 * @author Ariel Nehmad
	 */
	public class Step03_Mirror extends Sprite 
	{
		[Embed(source = "ocean06.flsl.compiled", mimeType = "application/octet-stream")]
		private var Ocean:Class;
		
		private var scene:Scene3D;
		private var ship:Flare3DLoader;
		private var water:Plane;
		private var skybox:SkyBox;
		
		private var oceanGridSize:int = 64;
		private var oceanShader:FLSLMaterial;
		
		private var mirrorTexture:Texture3D;
		private var mirrorCam:Camera3D;
		
		public function Step03_Mirror() 
		{
			scene = new Viewer3D( this, "", 0.2 );
			scene.camera = new Camera3D();
			scene.camera.setPosition( 120, 40, -30 );
			scene.camera.lookAt( 0, 0, 0 );
			
			mirrorCam = new Camera3D();
			mirrorTexture = new Texture3D( new Point( 512, 512 ) );
			mirrorTexture.mipMode = Texture3D.MIP_NONE;
			mirrorTexture.wrapMode = Texture3D.WRAP_CLAMP;
			mirrorTexture.upload( scene );
			
			oceanShader = new FLSLMaterial( "oceanShader", new Ocean );
			oceanShader.params.skyMap.value = new Texture3D( "assets/Flare3DWaterStep3Mirror/highlights.png", false, Texture3D.FORMAT_CUBEMAP );
			oceanShader.params.normalMap.value = new Texture3D( "assets/Flare3DWaterStep3Mirror/normalMap.jpg" );
			oceanShader.params.mirrorMap.value = mirrorTexture;
			
			water = new Plane( "water", 3000, 3000, oceanGridSize - 1, oceanShader, "+xz" );			
			skybox = new SkyBox( "assets/Flare3DWaterStep3Mirror/skybox.png", SkyBox.HORIZONTAL_CROSS, null, 1 );
			ship = new Flare3DLoader( "assets/Flare3DWaterStep3Mirror/ship.zf3d" );
			ship.load();
			
			scene.addChild( skybox );
			scene.addChild( water );
			scene.addChild( ship );
			scene.addEventListener( Scene3D.RENDER_EVENT, renderEvent );
			scene.addEventListener( Scene3D.POSTRENDER_EVENT, postRenderEvent );
		}
		
		private function postRenderEvent(e:Event):void 
		{
			scene.drawQuadTexture( mirrorTexture, 0, 0, 300, 300 );
		}
		
		private function renderEvent(e:Event):void 
		{
			// copy from the main camera and invert in Y axis.
			mirrorCam.copyTransformFrom( scene.camera );
			mirrorCam.transform.appendScale( 1, -1, 1 );
			
			// setup the mirror cam to start rendering on the mirror texture.
			scene.setupFrame( mirrorCam );
			scene.context.setRenderToTexture( mirrorTexture.texture, true );
			scene.context.clear( 0, 0, 0, 0 );
			
			// draw objects into the mirror texture.
			ship.draw();
			
			// get back to the main render.
			scene.context.setRenderToBackBuffer();
			scene.setupFrame( scene.camera );
		}
	}
}