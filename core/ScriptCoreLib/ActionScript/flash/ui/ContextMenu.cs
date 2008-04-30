using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.events;
using ScriptCoreLib.ActionScript.flash.display;

namespace ScriptCoreLib.ActionScript.flash.ui
{
    // http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/flash/ui/ContextMenu.html
    [Script(IsNative = true)]
    public class ContextMenu : EventDispatcher
    {
        #region Properties
        /// <summary>
        /// An object that has the following properties of the ContextMenuBuiltInItems class: forwardAndBack, loop, play, print, quality, rewind, save, and zoom.
        /// </summary>
        public ContextMenuBuiltInItems builtInItems { get; set; }

        /// <summary>
        /// An array of ContextMenuItem objects.
        /// </summary>
        public ContextMenuItem[] customItems { get; set; }

        #endregion


        #region Methods
        /// <summary>
        /// Pops up this menu at the specified location.
        /// </summary>
        public void display(Stage stage, double stageX, double stageY)
        {
        }

        /// <summary>
        /// Hides all built-in menu items (except Settings) in the specified ContextMenu object.
        /// </summary>
        public void hideBuiltInItems()
        {
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Creates a ContextMenu object.
        /// </summary>
        public ContextMenu()
        {
        }

        #endregion


        #region Events
        /// <summary>
        /// Dispatched when a user first generates a context menu but before the contents of the context menu are displayed.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<ContextMenuEvent> menuSelect;

        #endregion

    


    }
}
