using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;

using FlashSpaceInvaders.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.media;
using ScriptCoreLib.ActionScript.flash.utils;
using ScriptCoreLib.ActionScript.flash.geom;

namespace FlashSpaceInvaders.ActionScript.StarShips
{
	[Script]
	public class EnemyCloud
	{
		[Script]
		public class Member : ParentRelation<StarShip, EnemyCloud>
		{
			public int x;
			public int y;

			public Member(StarShip Element, EnemyCloud Parent, int x, int y)
			{
				this.Element = Element;
				this.Parent = Parent;
				this.x = x;
				this.y = y;
			}
		}

		public double Speed = 12.0;
		public double SpeedAcc = 1.039;
		public Point NextMove = new Point();


		const int DefaultCloudWidth = 9;
		const int DefaultCloudHeight = 5;

		//const int DefaultCloudWidth = 2;
		//const int DefaultCloudHeight = 2;

		public const int DefaultCloudMargin = 32;

		public readonly List<Member> Members = new List<Member>();

		public Action<Sound> PlaySound;
		public Sound[] TickSounds;
		
		public int Counter = 0;


		public EnemyCloud()
		{
			Action<int, Func<int, StarShip>> Spawn =
				(y, ctor) =>
				{
					for (int x = 0; x < DefaultCloudWidth; x++)
					{
						var n = ctor(y);

						n.MaxStep = DefaultCloudMargin / 2;
						Members.Add(new Member(n, this, x, y));
					}
				};


			var colors = new[]
			{
				Filters.ColorFillFilter(0xffffff.Random()),
				Filters.ColorFillFilter(0xffffff.Random()),
				Filters.ColorFillFilter(0xffffff.Random()),
				Filters.ColorFillFilter(0xffffff.Random()),
				Filters.ColorFillFilter(0xffffff.Random()),

			};

			var factory = new Func<StarShip>[]
				{
					() => new EnemyA(),
					() => new EnemyB(),
					() => new EnemyB(),
					() => new EnemyC(),
					() => new EnemyC(),
				};

			for (int i = 0; i < DefaultCloudHeight; i++)
			{
				Spawn(i, y => factory[y]().ApplyFilter(colors[y]));
				
			}


			var Timer = default(Timer);

			Action InternalTick =
				delegate
				{
					Counter++;

					if (PlaySound != null)
						if (TickSounds != null)
							PlaySound(TickSounds[Counter  % TickSounds.Length]);

					if (this.Tick != null)
						this.Tick();
				};

			Action Reset =
				delegate
				{
					Timer = TickInterval.Value.AtInterval(
						t =>
						{
							InternalTick();
					
						}
					);

					InternalTick();

				};


			TickInterval.ValueChangedTo +=
				e =>
				{
					if (Timer != null)
						Timer.stop();

					if (e > 0)
						Reset();
				};

		}

		public void AttachTo(DisplayObjectContainer c)
		{
			foreach (var v in Members)
			{
				v.Element.AttachTo(c);
			}
		}

		public void TeleportTo(double x, double y)
		{
			foreach (var v in Members)
			{
				v.Element.TeleportTo(x + DefaultCloudMargin * v.x, y + DefaultCloudMargin * v.y);
			}
		}


		public void ResetColors()
		{
			var colors = new[]
			{
				Filters.ColorFillFilter(0xffffff.Random()),
				Filters.ColorFillFilter(0xffffff.Random()),
				Filters.ColorFillFilter(0xffffff.Random()),
				Filters.ColorFillFilter(0xffffff.Random()),
				Filters.ColorFillFilter(0xffffff.Random()),

			};

			foreach (var v in Members)
			{
				v.Element.ApplyFilter(colors[v.y]);
			}
		}


		public void ResetLives()
		{

			foreach (var v in Members)
			{
				v.Element.alpha = 1;
			}
		}

		public readonly Int32Property TickInterval = 0;

		public event Action Tick;

		public double Direction;

		public void MoveToOffset(Point p)
		{
			var x = p.x;
			var y = p.y;

			foreach (var v in Members)
			{
				v.Element.TweenMoveTo(v.Element.MoveToTarget.Value.x + x, v.Element.MoveToTarget.Value.y + y);
			}
		}

		public Rectangle Warzone
		{
			get
			{
				var r = default(Rectangle);



				foreach (var item in Members)
				{
					if (item.Element.HitPoints > 0)
						if (r == null)
							r = new Rectangle(item.Element.x, item.Element.y, 0, 0);
						else
						{
							r.left = item.Element.x.Min(r.left);
							r.top = item.Element.y.Min(r.top);

							r.right = item.Element.x.Max(r.right);
							r.bottom = item.Element.y.Max(r.bottom);
						}
				}

				return r;
			}
		}




		public void Stop()
		{
			foreach (var v in Members)
			{
				v.Element.TeleportTo(v.Element.x, v.Element.y);
			}
		}

		public Member[] FrontRow
		{
			get
			{
				var a = new List<Member>();

				for (int i = 0; i < DefaultCloudWidth; i++)
				{
					var p = Enumerable.LastOrDefault(
						from m in Members
						where m.Element.HitPoints > 0
						where m.x == i
						//orderby m.y descending
						select m
					);

					if (p != null)
						a.Add(p);
				}

				return a.ToArray();
			}
		}
	}
}
