using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using System.Collections.Generic;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.flash.filters;
using System;
using ScriptCoreLib.ActionScript.flash.events;
using ScriptCoreLib.ActionScript.flash.net;
using ScriptCoreLib.ActionScript.flash.ui;


namespace RayCaster6.ActionScript
{

    [ScriptImportsType("flash.utils.getTimer")]
    [Script]
    public partial class RayCaster4base : Sprite
    {
        // http://livedocs.adobe.com/flex/2/langref/flash/utils/package.html#getTimer()
        [Script(OptimizedCode = "return flash.utils.getTimer();")]
        internal static int getTimer()
        {
            return default(int);
        }

        protected TextField txtMain;
        protected TextFormat tfNormal;
        protected Loader bitmapLoader;
        protected BitmapData image;
        protected Sprite imageCont;
        protected string[] textureFiles;
        protected int textureLoadNum;
        protected uint[][][] textures;
        //protected double[] floorVals;

        protected double posX;
        protected double posY;  //x and y start position
        protected double dirX;
        protected double dirY; //initial direction vector
        protected double planeX;
        protected double planeY; //the 2d raycaster version of camera plane
        protected int w;
        protected int h;

        protected double moveSpeed;
        protected double rotSpeed;
        protected int texWidth;
        protected int texHeight;
        protected BitmapData screen;
        protected Bitmap screenImage;
        protected Sprite[] sprites; // ?
        protected double[] ZBuffer;
        protected int time;
        protected int counter;

        //protected int[][] worldMap;
        public Array2DSByte worldMap;

        protected int activeKey;

        [Script(NoDecoration = true)]
        protected void setWorldMap()
        {
            worldMap =
                new Array2DSByte(24, 24,
			        8,8,8,8,8,8,8,8,8,8,8,4,4,6,4,4,6,4,6,4,4,4,6,4,
			        8,0,0,0,0,0,0,0,0,0,8,4,0,0,0,0,0,0,0,0,0,0,0,4,
			        8,0,3,3,0,0,0,0,0,8,8,4,0,0,0,0,0,0,0,0,0,0,0,6,
			        8,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6,
			        8,0,3,3,0,0,0,0,0,8,8,4,0,0,0,0,0,0,0,0,0,0,0,4,
			        8,0,0,0,0,0,0,0,0,0,8,4,0,0,0,0,0,6,6,6,0,6,4,6,
			        8,8,8,8,0,8,8,8,8,8,8,4,4,4,4,4,4,6,0,0,0,0,0,6,
			        7,7,7,7,0,7,7,7,7,0,8,0,8,0,8,0,8,4,0,4,0,6,0,6,
			        7,7,0,0,0,0,0,0,7,8,0,8,0,8,0,8,8,6,0,0,0,0,0,6,
			        7,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,8,6,0,0,0,0,0,4,
			        7,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,8,6,0,6,0,6,0,6,
			        7,7,0,0,0,0,0,0,7,8,0,8,0,8,0,8,8,6,4,6,0,6,6,6,
			        7,7,7,7,0,7,7,7,7,8,8,4,0,6,8,4,8,3,3,3,0,3,3,3,
			        2,2,2,2,0,2,2,2,2,4,6,4,0,0,6,0,6,3,0,0,0,0,0,3,
			        2,2,0,0,0,0,0,2,2,4,0,0,0,0,0,0,4,3,0,0,0,0,0,3,
			        2,0,0,0,0,0,0,0,2,4,0,0,0,0,0,0,4,3,0,0,0,0,0,3,
			        1,0,0,0,0,0,0,0,1,4,4,4,4,4,6,0,6,3,3,0,0,0,3,3,
			        2,0,0,0,0,0,0,0,2,2,2,1,2,2,2,6,6,0,0,5,0,5,0,5,
			        2,2,0,0,0,0,0,2,2,2,0,0,0,2,2,0,5,0,5,0,0,0,5,5,
			        2,0,0,0,0,0,0,0,2,0,0,0,0,0,2,5,0,5,0,5,0,5,0,5,
			        1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5,
			        2,0,0,0,0,0,0,0,2,0,0,0,0,0,2,5,0,5,0,5,0,5,0,5,
			        2,2,0,0,0,0,0,2,2,2,0,0,0,2,2,0,5,0,5,0,0,0,5,5,
			        2,2,2,2,1,2,2,2,2,2,2,1,2,2,2,5,5,5,5,5,5,5,5,5
                );
        }



