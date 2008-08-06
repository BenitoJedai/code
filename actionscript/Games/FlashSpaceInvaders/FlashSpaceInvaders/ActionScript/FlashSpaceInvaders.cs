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
using System.IO;
using System.Text;

namespace FlashSpaceInvaders.ActionScript
{
	/// <summary>
	/// testing...
	/// </summary>
	[Script, ScriptApplicationEntryPoint]
	[SWF(backgroundColor = Colors.Black, width = DefaultWidth, height = DefaultHeight)]
	public class FlashSpaceInvaders : Sprite
	{
		// todo: add http://gimme.badsectoracula.com/flashmodplayer/modplayer.html

		public const int DefaultWidth = 480;
		public const int DefaultHeight = 480;


		public const int KeyLeft = 37;
		public const int KeyRight = 39;



		readonly Sprite Canvas = new Sprite();

		public FlashSpaceInvaders()
		{
		

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

			var LifeBar = new SpriteWithMovement { Life1, Life2, Life3 }.AttachTo(this);

			var EvilLife1 = AddEvilLife(40 * 0);
			var EvilLife2 = AddEvilLife(40 * 1);
			var EvilLife3 = AddEvilLife(40 * 2);

			var EvilLifeBar = new SpriteWithMovement { EvilLife1, EvilLife2, EvilLife3 };
			#endregion

			#region npc
			var cp1 = new PlayerShip(DefaultWidth, DefaultHeight)
				{
					Name = "cp1"
				}.AddTo(CoPlayers);

			cp1.GoodEgo.AttachTo(Canvas);
			cp1.EvilEgo.AttachTo(Canvas);



			var cp2 = new PlayerShip(DefaultWidth, DefaultHeight)
				{
					Name = "cp2"
				}.AddTo(CoPlayers);

			cp2.GoodEgo.AttachTo(Canvas);
			cp2.EvilEgo.AttachTo(Canvas);

			(1000 / 30).AtInterval(
				delegate
				{
					//cp2.GoodEgo.MoveToTarget.Value.x -= 6;
				}
			);
			#endregion

			#region info
			var TextInfo = new TextField
			{

				y = DefaultHeight / 4,
				x = 0,

				width = DefaultWidth,
				height = DefaultHeight / 2,

				textColor = Colors.White,
				embedFonts = true,

				mouseEnabled = false,

				defaultTextFormat = new TextFormat
				{
					font = Assets.FontFixedSys,
					size = 12,
				},
				//selectable = false,
				condenseWhite = false,

				background = true,
				backgroundColor = 0x101010,

				multiline = true,
				text = "",
			}.AttachTo(this);

			var DebugDumpQueue = new Queue<string>();
			Action DebugDumpUpdate =
				delegate
				{
					var w = new StringBuilder();

					foreach (var v in DebugDumpQueue)
					{
						w.AppendLine(v);
					}

					TextInfo.text = w.ToString();
				};

			DebugDump =
				o =>
				{
					if (DebugDumpQueue.Count > 16)
						DebugDumpQueue.Dequeue();

					DebugDumpQueue.Enqueue(o.ToString());

					DebugDumpUpdate();
				};

			1000.AtInterval(
				delegate
				{
					DebugDumpQueue.Dequeue();
					DebugDumpUpdate();

					//AddBullet(cp1.FireBullet().Do(n => n.Element.AttachTo(Canvas)));
					//AddBullet(cp2.FireBullet(4).Do(n => n.Element.AttachTo(Canvas)));


				}
			);

			#endregion


			this.Ego = new PlayerShip(DefaultWidth, DefaultHeight)
				{
					Name = "Ego"
				};

			#region evilmode indicator
			this.Ego.EvilMode.ValueChangedToTrue +=
				delegate
				{
					EvilLifeBar.AttachTo(this);
					LifeBar.Orphanize();

					this.filters = new[] { Filters.RedChannelFilter };
				};

			this.Ego.EvilMode.ValueChangedToFalse +=
				delegate
				{
					LifeBar.AttachTo(this);
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
					v.AddTo(DefenseBlocks);
					v.AddTo(FragileEntities);
				}
			}
			#endregion


			cp1.AddTo(FragileEntities);
			cp2.AddTo(FragileEntities);
			Ego.AddTo(FragileEntities);

			var input = new PlayerInput(stage, Ego)
			{
				StepLeft = () => Ego.GoodEgo.MoveToTarget.Value = Ego.GoodEgo.ToPoint().MoveToArc(Math.PI, Ego.GoodEgo.MaxStep * 2),
				StepLeftEnd = () => Ego.GoodEgo.MoveToTarget.Value = Ego.GoodEgo.ToPoint().MoveToArc(Math.PI, Ego.GoodEgo.MaxStep / 2),

				StepRight = () => Ego.GoodEgo.MoveToTarget.Value = Ego.GoodEgo.ToPoint().MoveToArc(0, Ego.GoodEgo.MaxStep * 2),
				StepRightEnd = () => Ego.GoodEgo.MoveToTarget.Value = Ego.GoodEgo.ToPoint().MoveToArc(0, Ego.GoodEgo.MaxStep / 2),

				FireBullet = () => AddBullet(Ego.FireBullet())
			};

	
	

			this.AddDamage +=
				(target, bullet) =>
				{
					target.TakeDamage(bullet.TotalDamage);


					DebugDump(
						new
						{
							bullet.TotalDamage,
							From = bullet.Parent.ActiveEgo.Name,
							To = target.Name
						}
					);
				};

			var BorderOverlay = new Shape().AttachTo(this);

			BorderOverlay.graphics.lineStyle(1, Colors.Green, 1);
			BorderOverlay.graphics.drawRect(0, 0, DefaultWidth - 1, DefaultHeight - 1);

		}

		public Action<object> DebugDump;

		public PlayerShip[] KnownEgos
		{
			get
			{
				return this.CoPlayers.Concat(this.Ego).ToArray();
			}
		}


		public readonly List<IFragileEntity> FragileEntities = new List<IFragileEntity>();

		public event Action<IFragileEntity, BulletInfo> AddDamage;

		int BulletHitTestCounter = 0;

		public void BulletHitTest(BulletInfo n)
		{
			BulletHitTestCounter++;

			var p = n.Element.ToPoint();

			var GroupGoodEgos = KnownEgos.Select(i => i.GoodEgo).ToArray();
			var GroupEvilEgos = KnownEgos.Select(i => i.EvilEgo).ToArray();


			// spare yourself
			var query = FragileEntities.Where(x => x != n.Parent.ActiveEgo);

			// spare coplayers in the same mode
			if (n.Parent.EvilMode)
				query = query.Where(x => !GroupEvilEgos.Contains(x));
			else
				query = query.Where(x => !GroupGoodEgos.Contains(x));

			query = from x in query
					where x.HitPoints > 0
					let distance = (x.Location - p).length
					where distance <= x.HitRange
					orderby distance
					select x;

			DebugDump(
				new { counter = BulletHitTestCounter, targets = query.Count() }
				);

			var v = query.FirstOrDefault();

			if (v != null)
			{
				if (AddDamage != null)
					AddDamage(v, n);

				n.Element.Orphanize();
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


					if ((k - p).length > 1)
					{
						// only check for hit on each moved one pixel

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
