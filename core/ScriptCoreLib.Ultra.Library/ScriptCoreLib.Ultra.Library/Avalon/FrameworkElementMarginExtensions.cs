using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ScriptCoreLib.Avalon
{
    public static class FrameworkElementMarginExtensions
    {
        public static IEnumerable<T> WithSomeMargins<T>(this IEnumerable<T> e) where T : FrameworkElement
        {
            return e.WithSomeMarginBelow().WithSomeMarginAbove();
        }

        public static IEnumerable<T> WithSomeMarginAbove<T>(this IEnumerable<T> e) where T : FrameworkElement
        {
            var x = e.ToArray();

            x.FirstOrDefault().WithSomeMarginAbove();

            return x;
        }

        public static IEnumerable<T> WithSomeMarginBelow<T>(this IEnumerable<T> e) where T : FrameworkElement
        {
            var x = e.ToArray();
            x.LastOrDefault().WithSomeMarginBelow();

            return x;
        }

        public static T WithSomeMarginBelow<T>(this T e) where T : FrameworkElement
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201401/20140111-iquery/new

            if (e != null)
                e.Margin = new Thickness(0, 0, 0, 6);

            return e;
        }

        public static T WithSomeMarginAbove<T>(this T e) where T : FrameworkElement
        {
            if (e != null)
                e.Margin = new Thickness(0, 4, 0, 0);

            return e;
        }
    }
}
