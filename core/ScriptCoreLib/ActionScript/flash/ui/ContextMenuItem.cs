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
        #region Properties
        /// <summary>
        /// Specifies the menu item caption (text) displayed in the context menu.
        /// </summary>
        public string caption { get; set; }

        /// <summary>
        /// Indicates whether a separator bar should appear above the specified menu item.
        /// </summary>
        public bool separatorBefore { get; set; }

        /// <summary>
        /// Indicates whether the specified menu item is visible when the Flash Player context menu is displayed.
        /// </summary>
        public bool visible { get; set; }

        #endregion

        #region Events
        /// <summary>
        /// Dispatched when a user selects an item from a context menu.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<ContextMenuEvent> menuItemSelect;

        #endregion


        #region Methods
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new ContextMenuItem object that can be added to the ContextMenu.customItems array.
        /// </summary>
        public ContextMenuItem(string caption, bool separatorBefore, bool enabled, bool visible)
        {
        }

        /// <summary>
        /// Creates a new ContextMenuItem object that can be added to the ContextMenu.customItems array.
        /// </summary>
        public ContextMenuItem(string caption, bool separatorBefore, bool enabled)
        {
        }

        /// <summary>
        /// Creates a new ContextMenuItem object that can be added to the ContextMenu.customItems array.
        /// </summary>
        public ContextMenuItem(string caption, bool separatorBefore)
        {
        }

        /// <summary>
        /// Creates a new ContextMenuItem object that can be added to the ContextMenu.customItems array.
        /// </summary>
        public ContextMenuItem(string caption)
        {
        }

        #endregion

    }
}
