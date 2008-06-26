using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;

namespace FlashTowerDefense.ActionScript
{
    [Script]
    public class KeyboardButton
    {
        public Action Up;
        public Action Down;

        /// <summary>
        /// When the filter is set it must return true in order to fire keyboard events
        /// </summary>
        public Func<bool> Filter;

        public uint[] Buttons;



        public KeyboardButton(Stage s)
        {
            s.keyDown +=
                e =>
                {
                    if (this.Down == null)
                        return;

                    if (this.Buttons == null)
                        return;

                    if (this.Buttons.Contains(e.keyCode))
                        if (this.Filter == null || this.Filter())
                        {
                            this.Down();
                        }
                };

            s.keyUp +=
                  e =>
                  {
                      if (this.Up == null)
                          return;

                      if (this.Buttons == null)
                          return;

                      if (this.Buttons.Contains(e.keyCode))
                          if (this.Filter == null || this.Filter())
                          {
                              this.Up();
                          }
                  };
        }
    }
}
