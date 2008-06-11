using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.ActionScript.com.google.maps.styles;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.com.google.maps.interfaces;

namespace ScriptCoreLib.ActionScript.com.google.maps.controls
{
    [Script(IsNative = true)]
    public class MapTypeControlOptions 
    {
        #region Properties
        /// <summary>
        /// A Number value that specifies button alignment (one of MapTypeControlOptions.ALIGN_HORIZONTALLY or MapTypeControlOptions.ALIGN_VERTICALLY).
        /// </summary>
        public Align buttonAlignment { get; set; }

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
        /// Constructs an MapTypeControlOptions object, optionally initializing it from an object.
        /// </summary>
        public MapTypeControlOptions(object param)
        {
        }

        /// <summary>
        /// Constructs an MapTypeControlOptions object, optionally initializing it from an object.
        /// </summary>
        public MapTypeControlOptions()
        {
        }

        #endregion


        public enum Align
        {
            /// <summary>
            /// [static] Align buttons horizontally
            /// </summary>
            Horizontally = 0,

            /// <summary>
            /// [static] Align buttons vertically
            /// </summary>
            Vertically = 1,

        }



    }
}
