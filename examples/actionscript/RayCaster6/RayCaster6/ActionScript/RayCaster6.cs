using System;
using System.Collections.Generic;
using System.Linq;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Lambda;
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
using System.Collections.Specialized;




namespace RayCaster6.ActionScript
{


    /// <summary>
    /// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
    /// </summary>
    [Script, ScriptApplicationEntryPoint]
    [SWF(width = DefaultWidth * DefaultScale, height = DefaultHeight * DefaultScale, frameRate = 60)]
    public class RayCaster6 : Sprite
    {
        // http://www.lostinactionscript.com/blog/index.php/2007/10/13/flash-you-tube-api/
        // http://www.digital-ist-besser.de/
        // http://www.fredheintz.com/sitefred/main.html

        // 120x90
        // 160x120
        const int DefaultWidth = DefaultHeight * 3 / 2;
        const int DefaultHeight = 180;

        const int DefaultScale = 2;

        public RayCaster6()
        {
            AddFullscreenMenu();


            var r2 = new RayCaster4base(64, 48)
            {
                RenderMinimapEnabled = false
            };


            var r = new RayCaster4base(DefaultWidth, DefaultHeight)
            {
                RenderFloorAndCeilingEnabled = false,
                RenderMinimapEnabled = true,

                ViewPosition = new Point { x = 4, y = 22 },
                ViewDirection = 0.DegreesToRadians(),

            };



            r.Image.AttachTo(this);
            r.txtMain.AttachTo(this);

            r.Image.scaleX = DefaultScale;
            r.Image.scaleY = DefaultScale;
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

            var Ego = default(SpriteInfo);

            Action UpdateEgoPosition =
                delegate
                {
                    if (Ego != null)
                    {
                        Ego.Position = r.ViewPosition;
                        Ego.Direction = r.ViewDirection;
                    }
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

                    UpdateEgoPosition();
                }
            );


            stage.keyUp +=
                   e =>
                   {
                       if (e.keyCode == Keyboard.N)
                       {
                           r.RenderLowQualityWalls = !r.RenderLowQualityWalls;
                       }

                       if (e.keyCode == Keyboard.M)
                       {
                           r.RenderMinimapEnabled = !r.RenderMinimapEnabled;
                       }

                       if (e.keyCode == Keyboard.F)
                       {
                           r.RenderFloorAndCeilingEnabled = !r.RenderFloorAndCeilingEnabled;
                       }
                   };


            Action<Bitmap[]> BitmapsLoadedAction =
                Bitmaps =>
                {
                    Func<Texture64[], Texture64[]> Reorder8 =
                        p =>
                            Enumerable.ToArray(
                                from i in Enumerable.Range(0, 8)
                                select p[(i + 6) % 8]
                            );

                    var BitmapStream = Bitmaps.Select(i => (Texture64)i).GetEnumerator();

                    Func<Texture64[]> Next8 =
                        delegate
                        {
                            // keeping compiler happy with full delegate form

                            if (BitmapStream == null)
                                throw new Exception("BitmapStream is null");

                            return Reorder8(BitmapStream.Take(8));
                        };


                    var Stand = Next8();
                    var Walk = new[]
                        {
                            Next8(),
                            Next8(),
                            Next8(),
                            Next8(),
                        };



                    Ego = r.CreateWalkingDummy(Stand, Walk);
                    UpdateEgoPosition();

                    stage.keyUp +=
                          e =>
                          {
                              if (e.keyCode == Keyboard.SPACE)
                              {
                                  var s = r.CreateWalkingDummy(Stand, Walk);

                                  s.Direction += 180.DegreesToRadians();

                                  r2.ViewPosition = s.Position;
                                  r2.ViewDirection = s.Direction;
                              }

                              if (e.keyCode == Keyboard.INSERT)
                              {
                                  var s = r.CreateWalkingDummy(Stand, Walk);

                                  s.Direction += 180.DegreesToRadians();
                                  s.Position = Ego.Position.MoveToArc(Ego.Direction, 0.5);
                              }

                              if (e.keyCode == Keyboard.ENTER)
                              {
                                  r.ViewPosition = new Point { x = 4, y = 22 };
                                  r.ViewDirection = 270.DegreesToRadians();


                              }



                              if (e.keyCode == Keyboard.BACKSPACE)
                              {

                                  (1000 / 30).AtInterval(
                                      t =>
                                      {
                                          r.ViewDirection += 18.DegreesToRadians();

                                          if (t.currentCount == 10)
                                              t.stop();
                                      }
                                  );
                              }
                          };
                };


            MyZipFile
                .ToFiles()
                .Where(f => f.FileName.EndsWith(".png"))
                .ToBitmapArray(BitmapsLoadedAction);

