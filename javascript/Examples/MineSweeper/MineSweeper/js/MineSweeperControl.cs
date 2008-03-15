using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM;

using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Lambda;
using System;
using System.Linq;
using System.Collections.Generic;
using ScriptCoreLib.Shared;


namespace MineSweeper.js
{
    [Script, ScriptApplicationEntryPoint]
    public class MineSweeperControl
    {

        public const int ButtonSize = 16;

        public readonly IHTMLDiv Control = new IHTMLDiv();

        public MineSweeperControl() : this(24, 16, 0.2)
        {
            
        }

        public int ButtonsX { get; private set; }
        public int ButtonsY { get; private set; }

        public int Width { get { return ButtonsX * ButtonSize; } }
        public int Height { get { return ButtonsY * ButtonSize; } }

        public event Action LookingForMines;
        public event Action Bang;
        public event Action DoneLookingForMines;

        public double Mines { get; private set; }
        public int MinesTotal { get { return (Buttons.Count * Mines).ToInt32(); } }

        public bool Alive { get; private set; }

        public event Action MinesFoundChanged;

        public int MinesFound
        {
            get
            {
                return Buttons.Count(i => i.Source == Assets.flag);
            }
        }

        public MineSweeperControl(int ButtonsX, int ButtonsY, double Mines)
        {
            this.Alive = true;
            this.Mines = Mines;
            this.ButtonsX = ButtonsX;
            this.ButtonsY = ButtonsY;

            Control.style.backgroundColor = Color.Gray;
            Control.style.SetSize(Width, Height);
            Control.style.position = IStyle.PositionEnum.relative;

            for (int x = 0; x < ButtonsX; x++)
                for (int y = 0; y < ButtonsY; y++)
                {
                    var btn = new MineButton().AddTo(Buttons);

                    btn.MouseDownChanged +=
                        v =>
                        {
                            if (!Alive)
                                return;

                            if (v)
                            {
                                if (LookingForMines != null)
                                    LookingForMines();
                            }
                            else
                            {
                                if (DoneLookingForMines != null)
                                    DoneLookingForMines();
                            }
                                
                                    
                        };

                    btn.Source = Assets.button;
                    btn.MouseDownSource = Assets.empty;

                    btn.Control.AttachTo(Control);
                    btn.MoveTo(x, y);

                    var NearbyButtons =
                               from i in Buttons
                               where i.IsNearTo(btn)
                               select i;

                    var NearbyMines =
                                from i in NearbyButtons
                                where i.IsMined
                                select i;

                    var NearbyFlags =
                               from i in NearbyButtons
                               where i.Source == Assets.flag
                               select i;

                    var NearbyNonFlags =
                               from i in NearbyButtons
                               where i.Source != Assets.flag
                               select i;

                    Action Resolve =
                        delegate
                        {
                            foreach (var v in NearbyNonFlags)
                            {
                                v.RaiseClick();
                            }
                        };


                    btn.ContextClick +=
                        delegate
                        {
          

                            if (Assets.numbers.Contains(btn.Source))
                            {
                                if (NearbyMines.Count() == NearbyFlags.Count())
                                    Resolve();

                                return;
                            }

                            if (btn.Source == Assets.button)
                                btn.Source = Assets.flag;
                            else
                                if (btn.Source == Assets.flag)
                                    btn.Source = Assets.question;
                                else
                                    if (btn.Source == Assets.question)
                                        btn.Source = Assets.button;

                            if (MinesFoundChanged != null)
                                MinesFoundChanged();
                        };

                    btn.Click +=
                        delegate
                        {
                            btn.Enabled = false;

                            if (btn.IsMined)
                            {
                                // end of game

                                foreach (var v in from i in Buttons
                                                  where !i.IsMined
                                                  where i.Source == Assets.flag
                                                  select i)
                                {
                                    v.Source = Assets.notmine;
                                }

                                foreach (var v in from i in Buttons
                                                  where i.IsMined
                                                  where i != btn
                                                  select i)
                                {
                                    v.Source = Assets.mine;
                                }

                                btn.Source = Assets.mine_found;

                                foreach (var v in Buttons)
                                {
                                    v.Enabled = false;
                                    v.ContextEnabled = false;
                                }

                                Alive = false;

                                if (Bang != null)
                                    Bang();
                            }
                            else
                            {
                                // how many mines are near me?

                                var MineCount = NearbyMines.Count();

                                btn.Source = Assets.numbers[MineCount];

                                if (MineCount == 0)
                                    Resolve();
                            }
                        };
                }

            AttachMinesToButtons();
        }

        private void AttachMinesToButtons()
        {
            Buttons.Random(MinesTotal).ForEach(i => i.IsMined = true);
        }


        readonly List<MineButton> Buttons = new List<MineButton>();


        [Script]
        class MineButton : Button
        {
            public bool IsMined;

            public MineButton() : base(ButtonSize, ButtonSize)
            {

            }

            public bool IsNearTo(MineButton btn)
            {
                if (btn.X - 1 == X && btn.Y - 1 == Y) return true;
                if (btn.X + 0 == X && btn.Y - 1 == Y) return true;
                if (btn.X + 1 == X && btn.Y - 1 == Y) return true;
                if (btn.X + 1 == X && btn.Y + 0 == Y) return true;

                if (btn.X + 1 == X && btn.Y + 1 == Y) return true;
                if (btn.X - 0 == X && btn.Y + 1 == Y) return true;
                if (btn.X - 1 == X && btn.Y + 1 == Y) return true;
                if (btn.X - 1 == X && btn.Y - 0 == Y) return true;

                return false;
            }

            public int X { get; private set; }
            public int Y { get; private set; }


            public void MoveTo(int x, int y)
            {
                this.X = x;
                this.Y = y;

                this.Control.style.SetLocation(x * ButtonSize, y * ButtonSize);
            }
        }


        static MineSweeperControl()
        {
            typeof(MineSweeperControl).SpawnTo(i => i.replaceWith(new MineSweeperControl().Control));
        }


        public void Reset()
        {
            Alive = true;

            foreach (var v in Buttons)
            {
                v.IsMined = false;
                v.Enabled = true;
                v.ContextEnabled = true;
                v.Source = Assets.button;
            }

            AttachMinesToButtons();
        }
    }

}
