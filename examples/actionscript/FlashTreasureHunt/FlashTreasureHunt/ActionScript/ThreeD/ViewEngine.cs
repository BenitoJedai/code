using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.RayCaster;
using ScriptCoreLib.ActionScript.flash.geom;

namespace FlashTreasureHunt.ActionScript.ThreeD
{
	[Script]
	public partial class ViewEngine : ViewEngineBase
	{
		int HorizonStep = 4;

		int HorizonGradientCount;


		uint[] HorizonGradientUpper;

		uint[] HorizonGradientLower;

		const double PlayerRadiusMargin = FlashTreasureHunt.PlayerRadiusMargin;

		public ViewEngine(int w, int h)
			: base(w, h)
		{
			this.RenderOverlay += DrawMinimap;

			Func<byte, byte, int, Func<int, uint>> f =
				(g0, g1, max) =>
				{
					return
						i =>
						{
							var c = (g1 + (g0 - g1) * i / max).Min(255).Max(0);

							return (uint)((c << 16) + (c << 8) + c);
						};
				};

			HorizonGradientCount = h / 2 / HorizonStep + 1;

			HorizonGradientUpper = Enumerable.Range(0, HorizonGradientCount).Select(
				f(0x30, 0x50, HorizonGradientCount)
			).ToArray();

			HorizonGradientLower = Enumerable.Range(0, HorizonGradientCount).Select(
				f(0x90, 0x50, HorizonGradientCount)
			).ToArray();

		}

		public override void RenderHorizon()
		{
			var r = new Rectangle { width = _ViewWidth };

			Action<int, int, uint> fill =
				(y, h, c) =>
				{
					r.y = y;
					r.height = h;

					buffer.fillRect(r, c);
				};

			//fill(0, _ViewHeight / 3, 0x404040);
			//fill(_ViewHeight / 3, _ViewHeight / 3, 0x202020);
			//fill(_ViewHeight * 2 / 3, _ViewHeight / 3, 0x808080);

			for (int i = 0; i < HorizonGradientCount; i++)
				fill(i * HorizonStep, HorizonStep, HorizonGradientUpper[i]);

			for (int i = 0; i < HorizonGradientCount; i++)
				fill(i * HorizonStep + _ViewHeight / 2, HorizonStep, HorizonGradientLower[i]);
		}


	}
}
