﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.Shared.Drawing;

namespace NatureBoy.js
{
    [Script]
    static class Extensions
    {
        public static double GetRange(this Point a, Point b)
        {
            var dx = a.X - b.X;
            var dy = a.Y - b.Y;

            return System.Math.Sqrt(dx * dx + dy * dy);
        }

        public static double GetAngle(this Point p, double _x, double _y)
        {
            var x = p.X - _x;
            var y = p.Y - _y;

            if (x == 0)
                return System.Math.PI / 2;

            var a = System.Math.Atan(y / x);

            if (x < 0)
                a += System.Math.PI;
            else if (y < 0)
                a += System.Math.PI * 2;


            return a;
        }

        public static string ToCSSImage(this string url)
        {
            return "url(" + url + ")";
        }

        public static Timer AutoRotate(this Dude e, double multiplier)
        {
            return new Timer(
                t => e.Rotation16 = System.Convert.ToInt32(t.Counter * multiplier), 0, 100
            );
        }

        public static void AutoRotateToCursor(this Dude e, IHTMLElement stage)
        {
            stage.onmousemove +=
                delegate(IEvent ev)
                {
                    e.LookAt(ev.CursorPosition);

                };
        }

    }
}
