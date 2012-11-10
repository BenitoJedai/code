﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Windows.Forms
{
    public static class ControlExtensions
    {
        public static T MoveBy<T>(this T e, int dx, int dy) where T : Control
        {
            var x = e.Left + dx;
            var y = e.Top + dy;

            return e.MoveTo(dx, dy);
        }

        public static T MoveTo<T>(this T e, int x, int y) where T : Control
        {
            e.Location = new Drawing.Point(x, y);

            return e;
        }

        public static T SizeTo<T>(this T e, int x, int y) where T : Control
        {
            e.Size = new Drawing.Size(x, y);

            return e;
        }
    }
}
