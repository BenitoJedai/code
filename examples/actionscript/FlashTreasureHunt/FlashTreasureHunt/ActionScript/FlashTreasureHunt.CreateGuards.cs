using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using System.Collections.Generic;
using System;
using System.Linq;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Archive.Extensions;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.RayCaster;
using ScriptCoreLib.ActionScript.flash.filters;
using ScriptCoreLib.Shared.Maze;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.ActionScript.flash.utils;

namespace FlashTreasureHunt.ActionScript
{
	partial class FlashTreasureHunt
	{
		public event Action<int, Point> Sync_GuardWalkTo;
		public event Action<int, double> Sync_GuardLookAt;

		public Bitmap[] CachedGuardTextures;

		public Func<SpriteInfoExtended> CreateGuard;

		public readonly List<SpriteInfo> BloodSprites = new List<SpriteInfo>();

		public readonly List<SpriteInfoExtended> GuardSprites = new List<SpriteInfoExtended>();

		private void CreateGuards(IEnumerator<TextureBase.Entry> FreeSpaceForStuff)
		{
			CreateGuards(FreeSpaceForStuff.Select(kk => new Point(kk.XIndex + 0.5, kk.YIndex + 0.5)), 2 + CurrentLevel, true);
		}

		public const int ConstructorIndexForSync_Guards = 0x1000;

		public event Action<int, double, object> Sync_GuardAddDamage;
		public event Action<object> Sync_GuardKilled;

		public IEnumerable<SpriteInfoExtended> CreateGuards(IEnumerator<Point> FreeSpaceForStuff, int Count, bool DoAttachGuardLogic)
		{
			for (int i = 0; i < Count; i++)
			{
				var g = CreateGuard();

				GuardSprites.Add(g);

				EgoView.BlockingSprites.Add(g);

				g.ConstructorIndexForSync = i + ConstructorIndexForSync_Guards;
				g.Position = FreeSpaceForStuff.Take();
				g.Direction = 0;

				g.TakeDamage +=
					(damage, DamageOwner) =>
					{
						if (Sync_GuardAddDamage != null)
							Sync_GuardAddDamage(g.ConstructorIndexForSync, damage, DamageOwner);
					};

				// state machine for AI guard

				if (DoAttachGuardLogic)
					AttachGuardLogic(g);


			}

			return GuardSprites;
		}

		private void AttachGuardLogic(SpriteInfoExtended g)
		{
			var TurnAndWalk = default(Action);
			var WalkToQueue = new Queue<Point>();

			TurnAndWalk =
				delegate
				{
					if (g.Health <= 0)
						return;

					if (!this.EgoView.Sprites.Contains(g))
						return;

					if (WalkToQueue.Count > 0)
					{
						var p = WalkToQueue.Dequeue();

						g.WalkTo(p.x, p.y);

						if (Sync_GuardWalkTo != null)
							Sync_GuardWalkTo(g.ConstructorIndexForSync, p);

						return;
					}

					var PossibleDestination = g.Position.MoveToArc(g.Direction, 1);

					var AsMapLocation = new PointInt32
					{
						X = PossibleDestination.x.Floor(),
						Y = PossibleDestination.y.Floor()
					};

					if (EgoView.Map.WallMap[AsMapLocation.X, AsMapLocation.Y] == 0)
					{
						WalkToQueue.Enqueue(g.Position.MoveToArc(g.Direction, 0.2));
						WalkToQueue.Enqueue(g.Position.MoveToArc(g.Direction, 0.4));
						WalkToQueue.Enqueue(g.Position.MoveToArc(g.Direction, 0.6));
						WalkToQueue.Enqueue(g.Position.MoveToArc(g.Direction, 0.8));
						WalkToQueue.Enqueue(g.Position.MoveToArc(g.Direction, 1.0));
					}
					else
					{
						g.Direction += 90.DegreesToRadians();

						if (Sync_GuardLookAt != null)
							Sync_GuardLookAt(g.ConstructorIndexForSync, g.Direction);

					}

					3000.AtDelayDo(TurnAndWalk);
				};

			g.WalkToDone +=
				delegate
				{
					TurnAndWalk();
				};

			g.WalkToTeleported +=
				delegate
				{
					TurnAndWalk();
				};

			TurnAndWalk();
		}



