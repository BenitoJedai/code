using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.utils;
using ScriptCoreLib.ActionScript.flash.geom;

namespace FlashConsoleWorm.ActionScript
{
    /// <summary>
    /// This class defines the extension methods for this project
    /// </summary>
    [Script]
    internal static class MyExtensions
    {
		public static Timer AtDelayDo(this int e, Action a)
		{
			var t = new Timer(e, 1);

			t.timer += delegate { a(); };

			t.start();

			return t;
		}

        public static bool IsEqual(this Point p, Point x)
        {
            if (p.x != x.x)
                return false;

            if (p.y != x.y)
                return false;

            return true;
        }

        public static Timer AtInterval(this int e, Action<Timer> a)
        {
            var t = new Timer(e);

            t.timer += delegate { a(t); };

            t.start();

            return t;
        }

        

        public static int Random(this int i)
        {
            return new Random().Next(i);
        }


        public static void Times(this int e, Action h)
        {
            for (int i = 0; i < e; i++)
            {
                h();
            }
        }
    }
}
