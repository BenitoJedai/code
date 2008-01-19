using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.ui;

namespace ScriptCoreLib.ActionScript.flash.display
{
    // http://livedocs.adobe.com/flex/2/langref/flash/display/InteractiveObject.html
    [Script(IsNative = true)]
    public class InteractiveObject : DisplayObject
    {
        /// <summary>
        /// Specifies the context menu associated with this object. 
        /// </summary>
        public ContextMenu contextMenu { get; set; }
    }
}
