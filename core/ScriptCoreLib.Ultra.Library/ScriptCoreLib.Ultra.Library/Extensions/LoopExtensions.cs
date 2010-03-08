using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra.Library.Extensions
{
	public static class LoopExtensions
	{
		public delegate void ForCallback(int i);

		public static void For(int from, int to, ForCallback h)
		{
			for (int i = from; i < to; i++)
			{
				h(i);
			}
		}
	}
}
