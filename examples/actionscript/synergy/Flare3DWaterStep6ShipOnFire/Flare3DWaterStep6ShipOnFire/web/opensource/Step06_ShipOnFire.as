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
	import flash.utils.*;
	
	/**
	 * @author Ariel Nehmad
	 */
	public class Step06_ShipOnFire extends Sprite 
	{
		[Embed(source = "ocean07.flsl.compiled", mimeType = "application/octet-stream")]
		private var Ocean:Class;
		[Embed(source = "water.pbj", mimeType = "application/octet-stream")]
		private var PBWater:Class
		
		// scene objects.
		private var scene:Scene3D;
		private var ship:Flare3DLoader;
		private var particles:Flare3DLoader;
		private var water:Plane;
		private var skybox:SkyBox;
		
		// flsl ocean shader.
		private var oceanGridSize:int = 64;
		private var oceanShader:FLSLMaterial;
		
		// mirror rendering.
		private var mirror:Texture3D;
		private var mirrorCam:Camera3D;
		
		// big waves.
		private var bmp:BitmapData = new BitmapData( oceanGridSize, oceanGridSize, false );
		private var point0:Point = new Point();
		private var point1:Point = new Point();
		private var surf:Surface3D;
		private var bytes:ByteArray;
		private var shader:Shader;
		private var job:ShaderJob;
		
		public function Step06_ShipOnFire() 
		{
			scene = new Viewer3D( this, "", 0.2 );
			scene.camera = new Camera3D();
			scene.camera.setPosition( 120, 40, -30 );
			scene.camera.lookAt( 0, 0, 0 );
			
			mirrorCam = new Camera3D();
			mirror = new Texture3D( new Point( 512, 512 ) );
			mirror.mipMode = Texture3D.MIP_NONE;
			mirror.wrapMode = Texture3D.WRAP_CLAMP;
			mirror.upload( scene );
			
			oceanShader = new FLSLMaterial( "oceanShader", new Ocean );
			oceanShader.params.skyMap.value = new Texture3D( "assets/Flare3DWaterStep6ShipOnFire/highlights.png", false, Texture3D.FORMAT_CUBEMAP );
			oceanShader.params.normalMap.value = new Texture3D( "assets/Flare3DWaterStep6ShipOnFire/normalMap.jpg" );
			oceanShader.params.mirrorMap.value = mirror;
			
			water = new Plane( "water", 3000, 3000, oceanGridSize - 1, oceanShader, "+xz" );			
			skybox = new SkyBox( "assets/Flare3DWaterStep6ShipOnFire/skybox.png", SkyBox.HORIZONTAL_CROSS, null, 1 );
			ship = new Flare3DLoader( "assets/Flare3DWaterStep6ShipOnFire/ship.zf3d" );
			ship.load();
			particles = new Flare3DLoader( "assets/Flare3DWaterStep6ShipOnFire/particles.zf3d" );
			particles.load();
			
			initWaves();
			
			ship.addChild( particles );
			scene.addChild( skybox );
			scene.addChild( water );
			scene.addChild( ship );
			scene.addEventListener( Scene3D.RENDER_EVENT, renderEvent );
		}
		
		private function renderEvent(e:Event):void 
		{
			// render the big waves.
			renderWaves();
			
			// copy from the main camera and invert in Y axis.
			mirrorCam.copyTransformFrom( scene.camera );
			mirrorCam.transform.appendScale( 1, -1, 1 );
			
			// setup the mirror cam to start rendering on the mirror texture.
			scene.setupFrame( mirrorCam );
			scene.context.setRenderToTexture( mirror.texture, true );
			scene.context.clear( 0, 0, 0, 0 );
			
			// get the pixel color height.
			var color:uint = bmp.getPixel( oceanGridSize * 0.5, oceanGridSize * 0.5 ) & 0xff;
			var height:Number =  color / 255 * 20 + 1;
			
			// draw objects into the mirror texture.
			ship.y = -height;
			ship.draw();
			ship.y = height;
			
			// get back to the main render.
			scene.context.setRenderToBackBuffer();
			scene.setupFrame( scene.camera );
		}
		
		private function initWaves():void
		{
			bytes = new ByteArray();
			bytes.endian = Endian.LITTLE_ENDIAN;
			bytes.length = oceanGridSize * oceanGridSize * 12; // 4 btyes * RGB = 12.
			
			shader = new Shader( new PBWater );
			shader.data.src.input = bmp;
			
			surf = new Surface3D( "data" );
			surf.addVertexData( Surface3D.COLOR0, 3 );
			surf.vertexBytes = bytes;
			surf.upload( scene );
			
			water.surfaces[0].sources[Surface3D.COLOR0] = surf;
		}
		
		private function renderWaves():void
		{
			var timer:int = getTimer();
			point0.y = timer / 400;
			point1.y = timer / 640;
			
			bmp.perlinNoise( 3, 3, 2, 0, false, true, 7, true, [point0, point1] );
			
			job = new ShaderJob( shader, bytes, oceanGridSize, oceanGridSize );
			job.addEventListener( ShaderEvent.COMPLETE, shaderCompleteEvent, false, 0, true );
			job.start();
		}
		
		private function shaderCompleteEvent(e:ShaderEvent):void 
		{
			if ( surf.vertexBuffer )
				surf.vertexBuffer.uploadFromByteArray( bytes, 0, 0, oceanGridSize * oceanGridSize );
		}
	}
}