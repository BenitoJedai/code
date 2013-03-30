using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.Runtime;

namespace MineSweeper.js
{
    public class MineSweeperPanel
    {
        public readonly IHTMLDiv Control = new IHTMLDiv();

        readonly MineSweeperControl MineField;

        const int FaceSize = 26;



        public int ControlWidth;
        public int ControlHeight;
        public MineSweeperPanel(int ButtonsX = 15, int ButtonsY = 16, double Mines = 0.2, Assets MyAssets = null)
        {
            if (MyAssets == null)
                MyAssets = Assets.Default;

            Control.style.position = IStyle.PositionEnum.relative;
            Control.style.backgroundColor = Color.FromGray(192);

            MineField = new MineSweeperControl(ButtonsX, ButtonsY, Mines, MyAssets);

            ControlWidth = MineField.Width + 20;
            ControlHeight = MineField.Height + 50;
            Control.style.SetSize(ControlWidth, ControlHeight);

            MineField.Control.AttachTo(Control).style.SetLocation(10, 40);


            var face = new Button(FaceSize, FaceSize);

            face.Source = MyAssets.face_ok;
            face.MouseDownSource = MyAssets.face_ok_down;

            var timer = new RedNumberDisplay(3, 0, MyAssets);


            var actualtimer = new Timer(
                t =>
                {
                    timer.Value = t.Counter;
                }
            );

            MineField.Bang += () =>
            {
                face.Source = MyAssets.face_dead;
                actualtimer.Stop();
            };

            MineField.LookingForMines += () => face.Source = MyAssets.face_scared;
            MineField.DoneLookingForMines += () => face.Source = MyAssets.face_ok;



            face.Click +=
                () =>
                {
                    face.Source = MyAssets.face_ok;

                    actualtimer.Stop();
                    timer.Value = 0;

                    MineField.Reset();
                };

            MineField.AllMinesFound +=
                delegate
                {


                    face.Source = MyAssets.face_cool;
                    actualtimer.Stop();

                    MineField.Alive = false;
                    MineField.DisableButtons();
                };

            face.Control.AttachTo(Control);
            face.Control.style.SetLocation(10 + (MineField.Width - FaceSize) / 2, 6);


            var minecounter = new RedNumberDisplay(3, MineField.MinesTotal, MyAssets);

            minecounter.Control.AttachTo(Control);
            minecounter.Control.style.SetLocation(10, 6);

            timer.Control.AttachTo(Control);
            timer.Control.style.SetLocation(10 + MineField.Width - timer.Width, 6);

            MineField.MinesFoundChanged += () => minecounter.Value = MineField.MinesTotal - MineField.MinesFound;

            MineField.Control.style.border = "1px inset gray";
            minecounter.Control.style.border = "1px inset gray";
            timer.Control.style.border = "1px inset gray";
            this.Control.style.border = "1px outset gray";


            MineField.DoneLookingForMines += () =>
                {
                    if (!actualtimer.IsAlive)
                        actualtimer.StartInterval(1000);
                };

        }


    }
}
