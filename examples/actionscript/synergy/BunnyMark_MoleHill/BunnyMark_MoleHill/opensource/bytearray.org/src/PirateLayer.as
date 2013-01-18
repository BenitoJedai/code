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
	import com.adobe.example.GPUSprite.GPUSpriteSheet;
	
	import flash.display.Bitmap;
	import flash.display3D.*;
	import flash.geom.Point;
	import flash.geom.Rectangle;
	
	public class PirateLayer
	{
		[Embed(source="../assets/pirate.png")]
		private var PirateImage : Class;
		private var pirate:GPUSprite;
		private var pirateHalfWidth:int;
		private var pirateHalfHeight:int;
		private var _spriteSheet : GPUSpriteSheet;
		private var _pirateSpriteID:uint;
		private var maxX:int;
		private var minX:int;
		private var maxY:int;
		private var minY:int;
		
		public var _renderLayer : GPUSpriteRenderLayer;
		
		public function PirateLayer(view:Rectangle)
		{
			maxX = view.width;
			minX = view.x;
			maxY = view.height;
			minY = view.y;
			
		}
		public function createRenderLayer(context3D:Context3D) : GPUSpriteRenderLayer {
			_spriteSheet = new GPUSpriteSheet(256, 256);
			//add pirate image to sprite sheet
			var pirateBitmap:Bitmap = new PirateImage();
			//adjust for different anchor point of GPUSprite vs DisplayList
			pirateHalfWidth = pirateBitmap.width/2;
			pirateHalfHeight = pirateBitmap.height/2;
			
			var destPt:Point = new Point(0,0);	
			_pirateSpriteID = _spriteSheet.addSprite(pirateBitmap.bitmapData, pirateBitmap.bitmapData.rect, destPt);
			
			// Create new render layer 
			_renderLayer = new GPUSpriteRenderLayer(context3D, _spriteSheet);
			
			return _renderLayer;
		}
		public function setPosition(view:Rectangle):void {
			maxX = view.width;
			minX = view.x;
			maxY = view.height;
			minY = view.y;
		}
		public function addPirate():void {
			pirate = _renderLayer.createChild(_pirateSpriteID);
			pirate.position = new Point((maxX - pirateHalfWidth) * (0.5), (maxY - pirateHalfHeight + 70));
		}
		public function update(currentTime:Number) : void
		{		
			pirate.position.x = (maxX - (pirateHalfWidth)) * (0.5 + 0.5 * Math.sin(currentTime / 3000));
			pirate.position.y = (maxY - (pirateHalfHeight) + 70 - 30 * Math.sin(currentTime / 100));
		}
	}
}