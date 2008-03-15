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
    [Script, ScriptApplicationEntryPoint]
    class MineSweeperPanel
    {
        readonly IHTMLDiv Control = new IHTMLDiv();

        readonly MineSweeperControl MineField;

        const int FaceSize = 26;

        public MineSweeperPanel()
        {
            Control.style.position = IStyle.PositionEnum.relative;
            Control.style.backgroundColor = Color.FromGray(192);

            MineField = new MineSweeperControl(16, 16, 0.2);

            Control.style.SetSize(MineField.Width + 20, MineField.Height + 50);

            MineField.Control.AttachTo(Control).style.SetLocation(10, 40);


            var face = new Button(FaceSize, FaceSize);

            face.Source = Assets.face_ok;
            face.MouseDownSource = Assets.face_ok_down;

            var timer = new RedNumberDisplay(3, 0);


            var actualtimer = new Timer(
                t =>
                {
                    timer.Value = t.Counter;
                }
            );

            MineField.Bang += () =>
            {
                face.Source = Assets.face_dead;
                actualtimer.Stop();
            };

            MineField.LookingForMines += () => face.Source = Assets.face_scared;
            MineField.DoneLookingForMines += () => face.Source = Assets.face_ok;

            face.Click +=
                () =>
                {
                    face.Source = Assets.face_ok;
                    timer.Value = 0;

                    MineField.Reset();
                };

            face.Control.AttachTo(Control);
            face.Control.style.SetLocation(10 + (MineField.Width - FaceSize) / 2, 6);


            var minecounter = new RedNumberDisplay(3, MineField.MinesTotal);

            minecounter.Control.AttachTo(Control);
            minecounter.Control.style.SetLocation(10, 6);

            timer.Control.AttachTo(Control);
            timer.Control.style.SetLocation(10 + MineField.Width - timer.Width, 6);

            MineField.MinesFoundChanged += () => minecounter.Value = MineField.MinesTotal - MineField.MinesFound;



            MineField.DoneLookingForMines += () =>
                {
                    if (!actualtimer.IsAlive)
                        actualtimer.StartInterval(1000);
                };

        }

        static MineSweeperPanel()
        {
            typeof(MineSweeperPanel).SpawnTo(i => i.replaceWith(new MineSweeperPanel().Control));
        }
    }
}
