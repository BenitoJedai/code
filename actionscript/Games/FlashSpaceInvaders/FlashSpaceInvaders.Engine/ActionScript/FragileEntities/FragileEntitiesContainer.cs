using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.geom;
namespace FlashSpaceInvaders.ActionScript.FragileEntities
{
	[Script]
	public class FragileEntitiesContainer
	{
		public static implicit operator List<IFragileEntity>(FragileEntitiesContainer c)
		{
			return c.Items;
		}

		public readonly List<IFragileEntity> Items = new List<IFragileEntity>();

		public Action PrepareFilter;
		public Func<IEnumerable<IFragileEntity>, BulletInfo, IEnumerable<IFragileEntity>> Filter;

		public RoutedActionInfo<IFragileEntity, BulletInfo> AddDamage;

		int BulletHitTestCounter = 0;

		public void BulletHitTest(BulletInfo n)
		{
			BulletHitTestCounter++;

			var p = n.Element.ToPoint();


			PrepareFilter();

			var query = from x in Filter(Items, n)
					where x.HitPoints > 0
					let distance = (x.Location - p).length
					where distance <= x.HitRange
					orderby distance
					select x;

			//DebugDump(
			//    new { counter = BulletHitTestCounter, targets = query.Count() }
			//    );

			var v = query.FirstOrDefault();

			if (v != null)
			{
				AddDamage.Chained(v, n);

				n.Element.Orphanize();
			}
		}

		public void AddBullet(BulletInfo n)
		{
			n.AddTo(Bullets);


			var p = default(Point);

			n.Element.PositionChanged +=
				delegate
				{
					var k = n.Element.ToPoint();


					if ((k - p).length > 2)
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
	}
}
