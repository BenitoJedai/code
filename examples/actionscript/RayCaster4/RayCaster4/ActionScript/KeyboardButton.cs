using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace RayCaster4.ActionScript
{
    [Script]
    public class KeyboardButton
    {
        public uint[] Buttons;

        public bool IsPressed;

        public static implicit operator bool(KeyboardButton b)
        {
            return b.IsPressed;
        }

        public static implicit operator KeyboardButton(uint[] b)
        {
            return new KeyboardButton { Buttons = b };
        }

        public bool ProcessKeyDown(uint key)
        {
            if (Buttons.Contains(key))
            {
                this.IsPressed = true;
                return true;
            }

            return false;
        }

        public bool ProcessKeyUp(uint key)
        {
            if (Buttons.Contains(key))
            {
                this.IsPressed = false;
                return true;
            }

            return false;
        }
    }
}
