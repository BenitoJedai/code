using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.utils;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.ActionScript.flash.events;

namespace FlashTabletDesign.ActionScript
{
	/// <summary>
	/// This class defines the extension methods for this project
	/// </summary>
	[Script]
	internal static class Extensions
	{
		public static T Attach<T>(this DisplayObjectContainer c, T e) where T : DisplayObject
		{
			return e.AttachTo(c);
		}

		public static Timer AtInterval(this int e, Action<Timer> a)
		{
			var t = new Timer(e);

			t.timer += delegate { a(t); };

			t.start();

			return t;
		}

		public static Point ToStagePoint(this MouseEvent m)
		{
			return new Point(m.stageX, m.stageY);
		}

		public static Point MoveToArc(this Point e, double direction, double distance)
		{
			var p = new Point(e.x, e.y);
			p.x += Math.Cos(direction) * distance;
			p.y += Math.Sin(direction) * distance;

			return p;
		}
	}
}
