using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ScriptCoreLib.Avalon
{
    public static class FrameworkElementMarginExtensions
    {
        public static T WithSomeMarginAbove<T>(this T e) where T : FrameworkElement
        {
            e.Margin = new Thickness(0, 4, 0, 0);

            return e;
        }
    }
}
