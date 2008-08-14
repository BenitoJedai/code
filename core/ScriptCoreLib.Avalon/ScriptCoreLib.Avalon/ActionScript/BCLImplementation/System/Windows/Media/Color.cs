using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Media
{
	[Script(Implements = typeof(global::System.Windows.Media.Color))]
	internal class __Color
	{
		[Script]
		internal class __MILColor
		{
			public byte a;
			public byte r;
			public byte g;
			public byte b;
		}

		private __MILColor sRgbColor = new __MILColor();


		public byte A { get { return sRgbColor.a; } set { sRgbColor.a = value; } }

		public static implicit operator global::System.Windows.Media.Color(__Color e)
		{
			return (Color)(object)e;
		}

		public static implicit operator __Color(Color e)
		{
			return (__Color)(object)e;
		}

		public static implicit operator __Color(uint argb)
		{
			__Color color = new __Color();
			color.sRgbColor.a = (byte)((argb & 0xff000000) >> 0x18);
			color.sRgbColor.r = (byte)((argb & 0xff0000) >> 0x10);
			color.sRgbColor.g = (byte)((argb & 0xff00) >> 8);
			color.sRgbColor.b = (byte)(argb & 0xff);

			return color;
		}

		public static implicit operator uint(__Color e)
		{
			uint c = 0;

			c += (uint)(e.sRgbColor.a << 0x18);
			c += (uint)(e.sRgbColor.r << 0x10);
			c += (uint)(e.sRgbColor.g << 0x8);
			c += (uint)(e.sRgbColor.b << 0x0);

			return c;
		}

	}
}
