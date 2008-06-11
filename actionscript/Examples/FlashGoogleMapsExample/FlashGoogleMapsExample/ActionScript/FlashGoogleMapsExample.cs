using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.com.google.maps;

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
            // This key is good for all URLs in this directory:
            // http://jsc.sourceforge.net/
            this.key = "ABQIAAAAP8RnR45oCW_IQn841NsRUxTWTeJzEP4t7om06_BG8dFdNnzkzRSRwPJSLIikpLP_90z2Fvj1rJhFWw";

            this.MapReady +=
                delegate
                {
                    // ding
                };
        }
    }
}