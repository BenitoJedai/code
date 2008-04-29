using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.flash.geom;

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


        #region Methods
        /// <summary>
        /// Adds a child DisplayObject instance to this DisplayObjectContainer instance.
        /// </summary>
        public DisplayObject addChild(DisplayObject child)
        {
            return default(DisplayObject);
        }

        /// <summary>
        /// Adds a child DisplayObject instance to this DisplayObjectContainer instance.
        /// </summary>
        public DisplayObject addChildAt(DisplayObject child, int index)
        {
            return default(DisplayObject);
        }

        /// <summary>
        /// Indicates whether the security restrictions would cause any display objects to be omitted from the list returned by calling the DisplayObjectContainer.getObjectsUnderPoint() method with the specified point point.
        /// </summary>
        public bool areInaccessibleObjectsUnderPoint(Point point)
        {
            return default(bool);
        }

        /// <summary>
        /// Determines whether the specified display object is a child of the DisplayObjectContainer instance or the instance itself.
        /// </summary>
        public bool contains(DisplayObject child)
        {
            return default(bool);
        }

        /// <summary>
        /// Returns the child display object instance that exists at the specified index.
        /// </summary>
        public DisplayObject getChildAt(int index)
        {
            return default(DisplayObject);
        }

        /// <summary>
        /// Returns the child display object that exists with the specified name.
        /// </summary>
        public DisplayObject getChildByName(string name)
        {
            return default(DisplayObject);
        }

        /// <summary>
        /// Returns the index position of a child DisplayObject instance.
        /// </summary>
        public int getChildIndex(DisplayObject child)
        {
            return default(int);
        }

        /// <summary>
        /// Returns an array of objects that lie under the specified point and are children (or grandchildren, and so on) of this DisplayObjectContainer instance.
        /// </summary>
        public Array getObjectsUnderPoint(Point point)
        {
            return default(Array);
        }

        /// <summary>
        /// Removes the specified child DisplayObject instance from the child list of the DisplayObjectContainer instance.
        /// </summary>
        public DisplayObject removeChild(DisplayObject child)
        {
            return default(DisplayObject);
        }

        /// <summary>
        /// Removes a child DisplayObject from the specified index position in the child list of the DisplayObjectContainer.
        /// </summary>
        public DisplayObject removeChildAt(int index)
        {
            return default(DisplayObject);
        }

        /// <summary>
        /// Changes the position of an existing child in the display object container.
        /// </summary>
        public void setChildIndex(DisplayObject child, int index)
        {
        }

        /// <summary>
        /// Swaps the z-order (front-to-back order) of the two specified child objects.
        /// </summary>
        public void swapChildren(DisplayObject child1, DisplayObject child2)
        {
        }

        /// <summary>
        /// Swaps the z-order (front-to-back order) of the child objects at the two specified index positions in the child list.
        /// </summary>
        public void swapChildrenAt(int index1, int index2)
        {
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Calling the new DisplayObjectContainer() constructor throws an ArgumentError exception.
        /// </summary>
        public DisplayObjectContainer()
        {
        }

        #endregion


    }
}
