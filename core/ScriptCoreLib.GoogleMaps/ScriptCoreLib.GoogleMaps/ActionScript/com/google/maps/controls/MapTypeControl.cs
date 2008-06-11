using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.ActionScript.com.google.maps.interfaces;

namespace ScriptCoreLib.ActionScript.com.google.maps.controls
{
    [Script(IsNative = true)]
    public class MapTypeControl : Sprite, IControl
    {
        #region Methods
        /// <summary>
        /// Retrieves the control position.
        /// </summary>
        public ControlPosition getControlPosition()
        {
            return default(ControlPosition);
        }

        /// <summary>
        /// Retrieves the control's display object (typically, the control itself).
        /// </summary>
        public DisplayObject getDisplayObject()
        {
            return default(DisplayObject);
        }

        /// <summary>
        /// Retrieves the control's size.
        /// </summary>
        public Point getSize()
        {
            return default(Point);
        }

        /// <summary>
        /// Sets the instance of the map that this control operates on.
        /// </summary>
        public void initControlWithMap(IMap map)
        {
        }

        /// <summary>
        /// Sets the control's position and updates its position on the map.
        /// </summary>
        public void setControlPosition(ControlPosition position)
        {
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Constructs a MapTypeControl object.
        /// </summary>
        public MapTypeControl(MapTypeControlOptions options)
        {
        }

        /// <summary>
        /// Constructs a MapTypeControl object.
        /// </summary>
        public MapTypeControl()
        {
        }

        #endregion

    }
}
