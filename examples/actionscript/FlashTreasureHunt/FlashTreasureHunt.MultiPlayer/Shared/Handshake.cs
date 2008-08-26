using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace FlashTreasureHunt.Shared
{
	[Script]
	public class Handshake
	{
		public const int MagicValue1 = (int)0x1230BEEF;
		public const int MagicValue2 = (int)0x7F30DEAD;

		public int[] ToArray()
		{
			return new[]
			{
				MagicValue1,
				MagicValue2
			};
		}

		public void VerifyArray(int[] e)
		{
			var v = ToArray();

			if (v.Length != e.Length)
				throw new Exception("length mismatch was " + e.Length + ", expected " + v.Length);

			for (int i = 0; i < v.Length; i++)
			{
				var was = e[i];
				var expected = v[i];

				if (was != expected)
					throw new Exception("value mismatch was " + was + ", expected " + expected);

			}

		}
	}
}
