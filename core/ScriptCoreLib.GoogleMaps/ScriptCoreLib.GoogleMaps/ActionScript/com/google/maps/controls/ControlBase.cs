using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.com.google.maps.interfaces;
using ScriptCoreLib.ActionScript.flash.geom;

namespace ScriptCoreLib.ActionScript.com.google.maps.controls
{
    [Script(IsNative = true)]
    public abstract class ControlBase : Sprite, IControl
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
        /// Sets the instance of the map that the control operates on.
        /// </summary>
        public void initControlWithMap(IMap map)
        {
        }

        /// <summary>
        /// Changes control's position.
        /// </summary>
        public void setControlPosition(ControlPosition controlPosition)
        {
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Construct a control located relative to a specfied corner of the map.
        /// </summary>
        public ControlBase(ControlPosition position)
        {
        }

        #endregion



    }
}
