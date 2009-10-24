using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Drawing;

namespace ScriptCoreLibJava.BCLImplementation.System.Drawing
{
	[Script(Implements = typeof(global::System.Drawing.Color))]
	internal class __Color
	{
		byte _R;
		byte _G;
		byte _B;

		public byte R
		{
			get {
				var e = _R;
				return e;
			}
		}

		public byte G
		{
			get {
				var e = _G;
				return e; }
		}

		public byte B
		{
			get { var e = _B;  return e; }
		}

		public __Color()
		{

		}


		public static Color FromArgb(int red, int green, int blue)
		{
			return new __Color { 
				_R = (byte)red,
				_G = (byte)green,
				_B = (byte)blue
			};
		}

		static __Color()
		{
			Green = FromArgb(0, 0xff, 0);
			Red = FromArgb(0xff, 0, 0);
			Yellow = FromArgb(0xff, 0xff, 0);
		}

		static public implicit operator Color(__Color e)
		{
			return (Color)(object)e;
		}

		static public implicit operator __Color(Color e)
		{
			return (__Color)(object)e;
		}

		static public Color Green { get; set; }
		static public Color Red { get; set; }
		static public Color Yellow { get; set; }
	}
}
