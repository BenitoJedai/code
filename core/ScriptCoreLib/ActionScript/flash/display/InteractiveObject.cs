using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.ui;
using ScriptCoreLib.ActionScript.flash.events;

namespace ScriptCoreLib.ActionScript.flash.display
{
    // http://livedocs.adobe.com/flex/2/langref/flash/display/InteractiveObject.html
    [Script(IsNative = true)]
    public class InteractiveObject : DisplayObject
    {
        [method: Script(NotImplementedHere = true)]
        public event Action<MouseEvent> click;

        [method: Script(NotImplementedHere = true)]
        public event Action<MouseEvent> mouseWheel;


        /// <summary>
        /// Specifies the context menu associated with this object. 
        /// </summary>
        public ContextMenu contextMenu { get; set; }
    }
}
