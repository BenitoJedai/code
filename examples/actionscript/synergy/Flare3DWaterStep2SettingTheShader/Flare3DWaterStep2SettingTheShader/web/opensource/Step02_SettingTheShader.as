package  
{
	import flare.basic.*;
	import flare.core.*;
	import flare.flsl.*;
	import flare.loaders.*;
	import flare.primitives.*;
	import flash.display.*;
	
	/**
	 * @author Ariel Nehmad
	 */
	public class Step02_SettingTheShader extends Sprite 
	{
		[Embed(source = "ocean06.flsl.compiled", mimeType = "application/octet-stream")]
		private var Ocean:Class;
		
		private var scene:Scene3D;
		private var ship:Flare3DLoader;
		private var water:Plane;
		private var skybox:SkyBox;
		
		private var oceanGridSize:int = 64;
		private var oceanShader:FLSLMaterial;
		
		public function Step02_SettingTheShader() 
		{
			scene = new Viewer3D( this, "", 0.2 );
			scene.camera = new Camera3D();
			scene.camera.setPosition( 120, 40, -30 );
			scene.camera.lookAt( 0, 0, 0 );
			
			oceanShader = new FLSLMaterial( "oceanShader", new Ocean );
			oceanShader.params.skyMap.value = new Texture3D( "assets/Flare3DWaterStep2SettingTheShader/highlights.png", false, Texture3D.FORMAT_CUBEMAP );
			oceanShader.params.normalMap.value = new Texture3D( "assets/Flare3DWaterStep2SettingTheShader/normalMap.jpg" );
			
			water = new Plane( "water", 3000, 3000, oceanGridSize - 1, oceanShader, "+xz" );			
			skybox = new SkyBox( "assets/Flare3DWaterStep2SettingTheShader/skybox.png", SkyBox.HORIZONTAL_CROSS, null, 1 );
			ship = new Flare3DLoader( "assets/Flare3DWaterStep2SettingTheShader/ship.zf3d" );
			ship.load();
			
			scene.addChild( skybox );
			scene.addChild( water );
			scene.addChild( ship );
		}
	}
}