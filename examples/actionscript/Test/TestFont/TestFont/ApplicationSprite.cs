using ScriptCoreLib;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.Extensions;

namespace TestFont
{
    public sealed class ApplicationSprite : Sprite
    {
        public const uint White = 0xffffff;

        public ApplicationSprite()
        {
            //var ref0 = "assets/TestFont/Fixedsys500c.ttf";

            Font.registerFont(Fonts.Asset_Fixedsys500c);

            new TextField
            {

                y = 0,
                x = 8,
                autoSize = TextFieldAutoSize.LEFT,

                //background = true,
                //backgroundColor = Colors.Gray,

                defaultTextFormat = new TextFormat
                {
                    size = 28,
                },
                selectable = false,
                condenseWhite = false,
                htmlText = "Score: <font color='#00ff00'>15</font>",
            }.AttachTo(this);

            new TextField
            {

                y = 28,
                x = 8,
                autoSize = TextFieldAutoSize.LEFT,
                embedFonts = true,

                //background = true,
                //backgroundColor = Colors.Gray,

                defaultTextFormat = new TextFormat
                {
                    font = "Fixedsys500c",
                    size = 28,
                },
                selectable = false,
                condenseWhite = false,
                htmlText = "Score: <font color='#00ff00'>15</font>",
            }.AttachTo(this);
        }





    }

    class Fonts
    {
        // http://divillysausages.com/blog/as3_font_embedding_masterclass
        // setting this to "false" means that you can actually use your font with the Flex 4 SDK.
        [Embed("/assets/TestFont/Fixedsys500c.ttf"
            //, fontName = "Fixedsys500c"
            )
        ]
        // You do not use this variable directly. It exists so that 
        // the compiler will link in the font.
        public static readonly Class Asset_Fixedsys500c;

    }
}
