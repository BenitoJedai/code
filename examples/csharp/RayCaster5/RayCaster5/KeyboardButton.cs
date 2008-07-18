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
        public int[] Buttons;

        public bool IsPressed;

        public static implicit operator bool(KeyboardButton b)
        {
            return b.IsPressed;
        }

        public static implicit operator KeyboardButton(int[] b)
        {
            return new KeyboardButton { Buttons = b };
        }

        public bool ProcessKeyDown(int key)
        {
            if (Buttons.Contains(key))
            {
                this.IsPressed = true;
                return true;
            }

            return false;
        }

        public bool ProcessKeyUp(int key)
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
