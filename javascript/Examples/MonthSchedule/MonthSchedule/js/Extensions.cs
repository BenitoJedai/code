using System;
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
        
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> e, TKey k)
        {
            if (e.ContainsKey(k))
                return e[k];

            return default(TValue);
        }

        public static void DictonaryToArguments(this ILocation location, Dictionary<string, string> e)
        {
            location.search = "?" + string.Join("&", e.Keys.Select(key => key + "=" + e[key]).ToArray());
        }

        public static Dictionary<string, string> ArgumentsToDictonary(this ILocation e)
        {
            var d = new Dictionary<string, string>();

            if (e.search.StartsWith("?"))
            {
                var x = e.search.Substring(1);

                foreach (var p in x.Split('&'))
                {
                    var z = p.Split('=');

                    if (z.Length == 2)
                    {
                        d[z[0]] = z[1];
                    }
                }


            }

            return d;
        }

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
            if (e == DayOfWeek.Sunday) return "Su".Localize();
            if (e == DayOfWeek.Monday) return "M".Localize();
            if (e == DayOfWeek.Tuesday) return "Tu".Localize();
            if (e == DayOfWeek.Wednesday) return "W".Localize();
            if (e == DayOfWeek.Thursday) return "Th".Localize();
            if (e == DayOfWeek.Friday) return "F".Localize();
            if (e == DayOfWeek.Saturday) return "Sa".Localize();

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

        public static IHTMLElement WithText(this IHTMLElement e, string text)
        {
            e.innerText = text;

            return e;
        }
    }
}
