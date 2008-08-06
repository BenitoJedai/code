using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.Shared.Lambda;

using FlashSpaceInvaders.ActionScript.Extensions;

namespace FlashSpaceInvaders.ActionScript
{
	[Script]
	public class Statusbar
	{
		public readonly Int32Property Score = 0;
		public readonly Int32Property Lives = 3;

		public readonly Sprite Element;

		public readonly BooleanProperty EvilMode = false;

		public Statusbar()
		{
			Element = new Sprite();

			#region TextScore
			var TextScore = new TextField
			{

				y = 8,
				x = 8,
				autoSize = TextFieldAutoSize.LEFT,
				textColor = Colors.White,
				embedFonts = true,

				//background = true,
				//backgroundColor = Colors.Gray,

				defaultTextFormat = new TextFormat
				{
					font = Fonts.FontFixedSys,
					size = 28,
				},
				selectable = false,
				condenseWhite = false,
				htmlText = "Score: <font color='#00ff00'>15</font>",
			}.AttachTo(Element);
			#endregion

			Score.ValueChangedTo +=
						score => TextScore.htmlText = "Score: <font color='#00ff00'>" + score + "</font>";


			#region TextLives
			var TextLives = new TextField
			{

				y = 8,
				x = 240,
				autoSize = TextFieldAutoSize.LEFT,
				textColor = Colors.White,
				embedFonts = true,

				//background = true,
				//backgroundColor = Colors.Gray,

				defaultTextFormat = new TextFormat
				{
					font = Fonts.FontFixedSys,
					size = 28,
				},
				selectable = false,
				condenseWhite = false,
				htmlText = "Lives:  ",
			}.AttachTo(Element);
			#endregion

			#region lifebar
			Func<int, Sprite> AddLife =
				offset =>
					Animations.Spawn_BigGun((int)(TextLives.x + TextLives.width) + offset, (int)(TextLives.y + TextLives.height / 2));


			Func<int, Sprite> AddEvilLife =
				offset =>
					Animations.Spawn_UFO((int)(TextLives.x + TextLives.width) + offset, (int)(TextLives.y + TextLives.height / 2));



			var Life1 = AddLife(40 * 0);
			var Life2 = AddLife(40 * 1);
			var Life3 = AddLife(40 * 2);

			var LifeBar = new SpriteWithMovement { Life1, Life2, Life3 }.AttachTo(Element);

			var EvilLife1 = AddEvilLife(40 * 0);
			var EvilLife2 = AddEvilLife(40 * 1);
			var EvilLife3 = AddEvilLife(40 * 2);

			var EvilLifeBar = new SpriteWithMovement { EvilLife1, EvilLife2, EvilLife3 };
			#endregion

			this.Lives.ValueChangedTo +=
				i =>
				{
					LifeBar.Children().ForEach(
						(c, j) =>
						{
							c.visible = j < i;
						}
					);

					EvilLifeBar.Children().ForEach(
						(c, j) =>
						{
							c.visible = j < i;
						}
					);
				};

			var Fader = new DualFader { Value = LifeBar };

			this.EvilMode.ValueChangedToTrue +=
				delegate
				{
					Fader.Value = EvilLifeBar;
				};

			this.EvilMode.ValueChangedToFalse +=
				delegate
				{
					Fader.Value = LifeBar;
				};
		}
	}
}
