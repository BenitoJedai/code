using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.ActionScript.com.google.maps.styles;

namespace ScriptCoreLib.ActionScript.com.google.maps.controls
{
    [Script(IsNative = true)]
    public class PositionControlOptions
    {
        #region Properties
        /// <summary>
        /// Button size.
        /// </summary>
        public Point buttonSize { get; set; }

        /// <summary>
        /// Button spacing.
        /// </summary>
        public Point buttonSpacing { get; set; }

        /// <summary>
        /// Button style.
        /// </summary>
        public ButtonStyle buttonStyle { get; set; }

        #endregion

        #region Methods
        #endregion

        #region Constructors
        /// <summary>
        /// Constructs an PositionControlOptions object, optionally initializing it from an object.
        /// </summary>
        public PositionControlOptions(object param)
        {
        }

        /// <summary>
        /// Constructs an PositionControlOptions object, optionally initializing it from an object.
        /// </summary>
        public PositionControlOptions()
        {
        }

        #endregion

    }
}
