using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.display
{
    // http://livedocs.adobe.com/flex/201/langref/flash/display/DisplayObjectContainer.html
    [Script(IsNative = true)]
    public class DisplayObjectContainer : InteractiveObject
    {
        /// <summary>
        /// Determines whether or not the children of the object are mouse enabled.
        /// </summary>
        public bool mouseChildren { get; set; }
        


        /// <summary>
        /// Adds a child DisplayObject instance to this DisplayObjectContainer instance.
        /// </summary>
        /// <param name="child"></param>
        /// <returns></returns>
        public DisplayObject addChild(DisplayObject child)
        {
            return default(DisplayObject);
        }


        /// <summary>
        /// Removes the specified child DisplayObject instance from the child list of the DisplayObjectContainer instance.
        /// </summary>
        /// <param name="child"></param>
        /// <returns></returns>
        public DisplayObject removeChild(DisplayObject child)
        {
            return default(DisplayObject);
        }

    }
}