        public RayCaster4base()
        {
            stage.keyDown +=
                e =>
                {
                    var key = e.keyCode;

                    fKeyUp.ProcessKeyDown(key);
                    fKeyDown.ProcessKeyDown(key);
                    fKeyLeft.ProcessKeyDown(key);
                    fKeyRight.ProcessKeyDown(key);
                };

            stage.keyUp +=
                e =>
                {
                    var key = e.keyCode;

                    fKeyUp.ProcessKeyUp(key);
                    fKeyDown.ProcessKeyUp(key);
                    fKeyLeft.ProcessKeyUp(key);
                    fKeyRight.ProcessKeyUp(key);
                };

            textures = new uint[0][][];
            textureFiles = new[] { "wall.jpg", "tech2.jpg", "roof.jpg" };
            textureLoadNum = 0;
            bitmapLoader = new Loader();
            bitmapLoader.load(new URLRequest("flashsrc/textures/" + textureFiles[textureLoadNum]));

            bitmapLoader.contentLoaderInfo.complete += onBitmapLoaded;

        }

        private void onBitmapLoaded(Event e)
        {
            var bd = (Bitmap)(bitmapLoader.getChildAt(0));
            var bdata = bd.bitmapData;

            textures[textureLoadNum] = new uint[0][];
            for (var j = 0; j < 256; j++)
            {
                textures[textureLoadNum][j] = new uint[0];
                for (var k = 0; k < 256; k++)
                {
                    textures[textureLoadNum][j][k] = bdata.getPixel(j, k);
                }
            }

            if (textureLoadNum < textureFiles.Length - 1)
            {
                textureLoadNum++;
                bitmapLoader.unload();
                bitmapLoader.load(new URLRequest("flashsrc/textures/" + textureFiles[textureLoadNum]));
            }
            else
            {
                prepare();
            }

        }

        [Script(NoDecoration = true)]
        protected uint RGBToInt(int[] rgb)
        {
            var r = (uint)(rgb[0]) << 16;
            var g = (uint)(rgb[1]) << 8;
            var b = (uint)(rgb[2]);
            return r | g | b;
        }

        [Script(NoDecoration = true)]
        protected uint[] intToRGB(uint number)
        {
            var b = number & 255; number >>= 8;
            var g = number & 255; number >>= 8;
            var r = number;
            return new[] { r, g, b };
        }

        // movement flag
        protected KeyboardButton fKeyUp = new uint[] { Keyboard.UP, 'i', 'I', 'w', 'W' };
        protected KeyboardButton fKeyDown = new uint[] { Keyboard.DOWN, 'k', 'K', 's', 'S' };
        protected KeyboardButton fKeyLeft = new uint[] { Keyboard.LEFT, 'j', 'J', 'a', 'A' };
        protected KeyboardButton fKeyRight = new uint[] { Keyboard.RIGHT, 'l', 'L', 'd', 'D' };



