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
	
	public class BunnySprite
	{
		private var _speedX : Number;
		private var _speedY : Number;
		private var gpuSprite:GPUSprite;
		
		//wrapper class for GPUSprite to add additional information, in this case the speedX/Y values
		public function BunnySprite(gs:GPUSprite = null)
		{
			gpuSprite = gs;
			_speedX = 0.0;
			_speedY = 0.0;
		}
		public function get speedX() : Number 
		{
			return _speedX;
		}
		
		public function set speedX(sx:Number) : void 
		{
			_speedX = sx;
		}
		
		public function get speedY() : Number 
		{
			return _speedY;
		}
		
		public function set speedY(sy:Number) : void 
		{
			_speedY = sy;
		}
		public function get sprite():GPUSprite 
		{	
			return gpuSprite;
		}
		public function set sprite(gs:GPUSprite):void {
			gpuSprite = gs;
		}
	}
}