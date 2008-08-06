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
using FlashSpaceInvaders.ActionScript.FragileEntities;

namespace FlashSpaceInvaders.ActionScript
{
	/// <summary>
	/// testing...
	/// </summary>
	[Script, ScriptApplicationEntryPoint]
	[SWF(backgroundColor = Colors.Black, width = DefaultWidth, height = DefaultHeight)]
	public class FlashSpaceInvaders : FixedBorderCanvas
	{
		// todo: add http://gimme.badsectoracula.com/flashmodplayer/modplayer.html

		// http://zproxy.wordpress.com/2007/03/03/jsc-space-invaders/

		public const int DefaultWidth = 480;
		public const int DefaultHeight = 480;


		public const int KeyLeft = 37;
		public const int KeyRight = 39;



		const string LinkPoweredByJSC = @"<a href='http:/jsc.sf.net' target='_blank'><u>powered by jsc</u></a>";
		const string LinkPlayMoreGames = @"<a href='http://nonoba.com/zproxy/' target='_blank'><u>play more games</u></a>";

		public FlashSpaceInvaders() : base(DefaultWidth, DefaultHeight)
		{

			var Menu = new MenuSprite(DefaultWidth).AttachTo(base.InfoOverlay);

			Menu.TextExternalLink2.htmlText = LinkPlayMoreGames;


			var DebugDump = new DebugDumpTextField();

			DebugDump.Field.y = DefaultHeight / 4;
			DebugDump.Field.x = 0;

			DebugDump.Field.width = DefaultWidth;
			DebugDump.Field.height = DefaultHeight / 2;

			DebugDump.Visible.ValueChangedToTrue +=
				delegate
				{
					Menu.TextExternalLink2.htmlText = LinkPoweredByJSC;
					DebugDump.Field.AttachToBefore(BorderOverlay);
				};

			DebugDump.Visible.ValueChangedToFalse +=
				delegate
				{
					Menu.TextExternalLink2.htmlText = LinkPlayMoreGames;
				};

			var Statusbar = new Statusbar();

			Statusbar.Lives.Value = 2;
			Statusbar.Score.Value = 1234;

			Statusbar.Element.AttachTo(InfoOverlay);

			var MenuFader = new DualFader { Value = Menu };

			stage.keyUp +=
				e =>
				{
					if (e.keyCode == Keyboard.ENTER)
					{
						MenuFader.Value = CanvasOverlay;
					}

					if (e.keyCode == Keyboard.ESCAPE)
					{
						MenuFader.Value = Menu;
					}

					if (e.keyCode == Keyboard.T)
					{
						DebugDump.Visible.Toggle(); 
			
					}


					if (e.keyCode == Keyboard.M)
					{
						Sounds.miu.ToSoundAsset().play();

					}
				};

	

			#region npc
			var cp1 = new PlayerShip(DefaultWidth, DefaultHeight)
				{
					Name = "cp1"
				}.AddTo(CoPlayers);

			cp1.GoodEgo.AttachTo(CanvasOverlay);
			cp1.EvilEgo.AttachTo(CanvasOverlay);



			var cp2 = new PlayerShip(DefaultWidth, DefaultHeight)
				{
					Name = "cp2"
				}.AddTo(CoPlayers);

			cp2.GoodEgo.AttachTo(CanvasOverlay);
			cp2.EvilEgo.AttachTo(CanvasOverlay);

			(1000 / 30).AtInterval(
				delegate
				{
					//cp2.GoodEgo.MoveToTarget.Value.x -= 6;
				}
			);
			#endregion



			this.Ego = new PlayerShip(DefaultWidth, DefaultHeight)
				{
					Name = "Ego"
				};


			this.Ego.EvilMode.LinkTo(Statusbar.EvilMode);

			#region evilmode indicator
			this.Ego.EvilMode.ValueChangedToTrue +=
				delegate
				{
					this.filters = new[] { Filters.RedChannelFilter };
				};

			this.Ego.EvilMode.ValueChangedToFalse +=
				delegate
				{
					this.filters = null;
				};
			#endregion

			this.Ego.GoodEgo.AttachTo(CanvasOverlay);
			this.Ego.EvilEgo.AttachTo(CanvasOverlay);

			#region  build shared defense buildings
			for (int i = 0; i < 4; i++)
			{
				var offset = DefaultWidth * (i * 2 + 1) / 8;


				foreach (var v in DefenseBlock.CreateDefenseArray(offset, 420))
				{
					v.AttachTo(CanvasOverlay);
					v.AddTo(DefenseBlocks);
					v.AddTo(FragileEntities.Items);
				}
			}
			#endregion

			cp1.AddTo(FragileEntities);
			cp2.AddTo(FragileEntities);
			Ego.AddTo(FragileEntities);

			this.AddEnemy.Direct +=
				(e, p) =>
				{
					e.Name = "Enemy";

					e.TeleportTo(p.x, p.y)
				.AttachTo(CanvasOverlay)
				.AddTo(FragileEntities.Items);
				};

		

			AddEnemy.Chained(new StarShip { Animations.Spawn_A }, new Point(200, 200));
			AddEnemy.Chained(new StarShip { Animations.Spawn_B }, new Point(240, 200));
			AddEnemy.Chained(new StarShip { Animations.Spawn_C }, new Point(280, 200));

			#region AddBullet
			this.AddBullet.Direct +=
				bullet =>
				{
					bullet.Element.AttachTo(CanvasOverlay);
				};

			this.AddBullet.Handler +=
				bullet =>
				{
					// our bullets will need to check for collisions
					FragileEntities.AddBullet(bullet);
					// remote bullets check on their hosts for collision
				};
			#endregion

			#region DoPlayerMovement
			this.DoPlayerMovement.Direct +=
				(e, p) =>
				{
					e.GoodEgo.MoveToTarget.Value = p;
				};

			Action<double, double> DoEgoPlayerMovement =
				(arc, length) =>
					 this.DoPlayerMovement.Chained(Ego, Ego.GoodEgo.ToPoint().MoveToArc(arc, Ego.GoodEgo.MaxStep * length));


			var input = new PlayerInput(stage, Ego)
			{
				StepLeft = () => DoEgoPlayerMovement(Math.PI, 2),
				StepLeftEnd = () => DoEgoPlayerMovement(Math.PI, 0.5),

				StepRight = () => DoEgoPlayerMovement(0, 2),
				StepRightEnd = () => DoEgoPlayerMovement(0, 0.5),

				FireBullet = () => this.AddBullet.Chained(Ego.FireBullet()),

				SmartMoveTo = x =>
					this.DoPlayerMovement.Chained(Ego, new Point(Ego.Wrapper(x), Ego.GoodEgoY))

			};
			#endregion

			this.AddDamage.Direct +=
				(target, bullet) =>
				{
					target.TakeDamage(bullet.TotalDamage);

					if (target.HitPoints <= 0)
					{
						Sounds.baseexplode.ToSoundAsset().play();
					}

					DebugDump.Write(
						new
						{
							From = bullet.Parent.ActiveEgo.Name,
							Delta = bullet.TotalDamage,
							target.HitPoints,
							To = target.Name
						}
					);
				};


			#region FragileEntities
			this.FragileEntities.AddDamage = this.AddDamage;

			this.FragileEntities.PrepareFilter =
				delegate
				{
					var GroupGoodEgos = KnownEgos.Select(i => i.GoodEgo).ToArray();
					var GroupEvilEgos = KnownEgos.Select(i => i.EvilEgo).ToArray();

					this.FragileEntities.Filter =
						(source, n) =>
						{
							// spare yourself
							var query = source;

							// spare coplayers in the same mode
							if (n.Parent.EvilMode)
								query = query.Where(x => !GroupEvilEgos.Contains(x));
							else
								query = query.Where(x => !GroupGoodEgos.Contains(x));

							return query;
						};
				};
			#endregion




			Action<RoutedActionInfoBase> BaseHandler =
				e => DebugDump.Write(new { e.EventName });

			// events for network
			this.AddDamage.BaseHandler += BaseHandler;
			this.AddEnemy.BaseHandler += BaseHandler;
			this.AddBullet.BaseHandler += BaseHandler;
			this.DoPlayerMovement.BaseHandler += BaseHandler;

		}



		#region routed events

		public readonly RoutedActionInfo<IFragileEntity, BulletInfo> AddDamage = "AddDamage";
		public readonly RoutedActionInfo<StarShip, Point> AddEnemy = "AddEnemy";
		public readonly RoutedActionInfo<BulletInfo> AddBullet = "AddBullet";
		public readonly RoutedActionInfo<PlayerShip, Point> DoPlayerMovement = "DoMovement";

		#endregion


		public readonly FragileEntitiesContainer FragileEntities = new FragileEntitiesContainer();


		public readonly List<DefenseBlock> DefenseBlocks =
			new List<DefenseBlock>();

		#region friendly units, human controlled
		public readonly List<PlayerShip> CoPlayers = new List<PlayerShip>();

		public PlayerShip Ego;

		public PlayerShip[] KnownEgos
		{
			get
			{
				return this.CoPlayers.Concat(this.Ego).ToArray();
			}
		}
		#endregion


	}

}
