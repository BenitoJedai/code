using System;
using System.Linq;
using System.Collections.Generic;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.flash.ui;

namespace FlashConsoleWorm.ActionScript
{
    /// <summary>
    /// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
    /// </summary>
    [Script, ScriptApplicationEntryPoint]
    [SWF(width = DefaultWidth, height = DefaultHeight, backgroundColor = ColorBlack)]
    public class FlashConsoleWorm : Sprite
    {
        // based on javascript version

        // vNext should be semi 3D - http://www.freeworldgroup.com/games/3dworm/index.html


        public const uint ColorBlack = 0;
        public const uint ColorRed = 0xff0000;
        public const uint ColorGreen = 0x00ff00;

        public const int DefaultWidth = RoomWidth * DefaultZoom;
        public const int DefaultHeight = RoomHeight * DefaultZoom;

        public const int DefaultZoom = 10;

        public const int RoomWidth = 48;
        public const int RoomHeight = 48;

        public readonly KeyboardButtonGroup MovementWASD;
        public readonly KeyboardButtonGroup MovementArrows;
        /// <summary>
        /// Default constructor
        /// </summary>
        public FlashConsoleWorm()
        {
            var canvas = new Sprite();

            //s.bitmapData.setPixel(1, 1, ColorGreen);
            canvas.scaleX = DefaultZoom;
            canvas.scaleY = DefaultZoom;

            canvas.AttachTo(this);

            // add scull ani here
            // add status text here


            Func<Point> GetRandomLocation =
                () => new Point
                    {
                        x = (RoomWidth - 1).Random(),
                        y = (RoomHeight - 1).Random()
                    };

            Func<Point, Point> Wrapper =
                p => new Point
                {
                    x = (p.x + RoomWidth) % RoomWidth,
                    y = (p.y + RoomHeight) % RoomHeight
                };

            Func<Apple> CreateApple =
                   () => new Apple
                   {
                       GetRandomLocation = GetRandomLocation,
                       Wrapper = Wrapper
                   }.MoveToRandomLocation();

            var apples = new List<Apple>();

            15.Times(() =>
                CreateApple().AttachTo(canvas).AddTo(apples)
            );

            var worm = new Worm
            {
                Wrapper = Wrapper,
                Location = new Point { x = 4, y = 8 },
                Canvas = canvas,
                Vector = new Point { x = 0, y = 1 },
                // Color = game_colors.worm.active
            }
             .Grow()
             .GrowToVector()
             .GrowToVector();

            #region keyboard

            MovementWASD = new KeyboardButtonGroup { Name = "WASD" };
            MovementArrows = new KeyboardButtonGroup { Name = "Arrows" };

            var GoLeft = new KeyboardButton(stage)
            {
                Groups = new[]
                {
                    MovementWASD[Keyboard.A],
                    MovementArrows[Keyboard.LEFT],
                },
                Up = () => { worm.Vector = new Point { x = -1, y = 0 }; },
            }.Up;

            var GoRight = new KeyboardButton(stage)
            {
                Groups = new[]
                {
                    MovementWASD[Keyboard.D],
                    MovementArrows[Keyboard.RIGHT],
                },
                Up = () => { worm.Vector = new Point { x = 1, y = 0 }; },
            }.Up;

            var GoUp = new KeyboardButton(stage)
            {
                Groups = new[]
                {
                    MovementWASD[Keyboard.W],
                    MovementArrows[Keyboard.UP],
                },
                Up = () => { worm.Vector = new Point { x = 0, y = -1 }; },
            }.Up;

            var GoDown = new KeyboardButton(stage)
            {
                Groups = new[]
                {
                    MovementWASD[Keyboard.S],
                    MovementArrows[Keyboard.DOWN],
                },
                Up = () => { worm.Vector = new Point { x = 0, y = 1 }; },

            }.Up;

            #endregion

            stage.click +=
                e =>
                {
                    #region Dia
                    var A = e.stageX > e.stageY;
                    var B = e.stageX > (stage.stageWidth - e.stageY);

                    var DiaClipLeft = !A && !B;
                    var DiaClipRight = A && B;
                    var DiaClipTop = A && !B;
                    var DiaClipBottom = !A && B;
                    #endregion

                    if (DiaClipLeft) GoLeft();
                    else if (DiaClipRight) GoRight();
                    else if (DiaClipTop) GoUp();
                    else if (DiaClipBottom) GoDown();
                };

            100.AtInterval(
               t =>
               {
                   if (worm.Parts.Any(i => i.Location.IsEqual(worm.NextLocation)))
                   {
                       worm.IsAlive = false;

                       //AddScore(0);

                   }

                   if (worm.IsAlive)
                   {
                       worm.GrowToVector();

                       // did we find an apple?
                       var a = apples.Where(i => i.Location.IsEqual(worm.Location)).ToArray();

                       if (a.Length > 0)
                       {
                           foreach (var v in a)
                           {
                               v.MoveToRandomLocation();
                           }

                           // AddScore(1);
                       }
                       else
                       {
                           worm.Shrink();
                       }
                   }
               }
            );
        }
    }
}