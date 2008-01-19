using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.display
{
    // http://livedocs.adobe.com/flex/201/langref/flash/display/Stage.html#propertySummary
    [Script(IsNative = true)]
    public class Stage : DisplayObjectContainer
    {
        /// <summary>
        /// The current height, in pixels, of the Stage.
        /// </summary>
        public int stageHeight { get; set;}


        /// <summary>
        /// Specifies the current width, in pixels, of the Stage.
        /// </summary>
        public int stageWidth { get; set;}


        /// <summary>
        /// A value from the StageDisplayState class that specifies which display state to use.
        /// </summary>
        public string displayState { get; set; }

    }
}
