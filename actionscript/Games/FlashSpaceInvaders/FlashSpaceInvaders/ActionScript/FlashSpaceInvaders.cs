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



		readonly Sprite Canvas = new Sprite();

		public FlashSpaceInvaders()
		{
			this.graphics.lineStyle(1, Colors.Green, 1);
			this.graphics.drawRect(0, 0, DefaultWidth - 1, DefaultHeight - 1);


			var m = new MenuSprite(DefaultWidth).AttachTo(this);



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

			#region npc
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
			#endregion

			1000.AtInterval(
				delegate
				{
					AddBullet(cp1.FireBullet().Do(n => n.Element.AttachTo(Canvas)));
					AddBullet(cp2.FireBullet(4).Do(n => n.Element.AttachTo(Canvas)));


				}
			);

			this.Ego = new PlayerShip(DefaultWidth, DefaultHeight);

			#region evilmode indicator
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
			#endregion

			this.Ego.GoodEgo.AttachTo(Canvas);
			this.Ego.EvilEgo.AttachTo(Canvas);

			#region  build shared defense buildings
			for (int i = 0; i < 4; i++)
			{
				var offset = DefaultWidth * (i * 2 + 1) / 8;


				foreach (var v in CreateDefenseArray(offset, 420, Canvas))
				{
					v.AddTo(DefenseBlocks).AddTo(FragileEntities);
				}
			}

			#endregion



			var input = new PlayerInput(stage, Ego)
			{
				StepLeft = () => Ego.GoodEgo.MoveToTarget.Value = Ego.GoodEgo.ToPoint().MoveToArc(Math.PI, Ego.GoodEgo.MaxStep / 2),
				StepRight = () => Ego.GoodEgo.MoveToTarget.Value = Ego.GoodEgo.ToPoint().MoveToArc(0, Ego.GoodEgo.MaxStep / 2),
				FireBullet = () => AddBullet(Ego.FireBullet())
			};
		}

		public readonly List<IFragileEntity> FragileEntities = new List<IFragileEntity>();


		public void BulletHitTest(BulletInfo n)
		{
			var p = n.Element.ToPoint();
			foreach (var v in from x in FragileEntities
							  where (x.Location - p).length < n.Element.MaxStep
							  select x)
			{
				v.TakeDamage(n.Damage * n.Multiplier);
			}
		}

		public void AddBullet(BulletInfo n)
		{
			n.AddTo(Bullets);

			n.Element.AttachTo(Canvas);

			var p = default(Point);

			n.Element.PositionChanged +=
				delegate
				{
					var k = n.Element.ToPoint();

					var DoHitTest = false;

					if (p == null)
						DoHitTest = true;
					else if ((k - p).length > n.Element.MaxStep)
						DoHitTest = true;

					if (DoHitTest)
					{
						// only check for hit on each moved 8 pixels

						BulletHitTest(n);
					}

					p = k;
				};

			n.Element.removed +=
				delegate
				{
					Bullets.Remove(n);
				};
		}

		public readonly List<BulletInfo> Bullets =
			new List<BulletInfo>();

		public readonly List<DefenseBlock> DefenseBlocks =
			new List<DefenseBlock>();


		static DefenseBlock[] CreateDefenseArray(int x, int y, DisplayObjectContainer owner)
		{
			const int size = DefenseBlock.BlockSize;

			return new[]
			{
				new DefenseBlock { x = x  + size * 0.5, y = y }.AttachTo(owner),
				new DefenseBlock { x = x  + size * 1.5, y = y }.AttachTo(owner),
				new DefenseBlock { x = x  + size * 1.5, y = y  + size}.AttachTo(owner),

				new DefenseBlock { x = x  - size * 0.5, y = y }.AttachTo(owner),
				new DefenseBlock { x = x  - size * 1.5, y = y }.AttachTo(owner),
				new DefenseBlock { x = x  - size * 1.5, y = y + size }.AttachTo(owner),
			};
		}




		public readonly List<PlayerShip> CoPlayers = new List<PlayerShip>();

		public PlayerShip Ego;



	}

}
