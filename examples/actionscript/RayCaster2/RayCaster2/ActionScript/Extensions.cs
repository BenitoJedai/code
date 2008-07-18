using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.utils;
using ScriptCoreLib.ActionScript.flash.display;

namespace RayCaster2.ActionScript
{
    
    [Script]
    public class PointInt32
    {
        public int X;
        public int Y;

        public override string ToString()
        {
            return "{ X = " + X + ", Y = " + Y + " }";
        }

        public PointInt32 Clone()
        {
            return new PointInt32 { X = this.X, Y = this.Y };
        }
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
    public static class MyExtensions
    {
        public static int ToInt32(this int e)
        {
            return e;
        }

        public static int Floor(this double e)
        {
            return (int)Math.Floor(e);
        }

        public static bool FuzzyEquals(this double e, double x, double margin)
        {
            var v = e - x;

            if (v > margin)
                return false;

            if (v < margin)
                return false;

            return true;
        }

        public static string ToDebugString(this double[] e)
        {
            var v = "length: " + e.Length + "\n";

            for (int i = 0; i < e.Length; i++)
            {
                v += i + " = " + e[i] + "\n";
            }

            return v;
        }

        public static void drawLine(this Graphics g, int x, int y, int cx, int cy)
        {
            g.moveTo(x, y);
            g.lineTo(cx, cy);
        }

        public static Timer AtInterval(this int e, Action<Timer> a)
        {
            var t = new Timer(e);

            t.timer += delegate { a(t); };

            t.start();

            return t;
        }

        public static RectInt32 ToRectInt32(this Array2DSByte e)
        {
            return new RectInt32 { Width = e.XLength, Height = e.YLength };
        }

        public static int Max(this int i, int e)
        {
            if (e > i)
                return e;

            return i;
        }

        public static int Min(this int i, int e)
        {
            if (e < i)
                return e;

            return i;
        }

        public static double Min(this double i, double e)
        {
            if (e < i)
                return e;

            return i;
        }

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

    [Script]
    public class Int32ByRef
    {
        public int Value;
    }


    [Script]
    public class KeyboardButton
    {
        public uint[] Buttons;

        public bool IsPressed;

        public static implicit operator bool(KeyboardButton b)
        {
            return b.IsPressed;
        }

        public static implicit operator KeyboardButton(uint[] b)
        {
            return new KeyboardButton { Buttons = b };
        }

        public bool ProcessKeyDown(uint key)
        {
            if (Buttons.Contains(key))
            {
                this.IsPressed = true;
                return true;
            }

            return false;
        }

        public bool ProcessKeyUp(uint key)
        {
            if (Buttons.Contains(key))
            {
                this.IsPressed = false;
                return true;
            }

            return false;
        }
    }

    [Script]
    class GrayColor
    {
        public int GrayValue;

        public static implicit operator GrayColor(int e)
        {
            return new GrayColor { GrayValue = e };
        }

        public static implicit operator GrayColor(double e)
        {
            return (int)e;
        }

        public int Color
        {
            get
            {
                return GrayValue + (GrayValue << 8) + (GrayValue << 16);
            }
        }

        public static implicit operator uint(GrayColor c)
        {
            return (uint)c.Color;
        }
    }

}
