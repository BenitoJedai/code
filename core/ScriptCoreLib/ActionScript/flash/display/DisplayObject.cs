using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.filters;
using ScriptCoreLib.ActionScript.flash.events;
using System.Runtime.CompilerServices;

namespace ScriptCoreLib.ActionScript.flash.display
{
    // http://livedocs.adobe.com/flex/201/langref/flash/display/DisplayObject.html
    [Script(IsNative = true)]
    public class DisplayObject : EventDispatcher
    {
        // todo: implement

        [method: Script(NotImplementedHere = true)]
        public event Action<Event> added;

        /*
        public event Action<Event> addedToStage;
        public event Action<Event> removed;
        public event Action<Event> removedFromStage;
        public event Action<Event> render;
        */

        /// <summary>
        /// An indexed array that contains each filter object currently associated with the display object.
        /// </summary>
        public BitmapFilter[] filters { get; set; }

        /// <summary>
        /// Indicates the alpha transparency value of the object specified.
        /// </summary>
        public double alpha { get; set; }

        /// <summary>
        /// Indicates the rotation of the DisplayObject instance, in degrees, from its original orientation.
        /// </summary>
        public double rotation { get; set; }

        /// <summary>
        /// Indicates the x coordinate of the DisplayObject instance relative to the local coordinates of the parent DisplayObjectContainer.
        /// </summary>
        public double x { get; set; }

        /// <summary>
        /// Indicates the y coordinate of the DisplayObject instance relative to the local coordinates of the parent DisplayObjectContainer.
        /// </summary>
        public double y { get; set; }

        /// <summary>
        /// For a display object in a loaded SWF file, the root property is the top-most display object in the portion of the display list's tree structure represented by that SWF file.
        /// </summary>
        public DisplayObject root { get; private set; }

        /// <summary>
        /// Indicates the width of the display object, in pixels.
        /// </summary>
        public double width { get; set; }

        /// <summary>
        /// Indicates the height of the display object, in pixels.
        /// </summary>
        public double height { get; set; }

        /// <summary>
        /// The Stage of the display object.
        /// </summary>
        public Stage stage { get; private set; }


        /// <summary>
        /// Indicates the DisplayObjectContainer object that contains this display object.
        /// </summary>
        public DisplayObjectContainer parent { get; private set; }
    }
}
