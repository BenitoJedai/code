using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.com.google.maps;
using ScriptCoreLib.ActionScript.flash.ui;

namespace FlashGoogleMapsExample.ActionScript
{
    /// <summary>
    /// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
    /// </summary>
    [Script, ScriptApplicationEntryPoint]
    public class FlashGoogleMapsExample : Map
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public FlashGoogleMapsExample()
        {
            // based on http://code.google.com/p/gmaps-samples-flash/source/browse/trunk/samplecode/com/google/maps/examples/HelloWorld.as

            // This key is good for all URLs in this directory:
            // http://jsc.sourceforge.net/
            this.key = "ABQIAAAAP8RnR45oCW_IQn841NsRUxTWTeJzEP4t7om06_BG8dFdNnzkzRSRwPJSLIikpLP_90z2Fvj1rJhFWw";

            this.MapReady +=
                delegate
                {
                    setCenter(new LatLng(40.736072, -73.992062), 14, MapType.HYBRID_MAP_TYPE);
                };

            this.contextMenu = new ContextMenu();
            this.contextMenu.hideBuiltInItems();
        }
    }
}