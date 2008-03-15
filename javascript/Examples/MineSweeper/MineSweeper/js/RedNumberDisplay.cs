using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.JavaScript.DOM;

namespace MineSweeper.js
{
    [Script, ScriptApplicationEntryPoint]
    public class RedNumberDisplay
    {
        public RedNumberDisplay()
            : this(10, 0123456789)
        {

        }

        public readonly IHTMLDiv Control = new IHTMLDiv();

        const int DigitX = 13;
        const int DigitY = 23;

        public int Digits { get; private set; }

        public int Width { get { return DigitX * Digits; } }
        public int Height { get { return DigitY; } }

        IHTMLDiv[] DigitControls;

        public RedNumberDisplay(int Digits, int value)
        {
            this.Digits = Digits;

            this.Control.style.SetSize(Width, Height);
            this.Control.style.backgroundColor = Color.Black;
            this.Control.style.position = IStyle.PositionEnum.relative;

            DigitControls = Enumerable.Range(0, Digits).Select(
                i =>
                {
                    var d = new IHTMLDiv();

                    d.style.SetLocation(i * DigitX, 0, DigitX, DigitY);
                    d.style.SetBackground(Assets.red_numbers[0]);

                    d.AttachTo(this.Control);

                    return d;
                }
                ).ToArray();


            this.Value = value;
        }

        int _Value;
        public int Value
        {
            get { return _Value; }
            set
            {
                _Value = value;

                for (int i = 0; i < Digits; i++)
                {
                    ChangeDigit(i, value.GetDigit(Digits - i - 1));
                }
            }
        }

        private void ChangeDigit(int DigitIndex, int DigitValue)
        {
            DigitControls[DigitIndex].style.SetBackground(Assets.red_numbers[DigitValue]);
        }

        static RedNumberDisplay()
        {
            typeof(RedNumberDisplay).SpawnTo(i => i.replaceWith(new RedNumberDisplay().Control));
        }
    }
}
