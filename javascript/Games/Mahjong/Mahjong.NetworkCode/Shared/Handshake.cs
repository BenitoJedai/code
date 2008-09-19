using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace Mahjong.NetworkCode.Shared
{
	[Script]
	public class Handshake
	{
		const byte Count = 0xA0;

		public readonly int[] Bytes = new int[Count];

		public Handshake()
		{
			for (int i = 0; i < Count; i++)
			{
				Bytes[i] = i ^ Count;
			}
		}

	

		public void Verify(int[] e)
		{
			if (e.Length != Count)
				throw new Exception("invalid handshake length : " + e.Length);

			for (int i = 0; i < Count; i++)
			{
				if (e[i] != Bytes[i])
					throw new Exception("invalid handshake at : " + i);
			}
		}
	}
}
