using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Media
{
	[Script(Implements = typeof(global::System.Windows.Media.Colors))]
	internal static class __Colors
	{

		public static Color Transparent { get { return (__Color)0x00ffffff; } }
		public static Color Red { get { return (__Color)0xffff0000; } }
		public static Color White { get { return (__Color)0xffffffff; } }
		public static Color Black { get { return (__Color)0xff000000; } }
		public static Color GreenYellow { get { return (__Color)0xFFADFF2F; } }
		public static Color Blue { get { return (__Color)0xff0000ff; } }
		public static Color Gray { get { return (__Color)0xff808080; } }
		public static Color Green { get { return (__Color)0xff008000; } }
		public static Color Magenta { get { return (__Color)0xFFFF00FF; } }
		public static Color Cyan { get { return (__Color)0xFF00FFFF; } }
		public static Color Yellow { get { return (__Color)0xFFFFFF00; } }
		public static Color Brown { get { return (__Color)0xFFA52A2A; } }
	}
}
