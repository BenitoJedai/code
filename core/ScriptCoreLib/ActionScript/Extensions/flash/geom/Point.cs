using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.utils;
using ScriptCoreLib.ActionScript.flash.events;
using ScriptCoreLib.ActionScript.mx.core;
using ScriptCoreLib.ActionScript.flash.media;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.geom;


namespace ScriptCoreLib.ActionScript.Extensions.flash.geom
{
	[Script(Implements = typeof(Point))]
	internal class __Point
	{

		public static implicit operator Point(__Point e)
		{
			return (Point)(object)e;
		}

		#region Implementation for methods marked with [Script(NotImplementedHere = true)]

		public static Point operator -(__Point a, __Point b)
		{
			return new Point { x = ((Point)a).x - ((Point)b).x, y = ((Point)a).y - ((Point)b).y };
		}

		public static Point operator +(__Point a, __Point b)
		{
			return new Point { x = ((Point)a).x + ((Point)b).x, y = ((Point)a).y + ((Point)b).y };
		}

		public static Point operator *(__Point a, __Point b)
		{
			return new Point { x = ((Point)a).x * ((Point)b).x, y = ((Point)a).y * ((Point)b).y };
		}

		public static Point operator *(__Point a, double b)
		{
			return new Point { x = ((Point)a).x * b, y = ((Point)a).y * b };
		}

		public static Point operator /(__Point a, __Point b)
		{
			return new Point { x = ((Point)a).x / ((Point)b).x, y = ((Point)a).y / ((Point)b).y };
		}

		#endregion




	}
}
