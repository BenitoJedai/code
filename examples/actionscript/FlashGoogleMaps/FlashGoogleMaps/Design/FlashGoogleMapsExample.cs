using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using com.google.maps;
using ScriptCoreLib.ActionScript.flash.ui;
using ScriptCoreLib.ActionScript.flash.filters;
using ScriptCoreLib.ActionScript.Extensions;
using com.google.maps.controls;

namespace FlashGoogleMapsExample.ActionScript
{

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
            var MyMap = this;

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

            // based on http://code.google.com/p/gmaps-samples-flash/source/browse/trunk/samplecode/com/google/maps/examples/HelloWorld.as


            //MyMap.width = 500;
            //MyMap.height = 400;

            // This key is good for all URLs in this directory:
            // http://jsc.sourceforge.net/
            MyMap.key = "ABQIAAAAP8RnR45oCW_IQn841NsRUxTWTeJzEP4t7om06_BG8dFdNnzkzRSRwPJSLIikpLP_90z2Fvj1rJhFWw";

            CommonExtensions.CombineDelegate(MyMap,

                (MapEvent e) =>
                {
                    MyMap.setCenter(new LatLng(40.736072, -73.992062), 14, MapType.HYBRID_MAP_TYPE);

                    var status = new TextField
                    {
                        x = 64,
                        y = 164,
                        autoSize = TextFieldAutoSize.LEFT,
                        background = true,
                        mouseEnabled = false,
                        backgroundColor = ColorBlack,
                        textColor = ColorWhite,
                        filters = new[] { new GlowFilter(ColorBlack) }
                    }.AttachTo(this);

                    //MyMap.MapMoveStep += e =>
                    //    {
                    //        status.text = "move: " + e.latLng.ToString();
                    //    };

                    //MyMap.MapClick += e =>
                    //    {
                    //        status.text = "click: " + e.latLng.ToString();
                    //    };
                    powered_by_jsc.AttachTo(this);
                }

                , MapEvent.MAP_READY);



            MyMap.addControl(new ZoomControl());
            MyMap.addControl(new PositionControl());
            MyMap.addControl(new MapTypeControl());


            this.contextMenu = new ContextMenu();
            this.contextMenu.hideBuiltInItems();

            #region powered by jsc

        



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