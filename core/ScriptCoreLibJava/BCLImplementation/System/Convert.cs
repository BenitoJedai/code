using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;
using System.IO;

namespace ScriptCoreLibJava.BCLImplementation.System
{
	[Script(Implements = typeof(global::System.Convert))]
	internal class __Convert
	{
		public static long ToInt64(double e)
		{
			return (long)Math.Round(e);
		}

		public static int ToInt32(long e)
		{
			return (int)e;
		}

		// what about this : public static int ToInt32(int e)

		public static int ToInt32(uint e)
		{
			int x = (int)e;

			return x;
		}

		public static int ToInt32(double e)
		{
			return (int)Math.Floor((double)e);
		}

		internal readonly static string Base64Key = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";




		public static string ToBase64String(byte[] input)
		{
			var w = new StringBuilder();

			int chr1, chr2, chr3;
			int enc1, enc2, enc3, enc4;
			int i = 0;
			var length = input.Length;

			bool b = true;

			if (i < length)
				while (b)
				{
					enc3 = 64;
					enc4 = 64;

					chr1 = input[i++];
					enc1 = chr1 >> 2;
					enc2 = ((chr1 & 3) << 4);

					if (i < length)
					{
						chr2 = input[i++];
						enc2 |= (chr2 >> 4);
						enc3 = ((chr2 & 15) << 2);
					}

					if (i < length)
					{
						chr3 = input[i++];
						enc3 |= (chr3 >> 6);
						enc4 = chr3 & 63;
					}

					w.Append(Base64Key[enc1]);
					w.Append(Base64Key[enc2]);
					w.Append(Base64Key[enc3]);
					w.Append(Base64Key[enc4]);



					b = i < length;
				}

			return w.ToString();
		}

		public static byte[] FromBase64String(string input)
		{
			if (string.IsNullOrEmpty(input))
				return new byte[0];

			var m = new MemoryStream();

			var length = input.Length;

			int chr1, chr2, chr3;
			int enc1, enc2, enc3, enc4;

			int i = 0;

			bool b = true;

			if (i < length)
				while (b)
				{
					enc1 = 64;
					enc2 = 64;
					enc3 = 64;
					enc4 = 64;

					if (i < length)
						enc1 = Base64Key.IndexOf(input[i++]);
					if (i < length)
						enc2 = Base64Key.IndexOf(input[i++]);
					if (i < length)
						enc3 = Base64Key.IndexOf(input[i++]);
					if (i < length)
						enc4 = Base64Key.IndexOf(input[i++]);

					chr1 = (enc1 << 2) | (enc2 >> 4);
					chr2 = ((enc2 & 15) << 4) | (enc3 >> 2);
					chr3 = ((enc3 & 3) << 6) | enc4;

					m.WriteByte((byte)chr1);

					if (enc3 != 64)
					{
						m.WriteByte((byte)chr2);
					}
					if (enc4 != 64)
					{
						m.WriteByte((byte)chr3);
					}

					b = i < length;
				}

			return m.ToArray();
		}
	}
}
