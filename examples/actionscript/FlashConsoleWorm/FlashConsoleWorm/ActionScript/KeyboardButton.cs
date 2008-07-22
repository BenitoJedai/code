using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib;

namespace FlashConsoleWorm.ActionScript
{
    [Script]
    public class KeyboardButton
    {
        public KeyboardButtonGroupInfo[] Groups { get; set; }

        public Action Up;
        public Action Down;

        /// <summary>
        /// When the filter is set it must return true in order to fire keyboard events
        /// </summary>
        public Func<bool> Filter;

        public uint[] Buttons;


        bool CheckButton(uint button, uint location)
        {

            if (Groups == null)
            {
                if (this.Buttons == null)
                    return false;

                if (!this.Buttons.Contains(button))
                    return false;

                if (this.Filter != null && !this.Filter())
                    return false;

                return true;
            }
            else
            {
                var g = Groups.Where(i => i.Group.Enabled && i.Buttons.Any(p => p.Button == button && p.FilterLocation(location))).FirstOrDefault();

                if (g != null)
                {
                    if (this.Filter != null && !this.Filter())
                        return false;

                    //Console.WriteLine("keyCode " + button + " ok for " + g.Group.Name);
                    return true;
                }


                return false;
            }
        }

        public KeyboardButton(Stage s)
        {
            s.keyDown +=
                e =>
                {
                    if (this.Down == null)
                        return;

                    if (CheckButton(e.keyCode, e.keyLocation))
                    {
                        this.Down();
                    }
                };

            s.keyUp +=
                  e =>
                  {
                      if (this.Up == null)
                          return;

                      if (CheckButton(e.keyCode, e.keyLocation))
                      {
                          this.Up();
                      }
                  };
        }
    }

}