            MyStuff.ToFiles().ToBitmapDictionary(
                    f =>
                    {
                        // ! important
                        // ----------------------------------------------------
                        // ! loading png via bytes affects pixel values
                        // ! this is why map is in gif format

                        r.Map.WorldMap = Texture32.Of(f["Map1.gif"], false);

                        var FreeSpaceForStuff = r.Map.WorldMap.Entries.Where(i => i.Value == 0); //.Randomize().GetEnumerator();

                        Func<Texture64.Entry, bool> IsNearWall =
                           w =>
                           {
                               Func<int, int, bool> WallAtOffset =
                                   (x, y) => r.Map.WorldMap[w.XIndex + x, w.YIndex + y] != 0;

                               if (WallAtOffset(1, 0))
                                   return true;

                               if (WallAtOffset(-1, 0))
                                   return true;

                               if (WallAtOffset(0, 1))
                                   return true;

                               if (WallAtOffset(0, -1))
                                   return true;

                               return false;
                           };

                        var FreeSpaceNearWalls = FreeSpaceForStuff.Where(IsNearWall);
                        var FreeSpaceForLamps = FreeSpaceForStuff.Where(w => !IsNearWall(w));

                        var SpaceNearWalls = FreeSpaceNearWalls.Randomize().GetEnumerator();
                        var SpaceForLamps = FreeSpaceForLamps.Randomize().GetEnumerator();


                        Action<IEnumerator<Texture64.Entry>, Texture64> AddSpriteByTexture =
                            (SpaceForStuff, tex) => SpaceForStuff.Take().Do(p => r.CreateDummy(tex).Position.To(p.XIndex + 0.5, p.YIndex + 0.5));

                        Action<string> AddSpriteNearWall =
                            texname => AddSpriteByTexture(SpaceNearWalls, f[texname + ".png"]);


                        Action<string> AddSpaceForLamps =
                            texname => AddSpriteByTexture(SpaceForLamps, f[texname + ".png"]);


                        AddSpaceForLamps.Multiple(
                            new KeyValuePairList<int, string>
                            {
                                // multi dict?
                                {9, "lamp"},
                                {8, "chandelier"},
                        
                            }
                        );

                        AddSpriteNearWall.Multiple(
                           new KeyValuePairList<int, string>
                            {
                                // multi dict?
                  
                                {4, "armor"},
                                {32, "plantbrown"},
                                {32, "plantgreen"},
                            }
                       );

                        r.FloorTexture = f["floor.png"];
                        r.CeilingTexture = f["roof.png"];



                        var DynamicTextureBitmap = new Bitmap(new BitmapData(Texture64.SizeConstant, Texture64.SizeConstant, false, 0));
                        Texture64 DynamicTexture = DynamicTextureBitmap;
                        uint DynamicTextureKey = 0xffffff;

                        r.Map.WorldMap[2, 22] = DynamicTextureKey;
                        r.Map.WorldMap[3, 15] = DynamicTextureKey;


                        r.Map.Textures = new Dictionary<uint, Texture64>
                        {
                            {0xff0000, f["graywall.png"]},
                            {0x0000ff, f["bluewall.png"]},
                            {0x00ff00, f["greenwall.png"]},
                            {0x7F3300, f["woodwall.png"]},

                            {DynamicTextureKey, DynamicTexture}
                        };

                        r.ViewDirection = 270.DegreesToRadians();
                        r.ViewPosition = r.ViewPosition;

                        if (r.CurrentTile != 0)
                            throw new Exception("bad start position: " + new { r.ViewPositionX, r.ViewPositionY, r.CurrentTile }.ToString());


                        r.RenderScene();

                        stage.enterFrame += r.RenderScene;

                        var MirrorFrame = f["mirror.png"];

                        300.AtInterval(
                            t =>
                            {
                                DynamicTextureBitmap.bitmapData.fillRect(DynamicTextureBitmap.bitmapData.rect, (uint)(t.currentCount * 8 % 256));
                                var m = new Matrix();

                                // to center
                                m.translate(0, 10);
                                // m.scale(0.3, 0.3);

                                r2.RenderScene();

                                DynamicTextureBitmap.bitmapData.draw(r2.Image.bitmapData, m);
                                DynamicTextureBitmap.bitmapData.draw(MirrorFrame.bitmapData);

                                DynamicTexture.Update();
                            }
                        );




                        r2.Map.WorldMap = r.Map.WorldMap;
                        r2.Map.Textures = r.Map.Textures;
                        r2.Sprites = r.Sprites;
                        r2.ViewPosition = r.ViewPosition;

                    }
                );



        }

        private void AddFullscreenMenu()
        {
            var GoFullScreen = new ContextMenuItem("Fullscreen");

            GoFullScreen.menuItemSelect +=
                delegate
                {
                    stage.SetFullscreen(true);
                };

            this.contextMenu = new ContextMenu
            {
                customItems = new[] { GoFullScreen }
            };
        }


        [Embed("/flashsrc/textures/dude5.zip")]
        Class MyZipFile;

        [Embed("/flashsrc/textures/stuff.zip")]
        Class MyStuff;

        // fps: 58
    }
}