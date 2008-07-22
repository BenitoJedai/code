using ScriptCoreLib;
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
        const int DefaultHeight = 120;

        const int DefaultScale = 3;

        public RayCaster6()
        {
            var r = new RayCaster4base(DefaultWidth, DefaultHeight)
            {
                RenderFloorAndCeilingEnabled = true,
                RenderMinimapEnabled = false,
                ViewDirection = 270.DegreesToRadians(),
                WallMap = Texture32.Of(Map1.ToBitmapAsset(), false),
                posX = 4,
                posY = 28
            };

            if (r.CurrentTile != 0)
                throw new Exception("bad start position: " + new { r.posX, r.posY, r.CurrentTile }.ToString());

            r.screenImage.AttachTo(this);
            r.txtMain.AttachTo(this);

            this.scaleX = DefaultScale;
            this.scaleY = DefaultScale;
            //this.filters = new[] { new BlurFilter() };


            KeyboardButton fKeyLeft = new uint[] { Keyboard.LEFT, 'j', 'J', 'a', 'A' };
            KeyboardButton fKeyRight = new uint[] { Keyboard.RIGHT, 'l', 'L', 'd', 'D' };

            stage.keyDown +=
                e =>
                {
                    var key = e.keyCode;

                    r.fKeyUp.ProcessKeyDown(key);
                    r.fKeyDown.ProcessKeyDown(key);
                    fKeyLeft.ProcessKeyDown(key);
                    fKeyRight.ProcessKeyDown(key);
                };

            stage.keyUp +=
                e =>
                {
                    var key = e.keyCode;

                    r.fKeyUp.ProcessKeyUp(key);
                    r.fKeyDown.ProcessKeyUp(key);
                    fKeyLeft.ProcessKeyUp(key);
                    fKeyRight.ProcessKeyUp(key);
                };


            (1000 / 30).AtInterval(
                delegate
                {
                    if (fKeyRight.IsPressed)
                        r.ViewDirection += 10.DegreesToRadians();

                    if (fKeyLeft.IsPressed)
                        r.ViewDirection -= 10.DegreesToRadians();
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

            stage.enterFrame += r.render;

        }


        [Embed("/flashsrc/textures/dude5.zip")]
        Class MyZipFile;

        [Embed("/flashsrc/textures/Map1.png")]
        Class Map1;
    }
}