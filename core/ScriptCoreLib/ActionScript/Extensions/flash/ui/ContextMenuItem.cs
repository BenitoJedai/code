using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.ui;
using ScriptCoreLib.ActionScript.flash.events;

namespace ScriptCoreLib.ActionScript.Extensions.flash.ui
{
    [Script(Implements = typeof(ContextMenuItem))]
    public static class __ContextMenuItem
    {

        #region menuItemSelect
        public static void add_menuItemSelect(ContextMenuItem that, Action<ContextMenuEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, ContextMenuEvent.MENU_ITEM_SELECT);
        }

        public static void remove_menuItemSelect(ContextMenuItem that, Action<ContextMenuEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, ContextMenuEvent.MENU_ITEM_SELECT);
        }
        #endregion


    }
}
