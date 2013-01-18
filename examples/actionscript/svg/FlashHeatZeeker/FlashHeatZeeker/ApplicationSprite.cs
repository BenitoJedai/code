using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.BCLImplementation.GLSL;
using ScriptCoreLib.Shared.Lambda;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace FlashHeatZeeker
{
    static class X
    {
        public static double GetLength(this __vec2 p)
        {
            return Math.Sqrt(p.x * p.x + p.y * p.y);
        }

        public static double DegreesToRadians(this double Degrees)
        {
            return (Math.PI * 2) * Degrees / 360;
        }

        public static double DegreesToRadians(this int Degrees)
        {
            return (Math.PI * 2) * Degrees / 360;
        }

        public static int RadiansToDegrees(this double Arc)
        {
            return (int)(360 * Arc / (Math.PI * 2));
        }

        public static double GetRotation(this __vec2 p)
        {
            var x = p.x;
            var y = p.y;

            const double _180 = System.Math.PI;
            const double _90 = System.Math.PI / 2;
            const double _270 = System.Math.PI * 3 / 2;

            if (x == 0)
                if (y < 0)
                    return _270;
                else if (y == 0)
                    return 0;
                else
                    return _90;

            if (y == 0)
                if (x < 0)
                    return _180;
                else
                    return 0;

            var a = System.Math.Atan(y / x);

            if (x < 0)
                a += _180;
            else if (y < 0)
                a += System.Math.PI * 2;


            return a;
        }
    }

    [SWF(frameRate = 60)]
    public sealed class ApplicationSprite : Sprite
    {
        class GameUnit
        {
            public Sprite loc;
            public Sprite rot;
        }

        public ApplicationSprite()
        {
            stage.align = StageAlign.TOP_LEFT;
            stage.scaleMode = StageScaleMode.NO_SCALE;

            #region bg fill
            var fill = new Sprite().AttachTo(this);

            fill.graphics.beginFill(0xA26D41);
            fill.graphics.drawRect(0, 0, this.stage.stageWidth, this.stage.stageHeight);

            this.stage.resize +=
                delegate
                {
                    fill.graphics.beginFill(0xA26D41);
                    fill.graphics.drawRect(0, 0, this.stage.stageWidth, this.stage.stageHeight);
                };
            #endregion

            var egocenter = new Sprite();
            var egorotation = new Sprite().AttachTo(egocenter);

            var map = new Sprite().AttachTo(egorotation);

            map.graphics.beginFill(0xB27D51);
            map.graphics.drawRect(0, 0, 4000, 400);


            // http://wiki.openoffice.org/wiki/SVG_User_Experiences
            //Enclosed Exception:
            //The current document is unable to create an element of the requested type (namespace: http://www.w3.org/2000/svg, name: flowRoot).

            //        [Embed(source = "/assets/FlashHeatZeeker/touchdown.svg", mimeType = "image/svg-xml")]
            //        ^

            //U:\web\FlashHeatZeeker\ApplicationSprite.as(104): col: 9: Error: Unable to transcode /assets/FlashHeatZeeker/touchdown.svg.



            KnownEmbeddedResources.Default["assets/FlashHeatZeeker/touchdown.svg"].ToSprite().AttachTo(map).MoveTo(100, 0).With(
                svg =>
                {
                    svg.scaleX = 0.5;
                    svg.scaleY = 0.5;
                }
            );


            // unit 1

            Func<GameUnit> greentank = delegate
            {
                var unit1_loc = new Sprite().AttachTo(map);
                var unit1_rot = new Sprite().AttachTo(unit1_loc);

                KnownEmbeddedResources.Default["assets/FlashHeatZeeker/greentank.svg"].ToSprite().AttachTo(unit1_rot).MoveTo(-200, -200);

                return new GameUnit { loc = unit1_loc, rot = unit1_rot };
            };

            var unit1 = greentank();
            var unit2 = greentank();
            var unit3 = greentank();

            unit1.loc.MoveTo(300, 200);
            unit2.loc.MoveTo(0, 400);

            var CurrentUnit = unit1;



            KnownEmbeddedResources.Default["assets/FlashHeatZeeker/hill0.svg"].ToSprite().AttachTo(map).MoveTo(0, 0).With(
                svg =>
                {
                    svg.scaleX = 0.2;
                    svg.scaleY = 0.2;
                }
            );

            for (int iy = 0; iy < 128; iy++)
            {
                KnownEmbeddedResources.Default["assets/FlashHeatZeeker/tree0.svg"].ToSprite().AttachTo(map).MoveTo(400, 0).With(
                    svg =>
                    {
                        svg.cacheAsBitmap = true;

                        svg.scaleX = 0.2;
                        svg.scaleY = 0.2;

                        if (iy % 3 == 0)
                            svg.y += 50;

                        if (iy % 3 == 1)
                            svg.y += 100;


                        svg.x += 15 * iy;
                    }
                );
            }

            for (int iy = 0; iy < 128; iy++)
            {
                KnownEmbeddedResources.Default["assets/FlashHeatZeeker/tree0.svg"].ToSprite().AttachTo(map).MoveTo(400, 0).With(
                    svg =>
                    {
                        svg.y += 300;

                        svg.cacheAsBitmap = true;

                        svg.scaleX = 0.2;
                        svg.scaleY = 0.2;

                        if (iy % 3 == 0)
                            svg.y += 50;

                        if (iy % 3 == 1)
                            svg.y += 100;


                        svg.x += 15 * iy;
                    }
                );
            }



            // ego is in center
            map.MoveTo(-300, -200);

            this.stage.keyUp +=
                e =>
                {

                    //Console.WriteLine("keyUp " + new { e.keyCode });

                    if (e.keyCode == (uint)System.Windows.Forms.Keys.D3)
                    {
                        CurrentUnit = unit3;
                        egorotation.rotation = -CurrentUnit.rot.rotation;
                        map.MoveTo(-CurrentUnit.loc.x, -CurrentUnit.loc.y);
                    }

                    if (e.keyCode == (uint)System.Windows.Forms.Keys.D1)
                    {
                        CurrentUnit = unit1;
                        egorotation.rotation = -CurrentUnit.rot.rotation;
                        map.MoveTo(-CurrentUnit.loc.x, -CurrentUnit.loc.y);
                    }

                    if (e.keyCode == (uint)System.Windows.Forms.Keys.D2)
                    {
                        CurrentUnit = unit2;
                        egorotation.rotation = -CurrentUnit.rot.rotation;
                        map.MoveTo(-CurrentUnit.loc.x, -CurrentUnit.loc.y);
                    }


                    if (e.keyCode == (uint)System.Windows.Forms.Keys.F11)
                    {
                        this.stage.SetFullscreen(true);
                    }
                };

            this.stage.keyDown +=
              e =>
              {
                  //Console.WriteLine("keyDown " + new { e.keyCode });

                  if (e.keyCode == (uint)System.Windows.Forms.Keys.Up)
                  {
                      CurrentUnit.loc.x += 2 * Math.Cos((CurrentUnit.rot.rotation + 270).DegreesToRadians());
                      CurrentUnit.loc.y += 2 * Math.Sin((CurrentUnit.rot.rotation + 270).DegreesToRadians());
                  }

                  if (e.keyCode == (uint)System.Windows.Forms.Keys.Down)
                  {
                      CurrentUnit.loc.x += -2 * Math.Cos((CurrentUnit.rot.rotation + 270).DegreesToRadians());
                      CurrentUnit.loc.y += -2 * Math.Sin((CurrentUnit.rot.rotation + 270).DegreesToRadians());
                  }

                  if (e.keyCode == (uint)System.Windows.Forms.Keys.Left)
                  {
                      CurrentUnit.rot.rotation -= 5;
                  }

                  if (e.keyCode == (uint)System.Windows.Forms.Keys.Right)
                  {
                      CurrentUnit.rot.rotation += 5;
                  }


                  egorotation.rotation = -CurrentUnit.rot.rotation;
                  map.MoveTo(-CurrentUnit.loc.x, -CurrentUnit.loc.y);
              };

            #region egocrosshair
            var egocrosshair = new Sprite().AttachTo(egocenter);

            egocrosshair.graphics.lineStyle(2, 0x007f00, 1);

            egocrosshair.graphics.moveTo(-32, 0);
            egocrosshair.graphics.lineTo(32, 0);


            egocrosshair.graphics.moveTo(0, -32);
            egocrosshair.graphics.lineTo(0, 32);
            #endregion

            egocenter.AttachTo(this);



            egocenter.MoveTo(this.stage.stageWidth / 2, this.stage.stageHeight / 2);
            this.stage.resize +=
               delegate
               {
                   egocenter.MoveTo(this.stage.stageWidth / 2, this.stage.stageHeight / 2);
               };


            #region AtInitializeConsoleFormWriter
            this.AtInitializeConsoleFormWriter = (
                Action<string> Console_Write,
                Action<string> Console_WriteLine
            ) =>
            {

                try
                {
                    var w = new __OutWriter();

                    var o = Console.Out;

                    var __reentry = false;

                    w.AtWrite =
                        x =>
                        {
                            o.Write(x);

                            if (!__reentry)
                            {
                                __reentry = true;
                                Console_Write(x);
                                __reentry = false;
                            }
                        };

                    w.AtWriteLine =
                        x =>
                        {
                            o.WriteLine(x);

                            if (!__reentry)
                            {
                                __reentry = true;
                                Console_WriteLine(x);
                                __reentry = false;
                            }
                        };

                    Console.SetOut(w);

                    Console.WriteLine("flash Console.WriteLine should now appear in JavaScript form!");
                }
                catch
                {

                }
            };
            #endregion

            var info = new TextField
            {
                selectable = false,
                mouseEnabled = false,
                autoSize = TextFieldAutoSize.LEFT
            }.AttachTo(this).MoveTo(8, 8);


            var maxframe = new Stopwatch();
            var maxframe_elapsed = 0.0;

            #region fps
            var sw = new Stopwatch();

            sw.Start();

            var ii = 0;

            maxframe.Start();
            this.enterFrame +=
                delegate
                {
                    maxframe.Stop();

                    //                    System.TimeSpan for Boolean op_GreaterThan(System.TimeSpan, System.TimeSpan) used at
                    //FlashHeatZeeker.ApplicationSprite+<>c__DisplayClass11.<.ctor>b__d at offset 001e.

                    //                TypeError: Error #1009: Cannot access a property or method of a null object reference.
                    //at FlashHeatZeeker::ApplicationSprite___c__DisplayClass11/__ctor_b__d_100663322()[U:\web\FlashHeatZeeker\ApplicationSprite___c__DisplayClass11.as:141]

                    if (maxframe.Elapsed.TotalMilliseconds > maxframe_elapsed)
                        maxframe_elapsed = maxframe.Elapsed.TotalMilliseconds;

                    if (sw.ElapsedMilliseconds < 1000)
                    {
                        ii++;

                        maxframe.Restart();

                        return;
                    }

                    info.text = new { fps = ii, maxframe_elapsed }.ToString();

                    //if (fps != null)
                    //    fps("" + ii);

                    ii = 0;
                    maxframe_elapsed = 0;
                    sw.Restart();
                };
            #endregion

        }

        public event Action<string> fps;


        Action<Action<string>, Action<string>> AtInitializeConsoleFormWriter;


        #region InitializeConsoleFormWriter
        class __OutWriter : TextWriter
        {
            public Action<string> AtWrite;
            public Action<string> AtWriteLine;

            public override void Write(string value)
            {
                AtWrite(value);
            }

            public override void WriteLine(string value)
            {
                AtWriteLine(value);
            }

            public override Encoding Encoding
            {
                get { return Encoding.UTF8; }
            }
        }

        public void InitializeConsoleFormWriter(
            Action<string> Console_Write,
            Action<string> Console_WriteLine
        )
        {
            AtInitializeConsoleFormWriter(Console_Write, Console_WriteLine);
        }
        #endregion
    }
}
