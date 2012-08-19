using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ScriptCoreLib.Extensions
{
	public static class StreamExtensions
	{
        public static byte[] ReadToEnd(this Stream s)
        {
            var x = new byte[s.Length - s.Position];

            s.Read(x, 0, x.Length);

            return x;
        }

		public static byte[] ToBytes(this Stream s)
		{
			var x = new byte[s.Length];

            // what will we break if we fix this?
            s.Position = 0;
			s.Read(x, 0, x.Length);

			return x;
		}

        public static int ReadByteAtPosition(this Stream s, long p)
        {
            // cant go below zero. check your data.
            if (p < 0)
                return -1;

            s.Position = p;

            return s.ReadByte();
        }
	}
}

