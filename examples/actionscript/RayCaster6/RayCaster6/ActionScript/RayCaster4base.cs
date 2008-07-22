using System;
using System.Collections.Generic;
using System.Linq;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.events;
using ScriptCoreLib.ActionScript.flash.filters;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.ActionScript.flash.net;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.flash.ui;
using ScriptCoreLib.ActionScript.RayCaster;


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
        protected Loader bitmapLoader;
        protected BitmapData image;
        protected Sprite imageCont;

        //protected double[] floorVals;

        protected double posX;
        protected double posY;  //x and y start position

        protected double dirX;
        protected double dirY; //initial direction vector
        double dir;

        protected double planeX;
        protected double planeY; //the 2d raycaster version of camera plane
        protected int w;
        protected int h;

        protected double moveSpeed;
        protected double rotSpeed;

        protected BitmapData screen;
        protected Bitmap screenImage;
        //protected Sprite[] sprites; // ?
        protected double[] ZBuffer;
        protected int time;
        protected int counter;




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

            //textures = new uint[0][][];
            LoadTextures();

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

        protected KeyboardButton fKeySpace = new uint[] { Keyboard.SPACE };



        [Embed("/flashsrc/textures/dude5.zip")]
        Class MyZipFile;


        [Script(NoDecoration = true)]
        protected void prepare()
        {
            stage.align = StageAlign.TOP_LEFT;
            //stage.quality 	= StageQuality.LOW;

            txtMain = new TextField
            {
                defaultTextFormat = new TextFormat
                {
                    font = "Verdana",
                    align = TextFormatAlign.LEFT,
                    size = 10,
                    color = 0xffffff
                },
                autoSize = TextFieldAutoSize.LEFT,
                text = "0"
            };


            moveSpeed = 0.2;
            rotSpeed = 0.12;


            posX = 22.5;
            posY = 13.5;

            dirX = -1;
            dirY = 0;
            planeX = 0;
            planeY = 0.66;

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


            this.Sprites.Add(
                //new SpriteInfo
                //{
                //    Position = new Point { x = 21.5, y = 14.5 }
                //},
                 new SpriteInfo
                {
                    Position = new Point { x = 18.5, y = 13.5 },
                    CurrentFrame = new[] { this.textures[3] }
                });

            this.Sprites.Add(
                      new SpriteInfo
                {
                    Position = new Point { x = 19.5, y = 13.5 },
                    CurrentFrame = new[] { this.textures[3] }
                }
            );

            this.enterFrame += render;



            var BitmapsLoaded = 0;
            var Bitmaps = default(Func<Bitmap>[]);

            //var ghost = new SpriteInfo
            //{
            //    CurrentFrame = textures[3],
            //    Position = new Point { x = posX, y = posY }
            //}.AddTo(Sprites);

            //var ghost_pos = new Queue<Point>();

            //(1000 / 15).AtInterval(
            //    delegate
            //    {
            //        ghost_pos.Enqueue(
            //            new Point { x = posX, y = posY }
            //            );

            //        if (ghost_pos.Count > 64)
            //        {
            //            var p = ghost_pos.Dequeue();

            //            ghost.Position.x = p.x;
            //            ghost.Position.y = p.y;
            //        }
            //    }
            //);

            Action BitmapsLoadedAction =
                delegate
                {
                    var AsTexturesUnordered = Bitmaps.Take(8).Select(i => (Texture64)i()).ToArray();

                    var AsTextures = Enumerable.ToArray(
                        from i in Enumerable.Range(0, 8)
                        select AsTexturesUnordered[(i + 6) % 8]
                    );


                    stage.keyUp +=
                       e =>
                       {
                           if (e.keyCode == Keyboard.SPACE)
                           {
                               var s = new SpriteInfo
                                  {
                                      Position = new Point { x = posX + dirX * 2, y = posY + dirY * 2 },
                                      CurrentFrame = AsTextures,
                                      Direction = dir
                                  }.AddTo(Sprites);


                               //(1000).AtInterval(
                               //    t =>
                               //    {
                               //        s.CurrentFrame = AsTextures[t.currentCount % AsTextures.Length];
                               //    }
                               //);
                           }

                       };
                };

            Bitmaps = Enumerable.ToArray(
                from File in
                    from f in MyZipFile.ToFiles()
                    // you can filter your images here
                    where f.FileName.EndsWith(".png")
                    select f
                select

                    File.Bytes.LoadBytes<Bitmap>(
                        i =>
                        {
                            BitmapsLoaded++;

                            if (Bitmaps.Length == BitmapsLoaded)
                                BitmapsLoadedAction();
                        }
                    )

            );


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
                dir = new Point { x = dirX, y = dirY }.GetRotation();

                oldPlaneX = planeX;
                planeX = planeX * Math.Cos(-rotSpeed) - planeY * Math.Sin(-rotSpeed);
                planeY = oldPlaneX * Math.Sin(-rotSpeed) + planeY * Math.Cos(-rotSpeed);
            }
            if (fKeyLeft)
            { //both camera direction and camera plane must be rotated
                oldDirX = dirX;
                dirX = dirX * Math.Cos(rotSpeed) - dirY * Math.Sin(rotSpeed);
                dirY = oldDirX * Math.Sin(rotSpeed) + dirY * Math.Cos(rotSpeed);
                dir = new Point { x = dirX, y = dirY }.GetRotation();

                oldPlaneX = planeX;
                planeX = planeX * Math.Cos(rotSpeed) - planeY * Math.Sin(rotSpeed);
                planeY = oldPlaneX * Math.Sin(rotSpeed) + planeY * Math.Cos(rotSpeed);
            }
        }






    }

}
