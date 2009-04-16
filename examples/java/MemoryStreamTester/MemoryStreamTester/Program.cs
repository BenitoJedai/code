using System.Threading;
using System;

using ScriptCoreLib;
using System.IO;


namespace MemoryStreamTester
{
	[Script]
	public class Program
	{
		[Script]
		public class CAPDU
		{
			public byte CLA { set; get; }
			public byte INS { set; get; }

			public byte P1 { set; get; }
			public byte P2 { set; get; }

			public byte[] Data { get; set; }

			public byte Le { set; get; }

			public static implicit operator byte[](CAPDU e)
			{
				var m = new MemoryStream();

				m.WriteByte(e.CLA);
				m.WriteByte(e.INS);
				m.WriteByte(e.P1);
				m.WriteByte(e.P2);
				m.WriteByte((byte)e.Data.Length);
				m.Write(e.Data, 0, e.Data.Length);
				m.WriteByte(e.Le);

				return m.ToArray();
			}
		}

		public static void Main(string[] args)
		{
			Console.WriteLine("pwd: " + Environment.CurrentDirectory);

			var bytes = File.ReadAllBytes("data.bin");

			var copy = new byte[bytes.Length + 2];

			var m = new MemoryStream();

			m.Write(bytes, 0, bytes.Length);

			m.WriteByte(0x01);
			m.WriteByte(0xF7);

			Console.WriteLine("bytes: " + m.Length);
			foreach (var u in m.ToArray())
			{
				Console.Write(u + " ");
			}
			Console.WriteLine();

			File.WriteAllBytes("data.bin", m.ToArray());

			File.WriteAllBytes("GetFiles.apdu",
				new CAPDU
				{
					CLA = 0x00,
					INS = 0xFE,
					P1 = 1,
					P2 = 2,
					Data = new byte[] { 0, 3 }

				}
			);


		}
	}
}
