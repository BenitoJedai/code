using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.Shared.Lambda;

using FlashSpaceInvaders.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.mx.core;
using ScriptCoreLib.ActionScript.flash.text;
using FlashSpaceInvaders.ActionScript.StarShips;

namespace FlashSpaceInvaders.ActionScript
{
	[Script]
	public class MenuSprite : Sprite
	{
		public MenuSprite(int DefaultWidth)
		{
			var menu = new Sprite().AttachTo(this);

			




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
					font = Assets.FontFixedSys,
					size = 48,
				},
				selectable = false,
				condenseWhite = false,
				text = " SPACE ",
			}.AttachTo(menu);
			#endregion

			#region SurroundText

			Action<Func<double, double, Sprite>, TextField> SurroundTextLeft =
				(ctor, t) =>
				{
					ctor((int)t.x, (int)(t.y + (t.height) / 2)).AttachTo(menu);
				};


			Action<Func<double, double, Sprite>, TextField> SurroundTextRight =
				(ctor, t) =>
				{
					ctor((int)(t.x + t.width), (int)(t.y + (t.height) / 2)).AttachTo(menu);

				};

			Action<Func<double, double, Sprite>, TextField> SurroundText =
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
					font = Assets.FontFixedSys,
					size = 48,
				},
				selectable = false,
				text = " INVADERS ",
			}.AttachTo(menu);
			#endregion

			SurroundText(Animations.Spawn_C, TextSpace);
			SurroundText(Animations.Spawn_A, TextInvaders);


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
					font = Assets.FontFixedSys,
					size = 16,
				},
				selectable = false,
				htmlText = "Press <font color='#00ff00'>enter</font> to start a game",
			}.AttachTo(menu);
			#endregion

			#region CreateScoreInfo
			Action<StarShip, int, string> CreateScoreInfo =
				(ctor, offset, text) =>
				{
					#region TextEnemyA
					var t = new TextField
					{

						y = 240 + offset,
						x = DefaultWidth / 2 - 50,
						width = DefaultWidth,
						autoSize = TextFieldAutoSize.LEFT,

						//background = true,
						//backgroundColor = Colors.Gray,

						textColor = Colors.White,
						embedFonts = true,
						defaultTextFormat = new TextFormat
						{
							font = Assets.FontFixedSys,
							size = 16,
						},
						selectable = false,
						text = text,
					}.AttachTo(menu);
					#endregion

					ctor.TeleportTo((int)t.x, (int)(t.y + (t.height) / 2)).AttachTo(menu);
				};

			var KnownEnemies = 
				new StarShip []
				{
					new EnemyA(),
					new EnemyB(),
					new EnemyC(),
					new EnemyUFO()
				};

			KnownEnemies.ForEach(
				(v, i) => 
					CreateScoreInfo(v, i * 30, "  - " + v.HitPoints +  " points")
			);

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
					font = Assets.FontFixedSys,
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
					font = Assets.FontFixedSys,
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
					font = Assets.FontFixedSys,
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
			this.TextExternalLink2 = new TextField
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
					font = Assets.FontFixedSys,
					size = 16,
				},
				selectable = false,

				//    // how to make a link
				//    // http://www.actionscript.com/Article/tabid/54/ArticleID/actionscript-quick-tips-and-gotchas/Default.aspx
				//    htmlText = "<a href='http://jsc.sf.net' target='_blank'>powered by <b>jsc</b></a>",


				htmlText = "",
			}.AttachTo(menu);
			#endregion
		}

		public TextField TextExternalLink2;
	}
}
