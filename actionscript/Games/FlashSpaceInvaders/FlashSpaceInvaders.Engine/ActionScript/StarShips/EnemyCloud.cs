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

		const int DefaultCloudWidth = 9;
		const int DefaultCloudHeight = 5;
		const int DefaultCloudMargin = 32;

		public readonly List<Member> Members = new List<Member>();

		public Action<Sound> PlaySound;
		public Sound[] TickSounds;


		public EnemyCloud()
		{
			Action<int, Func<int, StarShip>> Spawn =
				(y, ctor) =>
				{
					for (int x = 0; x < DefaultCloudWidth; x++)
						Members.Add(new Member(ctor(y), this, x, y));
				};


			var colors = new[]
			{
				Filters.ColorFillFilter(0xffffff.Random()),
				Filters.ColorFillFilter(0xffffff.Random()),
				Filters.ColorFillFilter(0xffffff.Random()),
				Filters.ColorFillFilter(0xffffff.Random()),
				Filters.ColorFillFilter(0xffffff.Random()),

			};

			Spawn(0, y => new EnemyA().ApplyFilter(colors[y]));
			Spawn(1, y => new EnemyB().ApplyFilter(colors[y]));
			Spawn(2, y => new EnemyB().ApplyFilter(colors[y]));
			Spawn(3, y => new EnemyC().ApplyFilter(colors[y]));
			Spawn(4, y => new EnemyC().ApplyFilter(colors[y]));

			var Timer = default(Timer);

			Action Reset =
				delegate
				{
					Timer = TickInterval.Value.AtInterval(
						t =>
						{
							if (PlaySound != null)
								if (TickSounds != null)
									PlaySound(TickSounds[t.currentCount % TickSounds.Length]);

							if (this.Tick != null)
								this.Tick();
						}
					);
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
					if (r == null)
						r = new Rectangle(item.Element.x, item.Element.y, 0, 0);
					else
					{
						r.left = item.Element.x.Min(r.left);
						r.top = item.Element.y.Min(r.top);

						r.right = item.Element.x.Max(r.left);
						r.bottom = item.Element.y.Max(r.top);
					}
				}
		
				return r;
			}
		}


		
	}
}
