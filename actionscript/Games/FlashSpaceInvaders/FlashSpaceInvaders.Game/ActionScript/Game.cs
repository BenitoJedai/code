﻿using System;
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
using FlashSpaceInvaders.ActionScript;

namespace FlashSpaceInvaders.ActionScript
{
	[Script]
	public partial class Game : FixedBorderCanvas
	{
		public const int DefaultWidth = 480;
		public const int DefaultHeight = 480;





		const string LinkPoweredByJSC = @"<a href='http:/jsc.sf.net' target='_blank'><u>powered by jsc</u></a>";
		const string LinkPlayMoreGames = @"<a href='http://nonoba.com/zproxy/' target='_blank'><u>play more games</u></a>";

		public bool SoundEnabled { get; set; }

		public event Action GameOver;

		public readonly Statusbar Statusbar = new Statusbar();

		public Game()
			: base(DefaultWidth, DefaultHeight)
		{
			InitializeRoutedActions();
			InitializeSharedState();

			SoundEnabled = false;

			Action<Sound> play =
				s =>
				{
					if (CanvasOverlay.parent == null)
						return;

					if (SoundEnabled)
						s.play();
				};

			var Menu = new MenuSprite(DefaultWidth).AttachTo(base.InfoOverlay);

			Menu.TextExternalLink2.htmlText = LinkPlayMoreGames;

			var DefenseY = 420;

			#region DebugDump
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
			#endregion

			this.SendTextMessage.Direct =
				e => DebugDump.Write(new { Message = e });

			this.Statusbar = new Statusbar();

			Statusbar.Lives.Value = 3;
			Statusbar.Score.Value = 0;

			Statusbar.Element.AttachTo(InfoOverlay);

			var MenuFader = new DualFader { Value = Menu };

			#region common keys
			this.InvokeWhenStageIsReady(
				delegate
				{
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
				}
			);

			#endregion






			this.Ego = new PlayerShip(DefaultWidth, DefaultHeight)
				{
					Name = "Ego"
				};

			// addding our entities to list ensures we know under what id to send them
			this.Ego.GoodEgo.AddTo(this.SharedState.LocalObjects);
			this.Ego.EvilEgo.AddTo(this.SharedState.LocalObjects);

			// our ego cannot be hit while the menu is showing
			this.Ego.GodMode.ValueChangedTo +=
				GodMode => DebugDump.Write(new { GodMode });

			MenuFader.ValueChangedTo += e => this.Ego.GodMode.Value = e == Menu;
			
			this.Ego.GodMode.Value = true;


			#region input
			this.DoPlayerMovement.Direct +=
				(e, p) =>
				{
					e.GoodEgo.MoveToTarget.Value = p;
				};

			Action<double, double> DoEgoPlayerMovement =
				(arc, length) =>
					 this.DoPlayerMovement.Chained(Ego, Ego.GoodEgo.ToPoint().MoveToArc(arc, Ego.GoodEgo.MaxStep * length));

			this.InvokeWhenStageIsReady(
				delegate
				{

					var input = new PlayerInput(stage, Ego, this)
					{
						StepLeft = () => DoEgoPlayerMovement(Math.PI, 2),
						StepLeftEnd = () => DoEgoPlayerMovement(Math.PI, 0.5),

						StepRight = () => DoEgoPlayerMovement(0, 2),
						StepRightEnd = () => DoEgoPlayerMovement(0, 0.5),

						FireBullet = () => Ego.FireBullet(),

						SmartMoveTo = (x, y) =>
							this.DoPlayerMovement.Chained(Ego, new Point(Ego.Wrapper(x, y), Ego.GoodEgoY))

					};

					input.Enabled.ValueChangedTo +=
						InputEnabled =>
						{
							DebugDump.Write(new { InputEnabled });

						};

					this.Ego.GoodEgo.IsAlive.ValueChangedToFalse +=
						delegate
						{
							this.ApplyFilter(Filters.GrayScaleFilter);

							input.Enabled.Value = false;

							Statusbar.Lives.Value--;

							if (Statusbar.Lives > 0)
							{
								3000.AtDelayDo(
									delegate
									{
										this.Ego.GoodEgo.alpha = 1;

										this.filters = null;

										input.Enabled.Value = true;

										play(Sounds.insertcoin);
									}
								);
							}
							else
							{

								if (GameOver != null)
									GameOver();


							}
						};
				}
			);
			#endregion

			this.ComputerEnemies.Add(this.Ego.EvilEgo);

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

			this.Ego.GoodEgo.FireBullet = this.FireBullet;
			this.Ego.GoodEgo.AttachTo(CanvasOverlay);

			this.Ego.EvilEgo.FireBullet = this.FireBullet;
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

		
			Ego.AddTo(FragileEntities);

			#region Create and Move CoPlayer

			this.CreateCoPlayer.Direct =
				(user, handler) =>
				{
					var cp1 = new PlayerShip(DefaultWidth, DefaultHeight)
						{
							Name = "CoPlayer"
						}.AddTo(CoPlayers);

					cp1.GoodEgo.AttachTo(CanvasOverlay);
					cp1.EvilEgo.AttachTo(CanvasOverlay);

					// we are adding remote controlled objects
					cp1.GoodEgo.AddTo(this.SharedState.RemoteObjects[user]);
					cp1.EvilEgo.AddTo(this.SharedState.RemoteObjects[user]);

					// group as enemies
					cp1.EvilEgo.AddTo(this.ComputerEnemies);

					cp1.AddTo(FragileEntities);

					handler(cp1);

					// this entity only moves when that player wants to move...

					// yet we might need to notify of damage
				};

			this.MoveCoPlayer.Direct =
				(ego, p) =>
				{
					ego.GoodEgo.TweenMoveTo(p.x, p.y);
				};

			#endregion



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

			cloud1.Members.ForEach(
				m =>
				{
					// if a cloud member fires, it will go across network...
					m.Element.FireBullet = this.FireBullet;

					this.SharedState.SharedObjects.Add(m.Element);
				}
			);

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

					//DebugDump.Write(new { r.left, r.right, cloud1.FrontRow.Length });

					if (r.bottom > DefenseY)
					{
						ResetCloudSoon();

						return;
					}

					if (cloud1.Counter % 4 == 0)
					{
						// fire some bullets
						var rr = cloud1.FrontRow.Random();

						// invaders bullets should have different sound or be silent
						rr.Element.FireBulletChained(1, new Point(rr.Element.x, rr.Element.y), new Point(rr.Element.x, DefaultHeight), Ego.GoodEgoY);

						//rb.Silent = true;

						//AddBullet.Chained(
						//    rb
						//);
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
						CloudMove.y = 8;

						CloudSpeed *= CloudSpeedAcc;
					}
					else
					{
						if (WillStartVerticalMovement)
							CloudMove.y -= CloudSpeed / 2;
						else
						{

						}

						if (CloudMove.y <= 0)
						{
							CloudMove.y = 0;

							if (IsFarLeft)
								CloudMove.x = CloudSpeed;
							else if (IsFarRight)
								CloudMove.x = -CloudSpeed;
						}
					}

					//DebugDump.Write(new { CloudMove.x, CloudMove.y });

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

			#region FireBullet

			this.FireBullet.Direct =
				(StarShip starship, int Multiplier, Point From, Point To, double Limit, Action<BulletInfo> handler) =>
				{
					var bullet = new SpriteWithMovement();

					Multiplier = Multiplier.Max(1);

					for (int i = 1; i <= Multiplier; i++)
					{
						bullet.graphics.beginFill(Colors.Green);
						bullet.graphics.drawRect((i - Multiplier) * 2, -8, 1, 16);
					}


					bullet.StepMultiplier = 0.3;
					bullet.MaxStep = 24;

					if (From.y < To.y)
					{
						bullet.TeleportTo(From.x, From.y);
						bullet.TweenMoveTo(To.x + 0.00001, To.y);

						bullet.PositionChanged +=
							delegate
							{
								if (bullet.y > Limit)
									bullet.Orphanize();
							};
					}
					else
					{
						bullet.TeleportTo(From.x, From.y);
						bullet.TweenMoveTo(To.x + 0.00001, To.y);


						bullet.PositionChanged +=
							delegate
							{
								if (bullet.y < Limit)
									bullet.Orphanize();
							};
					}

					// it should not be null and provide the correct parent for the bullet
					if (starship == null)
						starship = this.Ego.ActiveEgo;

					var bulletp = new BulletInfo(bullet.WithParent(starship)) { Multiplier = Multiplier };

					// local only
					FragileEntities.AddBullet(bulletp);

					bulletp.Element.AttachTo(CanvasOverlay);
					bulletp.Element.removed +=
						delegate
						{
							FragileEntities.Bullets.Remove(bulletp);
						};

					if (!bulletp.Silent)
						play(Sounds.firemissile);

					if (handler != null)
						handler(bulletp);
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
						if (bullet.Parent == Ego.ActiveEgo)
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
						}

						play(target.GetDeathSound());
					}
					else
					{
						play(Sounds.shortwhite);
					}

					DebugDump.Write(
						new
						{
							From = bullet.Parent.Name,
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
					var GroupEvil = ComputerEnemies.ToArray();

					this.FragileEntities.Filter =
						(source, n) =>
						{
							// spare yourself
							var query = source;

							// spare coplayers in the same mode
							if (ComputerEnemies.Contains(n.Parent))
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
			//this.AddEnemy.BaseHandler += BaseHandler;
			////this.AddBullet.BaseHandler += BaseHandler;
			//this.DoPlayerMovement.BaseHandler += BaseHandler;
			//this.SetWeaponMultiplier.BaseHandler += BaseHandler;

		}






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
