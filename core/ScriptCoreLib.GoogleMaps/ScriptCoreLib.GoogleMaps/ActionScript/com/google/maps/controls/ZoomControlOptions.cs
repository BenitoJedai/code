using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.ActionScript.com.google.maps.styles;

namespace ScriptCoreLib.ActionScript.com.google.maps.controls
{
    [Script(IsNative = true)]
    public class ZoomControlOptions
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

        /// <summary>
        /// A Boolean value that specifies whether we have a scroll track.
        /// </summary>
        public object hasScrollTrack { get; set; }

        #endregion

        #region Methods
        #endregion

        #region Constructors
        /// <summary>
        /// Constructs an ZoomControlOptions object, optionally initializing it from an object.
        /// </summary>
        public ZoomControlOptions(object param)
        {
        }

        /// <summary>
        /// Constructs an ZoomControlOptions object, optionally initializing it from an object.
        /// </summary>
        public ZoomControlOptions()
        {
        }

        #endregion


    }
}