        [Script(NoDecoration = true)]
        protected void prepare()
        {
            stage.align = StageAlign.TOP_LEFT;
            //stage.quality 	= StageQuality.LOW;

            txtMain = new TextField();
            tfNormal = new TextFormat();
            tfNormal.font = "Verdana";
            tfNormal.align = TextFormatAlign.LEFT;
            tfNormal.size = 10;
            tfNormal.color = 0xffffff;
            txtMain.defaultTextFormat = tfNormal;
            txtMain.autoSize = "left";
            txtMain.appendText("0");

            moveSpeed = 0.2;
            rotSpeed = 0.12;
            texWidth = 256;
            texHeight = 256;

            posX = 22.5;
            posY = 13.5;

            dirX = -1;
            dirY = 0;
            planeX = 0;
            planeY = 0.66;
            w = 320;
            h = 240;
            time = getTimer();
            setWorldMap();

            //floorVals = new[] {
            //    80,40,26.6666666666667,20,16,13.3333333333333,11.4285714285714,10,8.88888888888889,8,7.27272727272727,6.66666666666667,6.15384615384615,5.71428571428571,5.33333333333333,5,4.70588235294118,4.44444444444444,4.21052631578947,4,3.80952380952381,3.63636363636364,3.47826086956522,3.33333333333333,3.2,3.07692307692308,2.96296296296296,2.85714285714286,2.75862068965517,2.66666666666667,2.58064516129032,2.5,2.42424242424242,2.35294117647059,2.28571428571429,2.22222222222222,2.16216216216216,2.10526315789474,2.05128205128205,2,
            //1.95121951219512,1.9047619047619,1.86046511627907,1.81818181818182,1.77777777777778,1.73913043478261,1.70212765957447,1.66666666666667,1.63265306122449,1.6,1.56862745098039,1.53846153846154,1.50943396226415,1.48148148148148,1.45454545454545,1.42857142857143,1.40350877192982,1.37931034482759,1.35593220338983,1.33333333333333,1.31147540983607,1.29032258064516,1.26984126984127,1.25,1.23076923076923,1.21212121212121,1.19402985074627,1.17647058823529,1.15942028985507,1.14285714285714,1.12676056338028,1.11111111111111,1.0958904109589,1.08108108108108,1.06666666666667,1.05263157894737,1.03896103896104,1.02564102564103,1.0126582278481 };

            time = getTimer();
            counter = 0;

            ZBuffer = new double[0];

            screen = new BitmapData(w, h, false, 0x0);
            screenImage = new Bitmap();
            screenImage.bitmapData = screen;

            addChild(screenImage);
            addChild(txtMain);

            this.enterFrame += render;

            //addEventListener(Event.ENTER_FRAME, render);

        }

    
        private void DoMovement()
        {
            double oldDirX;
            double oldPlaneX;

            if (fKeyUp)
            {
                if (worldMap[(posX + dirX * moveSpeed).Floor(), (posY).Floor()] == 0) posX += dirX * moveSpeed;
                if (worldMap[(posX).Floor(), (posY + dirY * moveSpeed).Floor()] == 0) posY += dirY * moveSpeed;
            }
            if (fKeyDown)
            {
                if (worldMap[(posX - dirX * moveSpeed).Floor(), (posY).Floor()] == 0) posX -= dirX * moveSpeed;
                if (worldMap[(posX).Floor(), (posY - dirY * moveSpeed).Floor()] == 0) posY -= dirY * moveSpeed;
            }
            if (fKeyRight)
            { //both camera direction and camera plane must be rotated
                oldDirX = dirX;
                dirX = dirX * Math.Cos(-rotSpeed) - dirY * Math.Sin(-rotSpeed);
                dirY = oldDirX * Math.Sin(-rotSpeed) + dirY * Math.Cos(-rotSpeed);
                oldPlaneX = planeX;
                planeX = planeX * Math.Cos(-rotSpeed) - planeY * Math.Sin(-rotSpeed);
                planeY = oldPlaneX * Math.Sin(-rotSpeed) + planeY * Math.Cos(-rotSpeed);
            }
            if (fKeyLeft)
            { //both camera direction and camera plane must be rotated
                oldDirX = dirX;
                dirX = dirX * Math.Cos(rotSpeed) - dirY * Math.Sin(rotSpeed);
                dirY = oldDirX * Math.Sin(rotSpeed) + dirY * Math.Cos(rotSpeed);
                oldPlaneX = planeX;
                planeX = planeX * Math.Cos(rotSpeed) - planeY * Math.Sin(rotSpeed);
                planeY = oldPlaneX * Math.Sin(rotSpeed) + planeY * Math.Cos(rotSpeed);
            }
        }






    }

}
