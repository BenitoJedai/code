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
	
	public class BunnyLayer
	{
		[Embed(source="../assets/wabbit_alpha.png")]
		private var BunnyImage : Class;
		private var _bunnies : Vector.<BunnySprite>;
		private var _spriteSheet : GPUSpriteSheet;
		public var _renderLayer : GPUSpriteRenderLayer;
		private var _bunnySpriteID:uint;
		
		private var gravity:Number = 0.5;
		private var maxX:int;
		private var minX:int;
		private var maxY:int;
		private var minY:int;	
		
		public function BunnyLayer(view:Rectangle)
		{
			maxX = view.width;
			minX = view.x;
			maxY = view.height;
			minY = view.y;
			_bunnies = new Vector.<BunnySprite>();
			
		}
		public function setPosition(view:Rectangle):void {
			maxX = view.width;
			minX = view.x;
			maxY = view.height;
			minY = view.y;
		}
		
		public function createRenderLayer(context3D:Context3D) : GPUSpriteRenderLayer {
			_spriteSheet = new GPUSpriteSheet(64, 64);
			//add bunny image to sprite sheet
			var bunnyBitmap:Bitmap = new BunnyImage();
			var destPt:Point = new Point(0,0);	
			_bunnySpriteID = _spriteSheet.addSprite(bunnyBitmap.bitmapData, bunnyBitmap.bitmapData.rect, destPt);
			
			// Create new render layer 
			_renderLayer = new GPUSpriteRenderLayer(context3D, _spriteSheet);
			
			return _renderLayer;
		}
		
		public function addBunny(numBunnies:int):void {
			var currentBunnyCount:int = _bunnies.length;
			var bunny:BunnySprite;
			var sprite:GPUSprite;
			for ( var i:uint = currentBunnyCount ; i < currentBunnyCount+numBunnies; i++ ) {
				
				sprite = _renderLayer.createChild(_bunnySpriteID);
				bunny = new BunnySprite(sprite);
				bunny.sprite.position = new Point();
				bunny.speedX = Math.random() * 5;
				bunny.speedY = (Math.random() * 5) - 2.5;
				bunny.sprite.scaleX = 0.3 + Math.random();
				bunny.sprite.scaleY = bunny.sprite.scaleX;
				bunny.sprite.rotation = 15 - Math.random() * 30;
				_bunnies.push(bunny);
			}
		}
		
		public function update(currentTime:Number) : void
		{		
			var bunny:BunnySprite;
			for(var i:int=0; i<_bunnies.length;i++)
			{
				bunny = _bunnies[i];
				bunny.sprite.position.x += bunny.speedX;
				bunny.sprite.position.y += bunny.speedY;
				bunny.speedY += gravity;
				bunny.sprite.alpha = 0.3 + 0.7 * bunny.sprite.position.y / maxY;
				
				if (bunny.sprite.position.x > maxX)
				{
					bunny.speedX *= -1;
					bunny.sprite.position.x = maxX;
				}
				else if (bunny.sprite.position.x < minX)
				{
					bunny.speedX *= -1;
					bunny.sprite.position.x = minX;
				}
				if (bunny.sprite.position.y > maxY)
				{
					bunny.speedY *= -0.8;
					bunny.sprite.position.y = maxY;
					if (Math.random() > 0.5) bunny.speedY -= 3 + Math.random() * 4;
				} 
				else if (bunny.sprite.position.y < minY)
				{
					bunny.speedY = 0;
					bunny.sprite.position.y = minY;
				}	
			}
		}
	}
}