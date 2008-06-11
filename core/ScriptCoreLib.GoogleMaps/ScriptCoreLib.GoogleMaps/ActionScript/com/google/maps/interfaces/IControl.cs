using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.ActionScript.com.google.maps.controls;

namespace ScriptCoreLib.ActionScript.com.google.maps.interfaces
{
    [Script(IsNative = true)]
    public interface IControl : IWrappable
    {
        #region Methods
        /// <summary>
        /// Retrieves the control position.
        /// </summary>
        ControlPosition getControlPosition();

        /// <summary>
        /// Retrieves the control's display object (typically, the control itself).
        /// </summary>
        DisplayObject getDisplayObject();

        /// <summary>
        /// Retrieves the control's size.
        /// </summary>
        Point getSize();

        /// <summary>
        /// Sets the instance of the map that this control operates on.
        /// </summary>
        void initControlWithMap(IMap map);

        /// <summary>
        /// Sets the control's position and updates its position on the map.
        /// </summary>
        void setControlPosition(ControlPosition position);

        #endregion

    }
}
