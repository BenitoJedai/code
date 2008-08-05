﻿using ScriptCoreLib;
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


			MovementWASD = new KeyboardButtonGroup { Name = "WASD" };
			MovementArrows = new KeyboardButtonGroup { Name = "Arrows" };

			this.Ego = new SpriteWithMovement { Animations.Spawn_BigGun(0, 0) }.AttachTo(Canvas);

			var EgoY = DefaultHeight - 20;
			var EvilEgoY = 60;

			Ego.y = EgoY;

			var EvilEgo = new SpriteWithMovement { Animations.Spawn_UFO(0, 0) }.AttachTo(Canvas);

			EvilEgo.y = EvilEgoY;

			this.Ego.MoveToTarget.ValueChanged +=
				delegate
				{
					if (this.Ego.MoveToTarget.Value.x > DefaultWidth / 2)
						EvilEgo.MoveTo(this.Ego.MoveToTarget.Value.x - DefaultWidth, EvilEgoY);
					else
						EvilEgo.MoveTo(this.Ego.MoveToTarget.Value.x + DefaultWidth, EvilEgoY);
				};

			var EvilMode = new BooleanProperty();








			EvilMode.ValueChangedToTrue +=
				delegate
				{
					EvilLifeBar.AttachTo(Canvas);
					LifeBar.Orphanize();

					this.filters = new[] { Filters.RedChannelFilter };
				};

			EvilMode.ValueChangedToFalse +=
				delegate
				{
					LifeBar.AttachTo(Canvas);
					EvilLifeBar.Orphanize();


					this.filters = null;
				};

			this.Ego.PositionChanged +=
				delegate
				{
					var EvilModePending = true;

					if (this.Ego.x < DefaultWidth)
						if (this.Ego.x > 0)
						{

							if (this.Ego.MoveToTarget.Value.x > DefaultWidth / 2)
								EvilEgo.TeleportTo(this.Ego.x - DefaultWidth, EvilEgoY);
							else
								EvilEgo.TeleportTo(this.Ego.x + DefaultWidth, EvilEgoY);

							EvilModePending = false;

						}

					EvilMode.Value = EvilModePending;

					if (this.Ego.x > DefaultWidth * 2)
					{
						this.Ego.MoveToTarget.Value.x -= DefaultWidth * 2;
						this.Ego.x -= DefaultWidth * 2;
					}

					if (this.Ego.x < -DefaultWidth)
					{
						this.Ego.MoveToTarget.Value.x += DefaultWidth * 2;
						this.Ego.x += DefaultWidth * 2;
					}
				};


			Ego.MoveTo(DefaultWidth / 2, EgoY);

			
			Ego.MaxStep = 12;
			EvilEgo.MaxStep = 12;

			#region Ego Movement
			// ego input
			stage.click +=
				e =>
				{
					if (EvilMode)
					{
						Ego.MoveTo(e.stageX + DefaultWidth, EgoY);
					}
					else
						Ego.MoveTo(e.stageX, EgoY);
				};

			var GoLeft = new KeyboardButton(stage)
			{
				Groups = new[]
                {
                    MovementWASD[Keyboard.A],
                    MovementArrows[Keyboard.LEFT],
                },
				Tick = () => { Ego.MoveToTarget.Value = Ego.ToPoint().MoveToArc(Math.PI, Ego.MaxStep * 2); }
			};

			var GoRight = new KeyboardButton(stage)
			{
				Groups = new[]
                {
                    MovementWASD[Keyboard.D],
                    MovementArrows[Keyboard.RIGHT],
                },
				Tick = () => { Ego.MoveToTarget.Value = Ego.ToPoint().MoveToArc(0, Ego.MaxStep * 2); }
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


		[Script]
		public class SolidColorShape : Shape
		{
			public readonly int Size;
			public readonly uint Color;

			public SolidColorShape(int size, uint color)
			{
				this.Size = size;
				this.Color = color;

				this.graphics.beginFill(color);
				this.graphics.drawRect(-size / 2, -size / 2, size, size);
			}
		}

		public SpriteWithMovement Ego;

		public KeyboardButtonGroup MovementWASD;
		public KeyboardButtonGroup MovementArrows;

	}

}
