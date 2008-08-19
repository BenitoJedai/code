using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace FlashMouseMaze.Shared
{
	[Script]
	public static class MySharedExtensions
	{
		public static int GetValueInt32(this int[][] e, int x, int y)
		{
			if (x < 0)
				throw new Exception("x = " + x);

			if (x >= e.Length)
				throw new Exception("x = " + x + " e = " + e.Length);

			var f = e[x];

			if (y < 0)
				throw new Exception("y = " + y);

			if (y >= f.Length)
				throw new Exception("y = " + y + " f = " + f.Length);

			return f[y];
		}
	}
}
