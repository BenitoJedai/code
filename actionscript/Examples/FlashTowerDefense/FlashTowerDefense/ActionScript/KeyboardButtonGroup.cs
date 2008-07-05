using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace FlashTowerDefense.ActionScript
{
    [Script]
    public class KeyboardButtonGroupInfo
    {
        public uint[] Buttons;

        public KeyboardButtonGroup Group;

    }

    [Script]
    public class KeyboardButtonGroup
    {

        public bool Enabled { get; set; }

        public KeyboardButtonGroup()
        {
            this.Enabled = true;
        }

        public KeyboardButtonGroupInfo this[uint button]
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
