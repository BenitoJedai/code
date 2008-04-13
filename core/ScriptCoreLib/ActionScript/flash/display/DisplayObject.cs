using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.filters;
using ScriptCoreLib.ActionScript.flash.events;
using System.Runtime.CompilerServices;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.ActionScript.flash.accessibility;

namespace ScriptCoreLib.ActionScript.flash.display
{
    // http://livedocs.adobe.com/flex/3/langref/flash/display/DisplayObject.html
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


        #region Properties
        /// <summary>
        /// The current accessibility options for this display object.
        /// </summary>
        public AccessibilityProperties accessibilityProperties { get; set; }

        /// <summary>
        /// Indicates the alpha transparency value of the object specified.
        /// </summary>
        public double alpha { get; set; }

        /// <summary>
        /// A value from the BlendMode class that specifies which blend mode to use.
        /// </summary>
        public string blendMode { get; set; }

        /// <summary>
        /// If set to true, Flash Player or Adobe AIR caches an internal bitmap representation of the display object.
        /// </summary>
        public bool cacheAsBitmap { get; set; }

        /// <summary>
        /// An indexed array that contains each filter object currently associated with the display object.
        /// </summary>
        public BitmapFilter[] filters { get; set; }

        /// <summary>
        /// Indicates the height of the display object, in pixels.
        /// </summary>
        public double height { get; set; }

        /// <summary>
        /// [read-only] Returns a LoaderInfo object containing information about loading the file to which this display object belongs.
        /// </summary>
        public LoaderInfo loaderInfo { get; private set; }

        /// <summary>
        /// The calling display object is masked by the specified mask object.
        /// </summary>
        public DisplayObject mask { get; set; }

        /// <summary>
        /// [read-only] Indicates the x coordinate of the mouse position, in pixels.
        /// </summary>
        public double mouseX { get; private set; }

        /// <summary>
        /// [read-only] Indicates the y coordinate of the mouse position, in pixels.
        /// </summary>
        public double mouseY { get; private set; }

        /// <summary>
        /// Indicates the instance name of the DisplayObject.
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Specifies whether the display object is opaque with a certain background color.
        /// </summary>
        public object opaqueBackground { get; set; }

        /// <summary>
        /// [read-only] Indicates the DisplayObjectContainer object that contains this display object.
        /// </summary>
        public DisplayObjectContainer parent { get; private set; }

        /// <summary>
        /// [read-only] For a display object in a loaded SWF file, the root property is the top-most display object in the portion of the display list's tree structure represented by that SWF file.
        /// </summary>
        public DisplayObject root { get; private set; }

        /// <summary>
        /// Indicates the rotation of the DisplayObject instance, in degrees, from its original orientation.
        /// </summary>
        public double rotation { get; set; }

        /// <summary>
        /// The current scaling grid that is in effect.
        /// </summary>
        public Rectangle scale9Grid { get; set; }

        /// <summary>
        /// Indicates the horizontal scale (percentage) of the object as applied from the registration point.
        /// </summary>
        public double scaleX { get; set; }

        /// <summary>
        /// Indicates the vertical scale (percentage) of an object as applied from the registration point of the object.
        /// </summary>
        public double scaleY { get; set; }

        /// <summary>
        /// The scroll rectangle bounds of the display object.
        /// </summary>
        public Rectangle scrollRect { get; set; }

        /// <summary>
        /// [read-only] The Stage of the display object.
        /// </summary>
        public Stage stage { get; private set; }

        /// <summary>
        /// An object with properties pertaining to a display object's matrix, color transform, and pixel bounds.
        /// </summary>
        public Transform transform { get; set; }

        /// <summary>
        /// Whether or not the display object is visible.
        /// </summary>
        public bool visible { get; set; }

        /// <summary>
        /// Indicates the width of the display object, in pixels.
        /// </summary>
        public double width { get; set; }

        /// <summary>
        /// Indicates the x coordinate of the DisplayObject instance relative to the local coordinates of the parent DisplayObjectContainer.
        /// </summary>
        public double x { get; set; }

        /// <summary>
        /// Indicates the y coordinate of the DisplayObject instance relative to the local coordinates of the parent DisplayObjectContainer.
        /// </summary>
        public double y { get; set; }

        #endregion

    }
}
