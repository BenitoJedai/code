using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Controls;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript.DOM;

namespace Toolbar.JavaScript
{

    [Script]
    public class ToolbarButtonGroup
    {
        ToolbarButton[] _Buttons;

        public ToolbarButton[] Buttons
        {
            get
            {
                return _Buttons;
            }
            set
            {
                if (_Buttons != null)
                    foreach (var Button in _Buttons)
                        Button.Clicked -= new Action<ToolbarButton>(Button_Clicked);

                _Buttons = value;

                if (_Buttons != null)
                    foreach (var Button in _Buttons)
                        Button.Clicked += new Action<ToolbarButton>(Button_Clicked);

            }
        }

        public event Action<ToolbarButton> Clicked;

        void Button_Clicked(ToolbarButton obj)
        {
            foreach (var i in from j in this.Buttons
                              where j != obj
                              where j.IsActivated
                              select j)
                i.SilentClick();

            Clicked(obj);
        }

        public bool IsActivated
        {
            get
            {
                return Buttons.Any(i => i.IsActivated);
            }
        }




    }


}
