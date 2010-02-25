using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UltraTutorial08
{
	public static class MyExtensions
	{
		static Random InternalRandom = new Random();
		public static int Random(this int e)
		{
			return InternalRandom.Next(e);
		}
	}
}