		public SpriteInfoExtended CreateWalkingDummy(Texture64[] _Stand, Texture64[][] _Walk, Texture64[] Hit, Texture64[] Death, Texture64[] Shooting)
		{
			var Walk = _Walk;
			var Stand = _Stand;

			var tt = default(Timer);
			var s = new SpriteInfoExtended();

			s.Health = GuardMaxHealth;

			int WalkingAnimationFrame = 0;

			Action start =
				delegate
				{
					s.WalkingAnimationRunning = true;

					if (Walk.Length > 0)
						tt = (200).AtInterval(
							t =>
							{
								#region dead people do not walk
								if (s.Health <= 0)
								{
									t.stop();
									return;
								}
								#endregion


								if (!s.AIEnabled)
									return;

								WalkingAnimationFrame = t.currentCount % Walk.Length;

								s.Frames = Walk[WalkingAnimationFrame];
							}
						);
				};

			Action stop =
				delegate
				{
					s.WalkingAnimationRunning = false;

					if (tt != null)
						tt.stop();

					s.Frames = Stand;
				};


			s.Position = new Point { x = EgoView.ViewPositionX, y = EgoView.ViewPositionY };
			s.Frames = Stand;
			s.Direction = EgoView.ViewDirection;
			s.StartWalkingAnimation = start;
			s.StopWalkingAnimation = stop;
			s.Range = PlayerRadiusMargin;

			s.AddTo(EgoView.Sprites);

			Action UpdateCurrentFrame =
				delegate
				{
					if (s.Health <= 0)
						return;
					if (!s.AIEnabled)
						return;

					if (s.WalkingAnimationRunning)
						s.Frames = Walk[WalkingAnimationFrame];
					else
						s.Frames = Stand;
				};

			if (Shooting != null)
			{
				#region prepare
				#region clone

				var WalkShooting = Enumerable.ToArray(
					_Walk.Select(r0 => Enumerable.ToArray(r0))
				);

				var StandShooting = Enumerable.ToArray(
					_Stand
				);

				#endregion

				// 0 .. 8
				Action<int> ApplyShootingFrame =
					ShootingFrameIndex =>
					{
						var k = Shooting.AtModulus(ShootingFrameIndex);

						StandShooting[1] = k;
						StandShooting[2] = k;
						StandShooting[3] = k;

						foreach (var v in WalkShooting)
						{
							v[1] = k;
							v[2] = k;
							v[3] = k;
						}

						UpdateCurrentFrame();
					};
				#endregion

				var PlayShootingAnimationTimer = default(Timer);

				s.PlayShootingAnimation +=
					delegate
					{
						// for a short period of time we need to overwrite
						// stand and walk frames

						Walk = WalkShooting;
						Stand = StandShooting;

						if (PlayShootingAnimationTimer != null)
							PlayShootingAnimationTimer.stop();

						PlayShootingAnimationTimer = FrameRate_ShootingAnimation.Chain(
							() => ApplyShootingFrame(0)
						).Chain(
							() => ApplyShootingFrame(1)
						).Chain(
							() => ApplyShootingFrame(2)
						).Chain(
							() => ApplyShootingFrame(1)
						).Chain(
							() => ApplyShootingFrame(0)
						).Chain(
							delegate
							{
								// revert soon after animation finishes
								Walk = _Walk;
								Stand = _Stand;
								UpdateCurrentFrame();

								PlayShootingAnimationTimer = null;
							}
						).Do();
					};
			}

			if (Hit != null)
				s.TakeDamage +=
					(DamageToBeTaken, DamageOwner) =>
					{
						// we have nothing to do here if we are dead 
						if (s.Health < 0)
							return;

						s.Health -= DamageToBeTaken;

						if (s.Health > 0)
						{
							Assets.Default.Sounds.hit.play();

							#region just show being hurt for a short moment
							s.AIEnabled = false;

							var q = s.Frames;
							s.Frames = Hit;

							300.AtDelayDo(
								delegate
								{
									s.Frames = q;
									s.AIEnabled = true;
								}
							);
							#endregion

							// remove old blood which is too near
							EmitBloodUnderSprite(s);
						}
						else
						{
							if (Sync_GuardKilled != null)
								Sync_GuardKilled(DamageOwner);

							RemoveBloodUnderSprite(s);

							Assets.Default.Sounds.death.play();

							// player wont be blocked by a corpse
							s.Range = 0;

							// animate death
							FrameRate_DeathAnimation.AtInterval(
								ttt =>
								{
									if (Death.Length == ttt.currentCount)
									{
										ttt.stop();
										return;
									}

									s.Frames = new[] { Death[ttt.currentCount] };
								}
							);
						}

						if (s.TakeDamageDone != null)
							s.TakeDamageDone(DamageToBeTaken, DamageOwner);

					};


			return s;
		}

		private void EmitBloodUnderSprite(SpriteInfoExtended s)
		{
			var old_blood = RemoveBloodUnderSprite(s);

			var blood_source = "blood_small.png";

			if (old_blood > 1)
				blood_source = "blood.png";

			if (s.Health < GuardCriticalHealth)
				blood_source = "blood.png";

			var blood = CreateDummy(this.StuffDictionary[blood_source]);

			blood.AddTo(BloodSprites);

			// move blood under target
			blood.Position.To(s.Position.x, s.Position.y);

			this.EgoView.UpdatePOV(true);
		}

		private int RemoveBloodUnderSprite(SpriteInfoExtended s)
		{
			var old_blood = 0;

			foreach (var k in BloodSprites.Where(k => k.Position.GetDistance(s.Position) < PlayerRadiusMargin * 2))
			{
				k.RemoveFrom(BloodSprites).RemoveFrom(this.EgoView.Sprites);

				old_blood++;
			}
			return old_blood;
		}



		public SpriteInfoExtended CreateDummy(Texture64 Stand)
		{
			return CreateWalkingDummy(new[] { Stand }, null, null, null, null);

		}
	}
}