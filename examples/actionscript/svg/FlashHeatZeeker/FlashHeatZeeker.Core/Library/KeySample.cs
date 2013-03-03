using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FlashHeatZeeker.Core.Library
{
    public class KeySample
    {
        // 32 bits!
        public int value;

        public double angle;

        public bool BodyIsActive = true;

        // drag to position if stopped moving!
        public bool fixup;
        public double x;
        public double y;

        bool this[int i]
        {
            get
            {
                if (i == 0)
                    return false;

                var mask = (1 << i);
                return (value & mask) == mask;
            }
            set
            {
                if (i == 0)
                    return;


                var mask = (1 << i);

                if (value)
                    this.value = this.value | mask;
                else
                    this.value = this.value & ~mask;
            }
        }

        public bool this[Keys k]
        {
            get
            {
                return this[KeysToOffset(k)];

            }
            set
            {
                this[KeysToOffset(k)] = value;
            }
        }

        public int KeysToOffset(Keys k)
        {

            if (k == Keys.Enter)
                return 15;
            if (k == Keys.Space)
                return 14;
            if (k == Keys.ControlKey)
                return 13;
            if (k == Keys.Alt)
                return 12;
            if (k == Keys.Left)
                return 11;
            if (k == Keys.Right)
                return 10;
            if (k == Keys.Up)
                return 9;
            if (k == Keys.Down)
                return 8;
            if (k == Keys.N)
                return 7;

            return 0;
        }
    }
}
