using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.ui;
using ScriptCoreLib.ActionScript.flash.events;

namespace ScriptCoreLib.ActionScript.Extensions.flash.ui
{
    [Script(Implements = typeof(ContextMenu))]
    public static class __ContextMenu
    {

        #region Implementation for methods marked with [Script(NotImplementedHere = true)]
        #region menuSelect
        public static void add_menuSelect(ContextMenu that, Action<ContextMenuEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, ContextMenuEvent.MENU_SELECT);
        }

        public static void remove_menuSelect(ContextMenu that, Action<ContextMenuEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, ContextMenuEvent.MENU_SELECT);
        }
        #endregion

        #endregion

    }
}
