using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.text;

namespace ScriptCoreLib.ActionScript.flash.display
{
    // http://livedocs.adobe.com/flex/201/langref/flash/display/DisplayObjectContainer.html
    [Script(IsNative = true)]
    public class DisplayObjectContainer : InteractiveObject
    {
        #region Properties
        /// <summary>
        /// Determines whether or not the children of the object are mouse enabled.
        /// </summary>
        public bool mouseChildren { get; set; }

        /// <summary>
        /// [read-only] Returns the number of children of this object.
        /// </summary>
        public int numChildren { get; private set; }

        /// <summary>
        /// Determines whether the children of the object are tab enabled.
        /// </summary>
        public bool tabChildren { get; set; }

        /// <summary>
        /// [read-only] Returns a TextSnapshot object for this DisplayObjectContainer instance.
        /// </summary>
        public TextSnapshot textSnapshot { get; private set; }

        #endregion



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
