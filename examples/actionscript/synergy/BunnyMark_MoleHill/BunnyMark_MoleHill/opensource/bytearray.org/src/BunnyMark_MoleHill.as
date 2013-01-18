/*
========================================================================
=      ================================  =====  ==================  ====
=  ===  ===============================   ===   ==================  ====
=  ====  ==============================  =   =  ==================  ====
=  ===  ===  =  ==  = ===  = ===  =  ==  == ==  ===   ===  =   ===  =  =
=      ====  =  ==     ==     ==  =  ==  =====  ==  =  ==    =  ==    ==
=  ===  ===  =  ==  =  ==  =  ===    ==  =====  =====  ==  =======   ===
=  ====  ==  =  ==  =  ==  =  =====  ==  =====  ===    ==  =======    ==
=  ===  ===  =  ==  =  ==  =  ==  =  ==  =====  ==  =  ==  =======  =  =
=      =====    ==  =  ==  =  ===   ===  =====  ===    ==  =======  =  =
========================================================================

* Copyright (c) 2012 Julian Wixson / Aaron Charbonneau - Adobe Systems
*
* Special thanks to Iain Lobb - iainlobb@googlemail.com for the original BunnyMark:
*
* http://blog.iainlobb.com/2010/11/display-list-vs-blitting-results.html 
*
* Special thanks to Philippe Elsass - philippe.elsass.me for the modified BunnyMark benchmark:
*
* http://philippe.elsass.me/2011/11/nme-ready-for-the-show/
*
* This program is distributed under the terms of the MIT License as found 
* in a file called LICENSE. If it is not present, the license
* is always available at http://www.opensource.org/licenses/mit-license.php.
*
* This program is distributed in the hope that it will be useful, but
* without any waranty; without even the implied warranty of merchantability
* or fitness for a particular purpose. See the MIT License for full details.
*/

package
{
	import com.adobe.example.GPUSprite.GPUSprite;
	import com.adobe.example.GPUSprite.GPUSpriteRenderLayer;
	import com.adobe.example.GPUSprite.GPUSpriteRenderStage;
	import com.adobe.example.GPUSprite.GPUSpriteSheet;
	
	import flash.display.Sprite;
	import flash.display.StageAlign;
	import flash.display.StageQuality;
	import flash.display.StageScaleMode;
	import flash.display3D.*;
	import flash.events.ErrorEvent;
	import flash.events.Event;
	import flash.events.MouseEvent;
	import flash.geom.Rectangle;
	import flash.system.Capabilities;
	import flash.text.TextField;
	import flash.text.TextFieldAutoSize;
	import flash.text.TextFormat;
	import flash.text.TextFormatAlign;
	import flash.utils.getTimer;
	
	import flashx.textLayout.formats.BackgroundColor;
	
	[SWF(width="480", height="640", frameRate="60", backgroundColor="#ffffff")]
	public class BunnyMark_MoleHill extends Sprite
	{
		public const RENDERMODE:String = Context3DRenderMode.AUTO;
		public var context3D:Context3D;
		private var bg:Background;
		private var _width:Number = 480;
		private var _height:Number = 640;
		private var tf:TextField;	
		
		private var fps:FPS;
		private var _spriteStage : GPUSpriteRenderStage;
		private var _bunnyLayer : BunnyLayer;
		private var _pirateLayer : PirateLayer;
		private var numBunnies:int = 100;	
		private var incBunnies:int = 100;
		
		public function BunnyMark_MoleHill() {
			stage.quality = StageQuality.LOW;
			stage.align = StageAlign.TOP_LEFT;
			stage.scaleMode = StageScaleMode.NO_SCALE;
			stage.addEventListener(Event.RESIZE, onResizeEvent);
			
			fps = new FPS();
			addChild(fps);
			createCounter();
			stage.stage3Ds[0].addEventListener(Event.CONTEXT3D_CREATE, onContext3DCreate);
			stage.stage3Ds[0].addEventListener(ErrorEvent.ERROR, errorHandler);
			stage.stage3Ds[0].requestContext3D(RENDERMODE);
			
		}
		
		private function onContext3DCreate(e:Event):void {
			context3D = stage.stage3Ds[0].context3D;
			initSpriteEngine();
		}
		private function errorHandler(e:ErrorEvent):void {
			trace("ErrorEvent: "+e.errorID);
		}
		protected function onResizeEvent(event:Event) : void
		{
			// Set correct dimensions if we resize
			_width = stage.stageWidth;
			_height = stage.stageHeight;
			
			// Resize Stage3D to continue to fit screen
			var view:Rectangle = new Rectangle(0, 0, _width, _height);
			if ( _spriteStage != null ) {
				_spriteStage.position = view;
			}
			if(_bunnyLayer != null) {
				_bunnyLayer.setPosition(view);
			}
			if(_pirateLayer != null) {
				_pirateLayer.setPosition(view);	
			}
			if(bg != null) {
				bg.setPosition(view);
			}
			if(tf != null) {
				tf.x = _width - 100;
			}
		}
		private function initSpriteEngine():void {
			var stageRect:Rectangle = new Rectangle(0, 0, _width, _height); 
			_spriteStage = new GPUSpriteRenderStage(stage.stage3Ds[0], context3D, stageRect);
			_spriteStage.configureBackBuffer(_width,_height);
			
			//add background which does not use any framework, use render() to make the necessary draw calls
			bg = new Background(context3D,_width,_height);
			
			//add bunny layer
			var view:Rectangle = new Rectangle(0,0,_width,_height)
			_bunnyLayer = new BunnyLayer(view);
			_bunnyLayer.createRenderLayer(context3D);
			_spriteStage.addLayer(_bunnyLayer._renderLayer);
			_bunnyLayer.addBunny(numBunnies);
			
			//add pirate layer on top
			_pirateLayer = new PirateLayer(view);
			_pirateLayer.createRenderLayer(context3D);
			_spriteStage.addLayer(_pirateLayer._renderLayer);
			_pirateLayer.addPirate();
			stage.addEventListener(Event.ENTER_FRAME,onEnterFrame);
			
		}
		private function onEnterFrame(e:Event):void {
			try {
				context3D.clear(0,1,0,1);
				bg.render();
				_bunnyLayer.update(getTimer());
				_pirateLayer.update(getTimer());
				_spriteStage.drawDeferred();

				context3D.present();
			}
			catch(e:Error) {
				//most likely device loss, do nothing, a more robust app should restore everything correctly, we will just restart
			}
		}
		private function createCounter():void
		{
			var format:TextFormat = new TextFormat("_sans", 20, 0, true);
			format.align = TextFormatAlign.RIGHT;
			
			tf = new TextField();
			tf.selectable = false;
			tf.defaultTextFormat = format;
			tf.text = "Click here\n"+ "Bunnies:" + numBunnies;
			tf.autoSize = TextFieldAutoSize.LEFT;
			tf.x = _width - 145;
			tf.y = 10;
			addChild(tf);
			
			tf.addEventListener(MouseEvent.CLICK, counter_click);
		}
		private function counter_click(e:MouseEvent):void {
			if(numBunnies == 16250) {
				//we've reached the limit for vertex buffer length
				tf.text = "Bunnies \n(Limit):\n" + numBunnies;
			}
			else {
				if (numBunnies >= 1500) incBunnies = 250;
			
				_bunnyLayer.addBunny(incBunnies);
				numBunnies += incBunnies;
			
				tf.text = "Click here\n"+ "Bunnies:" + numBunnies;
			}
		}
	}
}