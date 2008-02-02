using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.events;

namespace ScriptCoreLib.ActionScript.flash.ui
{
    // http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/flash/ui/ContextMenu.html#includeExamplesSummary
    [Script(IsNative = true)]
    public class ContextMenu : EventDispatcher
    {
        /// <summary>
        /// An array of ContextMenuItem objects.
        /// </summary>
        public Array customItems { get; set; }



        /// <summary>
        /// Hides all built-in menu items (except Settings) in the specified ContextMenu object.
        /// </summary>
        public void hideBuiltInItems()
        {
        }

    }
}
