using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.JavaScript.DOM;

namespace ConsoleWorm.js
{
    [Script]
    static class Extensions
    {
        public static bool IsKeyCode(this IEvent e, string c)
        {
            var r = false;

            foreach (var v in c)
            {
                if (v == e.KeyCode)
                {
                    r = true;
                    break;
                }
            }

            return r;
        }

        public static int ToInt32(this int e)
        {
            return (int)Math.Floor((double)e);
        }

        public static void Times(this int e, Action h)
        {
            for (int i = 0; i < e; i++)
            {
                h();
            }
        }

        public static bool IsEqual(this Point p, Point x)
        {
            if (p.X != x.X)
                return false;

            if (p.Y != x.Y)
                return false;

            return true;
        }

        public static bool IsZero(this Point p)
        {
            if (p.X != 0)
                return false;

            if (p.Y != 0)
                return false;

            return true;
        }

        public static int Random(this int i)
        {
            return new Random().Next(i);
        }

        public static Timer AtInterval(this int x, Action<Timer> h)
        {
            return new Timer(t => h(t), x, x);
        }

        public static void ToWindowText(this Type e, string s)
        {
            if (string.IsNullOrEmpty(s))
                Native.Window.document.title = e.Name;
            else
                Native.Window.document.title = e.Name + " - " + s;
        }

        public static void ToWindowText(this Type e)
        {
            ToWindowText(e, null);
        }
    }
}
