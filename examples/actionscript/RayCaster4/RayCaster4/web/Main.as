package {
	
	import flash.display.*;
	import flash.events.*;
	import flash.geom.*;
	import flash.net.URLRequest;
	import flash.text.TextField;
	import flash.text.TextFormat;
	import flash.utils.*;
	
	[SWF( backgroundColor='0xffffff', frameRate='80', width='640', height='480')]
	
	public class Main extends Sprite
	{
		private var txtMain:TextField;
        private var tfNormal:TextFormat;
		private var bitmapLoader: Loader;
		private var image: BitmapData;
		private var imageCont: Sprite;
		private var textureFiles: Array;
		private var textureLoadNum: Number;
		private var textures: Array;
		private var floorVals:Array;
		private var worldMap:Array;
		private var posX:Number;
		private var posY:Number;  //x and y start position
		private var dirX:Number;
		private var dirY:Number; //initial direction vector
		private var planeX:Number;
		private var planeY:Number; //the 2d raycaster version of camera plane
		private var w:int;
		private var h:int;
		private var activeKey:int;
		private var moveSpeed:Number;
		private var rotSpeed:Number;
		private var texWidth:int;
		private var texHeight:int;
		private var screen:BitmapData;
		private var screenImage:Bitmap;
		private var sprites:Array;
		private var ZBuffer:Array;
		private var time:Number;
		private var counter:int;
		
		public function Main()
		{
			textures = [];
			textureFiles = ['wall.jpg','tech2.jpg','roof.jpg'];
			textureLoadNum = 0;
			bitmapLoader = new Loader();
			bitmapLoader.load( new URLRequest( 'flashsrc/textures/'+textureFiles[textureLoadNum] ) );
			bitmapLoader.contentLoaderInfo.addEventListener( Event.COMPLETE , onBitmapLoaded );
		}
		
		private function onBitmapLoaded( event: Event ): void
		{		
			var bd:Bitmap = Bitmap(bitmapLoader.getChildAt(0));
			var bdata:BitmapData = bd.bitmapData;
			
			textures[textureLoadNum] = new Array();
			for (var j:int=0; j<256; j++) {
				textures[textureLoadNum][j] = new Array();
				for (var k:Number=0; k<256; k++) {
					textures[textureLoadNum][j][k] = bdata.getPixel(j,k);
				}
			}
			
			if (textureLoadNum < textureFiles.length-1) {
				textureLoadNum++;
				bitmapLoader.unload();
				bitmapLoader.load( new URLRequest( 'flashsrc/textures/'+textureFiles[textureLoadNum] ) );
			} else {
				prepare();
			}
		}
		
		private function prepare(): void
		{
            stage.align    = flash.display.StageAlign.TOP_LEFT;
            stage.quality 	= flash.display.StageQuality.LOW;
            
            txtMain  = new TextField();
            tfNormal = new TextFormat();
            tfNormal.font  = 'Verdana';
            tfNormal.align = flash.text.TextFormatAlign.LEFT;
            tfNormal.size  = 10;
            tfNormal.color = 0xffffff;
            txtMain.defaultTextFormat = tfNormal;
            txtMain.autoSize = "left";
            txtMain.appendText('0');
           
			moveSpeed = 0.2;
			rotSpeed = 0.12;
			texWidth = 256;
			texHeight = 256;
			posX = 22;
			posY = 11.5;
			dirX = -1;
			dirY = 0;
			planeX = 0;
			planeY = 0.66;
			w = 640;
			h = 480;
			time = getTimer();
			setWorldMap();
			
			floorVals = [80,40,26.6666666666667,20,16,13.3333333333333,11.4285714285714,10,8.88888888888889,8,7.27272727272727,6.66666666666667,6.15384615384615,5.71428571428571,5.33333333333333,5,4.70588235294118,4.44444444444444,4.21052631578947,4,3.80952380952381,3.63636363636364,3.47826086956522,3.33333333333333,3.2,3.07692307692308,2.96296296296296,2.85714285714286,2.75862068965517,2.66666666666667,2.58064516129032,2.5,2.42424242424242,2.35294117647059,2.28571428571429,2.22222222222222,2.16216216216216,2.10526315789474,2.05128205128205,2,
			1.95121951219512,1.9047619047619,1.86046511627907,1.81818181818182,1.77777777777778,1.73913043478261,1.70212765957447,1.66666666666667,1.63265306122449,1.6,1.56862745098039,1.53846153846154,1.50943396226415,1.48148148148148,1.45454545454545,1.42857142857143,1.40350877192982,1.37931034482759,1.35593220338983,1.33333333333333,1.31147540983607,1.29032258064516,1.26984126984127,1.25,1.23076923076923,1.21212121212121,1.19402985074627,1.17647058823529,1.15942028985507,1.14285714285714,1.12676056338028,1.11111111111111,1.0958904109589,1.08108108108108,1.06666666666667,1.05263157894737,1.03896103896104,1.02564102564103,1.0126582278481];

			time = getTimer();
			counter = 0;
			
			ZBuffer = new Array();
			
			screen = new BitmapData( w, h, false, 0x0 );
			screenImage = new Bitmap();
			screenImage.bitmapData = screen;
			
			addChild( screenImage );
			addChild( txtMain );
				
			stage.addEventListener(KeyboardEvent.KEY_DOWN, onKeyDownEvent);
			stage.addEventListener(KeyboardEvent.KEY_UP, onKeyUpEvent);
			addEventListener(Event.ENTER_FRAME, render);
		
		}		
		
		private function onKeyDownEvent(event:KeyboardEvent):void
		{
    		if (event.keyCode == 38) {
    				activeKey = 1;
			}
			if (event.keyCode == 40) {
					activeKey = 3;
			}    
			if (event.keyCode == 39) { 
					activeKey = 2;
			}
			if (event.keyCode == 37) {
					activeKey = 4;
			}
			
		}
		
		private function onKeyUpEvent(event:KeyboardEvent):void
		{
			activeKey = 0;
		}
		
				
		private function render(event: Event): void
		{					
/* 			try {
				screen.dispose();
				screen = new BitmapData( w, h, false, 0x0 );
				screen.lock();
			} catch(e:Error) {
				trace("err");
			} */
			
			screen.floodFill(0,0,0x0);
			screen.lock();
			
			var x:Number;
			var y:Number;
			var oldDirX:Number;
			var oldPlaneX:Number;
			
			if (activeKey == 1) {
					if(worldMap[int(posX + dirX * moveSpeed)][int(posY)] == false) posX += dirX * moveSpeed;
					if(worldMap[int(posX)][int(posY + dirY * moveSpeed)] == false) posY += dirY * moveSpeed;
			}
			if (activeKey == 3) {
					if(worldMap[int(posX - dirX * moveSpeed)][int(posY)] == false) posX -= dirX * moveSpeed;
					if(worldMap[int(posX)][int(posY - dirY * moveSpeed)] == false) posY -= dirY * moveSpeed;
			}    
			if (activeKey == 2) { //both camera direction and camera plane must be rotated
					oldDirX = dirX;
					dirX = dirX * Math.cos(-rotSpeed) - dirY * Math.sin(-rotSpeed);
					dirY = oldDirX * Math.sin(-rotSpeed) + dirY * Math.cos(-rotSpeed);
					oldPlaneX = planeX;
					planeX = planeX * Math.cos(-rotSpeed) - planeY * Math.sin(-rotSpeed);
					planeY = oldPlaneX * Math.sin(-rotSpeed) + planeY * Math.cos(-rotSpeed);
			}
			if (activeKey == 4) { //both camera direction and camera plane must be rotated
					oldDirX = dirX;
					dirX = dirX * Math.cos(rotSpeed) - dirY * Math.sin(rotSpeed);
					dirY = oldDirX * Math.sin(rotSpeed) + dirY * Math.cos(rotSpeed);
					oldPlaneX = planeX;
					planeX = planeX * Math.cos(rotSpeed) - planeY * Math.sin(rotSpeed);
					planeY = oldPlaneX * Math.sin(rotSpeed) + planeY * Math.cos(rotSpeed);
			}
				
			x = 0;		
			while (x <= w) {
								
				//calculate ray position and direction
				var cameraX:Number = 2 * x / w - 1; //x-coordinate in camera space
				var rayPosX:Number = posX;
				var rayPosY:Number = posY;
				var rayDirX:Number = dirX + planeX * cameraX;
				var rayDirY:Number = dirY + planeY * cameraX;
			
				//which box of the map we're in
				var mapX:Number = int(rayPosX);
				var mapY:Number = int(rayPosY);
				
				//length of ray from current position to next x or y-side
				var sideDistX:Number;
				var sideDistY:Number;
				
				//length of ray from one x or y-side to next x or y-side
				var deltaDistX:Number = Math.sqrt(1 + (rayDirY * rayDirY) / (rayDirX * rayDirX));
				var deltaDistY:Number = Math.sqrt(1 + (rayDirX * rayDirX) / (rayDirY * rayDirY));
				
				//what direction to step in x or y-direction (either +1 or -1)
				var stepX:Number;
				var stepY:Number;
	
			    //calculate step and initial sideDist
				if (rayDirX < 0) {
					stepX = -1;
					sideDistX = (rayPosX - mapX) * deltaDistX;
				} else {
					stepX = 1;
					sideDistX = (mapX + 1.0 - rayPosX) * deltaDistX;
				}           
				if (rayDirY < 0) {
					stepY = -1;
					sideDistY = (rayPosY - mapY) * deltaDistY;
				} else {
					stepY = 1;
					sideDistY = (mapY + 1.0 - rayPosY) * deltaDistY;
				}
		
				var hit:Number = 0; //was there a wall hit?
				var side:Number; //was a NS or a EW wall hit?
				
				while (hit == 0) { //perform DDA                 
					//jump to next map square, OR in x-direction, OR in y-direction
					if (sideDistX < sideDistY) {
							sideDistX += deltaDistX;
							mapX += stepX;
							side = 0;
					} else {
							sideDistY += deltaDistY;
							mapY += stepY;
							side = 1;
					}               
					if (worldMap[mapX][mapY] > 0) {
						hit = 1; //Check if ray has hit a wall   
					}
				}
							
				//Calculate distance projected on camera direction (oblique distance will give fisheye effect!)
				var perpWallDist:Number;
				if (side == 0) {
					perpWallDist = Math.abs((mapX - rayPosX + (1 - stepX) / 2) / rayDirX);
				} else {
					perpWallDist = Math.abs((mapY - rayPosY + (1 - stepY) / 2) / rayDirY);
				}
					
				//Calculate height of line to draw on screen
				var lineHeight:Number = Math.abs(int(h / perpWallDist));        
				
				//calculate lowest and highest pixel to fill in current stripe
				var drawStart:Number = int(-lineHeight / 2 + h / 2);
				if (drawStart < 0) drawStart = 0;
				var drawEnd:Number = int(lineHeight / 2 + h / 2);
				if (drawEnd >= h) drawEnd = h;    
				
	            var texNum:Number = worldMap[mapX][mapY] - 1; //1 subtracted from it so that texture 0 can be used!
				texNum = 0;
				
	            //calculate value of wallX
	            var wallX:Number; //where exactly the wall was hit
	            if (side == 1) {
					wallX = rayPosX + ((mapY - rayPosY + (1 - stepY) / 2) / rayDirY) * rayDirX;
				} else {
					wallX = rayPosY + ((mapX - rayPosX + (1 - stepX) / 2) / rayDirX) * rayDirY;
				}
	            wallX -= Math.floor((wallX));    
	            
	            //x coordinate on the texture
	            var texX:Number = int(wallX * texWidth);
	            if(side == 0 && rayDirX > 0) texX = texWidth - texX - 1;
	            if(side == 1 && rayDirY < 0) texX = texWidth - texX - 1;
				
				var hT:Number = h * 128;
				var lhT:Number = lineHeight * 128;
				
				y = drawStart;	
							
				while (y < drawEnd) {
					            
					var d:Number = y * 256 - hT + lhT;  //256 and 128 factors to avoid floats
					var texY:Number = ((d * texHeight) / lineHeight) / 256; 
					
					var color:Number = textures[texNum][int(texX)][int(texY)];

					if(side == 1) color = (color >>1) & 8355711;
					screen.setPixel(int(x),int(y),int(color));

					y++;
				}	
				
				//SET THE ZBUFFER FOR THE SPRITE CASTING
	            ZBuffer[x] = perpWallDist; //perpendicular distance is used
				
				//floor casting    
				var floorXWall:Number;
				var floorYWall:Number; //x, y position of the floor texel at the bottom of the wall
		
				//4 different wall directions possible
				if(side == 0 && rayDirX > 0)
				{
					floorXWall = mapX;
					floorYWall = mapY + wallX;
				}
				else if(side == 0 && rayDirX < 0)
				{
					floorXWall = mapX + 1.0;
					floorYWall = mapY + wallX;
				}        
				else if(side == 1 && rayDirY > 0)
				{
					floorXWall = mapX + wallX;
					floorYWall = mapY;
				}        
				else
				{               
					floorXWall = mapX + wallX;
					floorYWall = mapY + 1.0;       
				} 
						
				var distWall:Number = perpWallDist;
				var distPlayer:Number = 0.0;  
				var currentDist:Number = 0;
			
				if (drawEnd < 0) drawEnd = h; //becomes < 0 when the integer overflows
				
				//draw the floor from drawEnd to the bottom of the screen
				y = drawEnd;
				var weight:Number;
				var currentFloorX:Number;
				var currentFloorY:Number;
				var floorTexX:Number;
				var floorTexY:Number;
				
				while (y < h) {
					
					currentDist = h / (2 * y - h); //you could make a small lookup table for this instead
					//currentDist = floorVals[int(y-80)];
					
					weight = (currentDist - distPlayer) / (distWall - distPlayer);
				   
					currentFloorX = weight * floorXWall + (1.0 - weight) * posX;
					currentFloorY = weight * floorYWall + (1.0 - weight) * posY;
					
					floorTexX = int(currentFloorX * texWidth) % texWidth;
					floorTexY = int(currentFloorY * texHeight) % texHeight; 
					
					try {
						screen.setPixel(x,y,textures[1][floorTexX][floorTexY]); //floor
					} catch (e:Error) {
						trace("err");
					}
					
					try {
						screen.setPixel(x,h - y -1,textures[2][floorTexX][floorTexY]); //ceiling (symmetrical!)
					} catch (e:Error) {
						trace("err");
					}
					
					y++;
				}
				
				x++; 
			}
			
			counter++;
			
			if (getTimer()-1000 >= time) {
				txtMain.text = String(counter);
				counter = 0;
				time = getTimer();
			}
			
			//screenImage.bitmapData = screen;
			screen.unlock();

		}
		
		private function RGBToInt(rgb:Array):Number {
			var r:Number = int(rgb[0]) << 16;
			var g:Number = int(rgb[1]) << 8;
			var b:Number = int(rgb[2]);
			return r | g | b;
		}
		
		private function intToRGB(number:Number):Array {
			var b:Number = number & 255; number >>= 8;
			var g:Number = number & 255; number >>= 8;
			var r:Number = number;
			return [r,g,b];
		}
	
		private function setWorldMap():void {
			worldMap =
			[[8,8,8,8,8,8,8,8,8,8,8,4,4,6,4,4,6,4,6,4,4,4,6,4],
			[8,0,0,0,0,0,0,0,0,0,8,4,0,0,0,0,0,0,0,0,0,0,0,4],
			[8,0,3,3,0,0,0,0,0,8,8,4,0,0,0,0,0,0,0,0,0,0,0,6],
			[8,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6],
			[8,0,3,3,0,0,0,0,0,8,8,4,0,0,0,0,0,0,0,0,0,0,0,4],
			[8,0,0,0,0,0,0,0,0,0,8,4,0,0,0,0,0,6,6,6,0,6,4,6],
			[8,8,8,8,0,8,8,8,8,8,8,4,4,4,4,4,4,6,0,0,0,0,0,6],
			[7,7,7,7,0,7,7,7,7,0,8,0,8,0,8,0,8,4,0,4,0,6,0,6],
			[7,7,0,0,0,0,0,0,7,8,0,8,0,8,0,8,8,6,0,0,0,0,0,6],
			[7,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,8,6,0,0,0,0,0,4],
			[7,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,8,6,0,6,0,6,0,6],
			[7,7,0,0,0,0,0,0,7,8,0,8,0,8,0,8,8,6,4,6,0,6,6,6],
			[7,7,7,7,0,7,7,7,7,8,8,4,0,6,8,4,8,3,3,3,0,3,3,3],
			[2,2,2,2,0,2,2,2,2,4,6,4,0,0,6,0,6,3,0,0,0,0,0,3],
			[2,2,0,0,0,0,0,2,2,4,0,0,0,0,0,0,4,3,0,0,0,0,0,3],
			[2,0,0,0,0,0,0,0,2,4,0,0,0,0,0,0,4,3,0,0,0,0,0,3],
			[1,0,0,0,0,0,0,0,1,4,4,4,4,4,6,0,6,3,3,0,0,0,3,3],
			[2,0,0,0,0,0,0,0,2,2,2,1,2,2,2,6,6,0,0,5,0,5,0,5],
			[2,2,0,0,0,0,0,2,2,2,0,0,0,2,2,0,5,0,5,0,0,0,5,5],
			[2,0,0,0,0,0,0,0,2,0,0,0,0,0,2,5,0,5,0,5,0,5,0,5],
			[1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5],
			[2,0,0,0,0,0,0,0,2,0,0,0,0,0,2,5,0,5,0,5,0,5,0,5],
			[2,2,0,0,0,0,0,2,2,2,0,0,0,2,2,0,5,0,5,0,0,0,5,5],
			[2,2,2,2,1,2,2,2,2,2,2,1,2,2,2,5,5,5,5,5,5,5,5,5]];
		}
	}
}