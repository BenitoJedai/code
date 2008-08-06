using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using System;
using ScriptCoreLib.ActionScript.mx.core;
using ScriptCoreLib.ActionScript.flash.ui;
using System.Linq;
using ScriptCoreLib.ActionScript.flash.geom;



using FlashSpaceInvaders.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.filters;
using System.Collections.Generic;

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


		public const int KeyLeft = 37;
		public const int KeyRight = 39;




		public FlashSpaceInvaders()
		{
			this.graphics.lineStyle(1, Colors.Green, 1);
			this.graphics.drawRect(0, 0, DefaultWidth - 1, DefaultHeight - 1);


			var m = new MenuSprite(DefaultWidth).AttachTo(this);
			var Canvas = new Sprite();



			Canvas.mask = mask;
			stage.keyUp +=
				e =>
				{
					if (e.keyCode == Keyboard.ENTER)
					{
						m.Orphanize();
						Canvas.AttachTo(this);
					}

					if (e.keyCode == Keyboard.ESCAPE)
					{
						m.AttachTo(this);
						Canvas.Orphanize();
					}
				};

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
					font = Assets.FontFixedSys,
					size = 28,
				},
				selectable = false,
				condenseWhite = false,
				htmlText = "Score: <font color='#00ff00'>15</font>",
			}.AttachTo(this);
			#endregion

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
					font = Assets.FontFixedSys,
					size = 28,
				},
				selectable = false,
				condenseWhite = false,
				htmlText = "Lives:  ",
			}.AttachTo(this);
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

			var LifeBar = new SpriteWithMovement { Life1, Life2, Life3 }.AttachTo(Canvas);

			var EvilLife1 = AddEvilLife(40 * 0);
			var EvilLife2 = AddEvilLife(40 * 1);
			var EvilLife3 = AddEvilLife(40 * 2);

			var EvilLifeBar = new SpriteWithMovement { EvilLife1, EvilLife2, EvilLife3 };
			#endregion


			MovementWASD = new KeyboardButtonGroup { Name = "WASD" };
			MovementArrows = new KeyboardButtonGroup { Name = "Arrows" };

			var cp1 = new PlayerShip(DefaultWidth, DefaultHeight);

			cp1.GoodEgo.AttachTo(Canvas);
			cp1.EvilEgo.AttachTo(Canvas);

			(1000 / 30).AtInterval(
				delegate
				{
					cp1.GoodEgo.MoveToTarget.Value.x += 5;
				}
			);

			var cp2 = new PlayerShip(DefaultWidth, DefaultHeight);

			cp2.GoodEgo.AttachTo(Canvas);
			cp2.EvilEgo.AttachTo(Canvas);

			(1000 / 30).AtInterval(
				delegate
				{
					cp2.GoodEgo.MoveToTarget.Value.x -= 6;
				}
			);

			this.Ego = new PlayerShip(DefaultWidth, DefaultHeight);

			this.Ego.EvilMode.ValueChangedToTrue +=
				delegate
				{
					EvilLifeBar.AttachTo(Canvas);
					LifeBar.Orphanize();

					this.filters = new[] { Filters.RedChannelFilter };
				};

			this.Ego.EvilMode.ValueChangedToFalse +=
				delegate
				{
					LifeBar.AttachTo(Canvas);
					EvilLifeBar.Orphanize();


					this.filters = null;
				};

			this.Ego.GoodEgo.AttachTo(Canvas);
			this.Ego.EvilEgo.AttachTo(Canvas);

			#region Ego Movement
			// ego input
			stage.click +=
				e =>
				{
					if (Ego.EvilMode)
					{
						Ego.GoodEgo.MoveTo(e.stageX + DefaultWidth, Ego.GoodEgoY);
					}
					else
						Ego.GoodEgo.MoveTo(e.stageX, Ego.GoodEgoY);
				};

			var GoLeft = new KeyboardButton(stage)
			{
				Groups = new[]
                {
                    MovementWASD[Keyboard.A],
                    MovementArrows[Keyboard.LEFT],
                },
				Tick = () => { Ego.GoodEgo.MoveToTarget.Value = Ego.GoodEgo.ToPoint().MoveToArc(Math.PI, Ego.GoodEgo.MaxStep * 2); }
			};

			var GoRight = new KeyboardButton(stage)
			{
				Groups = new[]
                {
                    MovementWASD[Keyboard.D],
                    MovementArrows[Keyboard.RIGHT],
                },
				Tick = () => { Ego.GoodEgo.MoveToTarget.Value = Ego.GoodEgo.ToPoint().MoveToArc(0, Ego.GoodEgo.MaxStep * 2); }
			};
			#endregion



			var BlockSize = 16;

			DefenseArrays = new[]
				{
					CreateDefenseArray(BlockSize, DefaultWidth * 1 / 8, 420, Colors.Green, Canvas),
					CreateDefenseArray(BlockSize, DefaultWidth * 3 / 8, 420, Colors.Green, Canvas),
					CreateDefenseArray(BlockSize, DefaultWidth * 5 / 8, 420, Colors.Green, Canvas),
					CreateDefenseArray(BlockSize, DefaultWidth * 7 / 8, 420, Colors.Green, Canvas)
				};


			var DoFire = new KeyboardButton(stage, 400)
			{
				Groups = new[]
                {
                    MovementWASD[Keyboard.CONTROL , KeyLocation.LEFT],
                    MovementArrows[Keyboard.RIGHT , KeyLocation.RIGHT],
                },
				Tick =
					delegate
					{
						// shoot

						var bullet = new SpriteWithMovement().AttachTo(Canvas);

						bullet.graphics.beginFill(Colors.Green);
						bullet.graphics.drawRect(0, -8, 1, 16);
						bullet.StepMultiplier = 0.3;

						if (Ego.EvilMode)
						{
							bullet.TeleportTo(Ego.EvilEgo.x, Ego.EvilEgo.y);
							bullet.MoveTo(Ego.EvilEgo.x + 0.00001, DefaultHeight);

							bullet.PositionChanged +=
								delegate
								{
									if (bullet.y > Ego.GoodEgoY)
										bullet.Orphanize();
								};
						}
						else
						{
							bullet.TeleportTo(Ego.GoodEgo.x, Ego.GoodEgo.y);
							bullet.MoveTo(Ego.GoodEgo.x + 0.00001, 0);


							bullet.PositionChanged +=
								delegate
								{
									if (bullet.y < Ego.EvilEgoY)
										bullet.Orphanize();
								};
						}

						
					}
			};

		}

		public SolidColorShape[][] DefenseArrays;

		static SolidColorShape[] CreateDefenseArray(int size, int x, int y, uint color, DisplayObjectContainer owner)
		{
			return new[]
			{
				new SolidColorShape(size, color) { x = x  + size * 0.5, y = y }.AttachTo(owner),
				new SolidColorShape(size, color) { x = x  + size * 1.5, y = y }.AttachTo(owner),
				new SolidColorShape(size, color) { x = x  + size * 1.5, y = y  + size}.AttachTo(owner),

				new SolidColorShape(size, color) { x = x  - size * 0.5, y = y }.AttachTo(owner),
				new SolidColorShape(size, color) { x = x  - size * 1.5, y = y }.AttachTo(owner),
				new SolidColorShape(size, color) { x = x  - size * 1.5, y = y + size }.AttachTo(owner),
			};
		}




		public readonly List<PlayerShip> CoPlayers = new List<PlayerShip>();

		public PlayerShip Ego;

		public KeyboardButtonGroup MovementWASD;
		public KeyboardButtonGroup MovementArrows;

	}

}
