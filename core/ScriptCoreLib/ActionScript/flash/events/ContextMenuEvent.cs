using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.display;

namespace ScriptCoreLib.ActionScript.flash.events
{
    // http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/flash/events/ContextMenuEvent.html
    [Script(IsNative=true)]
    public class ContextMenuEvent : Event
    {
        /// <summary>
        /// Defines the value of the type property of a menuItemSelect event object.
        /// </summary>
        public static readonly string MENU_ITEM_SELECT = "menuItemSelect";

        /// <summary>
        /// Defines the value of the type property of a menuSelect event object.
        /// </summary>
        public static readonly string MENU_SELECT = "menuSelect";


        #region Properties
        /// <summary>
        /// The display list object to which the menu is attached.
        /// </summary>
        public InteractiveObject contextMenuOwner { get; set; }

        /// <summary>
        /// The display list object on which the user right-clicked to display the context menu.
        /// </summary>
        public InteractiveObject mouseTarget { get; set; }

        #endregion

    }
}
