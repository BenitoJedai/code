//using System.Linq;

using ScriptCoreLib;
using ScriptCoreLib.Shared;

using ScriptCoreLib.Shared.Drawing;

using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Controls;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM.XML;

using System.Linq;

using ScriptCoreLib.Shared.Lambda;
using System;

//using global::System.Collections.Generic;



namespace GameOfLife.js
{
    [Script]
    class Array2D<T>
    {
        readonly T[] items;

        readonly int x;
        readonly int y;

        public Array2D(int x, int y)
        {
            this.x = x;
            this.y = y;

            this.items = new T[x * y];
        }

        public void ForEach(Action<int, int> a)
        {
            for (int i = 0; i < this.x; i++)
                for (int j = 0; j < this.y; j++)
                    a(i, j);

        }



        public T this[int x, int y]
        {
            get
            {
                if (x < 0) return default(T);
                if (y < 0) return default(T);
                if (x >= this.x) return default(T);
                if (y >= this.y) return default(T);

                return this.items[this.x * y + x];
            }
            set
            {
                if (x < 0) return;
                if (y < 0) return;
                if (x >= this.x) return;
                if (y >= this.y) return;

                this.items[this.x * y + x] = value;
            }
        }
    }

    [Script, ScriptApplicationEntryPoint]
    public class GameOfLife
    {

        [Script]
        class __Type1
        {
            public Color  ColorDeath;
            public Color  ColorBirth;
        }

        /// <summary>
        /// Creates a new control
        /// </summary>
        /// <param name="DataElement">The hidden data element</param>
        public GameOfLife()
        {


            Native.Document.body.style.backgroundColor = Color.System.ThreeDFace;


            var vv = new ArenaControl();





            var cx = 24;
            var cy = 24;

            var w = 24;
            var h = 24;

            vv.SetCanvasSize(new Point(cx * w, cy * h));

            vv.SetLocation(new Rectangle { Left = 32, Top = 32, Width = 400, Height = 300 });
            vv.Layers.Canvas.style.backgroundColor = Color.White;

            vv.Control.AttachToDocument();


            var buffer = new Array2D<LayeredControl.CanvasRectangle>(cx, cy);


            vv.Layers.Canvas.Hide();

            Action<string, Point, Color> DrawTextWithShadow =
                (text, pos, c) =>
                {
                    vv.DrawTextToInfo(text, pos + new Point(2, 2), Color.Black);
                    vv.DrawTextToInfo(text, pos, Color.White);
                    vv.DrawTextToInfo(text, pos + new Point(1, 1), c);
                };

            DrawTextWithShadow("Game Of Life - Use middle mouse button to drag map around", new Point(8, 8), Color.Red);

            int index = 0;

            var State = new __Type1
                        {

                            ColorDeath = Color.White,
                            //ColorSurvival = Color.Gray,
                            ColorBirth = Color.Black
                        };

            Func<Color> RandomState = () => new System.Random().NextDouble() > 0.5 ? State.ColorDeath : State.ColorBirth;


            #region reset

            Action Reset =
                () =>
                buffer.ForEach(
                    (int x, int y) =>
                    {
                        var c = new LayeredControl.CanvasRectangle
                                {
                                    BackgroundColor = RandomState(),
                                    Location = new Rectangle { Left = x * w + 1, Top = y * h + 1, Width = w - 2, Height = h - 2 }
                                };

                        vv.DrawRectangleToCanvas(c);

                        buffer[x, y] = c;
                    }
                );

            Reset();

            #endregion

            vv.Layers.Canvas.Show();

            var f = new IHTMLElement(IHTMLElement.HTMLElementEnum.fieldset);

            var chk_enabled = new IHTMLInput(HTMLInputTypeEnum.checkbox);
            var btn_reset = new IHTMLButton("Randomize");

            btn_reset.onclick += (i) => Reset();

            f.appendChild(btn_reset, chk_enabled, new IHTMLLabel("Activate", chk_enabled));

            f.AttachToDocument();
            f.style.SetLocation(500, 60);

            var NextEvolution = default(Action);

            Action<int> SleepAndEvolve =
                (timeout) => new Timer((t) => NextEvolution(), timeout, 0);


            NextEvolution =
                () =>
                {

                    var nextframe = default(Action);
                    var t = IDate.Now.getTime();

                    if (chk_enabled.@checked)
                        buffer.ForEach(
                            (int x, int y) =>
                            {
                                Func<int, int, LayeredControl.CanvasRectangle> g =
                                    (ox, oy) => buffer[x + ox, y + oy];

                                var u = g(0, 0);

                                var c = new[] {
                                g(-1, -1),
                                g(-1, 0),
                                g(-1, 1),
                                g(0, 1),
                                g(0, -1),
                                g(1, -1),
                                g(1, 0),
                                g(1, 1)
                            }.Count(i => i != null && i.BackgroundColor == State.ColorBirth);


                                var ncolor = default(Color);

                                var IsDeath = u.BackgroundColor == State.ColorDeath;
                                var IsBirth = u.BackgroundColor == State.ColorBirth;

                                var Is2 = c == 2;
                                var Is3 = c == 3;
                                var Is2or3 = Is2 || Is3;

                                if (IsDeath && Is3)
                                {
                                    ncolor = State.ColorBirth;
                                }
                                else if (IsBirth && Is2or3)
                                {
                                    ncolor = State.ColorBirth;
                                }
                                else
                                {
                                    ncolor = State.ColorDeath;
                                }


                                nextframe += () =>
                                                 {
                                                     u.BackgroundColor = ncolor;
                                                     u.Update();
                                                 };
                            }
                        );

                    if (nextframe != null)
                    {
                        nextframe();
                        nextframe = null;
                    }

                    int timeout = (int)(IDate.Now.getTime() - t) - 0;

                    System.Console.WriteLine("time: " + timeout);

                    SleepAndEvolve(timeout.Max(100));
                };

            SleepAndEvolve(1);


        }




        static GameOfLife()
        {
            typeof(GameOfLife).Spawn();
        }


    }

}
