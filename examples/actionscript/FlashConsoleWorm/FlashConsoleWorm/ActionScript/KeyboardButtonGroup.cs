using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace FlashConsoleWorm.ActionScript
{
    [Script]
    public class KeyboardKeyInfo
    {
        public uint Button;

        // fixme: nullable uint?
        public uint Location = 0xff;

        public bool FilterLocation(uint location)
        {
            return (this.Location == 0xff) || this.Location == location;
        }

        public static implicit operator KeyboardKeyInfo(uint button)
        {
            return new KeyboardKeyInfo { Button = button };
        }
    }

    [Script]
    public class KeyboardButtonGroupInfo
    {
        public KeyboardKeyInfo[] Buttons;

        public KeyboardButtonGroup Group;

    }

    [Script]
    public class KeyboardButtonGroup
    {
        public string Name;
        public bool Enabled;

        public KeyboardButtonGroup()
        {
            this.Enabled = true;
        }

        public KeyboardButtonGroupInfo this[KeyboardKeyInfo button]
        {
            get
            {
                return new KeyboardButtonGroupInfo
                {
                    Group = this,
                    Buttons = new[] { button }
                };
            }
        }
    }

}
