using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using System;
using ScriptCoreLib.ActionScript.mx.core;


namespace FlashSpaceInvaders.ActionScript
{
    /// <summary>
    /// testing...
    /// </summary>
    [Script, ScriptApplicationEntryPoint]
    [SWF(backgroundColor = Colors.Black, width = DefaultWidth, height = DefaultHeight)]
    public class FlashSpaceInvaders : Sprite
    {
        public const int DefaultWidth = 480;
        public const int DefaultHeight = 480;

        [Script]
        public static class Colors
        {
            public const uint Green = 0xff00;
			public const uint Blue = 0xff;
			public const uint White = 0xffffff;
			public const uint Black = 0x0;
			public const uint Gray = 0x808080;
        }


        public const int KeyLeft = 37;
        public const int KeyRight = 39;

		public const string FontFixedSys = "Fixedsys500c";

		[Embed("/assets/FlashSpaceInvaders/Fixedsys500c.ttf", fontName = FontFixedSys)]
        // You do not use this variable directly. It exists so that 
        // the compiler will link in the font.
        static Class Asset_Fixedsys500c;

        static FlashSpaceInvaders()
        {
            Font.registerFont(Asset_Fixedsys500c);
        }
        
        public FlashSpaceInvaders()
        {
			this.graphics.lineStyle(1, Colors.Green, 1);
			this.graphics.drawRect(0, 0, DefaultWidth - 1, DefaultHeight - 1);

			var SizeA = Assets.aenemy_1.ToBitmapAsset();
			var SizeB = Assets.benemy_1.ToBitmapAsset();


            var menu = new Sprite().AttachTo(this);

            #region Spawn_A
            Func<int, int, Sprite> Spawn_A =
                (x, y) =>
                    new Sprite { x = x, y = y }.AttachTo(menu).AnimateAt(
                        new BitmapAsset[]
                        {
                            Assets.aenemy_1.ToBitmapAsset(),
                            Assets.aenemy_2.ToBitmapAsset()
                        }
                    , 500);
            #endregion

            #region Spawn_B
            Func<int, int, Sprite> Spawn_B =
                (x, y) =>
                    new Sprite { x = x, y = y }.AttachTo(menu).AnimateAt(
                        new BitmapAsset[]
                        {
                            Assets.benemy_1.ToBitmapAsset(),
                            Assets.benemy_2.ToBitmapAsset()
                        }
                    , 500);
            #endregion

            #region Spawn_C
            Func<int, int, Sprite> Spawn_C =
                (x, y) =>
                    new Sprite { x = x, y = y }.AttachTo(menu).AnimateAt(
                        new BitmapAsset[]
                        {
                            Assets.cenemy_1.ToBitmapAsset(),
                            Assets.cenemy_2.ToBitmapAsset()
                        }
                    , 500);
            #endregion

            #region Spawn_BigGun
            Func<int, int, Sprite> Spawn_BigGun =
                (x, y) =>
                    new Sprite { x = x, y = y }.AttachTo(menu).AnimateAt(
                        new BitmapAsset[]
                        {
                            Assets.biggun_1.ToBitmapAsset(),
                        }
                    , 500);
            #endregion

			#region Spawn_BigGun
			Func<int, int, Sprite> Spawn_UFO =
				(x, y) =>
					new Sprite { x = x, y = y }.AttachTo(menu).AnimateAt(
						new BitmapAsset[]
                        {
                            Assets.ufo_1.ToBitmapAsset(),
                        }
					, 500);
			#endregion
    


			//var Player = Spawn_BigGun(300, 360);

			//stage.doubleClickEnabled = true;
			//stage.doubleClick +=
			//    ev =>
			//    {
			//        stage.SetFullscreen(true);
			//    };

			//stage.keyDown +=
			//    ev =>
			//    {
			//        if (ev.keyCode == KeyLeft)
			//            Player.x -= 4;

			//        if (ev.keyCode == KeyRight)
			//            Player.x += 4;
			//    };

            
            // http://blog.paoloiulita.it/2008/03/11/as3-embedding-font-with-code-only/
			// http://fixedsys.moviecorner.de/?p=download&l=1

			#region SPACE
			var TextSpace = new TextField
            {

                y = 80,
                width = DefaultWidth,
                autoSize = TextFieldAutoSize.CENTER,
                textColor = Colors.White,
                embedFonts = true,

				//background = true,
				//backgroundColor = Colors.Gray,

                defaultTextFormat = new TextFormat
                {
					font = FontFixedSys,
                    size = 48,
                },
                selectable = false,
				condenseWhite = false,
                text = " SPACE ",
            }.AttachTo(menu);
			#endregion

			#region SurroundText

			Action<Func<int, int, Sprite>, TextField> SurroundTextLeft =
				(ctor, t) =>
				{
					ctor((int)t.x, (int)(t.y + (t.height) / 2));
				};


			Action<Func<int, int, Sprite>, TextField> SurroundTextRight =
				(ctor, t) =>
				{
					ctor((int)(t.x + t.width), (int)(t.y + (t.height) / 2));

				};

			Action<Func<int, int, Sprite>, TextField> SurroundText =
				(ctor, t) =>
				{
					SurroundTextLeft(ctor, t);
					SurroundTextRight(ctor, t);
				};
			#endregion

			#region INVADERS
			var TextInvaders = new TextField
            {

                y = 140,
                width = DefaultWidth,
                autoSize = TextFieldAutoSize.CENTER,

				//background = true,
				//backgroundColor = Colors.Gray,

                textColor = Colors.Green,
                embedFonts = true,
                defaultTextFormat = new TextFormat
                {
					font = FontFixedSys,
                    size = 48,
                },
                selectable = false,
                text = " INVADERS ",
            }.AttachTo(menu);
			#endregion

			SurroundText(Spawn_C, TextSpace);
			SurroundText(Spawn_A, TextInvaders);


			#region TextPressEnterToStartGame
			var TextPressEnterToStartGame = new TextField
			{

				y = 210,
				width = DefaultWidth,
				autoSize = TextFieldAutoSize.CENTER,

				//background = true,
				//backgroundColor = Colors.Gray,

				textColor = Colors.White,
				embedFonts = true,
				defaultTextFormat = new TextFormat
				{
					font = FontFixedSys,
					size = 16,
				},
				selectable = false,
				htmlText = "Press <font color='#00ff00'>enter</font> to start a game",
			}.AttachTo(menu);
			#endregion

			#region CreateScoreInfo
			Action<Func<int, int, Sprite>, int, string> CreateScoreInfo =
				(ctor, offset, text) =>
				{
					#region TextEnemyA
					var t = new TextField
					{

						y = 240 + offset,
						x = DefaultWidth / 2 - 50 ,
						width = DefaultWidth,
						autoSize = TextFieldAutoSize.LEFT,

						//background = true,
						//backgroundColor = Colors.Gray,

						textColor = Colors.White,
						embedFonts = true,
						defaultTextFormat = new TextFormat
						{
							font = FontFixedSys,
							size = 16,
						},
						selectable = false,
						text = text,
					}.AttachTo(menu);
					#endregion

					SurroundTextLeft(ctor, t);
				};

			CreateScoreInfo(Spawn_A, 0, "  - 4 points");
			CreateScoreInfo(Spawn_B, 30, "  - 2 points");
			CreateScoreInfo(Spawn_C, 60, "  - 1 points");
			CreateScoreInfo(Spawn_UFO, 90, "  - 10 points");
			#endregion

			#region TextInstructions1
			var TextInstructions1 = new TextField
			{

				y = 370,
				width = DefaultWidth,
				autoSize = TextFieldAutoSize.CENTER,

				//background = true,
				//backgroundColor = Colors.Gray,

				textColor = Colors.White,
				embedFonts = true,
				defaultTextFormat = new TextFormat
				{
					font = FontFixedSys,
					size = 16,
				},
				selectable = false,
				htmlText = "<font color='#00ff00'>Left/right arrow</font> - move, <font color='#00ff00'>SPACE</font> - fire",
			}.AttachTo(menu);
			#endregion


			#region TextInstructions2
			var TextInstructions2 = new TextField
			{

				y = 390,
				width = DefaultWidth,
				autoSize = TextFieldAutoSize.CENTER,

				//background = true,
				//backgroundColor = Colors.Gray,

				textColor = Colors.White,
				embedFonts = true,
				defaultTextFormat = new TextFormat
				{
					font = FontFixedSys,
					size = 16,
				},
				selectable = false,
				htmlText = "<font color='#00ff00'>Escape</font> - quit, <font color='#00ff00'>P</font> - pause",
			}.AttachTo(menu);
			#endregion


			#region TextComments
			var TextComments = new TextField
			{

				y = 430,
				width = DefaultWidth,
				autoSize = TextFieldAutoSize.CENTER,

				//background = true,
				//backgroundColor = Colors.Gray,

				textColor = Colors.Blue,
				embedFonts = true,
				defaultTextFormat = new TextFormat
				{
					font = FontFixedSys,
					size = 16,
				},
				selectable = false,

				//    // how to make a link
				//    // http://www.actionscript.com/Article/tabid/54/ArticleID/actionscript-quick-tips-and-gotchas/Default.aspx
				//    htmlText = "<a href='http://jsc.sf.net' target='_blank'>powered by <b>jsc</b></a>",


				htmlText = "<a href='http://zproxy.wordpress.com' target='_blank'><u>post a comment</u></a>",
			}.AttachTo(menu);
			#endregion

			#region TextPoweredByJSC
			var TextPoweredByJSC = new TextField
			{

				y = 450,
				width = DefaultWidth,
				autoSize = TextFieldAutoSize.CENTER,

				//background = true,
				//backgroundColor = Colors.Gray,

				textColor = Colors.Blue,
				embedFonts = true,
				defaultTextFormat = new TextFormat
				{
					font = FontFixedSys,
					size = 16,
				},
				selectable = false,

				//    // how to make a link
				//    // http://www.actionscript.com/Article/tabid/54/ArticleID/actionscript-quick-tips-and-gotchas/Default.aspx
				//    htmlText = "<a href='http://jsc.sf.net' target='_blank'>powered by <b>jsc</b></a>",


				htmlText = "<a href='http:/jsc.sf.net' target='_blank'><u>powered by jsc</u></a>",
			}.AttachTo(menu);
			#endregion

		}
    }

}
