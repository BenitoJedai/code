﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;

using FlashSpaceInvaders.ActionScript.Extensions;

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

		const int DefaultWidth = 9;
		const int DefaultHeight = 5;
		const int DefaultMargin = 32;

		public readonly List<Member> Members = new List<Member>();

		public EnemyCloud()
		{
			Action<int, Func<int, StarShip>> Spawn =
				(y, ctor) =>
				{
					for (int x = 0; x < DefaultWidth; x++)
						Members.Add(new Member(ctor(y), this, x, y));
				};


			var colors = new []
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
				v.Element.TeleportTo(x + DefaultMargin * v.x, y + DefaultMargin * v.y);
			}
		}
	}
}
