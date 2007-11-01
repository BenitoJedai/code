﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;

namespace MonthSchedule.js
{
    [Script]
    public static class Extensions
    {
        public static bool IsFirstDayOfMonth(this DateTime e)
        {
            return e.Day == 1;
        }

        public static bool IsLastDayOfMonth(this DateTime e)
        {
            return e.Day == e.DaysInMonth();
        }


        public static Func<int, int> MakePreviousFunc(this int x, int n)
        {
            return i =>
                {
                    if (i < x - n)
                        return x + n;

                    return i--;
                };
        }

        public static Func<int, int> MakeNextFunc(this int x, int n)
        {
            return i =>
                    {
                        if (i > x + n)
                            return x - n;

                        return i++;
                    };
        }

        public static string GetFractString(this double val)
        {
            if (val < 0)
                return "-" + GetAbsFractString(val);

            if (val > 0)
                return "+" + GetAbsFractString(val);

            return GetAbsFractString(val);
        }

        public static string GetAbsFractString(this double val)
        {
            var aa = Math.Abs(val);
            var z = aa % 1;

            if (z == 0.5)
            {
                if (aa - z == 0)
                    return "½";
                else
                    return (aa - z) + "½";
            }
            else
                return "" + aa;
        }

        public static bool IsWeekend(this DateTime date)
        {
            if (date.DayOfWeek == DayOfWeek.Saturday)
                return true;


            return date.DayOfWeek == DayOfWeek.Sunday;
        }
        public static DateTime GetDayWithinMonth(this DateTime date, int day)
        {
            return new DateTime(date.Year, date.Month, day);
        }

        public static IEnumerable<int> AsRange(this int e)
        {
            return Enumerable.Range(0, e);
        }

        public static bool IsNullOrEmpty(this string e)
        {
            // compiler bug: will inline when it should not
            object dummy = null;

            return string.IsNullOrEmpty(e);
        }

        public static int DaysInMonth(this DateTime date)
        {
            return DateTime.DaysInMonth(date.Year, date.Month);
        }

        public static T AddTo<T>(this T e, List<T> list)
        {
            list.Add(e);

            return e;
        }

        public static void WhenNoLongerNeeded(this IHTMLInput e, Func<bool> condition, Action done)
        {
            ScriptCoreLib.Shared.EventHandler<IEvent> h = null;

            h = ev =>
                {
                    //var Keys = new[] { 8, 46 };

                    if (e.value.Length == 0)
                        //if (Keys.Contains(ev.KeyCode))
                        if (condition())
                        {
                            // Back = 8,
                            // Delete = 46,

                            e.onkeyup -= h;
                            e.onchange -= h;

                            done();
                        }
                };

            e.onkeyup += h;
            e.onchange += h;
        }

        public static void WhenNoLongerEmpty(this IHTMLInput e, Action done)
        {
            ScriptCoreLib.Shared.EventHandler<IEvent> h = null;

            h = ev =>
                {
                    if (e.value.Length > 0)
                    {
                        e.onkeyup -= h;
                        e.onchange -= h;

                        done();
                    }
                };

            e.onkeyup += h;
            e.onchange += h;
        }

        public static string AsString(this DayOfWeek e)
        {
            if (e == DayOfWeek.Sunday) return "P";
            if (e == DayOfWeek.Monday) return "E";
            if (e == DayOfWeek.Tuesday) return "T";
            if (e == DayOfWeek.Wednesday) return "K";
            if (e == DayOfWeek.Thursday) return "N";
            if (e == DayOfWeek.Friday) return "R";
            if (e == DayOfWeek.Saturday) return "L";

            throw new Exception("DayOfWeek");
        }

        public static IHTMLElement AsElement(this string tag)
        {
            return new IHTMLElement(tag);
        }
        public static T AttachTo<T, C>(this T e, C c)
            where T : INode
            where C : INode
        {
            c.appendChild(e);

            return e;
        }

        public static IHTMLElement AttachToDocument(this string tag)
        {
            var e = tag.AsElement();

            Native.Document.body.appendChild(e);

            return e;
        }
    }
}
