using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FlashSpaceInvaders.ActionScript.Extensions;
using FlashSpaceInvaders.ActionScript.FragileEntities;
using FlashSpaceInvaders.ActionScript.StarShips;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.filters;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.ActionScript.flash.media;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.flash.ui;
using ScriptCoreLib.ActionScript.mx.core;

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

		// http://cdexos.sourceforge.net/?q=download

		public const int DefaultWidth = 480;
		public const int DefaultHeight = 480;





		const string LinkPoweredByJSC = @"<a href='http:/jsc.sf.net' target='_blank'><u>powered by jsc</u></a>";
		const string LinkPlayMoreGames = @"<a href='http://nonoba.com/zproxy/' target='_blank'><u>play more games</u></a>";

		public bool SoundEnabled { get; set; }

		public FlashSpaceInvaders()
			: base(DefaultWidth, DefaultHeight)
		{
			SoundEnabled = true;

			Action<Sound> play =
				s =>
				{
					if (SoundEnabled)
						s.play();
				};

			var Menu = new MenuSprite(DefaultWidth).AttachTo(base.InfoOverlay);

			Menu.TextExternalLink2.htmlText = LinkPlayMoreGames;

			var DefenseY = 420;

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
			Statusbar.Score.Value = 0;

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
						SoundEnabled = !SoundEnabled;

					}

					if (e.keyCode == Keyboard.C)
					{
						play(Sounds.miu);


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

			const int ClipMargin = 20;

			#region evilmode

			this.Ego.EvilMode.ValueChangedToTrue +=
				delegate
				{
					play(Sounds.fade);

					#region keep ego here for 10 secs

					this.Ego.GoodEgo.Clip =
						p =>
						{
							if (p.x < DefaultWidth / 2)
							{
								p.x = p.x.Min(-ClipMargin);
							}
							else
							{
								p.x = p.x.Max(DefaultWidth + ClipMargin);
							}

							return p;
						};

					5000.AtDelayDo(
						delegate
						{
							this.Ego.GoodEgo.Clip = null;
							play(Sounds.fade);
						}
					);
					#endregion

				};

			this.Ego.EvilMode.ValueChangedToFalse +=
				delegate
				{
					play(Sounds.insertcoin);
				};

			#endregion

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


				foreach (var v in DefenseBlock.CreateDefenseArray(offset, DefenseY))
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

			#region AddEnemy
			this.AddEnemy.Direct +=
				(e, p) =>
				{
					e.Name = "Enemy";

					e.TeleportTo(p.x, p.y)
					.AttachTo(CanvasOverlay)
					.AddTo(FragileEntities.Items)
					.AddTo(ComputerEnemies);
				};
			#endregion

			#region cloud
			var cloud1 = new EnemyCloud
			{
				PlaySound = play
			};

			cloud1.TickSounds =
					new Sound[] {
						Sounds.duh0,
						Sounds.duh1,
						Sounds.duh2,
						Sounds.duh3,
					};

			cloud1.AttachTo(this.CanvasOverlay);
			



			cloud1.TickInterval.ValueChangedTo +=
				e => DebugDump.Write(new { TickInterval = e });



			var CloudSpeedAcc = 1.04;
			var CloudSpeed = 12.0;
			var CloudMove = new Point();

			Action ResetCloudLocal =
				delegate
				{
					CloudMove.x = CloudSpeed;
					CloudMove.y = 0;

					cloud1.TickInterval.Value = 1000;
					CloudSpeed = 12;
					cloud1.TeleportTo(60, 80);

				};
			
			ResetCloudLocal();

			Action ResetCloudSoon =
				delegate
				{
					cloud1.TickInterval.Value = 0;

					1000.AtDelayDo(
						delegate
						{
							cloud1.ResetColors();

							ResetCloudLocal();

							cloud1.ResetLives();
						}
					);

				};

			cloud1.Tick +=
				delegate
				{
					var r = cloud1.Warzone;

					if (r == null)
					{
						ResetCloudSoon();
			
						return;
					}

					//this.graphics.clear();
					//this.graphics.beginFill(0xffffff);
					//this.graphics.drawRect(r.x, r.y, r.width, r.height);

					DebugDump.Write(new { r.left, r.right });

					if (r.bottom > Ego.GoodEgoY)
					{
						ResetCloudSoon();

						return;
					}

					var IsFarRight = r.right >= (DefaultWidth - EnemyCloud.DefaultCloudMargin);

					if (CloudMove.x < 0)
						IsFarRight = false;

					var IsFarLeft = r.left <= (EnemyCloud.DefaultCloudMargin);

					if (CloudMove.x > 0)
						IsFarLeft = false;


					var WillStartVerticalMovement = IsFarLeft || IsFarRight;


					if (WillStartVerticalMovement && CloudMove.y == 0)
					{


						CloudMove.x = 0;
						CloudMove.y = 8 ;

						CloudSpeed *= CloudSpeedAcc;
					}
					else
					{
						CloudMove.y -= CloudSpeed / 2;

						if (CloudMove.y <= 0)
						{
							CloudMove.y = 0;

							if (IsFarLeft)
								CloudMove.x = CloudSpeed;
							else if (IsFarRight)
								CloudMove.x = -CloudSpeed;
						}
					}

					DebugDump.Write(new { CloudMove.x, CloudMove.y });

					cloud1.MoveToOffset(CloudMove);

				};

			cloud1.Members.ForEach(v =>
				AddEnemy.Chained(v.Element, v.Element.ToPoint())
			);

			#endregion



			//AddEnemy.Chained(new EnemyA(), new Point(200, 200));
			//AddEnemy.Chained(new EnemyB(), new Point(240, 200));
			//AddEnemy.Chained(new EnemyC(), new Point(280, 200));
			//AddEnemy.Chained(new EnemyUFO(), new Point(160, 200));
			//AddEnemy.Chained(new EnemyBigGun(), new Point(120, 200));



			#region AddBullet
			this.AddBullet.Direct +=
				bullet =>
				{
					// local only
					FragileEntities.AddBullet(bullet);

					bullet.Element.AttachTo(CanvasOverlay);

					play(Sounds.firemissile);
				};

			this.AddBullet.Handler +=
				bullet =>
				{
					// our bullets will need to check for collisions

					// remote bullets check on their hosts for collision
				};
			#endregion

			#region input
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

				SmartMoveTo = (x, y) =>
					this.DoPlayerMovement.Chained(Ego, new Point(Ego.Wrapper(x, y), Ego.GoodEgoY))

			};
			#endregion

			#region SetWeaponMultiplier

			this.SetWeaponMultiplier.Direct =
				(p, value) =>
				{
					p.CurrentBulletMultiplier.Value = value;
				};
			#endregion

			#region AddDamage
			this.AddDamage.Direct +=
				(target, bullet) =>
				{
					target.TakeDamage(bullet.TotalDamage);

					if (target.HitPoints <= 0)
					{
						if (ComputerEnemies.Any(k => k == target))
						{

							cloud1.TickInterval.Value = (cloud1.TickInterval.Value - 50).Max(200);
							CloudSpeed *= CloudSpeedAcc;

						}

						Statusbar.Score.Value += target.ScorePoints;

						if (Statusbar.Score < 5)
							this.SetWeaponMultiplier.Chained(Ego, 1);
						else
							if (Statusbar.Score < 10)
								this.SetWeaponMultiplier.Chained(Ego, 2);
							else
								this.SetWeaponMultiplier.Chained(Ego, 3);

						play(target.GetDeathSound());
					}
					else
					{
						play(Sounds.shortwhite);
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
			#endregion



			#region FragileEntities
			this.FragileEntities.AddDamage = this.AddDamage;

			this.FragileEntities.PrepareFilter =
				delegate
				{
					var GroupGood = KnownEgos.Select(i => i.GoodEgo).ToArray();
					var GroupEvil = KnownEgos.Select(i => i.EvilEgo).Concat(ComputerEnemies).ToArray();

					this.FragileEntities.Filter =
						(source, n) =>
						{
							// spare yourself
							var query = source;

							// spare coplayers in the same mode
							if (n.Parent.EvilMode)
								query = query.Where(x => !GroupEvil.Contains(x));
							else
								query = query.Where(x => !GroupGood.Contains(x));

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
			this.SetWeaponMultiplier.BaseHandler += BaseHandler;

		}



		#region routed events

		public readonly RoutedActionInfo<IFragileEntity, BulletInfo> AddDamage = "AddDamage";
		public readonly RoutedActionInfo<StarShip, Point> AddEnemy = "AddEnemy";
		public readonly RoutedActionInfo<BulletInfo> AddBullet = "AddBullet";
		public readonly RoutedActionInfo<PlayerShip, Point> DoPlayerMovement = "DoMovement";
		public readonly RoutedActionInfo<PlayerShip, int> SetWeaponMultiplier = "SetWeaponMultiplier";

		#endregion


		public readonly FragileEntitiesContainer FragileEntities = new FragileEntitiesContainer();

		public readonly List<StarShip> ComputerEnemies = new List<StarShip>();

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
