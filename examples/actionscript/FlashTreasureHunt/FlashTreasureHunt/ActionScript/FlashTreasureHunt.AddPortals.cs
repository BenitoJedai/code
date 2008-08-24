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

namespace FlashTreasureHunt.ActionScript
{
	partial class FlashTreasureHunt
	{
		[Script]
		class DualPortal
		{
			public PortalInfo Orange;
			public PortalInfo Blue;
		}

		List<DualPortal> DualPortals = new List<DualPortal>();
		List<PortalInfo> Portals = new List<PortalInfo>();

		public void AddPortals()
		{
			ResetPortals();



			(1000 / 5).AtInterval(
				tt =>
				{
					if (EgoView.SpritesFromPointOfView == null)
						return;

					if (Portals.Count > 0)
					{
						var p = Portals.AtModulus(tt.currentCount);
						var s = EgoView.SpritesFromPointOfView.SingleOrDefault(k => k.Sprite == p.Sprite);

						if (s != null)
							if (s.ViewInfo.IsInView)
							{
								p.View.RenderLowQualityWalls = EgoView.RenderLowQualityWalls;
								p.Update();
							}
					}
				}
			);

			bool PortalsInactiveForEgo = false;

			var LastPosition = new Point();


			EgoView.ViewPositionChanged +=
				delegate
				{
					if (PortalsInactiveForEgo)
						return;

					// only check for items each ~ distance travelled
					if ((EgoView.ViewPosition - LastPosition).length < 0.3)
						return;

					foreach (var Portal in Portals)
					{
						var p = EgoView.SpritesFromPointOfView.SingleOrDefault(i => i.Sprite == Portal.Sprite);


						if (p != null)
						{
							if (p.Distance < Portal.Sprite.Range)
							{
								// we are going thro the portal, show it

								new Bitmap(EgoView.Buffer.clone())
								{
									scaleX = DefaultScale,
									scaleY = DefaultScale
								}.AttachTo(this).FadeOutAndOrphanize(1000 / 24, 0.2);

								Assets.Default.Sounds.teleport.play();

								// fixme: should use Ego.MovementDirection instead
								// currently stepping backwarads into the portal will behave recursivly
								EgoView.ViewPosition = Portal.View.ViewPosition; //.MoveToArc(EgoView.ViewDirection, Portal.Sprite.Range + p.Distance);
								EgoView.ViewDirection = Portal.View.ViewDirection;

								PortalsInactiveForEgo = true;

								10000.AtDelayDo(
									delegate
									{
										PortalsInactiveForEgo = false;
									}
								);

								break;
							}
						}
					}

					LastPosition = EgoView.ViewPosition;


				};

			//EgoView.ViewDirectionChanged += () => Portals.ForEach(Portal => Portal.View.ViewDirection = Portal.Sprite.Direction);


		}

		private void ResetPortals()
		{
			DualPortals.Clear();
			Portals.Clear();

			// first level does not get a portal
			if (CurrentLevel > 1)
				AddNextDualPortal();

			if (CurrentLevel > 4)
				AddNextDualPortal();

			if (CurrentLevel > 8)
				AddNextDualPortal();

			UpdatePortalPositions(FreeSpaceForPortals.GetEnumerator());

			foreach (var Portal in Portals)
			{
				Portal.View.Map.WorldMap = EgoView.Map.WorldMap;
				Portal.View.Map.Textures = EgoView.Map.Textures;
				Portal.View.Sprites = EgoView.Sprites;
				Portal.AlphaMask = StuffDictionary["portalmask.png"];
			}
		}

		void UpdatePortalPositions(IEnumerator<TextureBase.Entry> PortalPositions)
		{
			//WriteLine("portals: ");
			foreach (var v in DualPortals)
			{
				var PortalAPos = PortalPositions.Take().Do(u => new Point { x = u.XIndex + 0.5, y = u.YIndex + 0.5 });
				var PortalBPos = PortalPositions.Take().Do(u => new Point { x = u.XIndex + 0.5, y = u.YIndex + 0.5 });

				//WriteLine("A: " + new { PortalAPos.x, PortalAPos.y });
				//WriteLine("B: " + new { PortalBPos.x, PortalBPos.y });

				var PortalADir = GetGoodDirection(PortalAPos);
				var PortalBDir = GetGoodDirection(PortalBPos);


				v.Orange.ViewVector = new Vector { Direction = 0, Position = new Point() };
				v.Orange.ViewVector = new Vector { Direction = PortalADir, Position = PortalAPos };
				v.Orange.SpriteVector = new Vector { Direction = PortalBDir, Position = PortalBPos };

				v.Blue.ViewVector = new Vector { Direction = 0, Position = new Point() };
				v.Blue.ViewVector = v.Orange.SpriteVector;
				v.Blue.SpriteVector = v.Orange.ViewVector;
			}

		}

		private void AddNextDualPortal()
		{

			//WriteLine("Orange View: " + PortalADir.RadiansToDegrees());
			//WriteLine("Blue View: " + PortalBDir.RadiansToDegrees());


			#region create a dual portal
			var PortalA = new PortalInfo
			{
				Color = 0xFF6A00,

			}.AddTo(Portals);

			EgoView.Sprites.Add(PortalA.Sprite);


			var PortalB = new PortalInfo
			{
				Color = 0xff00,

			}.AddTo(Portals);


			EgoView.Sprites.Add(PortalB.Sprite);
			#endregion

			DualPortals.Add(new DualPortal { Orange = PortalA, Blue = PortalB });

		}

		public IEnumerable<TextureBase.Entry> FreeSpaceForPortals
		{
			get
			{
				return EgoView.Map.WallMap.Entries.Where(
					i =>
					{
						if (i.Value != 0)
							return false;

						var c = 0;

						if (EgoView.Map.WallMap[i.XIndex - 1, i.YIndex] == 0)
							c++;

						if (EgoView.Map.WallMap[i.XIndex + 1, i.YIndex] == 0)
							c++;

						if (EgoView.Map.WallMap[i.XIndex, i.YIndex + 1] == 0)
							c++;

						if (EgoView.Map.WallMap[i.XIndex, i.YIndex - 1] == 0)
							c++;

						return c == 1;
					}
				);
			}
		}

	}
}