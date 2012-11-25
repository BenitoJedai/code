using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;

namespace JellyworldExperiment.DualViewWithCamera
{
    public class ApplicationCanvas : Canvas
    {
        public readonly Rectangle f = new Rectangle();
        public readonly Rectangle avg = new Rectangle();


        public ApplicationCanvas()
        {

            avg = new Rectangle();

            avg.Fill = Brushes.Red;
            avg.AttachTo(this);
            avg.SizeTo(16, 16);
            avg.Opacity = 1;

            //Canvas.SetZIndex(avg, 1000);

            f.Fill = Brushes.Yellow;
            f.AttachTo(this);
            f.SizeTo(16, 16);
            f.Opacity = 0.5;
        }

        class Item
        {
            public Rectangle r;
            public double Left;
            public double Top;
            public double Width;
            public double Height;
        }
        List<Item> History = new List<Item>();

        public void MoveTrackerTo(
            double Left,
            double Top,
            double Width,
            double Height
            )
        {
            //var r = new Rectangle();

            //r.Fill = Brushes.Green;
            //r.AttachTo(this);
            //r.SizeTo(16, 16);
            //r.Opacity = 0.5;
            //Canvas.SetZIndex(r, 1);

            //r.MoveTo(Left, Top).SizeTo(Width, Height);

            History.Add(
                new Item
                {
                    //r = r,
                    Left = Left,
                    Top = Top,
                    Width = Width,
                    Height = Height
                }
            );



            if (History.Count > 16)
            {
                History[0].r.Orphanize();
                History.RemoveAt(0);
            }

            //var avgLeft = History.Average(k => Canvas.GetLeft(k));
            //var avgTop = History.Average(k => Canvas.GetTop(k));
            //var avgWidth = History.Average(k => k.Width);
            //var avgHeight = History.Average(k => k.Height);

            var avgLeft = 0.0;
            var avgTop = 0.0;
            var avgWidth = 0.0;
            var avgHeight = 0.0;

            History.WithEachIndex(
                (k, i) =>
                {
                    avgLeft += k.Left;
                    avgTop += k.Top;
                    avgWidth += k.Width;
                    avgHeight += k.Height;

                    //k.r.Opacity = (i / History.Count) * 0.2;
                }
            );

            avgLeft /= History.Count;
            avgTop /= History.Count;
            avgWidth /= History.Count;
            avgHeight /= History.Count;

            if (AverageChanged != null)
                AverageChanged(avgLeft, avgTop, avgWidth, avgHeight);

            avg.MoveTo(
                avgLeft,
                avgTop
            ).SizeTo(
                avgWidth,
                avgHeight
            );
        }

        public event Action<double, double, double, double> AverageChanged;
    }
}
