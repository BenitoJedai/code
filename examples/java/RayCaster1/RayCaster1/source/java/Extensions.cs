using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace RayCaster1.source.java
{
    [Script]
    public class PointInt32
    {
        public int X;
        public int Y;
    }

    [Script]
    public class RectInt32 : PointInt32
    {

        public int Width;
        public int Height;

        public int Right
        {
            get
            {
                return Width + X;
            }
        }

        public int Bottom
        {
            get
            {
                return Height + Y;
            }
        }

        public bool IsOutSide(PointInt32 p)
        {
            if (p.X < this.X)
                return true;

            if (p.Y < this.Y)
                return true;

            if (p.X >= this.Right)
                return true;

            if (p.Y >= this.Bottom)
                return true;

            return false;
        }
    }

    [Script]
    public static class Extensions
    {
        public static bool IsExact(this int i, int min, int max)
        {
            if (i == min)
                return true;

            return i == max;
        }

        public static bool IsBetween(this int i, int gte, int lt)
        {
            if (i < gte)
                return false;

            return i <= lt;
        }


        public static bool IsBetweenIncluding(this int i, int gte, int lt)
        {
            if (i < gte)
                return false;

            return i < lt;
        }

        public static bool Contains(this int[] e, int value)
        {
            var r = false;

            foreach (var i in e)
            {
                if (i == value)
                {
                    r = true;
                    break;
                }
            }

            return r;
        }
    }
}
