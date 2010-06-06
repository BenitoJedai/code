using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using InteractiveAffineCube.Avalon.Images;

namespace InteractiveAffineCube
{
    public static class Example
    {
        public static void Yield(Action<Func<Image>, Func<Type>, Func<string>> y)
        {
            y(Preview, GetCanvasType, GetText);
        }

        static Image Preview()
        {
            return new Preview();
        }

        static string GetText()
        {
            return "Interactive Affine Cube";
        }

        static Type GetCanvasType()
        {
            return typeof(ApplicationCanvas);
        }
    }
}
