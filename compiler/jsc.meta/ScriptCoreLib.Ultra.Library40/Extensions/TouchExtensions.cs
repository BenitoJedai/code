using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using ScriptCoreLib.Library;
using ScriptCoreLib.Extensions;
using System.Windows.Controls;

namespace ScriptCoreLib.Extensions
{
    public static class TouchExtensions
    {
        public static ToTouchEvents<T> ToTouchEvents<T>(this UIElement that, IEnumerable<T> source)
        {
            var a = source.AsEnumerable().GetEnumerator();

            return ToTouchEvents(
                that,
                () =>
                {
                    a.MoveNext();
                    return a.Current;
                }
            );
        }

        public static ToTouchEvents<T> ToTouchEvents<T>(this UIElement e, Func<T> s)
        {
            return new ToTouchEvents<T>(e, s);
        }

        public static T MoveTo<T>(this T e, TouchEventArgs a, UIElement relativeTo) where T : UIElement
        {
            var tp = a.GetTouchPoint(relativeTo);
            var p = tp.Position;
            var x = p.X;
            var y = p.Y;

            Canvas.SetLeft(e, x);
            Canvas.SetTop(e, y);

            return e;
        }
    }
}
