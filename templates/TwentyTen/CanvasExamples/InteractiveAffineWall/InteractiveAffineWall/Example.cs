using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using InteractiveAffineWall.Avalon.Images;

namespace InteractiveAffineWall
{
    public static class Example
    {
        public static Action<Action<Func<Image>, Func<Type>, Func<string>>> Yield
        {
            get
            {
                return y => y(Preview, GetCanvasType, GetText);
            }
        }

        static Image Preview()
        {
            return new Preview();
        }

        static string GetText()
        {
            return "Interactive Affine Wall";
        }

        static Type GetCanvasType()
        {
            return typeof(ApplicationCanvas);
        }
    }

}
