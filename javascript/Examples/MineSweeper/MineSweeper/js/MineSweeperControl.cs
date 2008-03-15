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

        public MineSweeperControl() : this(24, 16, 0.2, Assets.Default)
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

        public bool Alive { get; set; }

        public event Action MinesFoundChanged;

        public int MinesFound
        {
            get
            {
                return Buttons.Count(i => i.Source == Assets.Default.flag);
            }
        }

        public event Action AllMinesFound;

        readonly Assets MyAssets;

        public MineSweeperControl(int ButtonsX, int ButtonsY, double Mines, Assets MyAssets)
        {
            this.MyAssets = MyAssets;
            this.Alive = true;
            this.Mines = Mines;
            
            if (this.Mines > 0.8)
                this.Mines = 0.8;

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

                    btn.Source = MyAssets.button;
                    btn.MouseDownSource = MyAssets.empty;

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
                               where i.Source == MyAssets.flag
                               select i;

                    var NearbyNonFlags =
                               from i in NearbyButtons
                               where i.Source != MyAssets.flag
                               select i;

                    var IdleButtons =
                        from i in Buttons
                        where i.Source != MyAssets.flag
                        where !MyAssets.numbers.Contains(i.Source)
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


                            if (MyAssets.numbers.Contains(btn.Source))
                            {
                                if (NearbyMines.Count() == NearbyFlags.Count())
                                {
                                    Resolve();

                                    CheckIdle(IdleButtons);
                                }

                                return;
                            }

                            if (btn.Source == MyAssets.button)
                                btn.Source = MyAssets.flag;
                            else
                                if (btn.Source == MyAssets.flag)
                                    btn.Source = MyAssets.question;
                                else
                                    if (btn.Source == MyAssets.question)
                                        btn.Source = MyAssets.button;

                            if (MinesFoundChanged != null)
                                MinesFoundChanged();

                            CheckIdle(IdleButtons);
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
                                                  where i.Source == MyAssets.flag
                                                  select i)
                                {
                                    v.Source = MyAssets.notmine;
                                }

                                foreach (var v in from i in Buttons
                                                  where i.IsMined
                                                  where i != btn
                                                  select i)
                                {
                                    v.Source = MyAssets.mine;
                                }

                                btn.Source = MyAssets.mine_found;

                                DisableButtons();

                                Alive = false;

                                if (Bang != null)
                                    Bang();
                            }
                            else
                            {
                                // how many mines are near me?

                                var MineCount = NearbyMines.Count();

                                btn.Source = MyAssets.numbers[MineCount];

                                if (MineCount == 0)
                                {
                                    Resolve();

                                    CheckIdle(IdleButtons);
                              
                                }

                            }
                        };
                }

            AttachMinesToButtons();
        }

        private void CheckIdle(IEnumerable<MineButton> IdleButtons)
        {
            Console.WriteLine("idle: " + IdleButtons.Count());

            if (!IdleButtons.Any())
                if (AllMinesFound != null)
                    AllMinesFound();
        }

        public void DisableButtons()
        {
            foreach (var v in Buttons)
            {
                v.Enabled = false;
                v.ContextEnabled = false;
            }
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
                v.Source = MyAssets.button;
            }

            AttachMinesToButtons();
        }
    }

}
