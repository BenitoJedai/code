using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.com.google.maps.styles
{
    [Script(IsNative = true)]
    public class ButtonStyle
    {
        #region Properties
        /// <summary>
        /// [read-only] Plug for "allStates" property on the ButtonStyle.
        /// </summary>
        public ButtonFaceStyle allStates { get; private set; }

        /// <summary>
        /// Style of the button in the "down" state.
        /// </summary>
        public ButtonFaceStyle downState { get; set; }

        /// <summary>
        /// Style of the button in the "over" state.
        /// </summary>
        public ButtonFaceStyle overState { get; set; }

        /// <summary>
        /// Style of the button in the "up" state.
        /// </summary>
        public ButtonFaceStyle upState { get; set; }

        #endregion

        #region Methods
        /// <summary>
        /// [static] Merges two or more ButtonStyle objects.
        /// </summary>
        public static ButtonStyle mergeStyles(Array styles)
        {
            return default(ButtonStyle);
        }

        /// <summary>
        /// Specified a set of bevel style properties for all button states.
        /// </summary>
        public void setAllStates(ButtonFaceStyle shared)
        {
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Constructs a ButtonStyle instance, optionally initializing it from an object.
        /// </summary>
        public ButtonStyle(object @params)
        {
        }

        /// <summary>
        /// Constructs a ButtonStyle instance, optionally initializing it from an object.
        /// </summary>
        public ButtonStyle()
        {
        }

        #endregion

    }
}
