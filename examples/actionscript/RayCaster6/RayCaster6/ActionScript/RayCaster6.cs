﻿using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using System.Collections.Generic;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.filters;
using System;
using ScriptCoreLib.ActionScript.flash.events;
using ScriptCoreLib.ActionScript.flash.net;
using ScriptCoreLib.ActionScript.flash.ui;
using System.Linq;
using ScriptCoreLib.ActionScript.RayCaster;
using ScriptCoreLib.ActionScript.flash.geom;




namespace RayCaster6.ActionScript
{


    /// <summary>
    /// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
    /// </summary>
    [Script, ScriptApplicationEntryPoint]
    [SWF(width = DefaultWidth * DefaultScale, height = DefaultHeight * DefaultScale, frameRate = 60)]
    public class RayCaster6 : Sprite
    {
        // http://www.digital-ist-besser.de/
        // http://www.fredheintz.com/sitefred/main.html

        // 120x90
        // 160x120
        const int DefaultWidth = DefaultHeight * 3 / 2;
        const int DefaultHeight = 240;

        const int DefaultScale = 2;

        public RayCaster6()
        {
            var r = new RayCaster4base(DefaultWidth, DefaultHeight)
            {
                RenderFloorAndCeilingEnabled = false,
                RenderMinimapEnabled = false,
                ViewDirection = 270.DegreesToRadians(),
                WallMap = Texture32.Of(Map1.ToBitmapAsset(), false),
                ViewPosition = new Point { x = 4, y = 22 }
            };

            if (r.CurrentTile != 0)
                throw new Exception("bad start position: " + new { r.posX, r.posY, r.CurrentTile }.ToString());

            r.Image.AttachTo(this);
            r.txtMain.AttachTo(this);

            this.scaleX = DefaultScale;
            this.scaleY = DefaultScale;
            //this.filters = new[] { new BlurFilter() };


            KeyboardButton fKeyTurnLeft = new uint[] { Keyboard.LEFT, 'j', 'J', };
            KeyboardButton fKeyTurnRight = new uint[] { Keyboard.RIGHT, 'l', 'L', };

            KeyboardButton fKeyStrafeLeft = new uint[] { 'a', 'A' };
            KeyboardButton fKeyStrafeRight = new uint[] { 'd', 'D' };

            KeyboardButton fKeyUp = new uint[] { Keyboard.UP, 'i', 'I', 'w', 'W' };
            KeyboardButton fKeyDown = new uint[] { Keyboard.DOWN, 'k', 'K', 's', 'S' };


            stage.keyDown +=
                e =>
                {
                    var key = e.keyCode;

                    fKeyStrafeLeft.ProcessKeyDown(key);
                    fKeyStrafeRight.ProcessKeyDown(key);
                    fKeyTurnLeft.ProcessKeyDown(key);
                    fKeyTurnRight.ProcessKeyDown(key);

                    fKeyUp.ProcessKeyDown(key);
                    fKeyDown.ProcessKeyDown(key);


                };

            stage.keyUp +=
                e =>
                {
                    var key = e.keyCode;


                    fKeyStrafeLeft.ProcessKeyUp(key);
                    fKeyStrafeRight.ProcessKeyUp(key);

                    fKeyTurnLeft.ProcessKeyUp(key);
                    fKeyTurnRight.ProcessKeyUp(key);

                    fKeyUp.ProcessKeyUp(key);
                    fKeyDown.ProcessKeyUp(key);

                };


            (1000 / 30).AtInterval(
                delegate
                {
                    if (fKeyTurnRight.IsPressed)
                        r.ViewDirection += 10.DegreesToRadians();
                    else if (fKeyTurnLeft.IsPressed)
                        r.ViewDirection -= 10.DegreesToRadians();

                    if (fKeyUp.IsPressed || fKeyStrafeLeft.IsPressed || fKeyStrafeRight.IsPressed)
                    {
                        var d = r.ViewDirection;



                        if (fKeyStrafeLeft.IsPressed)
                            d -= 90.DegreesToRadians();
                        else if (fKeyStrafeRight.IsPressed)
                            d += 90.DegreesToRadians();


                        r.MoveTo(
                            r.ViewPositionX + Math.Cos(d) * 0.2,
                            r.ViewPositionY + Math.Sin(d) * 0.2
                        );
                    }
                    else if (fKeyDown.IsPressed)
                        r.MoveTo(
                           r.ViewPositionX + Math.Cos(r.ViewDirection) * -0.2,
                           r.ViewPositionY + Math.Sin(r.ViewDirection) * -0.2
                       );
                }
            );

            var BitmapsLoaded = 0;
            var Bitmaps = default(Func<Bitmap>[]);

            stage.keyUp +=
                   e =>
                   {
                       if (e.keyCode == Keyboard.M)
                       {
                           r.RenderMinimapEnabled = !r.RenderMinimapEnabled;
                       }

                       if (e.keyCode == Keyboard.F)
                       {
                           r.RenderFloorAndCeilingEnabled = !r.RenderFloorAndCeilingEnabled;
                       }
                   };

            Action BitmapsLoadedAction =
                delegate
                {
                    Func<Texture64[], Texture64[]> Reorder8 =
                        p =>
                            Enumerable.ToArray(
                                from i in Enumerable.Range(0, 8)
                                select p[(i + 6) % 8]
                            );

                    var BitmapStream = Bitmaps.Select(i => (Texture64)i()).GetEnumerator();

                    Func<Texture64[]> Next8 =
                        delegate
                        {
                            // keeping compiler happy with full delegate form

                            var a = new[]
                                {
                                    BitmapStream.TakeOrDefault(),
                                    BitmapStream.TakeOrDefault(),
                                    BitmapStream.TakeOrDefault(),
                                    BitmapStream.TakeOrDefault(),

                                    BitmapStream.TakeOrDefault(),
                                    BitmapStream.TakeOrDefault(),
                                    BitmapStream.TakeOrDefault(),
                                    BitmapStream.TakeOrDefault(),

                                };


                            return Reorder8(a.ToArray());
                        };


                    var Stand = Next8();
                    var Walk = new[]
                        {
                            Next8(),
                            Next8(),
                            Next8(),
                            Next8(),
                        };


                    //r.CreateWalkingDummy(Stand, Walk);

                    stage.keyUp +=
                          e =>
                          {
                              if (e.keyCode == Keyboard.SPACE)
                              {
                                  r.CreateWalkingDummy(Stand, Walk);
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

            stage.enterFrame += r.RenderScene;

        }


        [Embed("/flashsrc/textures/dude5.zip")]
        Class MyZipFile;

        [Embed("/flashsrc/textures/Map1.png")]
        Class Map1;
    }
}