using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.com.google.maps
{
    [Script(IsNative = true)]
    public class MapTypeOptions
    {
        #region Properties
        /// <summary>
        /// Alternative text.
        /// </summary>
        public string alt { get; set; }

        /// <summary>
        /// Error message.
        /// </summary>
        public string errorMessage { get; set; }

        /// <summary>
        /// A Number value that specifies the link color.
        /// </summary>
        public object linkColor { get; set; }

        /// <summary>
        /// A Number value that specifies the maximum zoom level of this map type.
        /// </summary>
        public object maxResolution { get; set; }

        /// <summary>
        /// A Number value that specifies the minimum zoom level of this map type.
        /// </summary>
        public object minResolution { get; set; }

        /// <summary>
        /// A Number value that specifies the radius of the map type measured in meters.
        /// </summary>
        public object radius { get; set; }

        /// <summary>
        /// Short name of the map type.
        /// </summary>
        public string shortName { get; set; }

        /// <summary>
        /// A Number value that specifies the text color.
        /// </summary>
        public object textColor { get; set; }

        /// <summary>
        /// A Number value that specifies the tile size.
        /// </summary>
        public object tileSize { get; set; }

        /// <summary>
        /// URL argument of the map type.
        /// </summary>
        public string urlArg { get; set; }

        #endregion

        #region Methods
        /// <summary>
        /// [static] Retrieves the MapTypeOptions instance that represents the default set of options that applies to all map types.
        /// </summary>
        public static MapTypeOptions getDefaultOptions()
        {
            return default(MapTypeOptions);
        }

        /// <summary>
        /// [static] Sets the default set of options that applies when new map types are created.
        /// </summary>
        public static void setDefaultOptions(MapTypeOptions defaults)
        {
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Constructs a new MapTypeOptions object, optionally initializing it from an object.
        /// </summary>
        public MapTypeOptions(object param)
        {
        }

        /// <summary>
        /// Constructs a new MapTypeOptions object, optionally initializing it from an object.
        /// </summary>
        public MapTypeOptions()
        {
        }

        #endregion

    }
}
