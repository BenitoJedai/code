using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.com.google.maps;
using ScriptCoreLib.ActionScript.flash.ui;
using ScriptCoreLib.ActionScript.flash.filters;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.com.google.maps.controls;

namespace FlashGoogleMapsExample.ActionScript
{
    /// <summary>
    /// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
    /// </summary>
    [Script, ScriptApplicationEntryPoint]
    public class FlashGoogleMapsExample : Map
    {
        public const uint ColorRed = 0xff0000;
        public const uint ColorBlack = 0x000000;
        public const uint ColorWhite = 0xffffff;
        public const uint ColorBlue = 0x0000ff;
        public const uint ColorYellow = 0xffff00;
        public const uint ColorBlueDark = 0x000080;
        public const uint ColorBlueLight = 0x9090ff;


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
                    this.setCenter(new LatLng(40.736072, -73.992062), 14, MapType.HYBRID_MAP_TYPE);

                    var status = new TextField
                    {
                        x = 64,
                        y = 164,
                        autoSize = TextFieldAutoSize.LEFT,
                        background = true,
                        mouseEnabled = false,
                        backgroundColor = ColorBlack,
                        textColor = ColorWhite,
                        filters = new [] { new GlowFilter(ColorBlack) }
                    }.AttachTo(this);

                    this.MapMoveEnd += e =>
                        {
                            status.text = "move: " + e.latLng.ToString();
                        };

                    this.MapClick += e =>
                        {
                            status.text = "click: " + e.latLng.ToString();
                        };
                };

            
            addControl(new ZoomControl());
            addControl(new PositionControl());
            addControl(new MapTypeControl());
            

            this.contextMenu = new ContextMenu();
            this.contextMenu.hideBuiltInItems();

            #region powered by jsc

            // lets create a hyperlink
            var powered_by_jsc = new TextField
            {

                x = 72,
                y = 32,

                defaultTextFormat = new TextFormat
                {
                    size = 24
                },
                autoSize = TextFieldAutoSize.LEFT,
                htmlText = "<a href='http://jsc.sf.net' target='_blank'>powered by <b>jsc</b></a>",
                selectable = false,
                filters = new[] { new BlurFilter() },
                textColor = ColorWhite
            }.AttachTo(this);

            
            this.MapReady +=
                delegate
                {
                    powered_by_jsc.AttachTo(this);
                };

            powered_by_jsc.mouseOver +=

                                   delegate
                                   {

                                       powered_by_jsc.textColor = ColorYellow;
                                       powered_by_jsc.filters = new[] { new DropShadowFilter() };


                                   };

            powered_by_jsc.mouseOut +=
                delegate
                {
                    powered_by_jsc.filters = new[] { new BlurFilter() };
                    powered_by_jsc.textColor = ColorWhite;

                };

            #endregion
        }
    }
}