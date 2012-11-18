using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.geom;

namespace ScriptCoreLib.ActionScript.RayCaster.Extensions
{
    [Script]
    internal static class MyExtensions
    {
		static readonly Rectangle fillRect_rect = new Rectangle();

		public static void fillRect(this BitmapData e, int x, int y, int w, int h, uint color)
		{
			fillRect_rect.x = x;
			fillRect_rect.y = y;
			fillRect_rect.width = w;
			fillRect_rect.height = h;

			e.fillRect(fillRect_rect, color);
		}

        public static int Floor(this double e)
        {
            return (int)e;
        }
    }
}
