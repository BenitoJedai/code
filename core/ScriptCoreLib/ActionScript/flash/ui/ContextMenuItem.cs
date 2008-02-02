using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.events;

namespace ScriptCoreLib.ActionScript.flash.ui
{
    // http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/flash/ui/ContextMenuItem.html
    [Script(IsNative = true)]
    public class ContextMenuItem : EventDispatcher
    {
        [method: Script(NotImplementedHere = true)]
        public event Action<ContextMenuEvent> menuItemSelect;

        public ContextMenuItem(string caption)
        {

        }
    }
}
