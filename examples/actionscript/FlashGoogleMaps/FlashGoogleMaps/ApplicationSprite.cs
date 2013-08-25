using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.Extensions;

namespace FlashGoogleMaps
{
    // The Google Maps API for Flash has been officially deprecated as of September 2, 2011. The API will continue to work until September 2, 2014.
    public sealed class ApplicationSprite : FlashGoogleMapsExample.ActionScript.FlashGoogleMapsExample
    {
        /*
         * Create Partial Type: com.google.maps.wrappers.IMapTypeWrapper
Method 'getProjection' in type 'com.google.maps.wrappers.IMapTypeWrapper' from assembly 'FlashGoogleMaps.ApplicationSprite, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null' does not have an implementation.
         * Method 'getControlPosition' in type 'com.google.maps.wrappers.IControlWrapper' from assembly 'FlashGoogleMaps.ApplicationSprite, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null' does not have an implementation.
         * 
         * */

        com.google.maps.wrappers.IMapTypeWrapper hack1;
        com.google.maps.wrappers.IControlWrapper hack2;

        public ApplicationSprite()
        {
            // AttachToStage
            //new FlashGoogleMapsExample.ActionScript.FlashGoogleMapsExample().AttachTo(this);
        }

    }
}
